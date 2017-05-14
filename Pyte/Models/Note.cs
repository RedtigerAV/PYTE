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

        public Note(string title, FlowDocument content) {
            Title = title;
            NoteDateTimeCreate = DateTime.Today.ToString("m");
            NoteContent = content;
            IsImportant = false;
        }

        private bool isImportant;
        public bool IsImportant {
            get { return isImportant; }
            set {
                isImportant = value;
                OnPropertyChange(nameof(IsImportant));
            }
        }

        private string title;
        public string Title {
            get { return title; }
            set {
                title = value;
                OnPropertyChange(nameof(Title));
            }
        }

        private string noteDateTimeCreate;
        public string NoteDateTimeCreate {
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
