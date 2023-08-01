using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PaceCalculator
{
    public class Interval
    {
        public float distance { get; set; }
        public float seconds { get; set; }
        public (int, int) avg_pace { get; set; }

        public Interval()
        {
            distance = 0;
            seconds = 0;
            avg_pace = (0, 0); // pace in format of (minutes, seconds) per unit (mile or km)
        }

        public Interval(float distance, float seconds)
        {
            this.distance = distance;
            this.seconds = seconds;
            avg_pace = (0, 0);
            calc_avg_pace();
        }

        // `time` is of the format (hours, minutes, seconds)
        public Interval(float distance, (int, int, float) time)
        {
            this.distance = distance;
            seconds = time.Item1 * 3600 + time.Item2 * 60 + time.Item3;
            avg_pace = (0, 0);
            calc_avg_pace();
        }

        public Interval(float distance, (int, int) avg_pace)
        {
            this.distance = distance;
            this.seconds = 0;
            this.avg_pace = avg_pace;
            calc_seconds();
        }

        public Interval((int, int, float) time, (int, int) avg_pace)
        {
            distance = 0;
            seconds = time.Item1 * 3600 + time.Item2 * 60 + time.Item3;
            this.avg_pace = avg_pace;
            calc_distance();
        }

        public void calc_avg_pace()
        {
            float secPerDistUnit = seconds / distance;

            int paceMinutes = (int)secPerDistUnit / 60;
            secPerDistUnit -= paceMinutes * 60;

            int paceSeconds = (int)Math.Round(secPerDistUnit);

            avg_pace = (paceMinutes, paceSeconds);
        }

        public void calc_seconds()
        {
            int paceSecondsPerUnit = 60 * avg_pace.Item1 + avg_pace.Item2;
            seconds = paceSecondsPerUnit * distance;
        }

        public void calc_distance()
        {
            int paceSecondsPerUnit = 60 * avg_pace.Item1 + avg_pace.Item2;
            distance = seconds / paceSecondsPerUnit;
        }
    }

    public class Calculator
    {
        private List<Interval> intervals;

        public (int, int) avg_pace { get; set; }
        public Calculator()
        {
            intervals = new List<Interval>();
            avg_pace = (0, 0);
        }

        public void add_interval(Interval interval)
        {
            intervals.Add(interval);
            calc_avg_pace();
        }

        private void calc_avg_pace()
        {
            float totalSeconds = 0.0f;
            float totalDistance = 0.0f;
            foreach(Interval interval in intervals)
            {
                totalSeconds += interval.seconds;
                totalDistance += interval.distance;
            }
            Interval calc_int = new Interval(totalDistance, totalSeconds);
            avg_pace = calc_int.avg_pace;
        }
    }
}