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
    public class MinMaxDateShowConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            DateTime dt = (DateTime)(value);
            if (dt == DateTime.MinValue.Date || dt == DateTime.MaxValue.Date)
                return "";
            return dt.ToLongDateString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
