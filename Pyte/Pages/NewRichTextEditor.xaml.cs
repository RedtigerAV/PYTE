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
    /// Логика взаимодействия для NewRichTextEditor.xaml
    /// </summary>
    public partial class NewRichTextEditor : Page {
        public NewRichTextEditor() {
            InitializeComponent();
        }

        private void BoldTextButton_Click(object sender, RoutedEventArgs e) {
            /*var boldSelection = new TextRange(RichTexBox_NewNote.Selection.Start, RichTexBox_NewNote.Selection.End);

            if ((bool)BoldTextButton.IsChecked)
                boldSelection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            else
                boldSelection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            RichTexBox_NewNote.Focus();
            */
            //EditingCommands.ToggleBold.Execute(null, null);

        }

        private void RichTexBox_NewNote_TextChanged(object sender, TextChangedEventArgs e) {
            /* var tr = new TextRange(RichTexBox_NewNote.Selection.Start, RichTexBox_NewNote.Selection.End);
            var oFont = (FontWeight)tr.GetPropertyValue(TextElement.FontWeightProperty);

            BoldTextButton.IsChecked = oFont == FontWeights.Bold; 

            var previousLetterPointer = RichTexBox_NewNote.CaretPosition.GetNextContextPosition(LogicalDirection.Backward);
            var tr = new TextRange(previousLetterPointer, RichTexBox_NewNote.CaretPosition);
            if ((bool)BoldTextButton.IsChecked)
                tr.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            else
                tr.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal); */
        }

        private void SaveTextButton_Click(object sender, RoutedEventArgs e) {
            Note newNote = new Note();

            newNote.Title = "Без названия";

            newNote.NoteContent = RichTexBox_NewNote.Document;

            NotesList.InstanceNoteList.AllNotes.Add(newNote);
        }
    }
}
