﻿using System.Collections.Generic;

namespace TrainStop2
{
    public interface IStation
    {
        void ReceiveTrain(ITrain train);
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
            _trains.Add(train);
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
