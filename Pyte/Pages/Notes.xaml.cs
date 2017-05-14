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
using Microsoft.Win32;
using System.IO;
using System.Windows.Documents.Serialization;
using System.Diagnostics;
using GemBox.Document;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace Pyte.Pages {
    /// <summary>
    /// Логика взаимодействия для Notes.xaml
    /// </summary>
    public partial class Notes : Page, INotifyPropertyChanged {
        public static bool isNewNote = false;
        public event PropertyChangedEventHandler PropertyChanged;

        public Notes() {
            InitializeComponent();
            NotesListBox.ItemsSource = NotesList.InstanceNoteList.AllNotes;
            AddNewNote.DataContext = this;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public void OnPropertyChange(string name) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private string flyoutHeader;
        public string FlyoutHeader {
            get { return flyoutHeader; }
            set {
                flyoutHeader = value;
                OnPropertyChange(nameof(FlyoutHeader));
            }
        }

        private void Add_Note_Click(object sender, RoutedEventArgs e) {
            AddNewNote.IsOpen = false;
            FlyoutHeader = "Новая заметка";
            NoteTitleTextBox.Text = "";
            RichTexBox_NewNote.Document = new FlowDocument();
            NotesListBox.SelectedItem = null;
            AddNewNote.IsOpen = true;
            isNewNote = true;
        }

        private void NotesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ListBox nt = (ListBox)sender;
            if (nt.SelectedItem == null) {
                AddNewNote.IsOpen = false;
                return;
            }
            Models.Note note = (Models.Note)nt.SelectedItem;
            FlyoutHeader = "Редактирование";
            NoteTitleTextBox.Text = note.Title;
            RichTexBox_NewNote.Document = note.NoteContent;
            isNewNote = false;
            AddNewNote.IsOpen = true;
        }


        private void SaveInPyte_Click(object sender, RoutedEventArgs e) {
            if (isNewNote) {
                Models.Note note = new Models.Note(Methods.TextIsEmpty(NoteTitleTextBox.Text) ? 
                    "Без названия" : NoteTitleTextBox.Text, RichTexBox_NewNote.Document);
                NotesList.InstanceNoteList.AllNotes.Add(note);
                AddNewNote.IsOpen = false;
                NoteTitleTextBox.Text = "";
                RichTexBox_NewNote.Document = new FlowDocument();
            } else {
                if (NotesListBox.SelectedItem == null)
                    return;
                Models.Note note = (Models.Note)NotesListBox.SelectedItem;
                note.Title = Methods.TextIsEmpty(NoteTitleTextBox.Text) ?
                    "Без названия" : NoteTitleTextBox.Text;
                note.NoteContent = RichTexBox_NewNote.Document;
            }
        }

        #region WorkWithDocument
        private void Open(object sender, ExecutedRoutedEventArgs e) {
            var dialog = new OpenFileDialog() {
                AddExtension = true,
                Filter =
                    "All Documents (*.docx;*.docm;*.doc;*.dotx;*.dotm;*.dot;*.htm;*.html;*.rtf;*.txt)|*.docx;*.docm;*.dotx;*.dotm;*.doc;*.dot;*.htm;*.html;*.rtf;*.txt|" +
                    "Word Documents (*.docx)|*.docx|" +
                    "Word Macro-Enabled Documents (*.docm)|*.docm|" +
                    "Word 97-2003 Documents (*.doc)|*.doc|" +
                    "Word Templates (*.dotx)|*.dotx|" +
                    "Word Macro-Enabled Templates (*.dotm)|*.dotm|" +
                    "Word 97-2003 Templates (*.dot)|*.dot|" +
                    "Web Pages (*.htm;*.html)|*.htm;*.html|" +
                    "Rich Text Format (*.rtf)|*.rtf|" +
                    "Text Files (*.txt)|*.txt"
            };

            if (dialog.ShowDialog() == true)
                using (var stream = new MemoryStream()) {
                    DocumentModel.Load(dialog.FileName).Save(stream, SaveOptions.RtfDefault);

                    stream.Position = 0;

                    var textRange = new TextRange(this.RichTexBox_NewNote.Document.ContentStart, this.RichTexBox_NewNote.Document.ContentEnd);
                    textRange.Load(stream, DataFormats.Rtf);
                }
        }

        private void Save(object sender, ExecutedRoutedEventArgs e) {
            var dialog = new SaveFileDialog() {
                AddExtension = true,
                Filter =
                    "Word Document (*.docx)|*.docx|" +
                    "Word Macro-Enabled Document (*.docm)|*.docm|" +
                    "Word Template (*.dotx)|*.dotx|" +
                    "Word Macro-Enabled Template (*.dotm)|*.dotm|" +
                    "PDF (*.pdf)|*.pdf|" +
                    "XPS Document (*.xps)|*.xps|" +
                    "Web Page (*.htm;*.html)|*.htm;*.html|" +
                    "Single File Web Page (*.mht;*.mhtml)|*.mht;*.mhtml|" +
                    "Rich Text Format (*.rtf)|*.rtf|" +
                    "Plain Text (*.txt)|*.txt|" +
                    "Image (*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.tif;*.tiff;*.wdp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.tif;*.tiff;*.wdp"
            };

            if (dialog.ShowDialog() == true)
                using (var stream = new MemoryStream()) {
                    var textRange = new TextRange(this.RichTexBox_NewNote.Document.ContentStart, this.RichTexBox_NewNote.Document.ContentEnd);
                    textRange.Save(stream, DataFormats.Rtf);
                    stream.Position = 0;
                    DocumentModel.Load(stream, LoadOptions.RtfDefault).Save(dialog.FileName);
                    Process.Start(dialog.FileName);
                }
        }

        private void CanSave(object sender, CanExecuteRoutedEventArgs e) {
            if (this.RichTexBox_NewNote != null) {
                var document = this.RichTexBox_NewNote.Document;
                var startPosition = document.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward);
                var endPosition = document.ContentEnd.GetNextInsertionPosition(LogicalDirection.Backward);
                e.CanExecute = startPosition != null && endPosition != null && startPosition.CompareTo(endPosition) < 0;
            }
            else
                e.CanExecute = false;
        }
        #endregion
    }
}
