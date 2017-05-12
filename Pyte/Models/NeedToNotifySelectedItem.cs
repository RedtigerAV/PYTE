using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

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

        private MiniMark selectedMark;
        public MiniMark SelectedMark {
            get { return selectedMark; }
            set {
                if (value != null) {
                    selectedMark = value;
                    OnPropertyChanged(nameof(SelectedMark));
                }
            }
        }

        private ObservableCollection<MiniMark> newTaskMarks = new ObservableCollection<MiniMark>();
        public ObservableCollection<MiniMark> NewTaskMarks {
            get { return newTaskMarks; }
            set {
                newTaskMarks = value;
            }
        }

        private bool newTaskFlyoutIsOpen = false;
        public bool NewTaskFlyoutIsOpen {
            get { return newTaskFlyoutIsOpen; }
            set {
                newTaskFlyoutIsOpen = value;
                //Instance.NewTaskMarks.Clear();
                OnPropertyChanged(nameof(NewTaskFlyoutIsOpen));
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
