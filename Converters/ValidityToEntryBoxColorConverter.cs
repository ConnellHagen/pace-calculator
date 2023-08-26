using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace PaceCalculator.Converters
{
    public class ValidityToEntryBoxColorConverter : IValueConverter
    {
        public object Convert(object isValid, Type targetType, object parameter, CultureInfo culture)
        {
            if (isValid as bool? == null)
            {
                throw new ArgumentNullException();
            }

            if ((bool)isValid)
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#676767"));
            }
            else
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C96964"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
