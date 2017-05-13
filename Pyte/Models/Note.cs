using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;

namespace Pyte.Models {
    [Serializable]
    public class Note : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChange(string name) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string title;
        public string Title {
            get { return title; }
            set {
                title = value;
                OnPropertyChange(nameof(Title));
            }
        }

        private DateTime noteDateTimeCreate;
        public DateTime NoteDateTimeCreate {
            get { return noteDateTimeCreate; }
            set {
                noteDateTimeCreate = value;
                OnPropertyChange(nameof(NoteDateTimeCreate));
            }
        }

        private FlowDocument noteContent;
        public FlowDocument NoteContent {
            get { return noteContent; }
            set {
                noteContent = value;
                OnPropertyChange(nameof(NoteContent));
            }
        }
    }
}
