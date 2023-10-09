using System;

namespace PaceCalculator.MVVM.Model
{
    public class Interval
    {
        public float Distance { get; set; }
        public float Seconds { get; set; }
        public (int, int) AvgPace { get; set; }

        public Interval()
        {
            Distance = 0;
            Seconds = 0;
            AvgPace = (0, 0); // pace in format of (minutes, Seconds) per unit (mile or km)
        }

        // `time` is of the format (hours, minutes, Seconds)
        public Interval(float Distance, (int, int, float) time)
        {
            this.Distance = Distance;
            Seconds = time.Item1 * 3600 + time.Item2 * 60 + time.Item3;
            AvgPace = (0, 0);
            CalcAvgPace();
        }

        public Interval(float Distance, (int, int) AvgPace)
        {
            this.Distance = Distance;
            Seconds = 0;
            this.AvgPace = AvgPace;
            CalcSeconds();
        }

        public Interval((int, int, float) time, (int, int) AvgPace)
        {
            Distance = 0;
            Seconds = time.Item1 * 3600 + time.Item2 * 60 + time.Item3;
            this.AvgPace = AvgPace;
            CalcDistance();
        }

        public void CalcAvgPace()
        {
            float secPerDistUnit = Seconds / Distance;

            int paceMinutes = (int)secPerDistUnit / 60;
            secPerDistUnit -= paceMinutes * 60;

            int paceSeconds = (int)Math.Round(secPerDistUnit);
            if (paceSeconds == 60)
            {
                paceSeconds = 0;
                paceMinutes++;
            }

            AvgPace = (paceMinutes, paceSeconds);
        }

        public void CalcSeconds()
        {
            int paceSecondsPerUnit = 60 * AvgPace.Item1 + AvgPace.Item2;
            Seconds = paceSecondsPerUnit * Distance;
        }

        public void CalcDistance()
        {
            int paceSecondsPerUnit = 60 * AvgPace.Item1 + AvgPace.Item2;
            Distance = Seconds / paceSecondsPerUnit;
        }
    }
}