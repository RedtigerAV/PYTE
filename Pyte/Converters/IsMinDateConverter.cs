using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Pyte.Models;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Pyte.Converters {
    public class IsMinDateConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            DateTime dt = (DateTime)(value);
            if (dt == DateTime.MinValue.Date)
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
