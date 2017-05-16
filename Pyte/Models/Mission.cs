using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using MongoDB.Bson.Serialization.Attributes;
using System.Xml.Serialization;

namespace Pyte.Models {

    [Serializable]
    public class Mission: INotifyPropertyChanged {

        public static long id_counter = 0;

        [XmlIgnore]
        public Predicate<Mission> Filter { get; set; }

        [XmlIgnore]
        public Predicate<Mission> TabControlFilter { get; set; } = null;

        [XmlIgnore]
        public ICollectionView ChildrenView {
            get { return _childrenSource.View; }
        }

        [XmlIgnore]
        public CollectionViewSource _childrenSource = new CollectionViewSource();
        
        public bool Appropriate(Mission item) {
            bool flag = false;
            foreach (Mission it in item.Children) {
                if (it.IsAccepted)
                    flag = true;
            }
            return flag;
        }

        public void _childrenSource_Filter(object sender, FilterEventArgs e) {
            Mission item = (Mission)e.Item;
            if (item == null) {
                e.Accepted = false;
                return;
            }

            if (WorkWithTabControl.InstanceTabControl.SelectedTabItem == 4) {

                item.IsAccepted = (Appropriate(item) || ((Filter == null || Filter(item)) &&
                                    (TabControlFilter == null || TabControlFilter(item))));
            }
            else {

                item.IsAccepted = ((Appropriate(item) || Filter == null || Filter(item)) &&
                                    (TabControlFilter == null || TabControlFilter(item)));
            }

            if (item.IsAccepted && Appropriate(item))
                item.IsExpanded = true;
            else
                item.IsExpanded = false;

            e.Accepted = item.IsAccepted;
        }

        #region Constructors

        public Mission (string name, long fathID){
            Children = new ObservableCollection<Mission>();
            FatherID = fathID;
            IsAccepted = true;
            ID = id_counter;
            IsFinished = false;
            IsFatherFinished = false;
            Name = name;
            IsSelected = false;
            id_counter++;
            StartDate = DateTime.MinValue.Date;
            FinishDate = DateTime.MaxValue.Date;

            FirstColor = true;
            SecondColor = false;
            ThirdColor = false;
            FourthColor = false;
            FifthColor = false;

            IsChecked = false;
            IsImportant = false;
            Marks = new ObservableCollection<MiniMark>();
            _childrenSource = new CollectionViewSource();
            _childrenSource.Source = Children;
            _childrenSource.Filter += _childrenSource_Filter;

        }

        public Mission(Mission item) {
            Children = item.Children;
            FatherID = item.FatherID;
            IsAccepted = item.IsAccepted;
            ID = item.ID;
            isFinished = item.IsFinished;
            Name = item.Name;
            IsFatherFinished = item.IsFatherFinished;
            IsSelected = item.IsSelected;
            IsChecked = item.IsChecked;
            StartDate = item.StartDate;
            FinishDate = item.FinishDate;
            IsImportant = item.IsImportant;
            Marks = item.Marks;

            FirstColor = true;
            SecondColor = false;
            ThirdColor = false;
            FourthColor = false;
            FifthColor = false;


            _childrenSource = new CollectionViewSource();
            _childrenSource.Source = Children;
            _childrenSource.Filter += _childrenSource_Filter;
        }

        public Mission() { }
        #endregion

        #region Members

        private bool firstColor;
        public bool FirstColor { get { return firstColor; }
            set { firstColor = value;
                OnPropertyChanged(nameof(FirstColor));
            }
        }
        private bool secondColor;
        public bool SecondColor {
            get { return secondColor; }
            set {
                secondColor = value;
                OnPropertyChanged(nameof(SecondColor));
            }
        }
        private bool thirdColor;
        public bool ThirdColor {
            get { return thirdColor; }
            set {
                thirdColor = value;
                OnPropertyChanged(nameof(ThirdColor));
            }
        }
        private bool fourthColor;
        public bool FourthColor {
            get { return fourthColor; }
            set {
                fourthColor = value;
                OnPropertyChanged(nameof(FourthColor));
            }
        }
        private bool fifthColor;
        public bool FifthColor {
            get { return fifthColor; }
            set {
                fifthColor = value;
                OnPropertyChanged(nameof(FifthColor));
            }
        }


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

        #region IsExpanded

        private bool isExpanded = true;
        public bool IsExpanded {
            get {
                return isExpanded;
            }
            set {
                isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
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

        #region IsFatherFinished
        private bool isFatherFinished;
        public bool IsFatherFinished {
            get {
                return isFatherFinished;
            }
            set {
                isFatherFinished = value;
                OnPropertyChanged(nameof(IsFatherFinished));
            }
        }
        #endregion

        #region Marks
        private ObservableCollection<MiniMark> marks;
        public ObservableCollection<MiniMark> Marks {
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


        public void Remove() {
            long fathId;
            if (this != null) {
                fathId = FatherID;
                Methods.idToMission[fathId].Children.Remove(this);
                TreeViewModels.Root.ChildrenView?.Refresh();
            }
        }

        public void RemoveMark(MiniMark item) {
            if (item != null) {
                Marks.Remove(item);
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
