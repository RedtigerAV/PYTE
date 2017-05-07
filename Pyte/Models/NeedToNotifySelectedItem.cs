using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Pyte.Models {

    public delegate void OpenNewTaskFlyoutEventHadler();
    public delegate void UpdateDatePickerEventHadler();

    public class NeedToNotifySelectedItem : INotifyPropertyChanged {

        public static NeedToNotifySelectedItem Instance { get; } = new NeedToNotifySelectedItem();

        public Mission NeedToNotify {
            get {
                return TreeViewModels.SelectedItemFromTreeView;
            }
            set {
                TreeViewModels.SelectedItemFromTreeView = value;
                OnPropertyChanged("NeedToNotify");
            }
        }

        public long NotifyParentID {
            get {
                return TreeViewModels.ParentID;
            }
            set {
                TreeViewModels.ParentID = value;
                OnPropertyChanged("NotifyParentID");
            }
        }

        public bool NotifyOpenFlyout {
            get {
                return TreeViewModels.OpenFlyout;
            }
            set {
                TreeViewModels.OpenFlyout = value;
                if (value) {
                    OpenNewTaskFlyout();
                }
                OnPropertyChanged("NotifyOpenFlyout");
            }
        }

        public event OpenNewTaskFlyoutEventHadler OpenNewTaskFlyout;

        public event UpdateDatePickerEventHadler UpdateDatePicker;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void UpdateBlackoutsDate() {
            UpdateDatePicker();
        }
    }
}
