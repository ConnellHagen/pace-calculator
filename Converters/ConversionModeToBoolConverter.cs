using PaceCalculator.MVVM.ViewModel;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PaceCalculator.Converters
{
    class ConversionModeToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.GetType().IsEnum)
            {
                return (Enum.Equals(value, parameter));
            }
            else
            {
                return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? v = value as bool?;
            if (v == null) { throw new ArgumentNullException(); }

            if ((bool)v) { return parameter;  }

            if ((PaceConversionMode)parameter == PaceConversionMode.MI_TO_KM)
            {
                return PaceConversionMode.KM_TO_MI;
            }
            else
            {
                return PaceConversionMode.MI_TO_KM;
            }
        }
    }
}
