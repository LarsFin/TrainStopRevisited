using Moq;
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
            _station.ReleaseTrain(fakeName);
            _station.Trains.Contains(mockTrain.Object).ShouldBeFalse();
        }
    }
}
