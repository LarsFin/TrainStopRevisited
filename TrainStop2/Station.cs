using System.Collections.Generic;

namespace TrainStop2
{
    public class Station
    {
        private readonly string _name;
        private readonly List<ITrain> _trains;

        public Station(string name)
        {
            _name = name;
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
