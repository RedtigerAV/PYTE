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
    public class StartDateToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            DateTime dt = System.Convert.ToDateTime(value);
            return ("Начало: " + dt.ToString("m"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
