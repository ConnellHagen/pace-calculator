using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PaceCalculator.Converters
{
    public class MultiValidityToEntryBoxColorConverter : IMultiValueConverter
    {
        public object Convert(object[] isValid, Type targetType, object parameter, CultureInfo culture)
        {
            bool allValid = true;
            foreach (object _condition in isValid)
            {
                bool? condition = _condition as bool?;
                if(condition == null) throw new ArgumentNullException();

                allValid = allValid && (bool)condition;
            }

            if (allValid)
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#676767"));
            }
            else
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C96964"));
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
