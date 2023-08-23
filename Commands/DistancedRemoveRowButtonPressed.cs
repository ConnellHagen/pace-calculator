using PaceCalculator.Core;
using PaceCalculator.MVVM.Model;
using PaceCalculator.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace PaceCalculator.Commands
{
    public class DistancedRemoveRowButtonPressed : RelayCommand
    {
        private DistancedIntervalViewModel _viewModel;
        public DistancedRemoveRowButtonPressed(DistancedIntervalViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
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

            _viewModel.DistancedGridRows.RemoveAt(row);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        { }
    }
}
