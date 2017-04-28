using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Pyte.Models;
using System.Collections.ObjectModel;

namespace Pyte.Converters {
    public class TaskNameConverter : IValueConverter {

        public static HashSet<long> FindedUnNamedCounters;

        public bool CheckUnNaming(string s) {
            bool flag = false;
            for (int i = 0; i < s.Length; i++) {
                if (s[i] != ' ')
                    flag = true;
            }
            return flag;
        }

        public void GetUnNamedCount(ObservableCollection<Mission> parametr) {
            if (parametr.Count == 0) {
                return;
            }

            for (int i = 0; i < parametr.Count; i++) {
                if (parametr[i].UnNamedCounter != -1 && parametr[i] != NeedToNotifySelectedItem.Instance.NeedToNotify) {
                    FindedUnNamedCounters.Add(parametr[i].UnNamedCounter);
                }
                GetUnNamedCount(parametr[i].Children);
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string name = System.Convert.ToString(value);

            long ans = 1;
            FindedUnNamedCounters.Clear();
            GetUnNamedCount(TreeViewModels.AllMissionCollection);
            if (FindedUnNamedCounters.Count == 0) {
                NeedToNotifySelectedItem.Instance.NeedToNotify.UnNamedCounter = 1;
                ans = 1;
            } else {
                for (int i = 1; i <= FindedUnNamedCounters.Count; i++) {
                    if (!FindedUnNamedCounters.Contains(i)) {
                        NeedToNotifySelectedItem.Instance.NeedToNotify.UnNamedCounter = i;
                        ans = i;
                    }
                }
            }
            return name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
