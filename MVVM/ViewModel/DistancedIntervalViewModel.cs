using PaceCalculator.Commands;
using PaceCalculator.Core;
using PaceCalculator.MVVM.Model;
using System.Collections.ObjectModel;

namespace PaceCalculator.MVVM.ViewModel
{
    public class DistancedIntervalViewModel : ObservableObject
    {
        private Calculator _calculator; //for pace related calculations
        public Calculator DistancedCalculator
        {
            get { return _calculator; }
        }

        private bool isPaceShown; // binds to the height of the bottom grid row
        public bool IsPaceShown
        {
            get { return isPaceShown; }
            set 
            { 
                if(isPaceShown != value)
                {
                    isPaceShown = value;
                    OnPropertyChanged(nameof(IsPaceShown));
                }
            }
        }

        private ObservableCollection<DistancedIntervalGridRow> _distancedGridRows;
        public ObservableCollection<DistancedIntervalGridRow> DistancedGridRows
        {
            get { return _distancedGridRows; }
            set
            {
                _distancedGridRows = value;
                OnPropertyChanged(nameof(DistancedGridRows));
            }
        }

        public DistancedCalculateButtonPressed CalculateButtonCommand { get; }
        public DistancedAddRowButtonPressed AddRowButtonCommand { get; }
        public DistancedRemoveRowButtonPressed RemoveRowButtonCommand { get; }

        public DistancedIntervalViewModel()
        {
            _calculator = new Calculator();
            isPaceShown = false;

            _distancedGridRows = new ObservableCollection<DistancedIntervalGridRow>();

            CalculateButtonCommand = new DistancedCalculateButtonPressed(this);
            AddRowButtonCommand = new DistancedAddRowButtonPressed(this);
            RemoveRowButtonCommand = new DistancedRemoveRowButtonPressed(this);

            DistancedGridRows.Add(new DistancedIntervalGridRow()); // one row by default to start with

        }

    }
}
