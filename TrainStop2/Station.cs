using System;
using System.Collections.Generic;

namespace TrainStop2
{
    public interface IStation
    {
        void ReceiveTrain(ITrain train);
        void ReleaseTrain(string trainName);
    }

    public class Station : IStation
    {
        private readonly string _name;
        private readonly List<ITrain> _trains;
        private const int _defaultCapacity = 6;
        private readonly int _capacity;

        public Station(string name)
        {
            _name = name;
            _trains = new List<ITrain>();
            _capacity = _defaultCapacity;
        }

        public void ReceiveTrain(ITrain train)
        {
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
    }
}
