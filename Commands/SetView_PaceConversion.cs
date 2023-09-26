using PaceCalculator.Core;
using PaceCalculator.MVVM.ViewModel;
using System.Diagnostics;

namespace PaceCalculator.Commands
{
    public class SetView_PaceConversion : RelayCommand
    {
        private MainViewModel _viewModel;

        public SetView_PaceConversion(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _viewModel.CurrentView = _viewModel.PaceConversionVM;
        }

    }
}
