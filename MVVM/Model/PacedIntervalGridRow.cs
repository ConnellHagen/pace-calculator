using PaceCalculator.Core;

namespace PaceCalculator.MVVM.Model
{
    public class PacedIntervalGridRow : ObservableObject
    {
        private string _timeHours = "";
        public string TimeHours
        {
            get { return _timeHours; }
            set
            {
                if(_timeHours != value)
                {
                    _timeHours = value;
                    IsTimeHoursValid = IsValidIntEntry(TimeHours);
                    ValidateRow();
                    OnPropertyChanged(nameof(TimeHours));
                }
            }
        }

        private bool _isTimeHoursValid = true;
        public bool IsTimeHoursValid
        {
            get { return _isTimeHoursValid; }
            set
            {
                if (_isTimeHoursValid != value)
                {
                    _isTimeHoursValid = value;
                    OnPropertyChanged(nameof(IsTimeHoursValid));
                }
            }
        }

        private string _timeMinutes = "";
        public string TimeMinutes
        {
            get { return _timeMinutes; }
            set
            {
                if (_timeMinutes != value)
                {
                    _timeMinutes = value;
                    IsTimeMinutesValid = IsValidIntEntry(TimeMinutes) && ToInt(TimeMinutes) < 60;
                    ValidateRow();
                    OnPropertyChanged(nameof(TimeMinutes));
                }
            }
        }

        private bool _isTimeMinutesValid = true;
        public bool IsTimeMinutesValid
        {
            get { return _isTimeMinutesValid; }
            set
            {
                if (_isTimeMinutesValid != value)
                {
                    _isTimeMinutesValid = value;
                    OnPropertyChanged(nameof(IsTimeMinutesValid));
                }
            }
        }

        private string _timeSeconds = "";
        public string TimeSeconds
        {
            get { return _timeSeconds; }
            set
            {
                if (_timeSeconds != value)
                {
                    _timeSeconds = value;
                    IsTimeSecondsValid = IsValidFloatEntry(TimeSeconds) && ToFloat(TimeSeconds) < 60;
                    ValidateRow();
                    OnPropertyChanged(nameof(TimeSeconds));
                }
            }
        }

        private bool _isTimeSecondsValid = true;
        public bool IsTimeSecondsValid
        {
            get { return _isTimeSecondsValid; }
            set
            {
                if (_isTimeSecondsValid != value)
                {
                    _isTimeSecondsValid = value;
                    OnPropertyChanged(nameof(IsTimeSecondsValid));
                }
            }
        }

        private string _paceMinutes = "";
        public string PaceMinutes
        {
            get { return _paceMinutes; }
            set
            {
                if (_paceMinutes != value)
                {
                    _paceMinutes = value;
                    IsPaceMinutesValid = IsValidIntEntry(PaceMinutes);
                    ValidateRow();
                    OnPropertyChanged(nameof(PaceMinutes));
                }
            }
        }

        private bool _isPaceMinutesValid = true;
        public bool IsPaceMinutesValid
        {
            get { return _isPaceMinutesValid; }
            set
            {
                if (_isPaceMinutesValid != value)
                {
                    _isPaceMinutesValid = value;
                    OnPropertyChanged(nameof(IsPaceMinutesValid));
                }
            }
        }

        private string _paceSeconds = "";
        public string PaceSeconds
        {
            get { return _paceSeconds; }
            set
            {
                if (_paceSeconds != value)
                {
                    _paceSeconds = value;
                    IsPaceSecondsValid = IsValidIntEntry(PaceSeconds) && ToInt(PaceSeconds) < 60;
                    ValidateRow();
                    OnPropertyChanged(nameof(PaceSeconds));
                }
            }
        }

        private bool _isPaceSecondsValid = true;
        public bool IsPaceSecondsValid
        {
            get { return _isPaceSecondsValid; }
            set
            {
                if (_isPaceSecondsValid != value)
                {
                    _isPaceSecondsValid = value;
                    OnPropertyChanged(nameof(IsPaceSecondsValid));
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

        private bool _isPaceValid = false;
        public bool IsPaceValid
        {
            get { return _isPaceValid; }
            set
            {
                if (_isPaceValid != value)
                {
                    _isPaceValid = value;
                    OnPropertyChanged(nameof(IsPaceValid));
                }
            }
        }

        public PacedIntervalGridRow()
        { }

        private bool IsValidFloatEntry(string test)
        {
            float? val = ToFloat(test);

            if (val == null)
            {
                return false;
            }

            return val >= 0;
        }
        public static float? ToFloat(string s)
        {
            if (s == "") { return 0.0f; }

            float val;

            if (float.TryParse(s, out val))
            {
                return val;
            }

            return null;
        }

        private bool IsValidIntEntry(string test)
        {
            int? val = ToInt(test);

            if (val == null)
            {
                return false;
            }

            return val >= 0;
        }
        public static int? ToInt(string s)
        {
            if (s == "") { return 0; }

            int val;

            if (int.TryParse(s, out val))
            {
                return val;
            }

            return null;
        }

        /* updates `IsValid` as false if:
         * 1. all fields do not individually evaluate to valid
         * OR
         * 2. the total distance or total time is 0
         * else, `IsValid` is set to true
         */
        private void ValidateRow()
        {
            IsTimeValid = !(ToInt(TimeHours) == 0 && ToInt(TimeMinutes) == 0 && ToFloat(TimeSeconds) == 0.0f);
            IsPaceValid = !(ToInt(PaceMinutes) == 0 && ToInt(PaceSeconds) == 0);
            IsValid = IsTimeHoursValid && IsTimeMinutesValid && IsTimeSecondsValid &&
                      IsPaceMinutesValid && IsPaceSecondsValid &&
                      IsTimeValid && IsPaceValid;
        }

    }
}
