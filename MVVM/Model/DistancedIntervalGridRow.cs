using PaceCalculator.Core;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

namespace PaceCalculator.MVVM.Model
{
    public class DistancedIntervalGridRow : ObservableObject
    {

        private string _distance;
        public string Distance
        {
            get { return _distance; }
            set
            {
                if(_distance != value)
                {
                    _distance = value;
                    IsDistanceValid = IsValidFloatEntry(Distance);
                    OnPropertyChanged(nameof(Distance));
                }
            }
        }

        private string _hours;
        public string Hours
        {
            get { return _hours; }
            set
            {
                if(_hours != value)
                {
                    _hours = value;
                    IsSecondsValid = IsValidIntEntry(Hours);
                    OnPropertyChanged(nameof(Hours));
                }
            }
        }

        private string _minutes;
        public string Minutes
        {
            get { return _minutes; }
            set
            {
                if(_minutes != value)
                {
                    _minutes = value;
                    IsMinutesValid = IsValidIntEntry(Minutes);
                    OnPropertyChanged(nameof(Minutes));
                }
            }
        }

        private string _seconds;
        public string Seconds
        {
            get { return _seconds; }
            set
            {
                if(_seconds != value)
                {
                    _seconds = value;
                    IsSecondsValid = IsValidFloatEntry(Seconds);
                    OnPropertyChanged(nameof(Seconds));
                }
            }

        }

        public bool IsDistanceValid { get; set; }
        public bool IsHoursValid { get; set; }
        public bool IsMinutesValid { get; set; }
        public bool IsSecondsValid { get; set; }

        public DistancedIntervalGridRow()
        {
            _distance = "";
            _hours = "";
            _minutes = "";
            _seconds = "";
            IsDistanceValid = false;
            IsHoursValid = false;
            IsMinutesValid = false;
            IsSecondsValid = false;
        }

        private bool IsValidFloatEntry(string test)
        {
            float? val = ToFloat(test);

            if (val == null)
            {
                return false;
            }

            return val >= 0;
        }
        private float? ToFloat(string s)
        {
            if (s == "") { return null; }

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
        private int? ToInt(string s)
        {
            if (s == "") { return null; }

            int val;

            if (int.TryParse(s, out val))
            {
                return val;
            }

            return null;
        }

    }
}
