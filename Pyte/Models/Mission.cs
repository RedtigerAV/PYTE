﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Pyte.Models {
    public class Mission: INotifyPropertyChanged {

        private static long id_counter = 0;

        public Predicate<Mission> Filter { get; set; }

        public Predicate<Mission> TabControlFilter { get; set; } = null;

        public ICollectionView ChildrenView {
            get { return _childrenSource.View; }
        }

        private readonly CollectionViewSource _childrenSource = new CollectionViewSource();

        public bool Appropriate(Mission item) {
            bool flag = false;
            foreach (Mission it in item.ChildrenView) {
                if (it.IsAccepted)
                    flag = true;
            }
            return flag;
        }

        private void _childrenSource_Filter(object sender, FilterEventArgs e) {
                Mission item = (Mission)e.Item;
                if (item == null) {
                    e.Accepted = false;
                    return;
                }

                item.IsAccepted =  ((Appropriate(item) || Filter == null || Filter(item)) && 
                                    (TabControlFilter == null || TabControlFilter(item)));

                e.Accepted = item.IsAccepted;
        }

        #region Constructors

        public Mission (string name, long fathID){
            Children = new ObservableCollection<Mission>();
            FatherID = fathID;
            IsAccepted = true;
            ID = id_counter;
            Name = name;
            IsSelected = false;
            id_counter++;
            StartDate = DateTime.MinValue.Date;
            FinishDate = DateTime.MaxValue.Date;

            IsChecked = false;
            IsImportant = false;
            Marks = new ObservableCollection<string> { "Lol", "What", "Okey"};
            _childrenSource.Source = Children;
            _childrenSource.Filter += _childrenSource_Filter;

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

        #region IsAccepted
        private bool isAccepted;
        public bool IsAccepted {
            get { return isAccepted; }
            set { isAccepted = value; }
        }
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

        #region fatherID

        private long fatherID;
        public long FatherID {
            get {
                return fatherID;
            }
            set {
                fatherID = value;
                OnPropertyChanged("FatherID");
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

        public void Add(Mission item) {
            if (item != null)
                Children.Add(item);
            ChildrenView?.Refresh();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
