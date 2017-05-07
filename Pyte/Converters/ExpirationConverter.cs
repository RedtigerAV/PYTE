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
    public class ExpirationConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            DateTime dt = (DateTime)(value);
            DateTime dtNow = DateTime.Today;
            int result = dtNow.CompareTo(dt);
            if (result > 0) {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
