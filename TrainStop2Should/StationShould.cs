﻿using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using TrainStop2;
using Xunit;

namespace TrainStop2Should
{
    public class StationShould
    {
        private Station _station;
        private string _name = "London Waterloo";

        public StationShould()
        {
            _station = new Station(_name);
        }

        [Fact]
        public void HavePassedName()
        {
            _station.Name.ShouldBe(_name);
        }

        [Fact]
        public void HaveListForTrains()
        {
            _station.Trains.ShouldBeOfType<List<ITrain>>();
        }

        [Fact]
        public void HaveEmptyListOfTrains()
        {
            _station.Trains.ShouldBeEmpty();
        }

        [Fact]
        public void TakeTrains()
        {
            var mockTrain = new Mock<ITrain>();
            mockTrain.SetupGet(train => train.IsMoving).Returns(true);
            _station.ReceiveTrain(mockTrain.Object);
            _station.Trains.Contains(mockTrain.Object).ShouldBeTrue();
        }

        [Fact]
        public void ThrowExceptionTakingStoppedTrains()
        {
            var mockTrain = new Mock<ITrain>();
            Should.Throw<ApplicationException>(() =>
            {
                _station.ReceiveTrain(mockTrain.Object);
            }).Message.ShouldBe("Station cannot accept stopped train");
        }

        [Fact]
        public void StopReceivedTrains()
        {
            var mockTrain = new Mock<ITrain>();
            mockTrain.SetupGet(train => train.IsMoving).Returns(true);
            _station.ReceiveTrain(mockTrain.Object);
            mockTrain.Verify(train => train.StopMovement(), Times.Once());
        }

        [Fact]
        public void ReleaseTrains()
        {
            string fakeName = "fake~name";
            var mockTrain = new Mock<ITrain>();
            mockTrain.SetupGet(train => train.IsMoving).Returns(true);
            _station.ReceiveTrain(mockTrain.Object);
            mockTrain.SetupGet(train => train.Name).Returns(fakeName);
            mockTrain.SetupGet(train => train.IsMoving).Returns(false);
            _station.ReleaseTrain(fakeName);
            _station.Trains.Contains(mockTrain.Object).ShouldBeFalse();
        }

        [Fact]
        public void ReleaseSpecifiedTrain()
        {
            string fakeName = "fake~name";
            string fakeName2 = "fake~name~2";
            var mockTrain = new Mock<ITrain>();
            mockTrain.SetupGet(train => train.IsMoving).Returns(true);
            var mockTrain2 = new Mock<ITrain>();
            mockTrain2.SetupGet(train => train.IsMoving).Returns(true);
            _station.ReceiveTrain(mockTrain.Object);
            _station.ReceiveTrain(mockTrain2.Object);
            mockTrain.SetupGet(train => train.Name).Returns(fakeName);
            mockTrain2.SetupGet(train => train.Name).Returns(fakeName2);
            mockTrain.SetupGet(train => train.IsMoving).Returns(false);
            mockTrain2.SetupGet(train => train.IsMoving).Returns(false);
            _station.ReleaseTrain(fakeName);
            _station.Trains.Contains(mockTrain.Object).ShouldBeFalse();
            _station.Trains.Contains(mockTrain2.Object).ShouldBeTrue();
        }

        [Fact]
        public void NotReleaseNonExistantTrain()
        {
            string fakeTrainName = "Non-existant train!";
            Should.Throw<ApplicationException>(() =>
            {
                _station.ReleaseTrain(fakeTrainName);
            }).Message.ShouldBe($"The train {fakeTrainName} does not exist at {_station.Name} station!");
        }

        [Fact]
        public void StartTrainWhenReleasing()
        {
            string fakeName = "fake~name";
            var mockTrain = new Mock<ITrain>();
            mockTrain.SetupGet(train => train.IsMoving).Returns(true);
            _station.ReceiveTrain(mockTrain.Object);
            mockTrain.SetupGet(train => train.Name).Returns(fakeName);
            mockTrain.SetupGet(train => train.IsMoving).Returns(false);
            _station.ReleaseTrain(fakeName);
            mockTrain.Verify(train => train.StartMovement(), Times.Once());
        }

        [Fact]
        public void OnlyReleaseStoppedTrains()
        {
            string fakeName = "fake~name";
            var mockTrain = new Mock<ITrain>();
            mockTrain.SetupGet(train => train.IsMoving).Returns(true);
            _station.ReceiveTrain(mockTrain.Object);
            mockTrain.SetupGet(train => train.Name).Returns(fakeName);
            Should.Throw<ApplicationException>(() =>
            {
                _station.ReleaseTrain(fakeName);
            }).Message.ShouldBe($"The train {fakeName} has not been stopped; so cannot be released!");
        }

        [Fact]
        public void HaveDefaultCapacity()
        {
            _station.Capacity.ShouldBe(_station.DefaultCapacity);
        }

        [Fact]
        public void SetPassedCapacity()
        {
            int customCapacity = 2;
            _station = new Station(_name, customCapacity);
            _station.Capacity.ShouldBe(customCapacity);
        }

        [Fact]
        public void NotAcceptTrainAtCapacity()
        {
            var mockTrain = new Mock<ITrain>();
            mockTrain.SetupGet(train => train.IsMoving).Returns(true);
            for (int i = 0; i < _station.Capacity; i++)
            {
                _station.ReceiveTrain(mockTrain.Object);
            }
            Should.Throw<ApplicationException>(() =>
            {
                _station.ReceiveTrain(mockTrain.Object);
            }).Message.ShouldBe($"{_name} cannot receive another train! At capacity.");
        }

        [Fact]
        public void AllowForMaintenance()
        {
            _station.UnderMaintenance.ShouldBeFalse();
            _station.StartMaintenance();
            _station.UnderMaintenance.ShouldBeTrue();
        }

        [Fact]
        public void NotReceiveTrainsWhileUnderMaintenance()
        {
            _station.StartMaintenance();
            var mockTrain = new Mock<ITrain>();
            mockTrain.SetupGet(train => train.IsMoving).Returns(true);
            Should.Throw<ApplicationException>(() =>
            {
                _station.ReceiveTrain(mockTrain.Object);
            }).Message.ShouldBe($"{_name} is currently under maintenance so cannot receive train!");
        }

        [Fact]
        public void NotReleaseTrainsWhileUnderMaintenance()
        {
            string fakeName = "fake~name";
            var mockTrain = new Mock<ITrain>();
            mockTrain.SetupGet(train => train.IsMoving).Returns(true);
            _station.ReceiveTrain(mockTrain.Object);
            mockTrain.SetupGet(train => train.Name).Returns(fakeName);
            mockTrain.SetupGet(train => train.IsMoving).Returns(false);
            _station.StartMaintenance();
            Should.Throw<ApplicationException>(() =>
            {
                _station.ReleaseTrain(fakeName);
            }).Message.ShouldBe($"{_name} is currently under maintenance so cannot release train!");
        }

        [Fact]
        public void AllowForStoppingOfMaintenance()
        {
            _station.StartMaintenance();
            _station.StopMaintenance();
            _station.UnderMaintenance.ShouldBeFalse();
        }
    }
}
