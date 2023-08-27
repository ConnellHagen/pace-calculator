using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PaceCalculator.Converters
{
    public class AveragePaceToStringConverter : IValueConverter
    {
        public object Convert(object avgPace, Type targetType, object parameter, CultureInfo culture)
        {
            if (avgPace as (int, int)? == null)
            {
                throw new ArgumentNullException();
            }

            var _avgPace = ((int, int))avgPace;
            string extra0 = "";
            if(_avgPace.Item2 < 10) extra0 = "0";

            return $"Average Pace: {_avgPace.Item1}'{extra0}{_avgPace.Item2}\"";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
