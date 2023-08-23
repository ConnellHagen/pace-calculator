using System.Collections.Generic;
using System.Diagnostics;

namespace PaceCalculator.MVVM.Model
{
    public class Calculator
    {
        private List<Interval> intervals;
        public (int, int) avgPace { get; set; }

        public Calculator()
        {
            intervals = new List<Interval>();
            avgPace = (0, 0);
        }

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
                totalSeconds += interval.seconds;
                totalDistance += interval.distance;
            }
            Interval calc_int = new Interval(totalDistance, (0, 0, totalSeconds));
            avgPace = calc_int.avgPace;

            Debug.WriteLine(avgPace);
        }
    }
}
