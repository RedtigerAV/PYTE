using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Pyte.Models {

    public class MissionCondition: INotifyPropertyChanged {
        public MissionCondition(string condition) {
            Condition = condition;
        }
        public string Condition { get; set; }

        private int countCondition;

        public int CountCondition {
            get { return countCondition; }
            set {
                countCondition = value;
                OnPropertyChange(nameof(CountCondition));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChange(string name) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }

    public static class WorkWithChart {
        public static ObservableCollection<MissionCondition> AllConditions { get; set; } = new ObservableCollection<MissionCondition>();
        static WorkWithChart() {
            AllConditions.Add(new MissionCondition("Активные"));
            AllConditions.Add(new MissionCondition("Завершенные"));
        }

        public static int GetCountActive(Mission item) {
            int cnt = 1;
            if (item == null)
                return 0;
            foreach (Mission child in item.ChildrenView) {
                if (child != null && !child.IsFinished) {
                    cnt += GetCountActive(child);
                }
            }
            return cnt;
        }

        public static int GetCountInActive(Mission item) {
            int cnt = 0;
            if (item == null)
                return 0;
            foreach (Mission child in item.ChildrenView) {
                if (child != null && child.IsFinished) {
                    cnt += GetCountActive(child);
                }
            }
            return cnt;
        }
    }
}
