using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace HomeNetwork.Helpers
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return new SolidColorBrush(Colors.PowderBlue);
            }

            return System.Convert.ToBoolean(value) ?
                new SolidColorBrush(Colors.Red) :
                new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
