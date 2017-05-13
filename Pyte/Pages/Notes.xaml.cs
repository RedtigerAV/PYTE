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

namespace Pyte.Pages {
    /// <summary>
    /// Логика взаимодействия для Notes.xaml
    /// </summary>
    public partial class Notes : Page {
        public Notes() {
            InitializeComponent();
            NotesListBox.ItemsSource = NotesList.InstanceNoteList.AllNotes;
        }

        private void Add_Note_Click(object sender, RoutedEventArgs e) {
            AddNewNote.IsOpen = true;
        }
    }
}
