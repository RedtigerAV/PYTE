using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows;

namespace Pyte.Models {
    [Serializable]
    public class NotesList: INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChange(string name) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public static NotesList InstanceNoteList = new NotesList();

        public NotesList() { }

        public ObservableCollection<Note> AllNotes;

        private static ObservableCollection<FlowDocument> flowNotes;

        private CollectionViewSource notesCollection;

        static NotesList() {
            try {
                XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<Note>));
                using (FileStream fs = new FileStream("Notes.xml", FileMode.OpenOrCreate)) {
                    InstanceNoteList.AllNotes = (ObservableCollection<Note>)formatter.Deserialize(fs);
                }
                flowNotes = LoadNotes("NotesContent.txt");
                for (int i = 0; i < InstanceNoteList.AllNotes.Count; i++) {
                    InstanceNoteList.AllNotes[i].NoteContent = flowNotes[i];
                }
                InstanceNoteList.notesCollection = new CollectionViewSource();
                InstanceNoteList.notesCollection.Source = InstanceNoteList.AllNotes;
                InstanceNoteList.notesCollection.Filter += NotesCollection_Filter;
            }
            catch {  }
        }


        public ICollectionView AllNotesView {
            get { return InstanceNoteList.notesCollection.View; }
        }


        private string filterNoteText;
        public string FilterNoteText {
            get { return filterNoteText; }
            set {
                filterNoteText = value;
                AllNotesView?.Refresh();
                OnPropertyChange(nameof(FilterNoteText));
            }
        }

        private static void NotesCollection_Filter(object sender, FilterEventArgs e) {
            if (string.IsNullOrEmpty(InstanceNoteList.FilterNoteText)) {
                e.Accepted = true;
                return;
            }

            Note note = e.Item as Note;
            if (note == null) {
                e.Accepted = false;
                return;
            }
            if (note.Title.ToUpper().Contains(InstanceNoteList.FilterNoteText.ToUpper())) {
                e.Accepted = true;
            }
            else {
                e.Accepted = false;
            }
        }

        private static ObservableCollection<FlowDocument> LoadNotes(string fileName) {
            var result = new ObservableCollection<FlowDocument>();

            var list = File.ReadAllText(fileName, Encoding.UTF8)
                .Split(new[] { "<!---!---!>" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(it => it.Trim())
                .Where(it => !string.IsNullOrEmpty(it))
                .Select(it => (FlowDocument)XamlReader.Parse(it))
                .ToList();

            foreach (var item in list)
                result.Add(item);

            return result;
        }

    }
}
