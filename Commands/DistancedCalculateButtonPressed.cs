using PaceCalculator.Core;
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
    public class DistancedCalculateButtonPressed : RelayCommand
    {
        private DistancedIntervalViewModel _viewModel;
        public DistancedCalculateButtonPressed(DistancedIntervalViewModel viewModel)
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
            Debug.WriteLine("hello");
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.IsPaceShown))
            {
                Debug.WriteLine("IsPaceShown Updated!!!!");
            }
        }

    }
}
