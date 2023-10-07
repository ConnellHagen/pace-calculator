using PaceCalculator.Commands;
using PaceCalculator.Core;
using PaceCalculator.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace PaceCalculator.MVVM.ViewModel
{
    public enum PaceConversionMode { MI_TO_KM = 1, KM_TO_MI = 2 }

    public class PaceConversionViewModel : ObservableObject
    {
        private string _mileMinutes = "";
        public string MileMinutes
        {
            get { return _mileMinutes; }
            set 
            { 
                if (_mileMinutes != value)
                {
                    _mileMinutes = value;
                    IsMileMinutesValid = PaceHelper.IsValidIntEntry(MileMinutes);
                    UpdateValidity();
                    OnPropertyChanged(nameof(MileMinutes));
                }
            }
        }

        private bool _isMileMinutesValid = true;
        public bool IsMileMinutesValid
        {
            get { return _isMileMinutesValid; }
            set
            {
                if(_isMileMinutesValid != value) 
                {
                    _isMileMinutesValid = value;
                    OnPropertyChanged(nameof(IsMileMinutesValid));
                }
            }
        }

        private string _mileSeconds = "";
        public string MileSeconds
        {
            get { return _mileSeconds; }
            set
            {
                if (_mileSeconds != value)
                {
                    _mileSeconds = value;
                    IsMileSecondsValid = PaceHelper.IsValidFloatEntry(MileSeconds) && PaceHelper.ToFloat(MileSeconds) < 60;
                    UpdateValidity();
                    OnPropertyChanged(nameof(MileSeconds));
                }
            }
        }

        private bool _isMileSecondsValid = true;
        public bool IsMileSecondsValid
        {
            get { return _isMileSecondsValid; }
            set
            {
                if (_isMileSecondsValid != value)
                {
                    _isMileSecondsValid = value;
                    OnPropertyChanged(nameof(IsMileSecondsValid));
                }
            }
        }

        private bool _isMilePaceValid = false;
        public bool IsMilePaceValid
        {
            get { return _isMilePaceValid; }
            set
            {
                if (_isMilePaceValid != value)
                {
                    _isMilePaceValid = value;
                    OnPropertyChanged(nameof(IsMilePaceValid));
                }
            }
        }

        private string _kmMinutes = "";
        public string KmMinutes
        {
            get { return _kmMinutes; }
            set
            {
                if (_kmMinutes != value)
                {
                    _kmMinutes = value;
                    IsKmMinutesValid = PaceHelper.IsValidIntEntry(KmMinutes);
                    UpdateValidity();
                    OnPropertyChanged(nameof(KmMinutes));
                }
            }
        }

        private bool _isKmMinutesValid = true;
        public bool IsKmMinutesValid
        {
            get { return _isKmMinutesValid; }
            set
            {
                if (_isKmMinutesValid != value)
                {
                    _isKmMinutesValid = value;
                    OnPropertyChanged(nameof(IsKmMinutesValid));
                }
            }
        }

        private string _kmSeconds = "";
        public string KmSeconds
        {
            get { return _kmSeconds; }
            set
            {
                if(_kmSeconds != value)
                {
                    _kmSeconds = value;
                    IsKmSecondsValid = PaceHelper.IsValidFloatEntry(KmSeconds) && PaceHelper.ToFloat(KmSeconds) < 60;
                    UpdateValidity();
                    OnPropertyChanged(nameof(KmSeconds));
                }
            }
        }
        private bool _isKmSecondsValid = true;
        public bool IsKmSecondsValid
        {
            get { return _isKmSecondsValid; }
            set
            {
                if (_isKmSecondsValid != value)
                {
                    _isKmSecondsValid = value;
                    OnPropertyChanged(nameof(IsKmSecondsValid));
                }
            }
        }

        private bool _isKmPaceValid = false;
        public bool IsKmPaceValid
        {
            get { return _isKmPaceValid; }
            set
            {
                if(_isKmPaceValid != value)
                {
                    _isKmPaceValid = value;
                    OnPropertyChanged(nameof(IsKmPaceValid));
                }
            }
        }

        private PaceConversionMode _conversionMode = PaceConversionMode.MI_TO_KM;
        public PaceConversionMode ConversionMode
        {
            get { return _conversionMode; }
            set
            {
                if (_conversionMode != value)
                {
                    _conversionMode = value;
                    OnPropertyChanged(nameof(ConversionMode));
                }
            }
        }

        public PaceConversionViewModel()
        { }

        public void PaceConversionViewModel_PropertyChanged(object? _sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "MileMinutes":
                case "MileSeconds":
                case "KmMinutes":
                case "KmSeconds":
                    CalculateConvertedPace();
                    break;
            }

        }

        private void CalculateConvertedPace()
        {
            if (ConversionMode == PaceConversionMode.MI_TO_KM)
            {
                if (!IsMilePaceValid) return;
            }
            else if (ConversionMode == PaceConversionMode.KM_TO_MI)
            {
                if (!IsKmPaceValid) return;
            }
        }

        private void UpdateValidity()
        {
            if (ConversionMode == PaceConversionMode.MI_TO_KM)
            {
                IsMilePaceValid = !(PaceHelper.ToInt(MileMinutes) == 0 && PaceHelper.ToFloat(MileSeconds) == 0);
            }
            else if (ConversionMode == PaceConversionMode.KM_TO_MI)
            {
                IsKmPaceValid = !(PaceHelper.ToInt(KmMinutes) == 0 && PaceHelper.ToFloat(KmSeconds) == 0);
            }
        }

    }
}
