using PaceCalculator.Commands;
using PaceCalculator.Core;
using PaceCalculator.MVVM.Model;
using System.Collections.ObjectModel;

namespace PaceCalculator.MVVM.ViewModel
{
    public enum PaceConversionMode { MI_TO_KM = 1, KM_TO_MI = 2 }

    public class PaceConversionViewModel : ObservableObject
    {
        private string mileMinutes;
        public string MileMinutes
        {
            get { return mileMinutes; }
            set 
            { 
                mileMinutes = value;
                OnPropertyChanged(nameof(MileMinutes));
            }
        }

        private string mileSeconds;
        public string MileSeconds
        {
            get { return mileSeconds; }
            set
            {
                mileSeconds = value;
                OnPropertyChanged(nameof(MileSeconds));
            }
        }

        private string kmMinutes;
        public string KmMinutes
        {
            get { return kmMinutes; }
            set
            {
                kmMinutes = value;
                OnPropertyChanged(nameof(KmMinutes));
            }
        }

        private string kmSeconds;
        public string KmSeconds
        {
            get { return kmSeconds; }
            set
            {
                kmSeconds = value;
                OnPropertyChanged(nameof(KmSeconds));
            }
        }

        private PaceConversionMode conversionMode;
        public PaceConversionMode ConversionMode
        {
            get { return conversionMode; }
            set
            {
                conversionMode = value; 
                OnPropertyChanged(nameof(ConversionMode)); 
            }
        }

        public PaceConversionViewModel()
        {
            mileMinutes = "";
            mileSeconds = "";
            kmMinutes = "";
            kmSeconds = "";
            conversionMode = PaceConversionMode.MI_TO_KM;
        }
    }
}
