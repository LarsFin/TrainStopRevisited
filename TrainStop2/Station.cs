﻿using System;
using System.Collections.Generic;

namespace TrainStop2
{
    public interface IStation
    {
        void ReceiveTrain(ITrain train);
        void ReleaseTrain(string trainName);
        void StartMaintenance();
        void StopMaintenance();
    }

    public class Station : IStation
    {
        private readonly string _name;
        private readonly List<ITrain> _trains;
        private const int _defaultCapacity = 6;
        private readonly int _capacity;
        private bool _underMaintenance;

        public Station(string name)
        {
            _name = name;
            _trains = new List<ITrain>();
            _capacity = _defaultCapacity;
        }

        public Station(string name, int customCapacity)
        {
            _name = name;
            _trains = new List<ITrain>();
            _capacity = customCapacity;
        }

        public void ReceiveTrain(ITrain train)
        {
            if (_underMaintenance)
            {
                throw new ApplicationException($"{_name} is currently under maintenance so cannot receive train!");
            }
            if (_trains.Count >= _capacity)
            {
                throw new ApplicationException($"{_name} cannot receive another train! At capacity.");
            }
            if (train.IsMoving)
            {
                _trains.Add(train);
                train.StopMovement();
            } else
            {
                throw new ApplicationException("Station cannot accept stopped train");
            }
        }

        public void ReleaseTrain(string trainName)
        {
            if (_underMaintenance)
            {
                throw new ApplicationException($"{_name} is currently under maintenance so cannot release train!");
            }
            List<ITrain> toBeRemoved = new List<ITrain>();
            bool trainAtStation = false;
            foreach (ITrain train in _trains)
            {
                if (train.Name.Equals(trainName))
                {
                    if (train.IsMoving)
                    {
                        throw new ApplicationException($"The train {trainName} has not been stopped; so cannot be released!");
                    }
                    toBeRemoved.Add(train);
                    trainAtStation = true;
                }
            }
            if (!trainAtStation)
            {
                throw new ApplicationException($"The train {trainName} does not exist at {_name} station!");
            }
            foreach (ITrain train in toBeRemoved)
            {
                _trains.Remove(train);
                train.StartMovement();
            }
        }

        public void StartMaintenance()
        {
            _underMaintenance = true;
        }

        public void StopMaintenance()
        {
            _underMaintenance = false;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public List<ITrain> Trains
        {
            get
            {
                return _trains;
            }
        }

        public int DefaultCapacity
        {
            get
            {
                return _defaultCapacity;
            }
        }

        public int Capacity
        {
            get
            {
                return _capacity;
            }
        }

        public bool UnderMaintenance
        {
            get
            {
                return _underMaintenance;
            }
        }
    }
}
