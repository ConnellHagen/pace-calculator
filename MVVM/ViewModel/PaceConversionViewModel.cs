using PaceCalculator.Commands;
using PaceCalculator.Core;
using PaceCalculator.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Documents;

namespace PaceCalculator.MVVM.ViewModel
{
    public enum PaceConversionMode { MI_TO_KM = 1, KM_TO_MI = 2 }

    public class PaceConversionViewModel : ObservableObject
    {
        public const float MILE_IN_KILOMETERS = 1.609344f;

        private Calculator _calculator; //for pace converting
        public Calculator Converter
        {
            get { return _calculator; }
        }

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
                    if (ConversionMode == PaceConversionMode.MI_TO_KM)
                    {
                        CalculateConvertedPace();
                    }
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
                    if (ConversionMode == PaceConversionMode.MI_TO_KM)
                    {
                        CalculateConvertedPace();
                    }
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
                    if (ConversionMode == PaceConversionMode.KM_TO_MI)
                    {
                        CalculateConvertedPace();
                    }
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
                    if (ConversionMode == PaceConversionMode.KM_TO_MI)
                    {
                        CalculateConvertedPace();
                    }
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
        {
            _calculator = new Calculator();
        }

        private void CalculateConvertedPace()
        {
            if (ConversionMode == PaceConversionMode.MI_TO_KM)
            {
                if ( !(IsMilePaceValid && IsMileMinutesValid && IsMileSecondsValid) )
                {
                    KmMinutes = "";
                    KmSeconds = "";
                    return;
                }

                int? mileMinutes = PaceHelper.ToInt(MileMinutes);
                float? mileSeconds = PaceHelper.ToFloat(MileSeconds);
                if (mileMinutes == null || mileSeconds == null)
                {
                    throw new ArgumentNullException();
                }

                float secondsMI = 60 * (int)mileMinutes + (float)mileSeconds;
                float secondsKM = secondsMI / MILE_IN_KILOMETERS;
                Converter.Clear();
                Converter.AddInterval(new Interval( 1.0f, (0, (int)secondsKM) ));
                Converter.CalcAvgPace();

                KmMinutes = Converter.AvgPace.Item1.ToString();
                KmSeconds = Converter.AvgPace.Item2.ToString();
            }
            else if (ConversionMode == PaceConversionMode.KM_TO_MI)
            {
                if ( !(IsKmPaceValid && IsKmMinutesValid && IsKmSecondsValid) )
                {
                    MileMinutes = "";
                    MileSeconds = "";
                    return;
                }

                int? kmMinutes = PaceHelper.ToInt(KmMinutes);
                float? kmSeconds = PaceHelper.ToFloat(KmSeconds);
                if (kmMinutes == null || kmSeconds == null)
                {
                    throw new ArgumentNullException();
                }

                float secondsKM = 60 * (int)kmMinutes + (float)kmSeconds;
                float secondsMI = secondsKM * MILE_IN_KILOMETERS;
                Converter.Clear();
                Converter.AddInterval(new Interval(1.0f, (0, (int)secondsMI)));
                Converter.CalcAvgPace();

                MileMinutes = Converter.AvgPace.Item1.ToString();
                MileSeconds = Converter.AvgPace.Item2.ToString();
            }
        }

        private void UpdateValidity()
        {
                IsMilePaceValid = !(PaceHelper.ToInt(MileMinutes) == 0 && PaceHelper.ToFloat(MileSeconds) == 0);
                IsKmPaceValid = !(PaceHelper.ToInt(KmMinutes) == 0 && PaceHelper.ToFloat(KmSeconds) == 0);
        }

    }
}
