using PaceCalculator.Core;
using PaceCalculator.MVVM.Model;
using PaceCalculator.MVVM.ViewModel;
using System.Collections.Specialized;
using System.ComponentModel;

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
                _viewModel.DistancedCalculator.AddInterval(
                    new Interval(
                        (float)DistancedIntervalGridRow.ToFloat(row.Distance), (
                            (int)DistancedIntervalGridRow.ToInt(row.Hours),
                            (int)DistancedIntervalGridRow.ToInt(row.Minutes),
                            (float)DistancedIntervalGridRow.ToFloat(row.Seconds)
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
