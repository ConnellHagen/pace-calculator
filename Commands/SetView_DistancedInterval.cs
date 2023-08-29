using PaceCalculator.Core;
using PaceCalculator.MVVM.ViewModel;
using System.Diagnostics;

namespace PaceCalculator.Commands
{
    public class SetView_DistancedInterval : RelayCommand
    {
        private MainViewModel _viewModel;

        public SetView_DistancedInterval(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _viewModel.CurrentView = _viewModel.DistancedIntervalVM;
        }

    }
}
