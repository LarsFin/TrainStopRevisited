namespace TrainStop2
{
    public interface ITrain
    {

    }

    public class Train : ITrain
    {
        private readonly string _name;
        private bool _isMoving;

        public Train(string name)
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

        public bool IsMoving
        {
            get
            {
                return _isMoving;
            }
        }
    }
}
