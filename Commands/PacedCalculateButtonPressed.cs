using PaceCalculator.Core;
using PaceCalculator.MVVM.Model;
using PaceCalculator.MVVM.ViewModel;
using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace PaceCalculator.Commands
{
    public class PacedCalculateButtonPressed : RelayCommand
    {
        private PacedIntervalViewModel _viewModel;
        public PacedCalculateButtonPressed(PacedIntervalViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.PacedGridRows.CollectionChanged += OnPacedGridCollectionChanged;
        }


        public override bool CanExecute(object? parameter)
        {
            if (_viewModel.PacedGridRows.Count == 0) return false;
            foreach(PacedIntervalGridRow row in _viewModel.PacedGridRows)
            {
                if (!row.IsValid) return false;
            }
            return true;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.PacedCalculator.Clear();

            foreach (PacedIntervalGridRow row in _viewModel.PacedGridRows)
            {
                int? timeHours = PaceHelper.ToInt(row.TimeHours);
                int? timeMinutes = PaceHelper.ToInt(row.TimeMinutes);
                float? timeSeconds = PaceHelper.ToFloat(row.TimeSeconds);
                int? paceMinutes = PaceHelper.ToInt(row.PaceMinutes);
                int? paceSeconds = PaceHelper.ToInt(row.PaceSeconds);

                if(timeHours == null || timeMinutes == null || timeSeconds == null || paceMinutes == null || paceSeconds == null)
                {
                    throw new ArgumentNullException();
                }

                _viewModel.PacedCalculator.AddInterval(
                    new Interval(
                        (
                            (int)timeHours,
                            (int)timeMinutes,
                            (float)timeSeconds
                        ),
                        (
                            (int)paceMinutes,
                            (int)paceSeconds
                        )
                    )
                );
            }

            _viewModel.PacedCalculator.CalcAvgPace();

            _viewModel.IsPaceShown = true;
        }

        private void OnPacedGridCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (PacedIntervalGridRow item in e.OldItems)
                    item.PropertyChanged -= OnPacedGridRowChanged;
            }
            if (e.NewItems != null)
            {
                foreach (PacedIntervalGridRow item in e.NewItems)
                    item.PropertyChanged += OnPacedGridRowChanged;
            }
            OnCanExecuteChanged();
        }

        private void OnPacedGridRowChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

    }
}
