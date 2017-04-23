using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Pyte.Models {
    public class Mission {

        #region Constructors
        public Mission (string name) {
            Name = name;
            IsChecked = false;
            IsImportant = false;

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
            }
        }
        #endregion

        #region FinishDate
        private DateTime finishDate;
        private DateTime FinishDate {
            get {
                return finishDate;
            }
            set {
                finishDate = value;
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

    }
}
