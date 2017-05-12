using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;


namespace Pyte.Models {
    public class MiniMark : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public MiniMark(string text) {
            MarkText = text;
            MarkDate = DateTime.Today.Date.ToString("m");
        }

        #region Members

        private string markText;
        public string MarkText {
            get { return markText; }
            set {
                markText = value;
                OnPropertyChanged(nameof(MarkText));
            }
        }

        private string markDate;
        public string MarkDate {
            get { return markDate; }
            set {
                markDate = value;
                OnPropertyChanged(nameof(MarkDate));
            }
        }

        #endregion
    }
}
