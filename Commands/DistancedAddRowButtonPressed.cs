using PaceCalculator.Core;
using PaceCalculator.MVVM.Model;
using PaceCalculator.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaceCalculator.Commands
{
    public class DistancedAddRowButtonPressed : RelayCommand
    {
        private DistancedIntervalViewModel _viewModel;
        public DistancedAddRowButtonPressed(DistancedIntervalViewModel viewModel)
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
            _viewModel.DistancedGridRows.Add(new DistancedIntervalGridRow());
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        { }

    }
}
