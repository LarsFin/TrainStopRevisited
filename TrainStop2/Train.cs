namespace TrainStop2
{
    public interface ITrain
    {
        void StartMovement();
    }

    public class Train : ITrain
    {
        private readonly string _name;
        private bool _isMoving;

        public Train(string name)
        {
            _name = name;
        }

        public void StartMovement()
        {
            _isMoving = true;
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
