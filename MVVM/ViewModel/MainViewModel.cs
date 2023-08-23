using PaceCalculator.Core;

namespace PaceCalculator.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public DistancedIntervalViewModel DistancedIntervalVM { get; set; }
        public PacedIntervalViewModel PacedIntervalVM { get; set; }

        private object _currentView;

		public object CurrentView
		{
			get { return _currentView; }
			set
			{
				_currentView = value;
				OnPropertyChanged();
			}
		}

		public MainViewModel()
		{
			DistancedIntervalVM = new DistancedIntervalViewModel();
			PacedIntervalVM = new PacedIntervalViewModel();
            _currentView = DistancedIntervalVM;
		}

	}
}
