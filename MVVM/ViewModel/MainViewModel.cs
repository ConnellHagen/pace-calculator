using PaceCalculator.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaceCalculator.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand DistancedIntervalCommand { get; set; }
        public RelayCommand PacedIntervalCommand { get; set; }

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
            CurrentView = DistancedIntervalVM;

			DistancedIntervalCommand = new RelayCommand(o =>
			{
				CurrentView = DistancedIntervalVM;
			});

			PacedIntervalCommand = new RelayCommand(o =>
			{
				CurrentView = PacedIntervalVM;
			});
		}

	}
}
