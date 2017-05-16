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
    public class NotesList {

        public NotesList() { }

        public static NotesList InstanceNoteList = new NotesList();

        public ObservableCollection<Note> AllNotes;

        private static ObservableCollection<FlowDocument> flowNotes;

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
            }
            catch { MessageBox.Show("Уупс, возможно произошла ошибка с записками. Обратитесь к разработчику."); }
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
