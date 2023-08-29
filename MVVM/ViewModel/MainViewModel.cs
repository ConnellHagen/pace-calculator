using PaceCalculator.Commands;
using PaceCalculator.Core;

namespace PaceCalculator.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public PacedIntervalViewModel PacedIntervalVM { get; } = new PacedIntervalViewModel();
        public DistancedIntervalViewModel DistancedIntervalVM { get; } = new DistancedIntervalViewModel();

        private object _currentView;

        public SetView_PacedInterval SetViewPacedInterval { get; }
        public SetView_DistancedInterval SetViewDistancedInterval { get; }

        public object CurrentView
		{
			get { return _currentView; }
			set
			{
				_currentView = value;
				OnPropertyChanged(nameof(CurrentView));
			}
		}

		public MainViewModel()
		{
            _currentView = PacedIntervalVM;
            SetViewPacedInterval = new SetView_PacedInterval(this);
            SetViewDistancedInterval = new SetView_DistancedInterval(this);
        }

    }
}
