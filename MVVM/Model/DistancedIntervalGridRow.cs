using PaceCalculator.Core;

namespace PaceCalculator.MVVM.Model
{
    public class DistancedIntervalGridRow : ObservableObject
    {

        private string _distance = "";
        public string Distance
        {
            get { return _distance; }
            set
            {
                if(_distance != value)
                {
                    _distance = value;
                    IsDistanceValid = PaceHelper.IsValidFloatEntry(Distance) && PaceHelper.ToFloat(Distance) != 0;
                    ValidateRow();
                    OnPropertyChanged(nameof(Distance));
                }
            }
        }

        private bool _isDistanceValid = false;
        public bool IsDistanceValid
        {
            get { return _isDistanceValid; }
            set
            {
                if (_isDistanceValid != value)
                {
                    _isDistanceValid = value;
                    OnPropertyChanged(nameof(IsDistanceValid));
                }
            }
        }

        private string _hours = "";
        public string Hours
        {
            get { return _hours; }
            set
            {
                if(_hours != value)
                {
                    _hours = value;
                    IsHoursValid = PaceHelper.IsValidIntEntry(Hours);
                    ValidateRow();
                    OnPropertyChanged(nameof(Hours));
                }
            }
        }

        private bool _isHoursValid = true;
        public bool IsHoursValid
        {
            get { return _isHoursValid; }
            set
            {
                if (_isHoursValid != value)
                {
                    _isHoursValid = value;
                    OnPropertyChanged(nameof(IsHoursValid));
                }
            }
        }

        private string _minutes = "";
        public string Minutes
        {
            get { return _minutes; }
            set
            {
                if(_minutes != value)
                {
                    _minutes = value;
                    IsMinutesValid = PaceHelper.IsValidIntEntry(Minutes) && PaceHelper.ToInt(Minutes) < 60;
                    ValidateRow();
                    OnPropertyChanged(nameof(Minutes));
                }
            }
        }

        private bool _isMinutesValid = true;
        public bool IsMinutesValid
        {
            get { return _isMinutesValid; }
            set
            {
                if (_isMinutesValid != value)
                {
                    _isMinutesValid = value;
                    OnPropertyChanged(nameof(IsMinutesValid));
                }
            }
        }

        private string _seconds = "";
        public string Seconds
        {
            get { return _seconds; }
            set
            {
                if(_seconds != value)
                {
                    _seconds = value;
                    IsSecondsValid = PaceHelper.IsValidFloatEntry(Seconds) && PaceHelper.ToFloat(Seconds) < 60;
                    ValidateRow();
                    OnPropertyChanged(nameof(Seconds));
                }
            }
        }

        private bool _isSecondsValid = true;
        public bool IsSecondsValid
        {
            get { return _isSecondsValid; }
            set
            {
                if (_isSecondsValid != value)
                {
                    _isSecondsValid = value;
                    OnPropertyChanged(nameof(IsSecondsValid));
                }
            }
        }

        private bool _isValid = false;
        public bool IsValid
        {
            get { return _isValid; }
            set
            {
                if(_isValid != value)
                {
                    _isValid = value;
                    OnPropertyChanged(nameof(IsValid));
                }
            }
        }

        private bool _isTimeValid = false;
        public bool IsTimeValid
        {
            get { return _isTimeValid; }
            set
            {
                if (_isTimeValid != value)
                {
                    _isTimeValid = value;
                    OnPropertyChanged(nameof(IsTimeValid));
                }
            }
        }

        public DistancedIntervalGridRow()
        { }

        /* updates `IsValid` as false if:
         * 1. all fields do not individually evaluate to valid
         * OR
         * 2. the total distance or total time is 0
         * else, `IsValid` is set to true
         */
        private void ValidateRow()
        {
            IsTimeValid = !(PaceHelper.ToInt(Hours) == 0 && PaceHelper.ToInt(Minutes) == 0 && PaceHelper.ToFloat(Seconds) == 0.0f);
            IsValid = (IsDistanceValid && IsHoursValid && IsMinutesValid && IsSecondsValid) && IsTimeValid;
        }

    }
}
