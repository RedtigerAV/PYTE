using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Pyte.Models {
    public delegate void ChangeTabItem();
    public delegate void TasksEmptyDelegate();

    public class WorkWithTabControl: INotifyPropertyChanged {
        public static WorkWithTabControl InstanceTabControl = new WorkWithTabControl();

        public event ChangeTabItem ChangeTabItemEvent;
        public event PropertyChangedEventHandler PropertyChanged;
        public event TasksEmptyDelegate TasksEmptyEvent;

        private void OnPropetryChanged(string name) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private string selectedTabName = "Все задачи";
        public string SelectedTabName {
            get {
                return selectedTabName;
            }
            set {
                selectedTabName = value;
                OnPropetryChanged(nameof(SelectedTabName));
            }
        }

        public int SelectedTabItem { get; set; } = 0;

        public void ChangeTabItemMethod() {
            ChangeTabItemEvent?.Invoke();
        }

        public void OnTasksEmpty() {
            TasksEmptyEvent?.Invoke();
        }
    }
}
