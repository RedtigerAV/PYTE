using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Pyte.Models {
    public delegate void ChangeTabItem();

    public static class WorkWithTabControl/*: INotifyPropertyChanged*/ {
        //public static WorkWithTabControl InstanceTabControl = new WorkWithTabControl();

        public static event ChangeTabItem ChangeTabItemEvent;
        //public event PropertyChangedEventHandler PropertyChanged;

        public static int SelectedTabItem { get; set; } = 0;

        public static void ChangeTabItemMethod() {
            ChangeTabItemEvent?.Invoke();
        }
    }
}
