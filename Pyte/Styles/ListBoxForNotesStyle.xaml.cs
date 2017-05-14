using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Pyte.Models;
using Pyte.Pages;
using System.Collections.ObjectModel;

namespace Pyte.Styles {
    public partial class ListBoxForNotesStyle {
        public ListBoxForNotesStyle() {
            InitializeComponent();
        }

        private void MakeNoteImportant_Click(object sender, RoutedEventArgs e) {
            Note note = (Note)((Button)e.OriginalSource).Tag;
            note.IsImportant = !note.IsImportant;
        }

        private void DelNoteButton_Click(object sender, RoutedEventArgs e) {
            Note note = (Note)((Button)e.OriginalSource).Tag;
            if (note == null)
                return;
            NotesList.InstanceNoteList.AllNotes.Remove(note);
        }
    }
}
