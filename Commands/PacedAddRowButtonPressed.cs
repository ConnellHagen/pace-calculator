using PaceCalculator.Core;
using PaceCalculator.MVVM.Model;
using PaceCalculator.MVVM.ViewModel;

namespace PaceCalculator.Commands
{
    public class PacedAddRowButtonPressed : RelayCommand
    {
        private PacedIntervalViewModel _viewModel;
        public PacedAddRowButtonPressed(PacedIntervalViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _viewModel.PacedGridRows.Add(new PacedIntervalGridRow());
        }

    }
}
