using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Pyte.Models {
    public class Mission: INotifyPropertyChanged {

        private static long id_counter = 1;

        #region Constructors
        public Mission (string name) {
            Name = (UnNamedCounter == -1)? name : $"Новая задача({UnNamedCounter})";
            ID = id_counter;
            IsSelected = false;
            id_counter++;
            StartDate = DateTime.MinValue;
            FinishDate = DateTime.MinValue;

            IsChecked = false;
            IsImportant = false;
            Children = new ObservableCollection<Mission>();
            Marks = new ObservableCollection<string>();

        }
        #endregion

        #region Members

        #region Name
        private string name;
        public string Name {
            get {
                return name;
            }
            set {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        #endregion

        #region IsSelected
        private bool isSelected;
        public bool IsSelected {
            get {
                return isSelected;
            }
            set {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
        #endregion

        #region UnNamedCounter
        private long unNamedCounter;
        public long UnNamedCounter { get; set; } = -1;
        #endregion

        #region ID
        private long id;
        public long ID {
            get {
                return id;
            }
            set {
                id = value;
            }
        }
        #endregion

        #region StartDate
        private DateTime startDate;
        public DateTime StartDate {
            get {
                return startDate;
            }
            set {
                startDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        #endregion

        #region FinishDate
        private DateTime finishDate;
        public DateTime FinishDate {
            get {
                return finishDate;
            }
            set {
                finishDate = value;
                OnPropertyChanged("FinishDate");
            }
        }
        #endregion

        #region IsChecked
        private bool isChecked;
        public bool IsChecked {
            get {
                return isChecked;
            }
            set {
                isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
        #endregion

        #region IsImportant
        private bool isImportant;
        public bool IsImportant {
            get {
                return isImportant;
            }
            set {
                isImportant = value;
                OnPropertyChanged("IsImportant");
            }
        }
        #endregion

        #region IsFinished
        private bool isFinished;
        public bool IsFinished {
            get {
                return isFinished;
            }
            set {
                isFinished = value;
                OnPropertyChanged("IsFinished");
            }
        }
        #endregion

        #region Marks
        private ObservableCollection<string> marks;
        public ObservableCollection<string> Marks {
            get {
                return marks;
            }
            set {
                marks = value;
            }
        }
        #endregion

        #region Children
        private ObservableCollection<Mission> children;
        public ObservableCollection<Mission> Children {
            get {
                return children;
            }
            set {
                children = value;
            }
        }
        #endregion

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
