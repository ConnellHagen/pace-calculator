using PaceCalculator.Core;
using PaceCalculator.MVVM.Model;
using PaceCalculator.MVVM.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;

namespace PaceCalculator.Commands
{
    public class DistancedCalculateButtonPressed : RelayCommand
    {
        private DistancedIntervalViewModel _viewModel;
        public DistancedCalculateButtonPressed(DistancedIntervalViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
            _viewModel.DistancedGridRows.CollectionChanged += OnDistancedGridCollectionChanged;
        }


        public override bool CanExecute(object? parameter)
        {
            foreach(DistancedIntervalGridRow row in _viewModel.DistancedGridRows)
            {
                if (!row.IsValid) return false;
            }
            return true;
        }

        public override void Execute(object? parameter)
        { }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.IsPaceShown))
            {
                Debug.WriteLine("IsPaceShown Updated");
            }
        }
        private void OnDistancedGridCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (DistancedIntervalGridRow item in e.OldItems)
                    item.PropertyChanged -= OnDistancedGridRowChanged;
            }
            if (e.NewItems != null)
            {
                foreach (DistancedIntervalGridRow item in e.NewItems)
                    item.PropertyChanged += OnDistancedGridRowChanged;
            }
            OnCanExecuteChanged();
        }

        private void OnDistancedGridRowChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

    }
}
