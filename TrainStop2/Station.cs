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

        public Station(string name)
        {
            _name = name;
            _trains = new List<ITrain>();
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
    }
}
