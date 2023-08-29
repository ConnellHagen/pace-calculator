using PaceCalculator.Core;
using System.Collections.Generic;
using System.Diagnostics;

namespace PaceCalculator.MVVM.Model
{
    public class Calculator : ObservableObject
    {
        private List<Interval> intervals = new List<Interval>();
        private (int, int) _avgPace = (0, 0);
        public (int, int) AvgPace
        { 
            get { return _avgPace; }
            set
            {
                if(_avgPace != value)
                {
                    _avgPace = value;
                    OnPropertyChanged(nameof(AvgPace));
                }
            }
        }

        public Calculator()
        { }

        public void Clear()
        {
            intervals = new List<Interval>();
        }

        public void AddInterval(Interval interval)
        {
            intervals.Add(interval);
        }

        public void CalcAvgPace()
        {
            float totalSeconds = 0.0f;
            float totalDistance = 0.0f;
            foreach (Interval interval in intervals)
            {
                totalSeconds += interval.Seconds;
                totalDistance += interval.Distance;
            }
            Interval calc_int = new Interval(totalDistance, (0, 0, totalSeconds));
            AvgPace = calc_int.AvgPace;
        }
    }
}
