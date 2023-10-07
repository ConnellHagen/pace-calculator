using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaceCalculator.MVVM.Model
{
    public class PaceHelper
    {
        public static bool IsValidFloatEntry(string s)
        {
            float? val = ToFloat(s);

            if (val == null)
            {
                return false;
            }

            return val >= 0;
        }
        public static float? ToFloat(string s)
        {
            if (s == "") { return 0.0f; }

            float val;

            if (float.TryParse(s, out val))
            {
                return val;
            }

            return null;
        }

        public static bool IsValidIntEntry(string s)
        {
            int? val = ToInt(s);

            if (val == null)
            {
                return false;
            }

            return val >= 0;
        }
        public static int? ToInt(string s)
        {
            if (s == "") { return 0; }

            int val;

            if (int.TryParse(s, out val))
            {
                return val;
            }

            return null;
        }
    }
}
