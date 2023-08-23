namespace PaceCalculator.MVVM.Model
{
    public class DistancedIntervalGridRow
    {
        private float _distance;
        public float Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        private float _hours;
        public float Hours
        {
            get { return _hours; }
            set { _hours = value; }
        }

        private float _minutes;
        public float Minutes
        {
            get { return _minutes; }
            set { _minutes = value; }
        }

        private float _seconds;
        public float Seconds
        {
            get { return _seconds; }
            set { _seconds = value; }
        }

        public DistancedIntervalGridRow()
        {
            _distance = 0;
            _hours = 0;
            _minutes = 0;
            _seconds = 0;
        }
    }
}
