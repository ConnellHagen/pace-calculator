using PaceCalculator.Core;
using PaceCalculator.MVVM.Model;
using PaceCalculator.MVVM.ViewModel;
using System.ComponentModel;
using System.Diagnostics;

namespace PaceCalculator.Commands
{
    public class DistancedAddRowButtonPressed : RelayCommand
    {
        private DistancedIntervalViewModel _viewModel;
        public DistancedAddRowButtonPressed(DistancedIntervalViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _viewModel.DistancedGridRows.Add(new DistancedIntervalGridRow());
        }

    }
}
