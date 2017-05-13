using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Pyte.Models {
    public class NotesList {
        public static NotesList InstanceNoteList = new NotesList();

        public ObservableCollection<Note> AllNotes = new ObservableCollection<Note>();
    }
}
