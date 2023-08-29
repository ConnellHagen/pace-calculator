﻿using PaceCalculator.Core;
using PaceCalculator.MVVM.ViewModel;

namespace PaceCalculator.Commands
{
    public class SetView_PacedInterval : RelayCommand
    {
        private MainViewModel _viewModel;

        public SetView_PacedInterval(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _viewModel.CurrentView = _viewModel.PacedIntervalVM;
        }

    }
}
