﻿using System;
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
            for (int i = 0; i < _trains.Count; i++)
            {
                _trains.Remove(_trains[i]);
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
