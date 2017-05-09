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
    public class TreeViewFilterConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            ObservableCollection<Mission> mainObs = (ObservableCollection<Mission>)(value);
            ObservableCollection<Mission> filteredColl = new ObservableCollection<Mission> { new Mission("kek", -1)};
            for (int i = 0; i < mainObs.Count; i++) {
                if (mainObs[i].IsImportant) {
                    filteredColl.Add(mainObs[i]);
                }
            }
            return mainObs;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
