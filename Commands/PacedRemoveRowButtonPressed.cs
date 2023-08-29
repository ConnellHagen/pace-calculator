using PaceCalculator.Core;
using PaceCalculator.MVVM.ViewModel;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace PaceCalculator.Commands
{
    public class PacedRemoveRowButtonPressed : RelayCommand
    {
        private PacedIntervalViewModel _viewModel;
        public PacedRemoveRowButtonPressed(PacedIntervalViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            int row;

            Button? button = parameter as Button;
            if(button == null) throw new ArgumentNullException(nameof(button));

            Grid? grid = VisualTreeHelper.GetParent(button) as Grid;
            if(grid == null) throw new ArgumentNullException(nameof(grid));

            ContentPresenter? presenter = VisualTreeHelper.GetParent(grid) as ContentPresenter;
            if(presenter == null) throw new ArgumentNullException(nameof(presenter));

            StackPanel? panel = VisualTreeHelper.GetParent(presenter) as StackPanel;
            if(panel == null) throw new ArgumentNullException(nameof(panel));

            row = panel.Children.IndexOf(presenter);

            _viewModel.PacedGridRows.RemoveAt(row);
        }

    }
}
