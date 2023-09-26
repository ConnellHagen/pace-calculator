using PaceCalculator.Commands;
using PaceCalculator.Core;
using PaceCalculator.MVVM.Model;
using System.Collections.ObjectModel;

namespace PaceCalculator.MVVM.ViewModel
{
    public class PacedIntervalViewModel : ObservableObject
    {
        private Calculator _calculator; //for pace related calculations
        public Calculator PacedCalculator
        {
            get { return _calculator; }
        }

        private bool isPaceShown; // binds to the height of the bottom grid row
        public bool IsPaceShown
        {
            get { return isPaceShown; }
            set
            {
                if (isPaceShown != value)
                {
                    isPaceShown = value;
                    OnPropertyChanged(nameof(IsPaceShown));
                }
            }
        }

        private ObservableCollection<PacedIntervalGridRow> _pacedGridRows;
        public ObservableCollection<PacedIntervalGridRow> PacedGridRows
        {
            get { return _pacedGridRows; }
            set
            {
                _pacedGridRows = value;
                OnPropertyChanged(nameof(PacedGridRows));
            }
        }

        public PacedCalculateButtonPressed CalculateButtonCommand { get; }
        public PacedAddRowButtonPressed AddRowButtonCommand { get; }
        public PacedRemoveRowButtonPressed RemoveRowButtonCommand { get; }

        public PacedIntervalViewModel()
        {
            _calculator = new Calculator();
            isPaceShown = false;

            _pacedGridRows = new ObservableCollection<PacedIntervalGridRow>();

            CalculateButtonCommand = new PacedCalculateButtonPressed(this);
            AddRowButtonCommand = new PacedAddRowButtonPressed(this);
            RemoveRowButtonCommand = new PacedRemoveRowButtonPressed(this);

            PacedGridRows.Add(new PacedIntervalGridRow()); // the grid starts with one row by default
        }
    }
}
