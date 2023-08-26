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
    public class PaceShownToPaceDisplayHeightConverter : IValueConverter
    {
        public object Convert(object paceDisplayShown, Type targetType, object parameter, CultureInfo culture)
        {
            if (paceDisplayShown as bool? == null)
            {
                throw new ArgumentNullException();
            }

            if ((bool)paceDisplayShown)
            {
                return new GridLength(70.0);
            }
            else
            {
                return new GridLength(0.0);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
