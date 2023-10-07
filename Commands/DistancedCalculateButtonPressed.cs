using PaceCalculator.Core;
using PaceCalculator.MVVM.Model;
using PaceCalculator.MVVM.ViewModel;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;

namespace PaceCalculator.Commands
{
    public class DistancedCalculateButtonPressed : RelayCommand
    {
        private DistancedIntervalViewModel _viewModel;
        public DistancedCalculateButtonPressed(DistancedIntervalViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.DistancedGridRows.CollectionChanged += OnDistancedGridCollectionChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            if (_viewModel.DistancedGridRows.Count == 0) return false;
            foreach(DistancedIntervalGridRow row in _viewModel.DistancedGridRows)
            {
                if (!row.IsValid) return false;
            }
            return true;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.DistancedCalculator.Clear();

            foreach (DistancedIntervalGridRow row in _viewModel.DistancedGridRows)
            {
                float? distance = PaceHelper.ToFloat(row.Distance);
                int? hours = PaceHelper.ToInt(row.Hours);
                int? minutes = PaceHelper.ToInt(row.Minutes);
                float? seconds = PaceHelper.ToFloat(row.Seconds);

                if (distance == null || hours == null || minutes == null || seconds == null)
                {
                    throw new ArgumentNullException();
                }

                _viewModel.DistancedCalculator.AddInterval(
                    new Interval(  
                        (float)distance, (
                            (int)hours,
                            (int)minutes,
                            (float)seconds
                        )
                    )
                );
            }

            _viewModel.DistancedCalculator.CalcAvgPace();

            _viewModel.IsPaceShown = true;
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
