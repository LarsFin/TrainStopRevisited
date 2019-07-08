namespace TrainStop2
{
    public class Station
    {
        private readonly string _name;

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
    }
}
