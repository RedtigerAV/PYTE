using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Pyte.Models {
    public class WorkWithCalendar: INotifyPropertyChanged {
        public static WorkWithCalendar CalendatInstance = new WorkWithCalendar();

        public int calendarWay = 0;

        private DateTime firstWayDay;
        public DateTime FirstWayDay {
            get { return firstWayDay; }
            set {
                firstWayDay = value;
                OnPropertyChanged(nameof(FirstWayDay));
            }
        }

        private DateTime secondWayStartDay;
        public DateTime SecondWayStartDay {
            get { return secondWayStartDay; }
            set {
                secondWayStartDay = value;
                OnPropertyChanged(nameof(SecondWayStartDay));
            }
        }

        private DateTime secondWayFinishDay;
        public DateTime SecondWayFinishDay {
            get { return secondWayFinishDay; }
            set {
                secondWayFinishDay = value;
                OnPropertyChanged(nameof(SecondWayFinishDay));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
