using System;

namespace PaceCalculator.MVVM.Model
{
    public class Interval
    {
        public float distance { get; set; }
        public float seconds { get; set; }
        public (int, int) avgPace { get; set; }

        public Interval()
        {
            distance = 0;
            seconds = 0;
            avgPace = (0, 0); // pace in format of (minutes, seconds) per unit (mile or km)
        }

        // `time` is of the format (hours, minutes, seconds)
        public Interval(float distance, (int, int, float) time)
        {
            this.distance = distance;
            seconds = time.Item1 * 3600 + time.Item2 * 60 + time.Item3;
            avgPace = (0, 0);
            CalcAvgPace();
        }

        public Interval(float distance, (int, int) avgPace)
        {
            this.distance = distance;
            seconds = 0;
            this.avgPace = avgPace;
            CalcSeconds();
        }

        public Interval((int, int, float) time, (int, int) avgPace)
        {
            distance = 0;
            seconds = time.Item1 * 3600 + time.Item2 * 60 + time.Item3;
            this.avgPace = avgPace;
            CalcDistance();
        }

        public void CalcAvgPace()
        {
            float secPerDistUnit = seconds / distance;

            int paceMinutes = (int)secPerDistUnit / 60;
            secPerDistUnit -= paceMinutes * 60;

            int paceSeconds = (int)Math.Round(secPerDistUnit);

            avgPace = (paceMinutes, paceSeconds);
        }

        public void CalcSeconds()
        {
            int paceSecondsPerUnit = 60 * avgPace.Item1 + avgPace.Item2;
            seconds = paceSecondsPerUnit * distance;
        }

        public void CalcDistance()
        {
            int paceSecondsPerUnit = 60 * avgPace.Item1 + avgPace.Item2;
            distance = seconds / paceSecondsPerUnit;
        }
    }
}