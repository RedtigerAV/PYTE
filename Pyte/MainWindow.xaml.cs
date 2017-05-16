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
using MahApps.Metro.Controls;
using Pyte.Models;
using System.IO;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Markup;

namespace Pyte {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {
        public MainWindow() {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("ru-RU");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            InitializeComponent();
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e) {

            try {
                XmlSerializer formatter = new XmlSerializer(typeof(Mission));

                using (FileStream fs = new FileStream("Root.xml", FileMode.Create)) {
                    formatter.Serialize(fs, TreeViewModels.Root);
                }
            }
            catch { MessageBox.Show("Ошибка в сохранении задач, попробуйте еще раз."); }

            try {
                XmlSerializer formatter2note = new XmlSerializer(typeof(ObservableCollection<Note>));

                using (FileStream fss = new FileStream("Notes.xml", FileMode.Create)) {
                    formatter2note.Serialize(fss, NotesList.InstanceNoteList.AllNotes);
                }

                ObservableCollection<FlowDocument> notes = new ObservableCollection<FlowDocument>();
                for (int i = 0; i < NotesList.InstanceNoteList.AllNotes.Count; i++) {
                    notes.Add(NotesList.InstanceNoteList.AllNotes[i].NoteContent);
                }

                SaveNotes(notes, "NotesContent.txt");
            }
            catch { MessageBox.Show("Ошибка в сохранении заметок, попробуйте еще раз."); }

            this.Close();
        }

        private static void SaveNotes(ObservableCollection<FlowDocument> notes, string fileName) {
            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8)) {
                for (int i = 0; i < notes.Count; i++) {
                    var note = notes[i];

                    var str = XamlWriter.Save(note);

                    writer.WriteLine(str);

                    if (i < notes.Count - 1)
                        writer.WriteLine("<!---!---!>");
                }
            }
        }

        private void Open_Calendar(object sender, RoutedEventArgs e) {
            CalendarFlyouts.IsOpen = true;
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var fatherOfEvent = e.OriginalSource as TabControl;
            if (fatherOfEvent != null) {
                int numb = MainTabControl.SelectedIndex;
                string name = (string)((TabItem)MainTabControl.SelectedItem).Header;
                WorkWithTabControl.InstanceTabControl.SelectedTabItem = numb;
                WorkWithTabControl.InstanceTabControl.SelectedTabName = name;

                if (numb == 5) {
                    if (WorkWithCalendar.CalendatInstance.calendarWay == 1) {
                        WorkWithTabControl.InstanceTabControl.SelectedTabName = ((DateTime)MainCalendar.SelectedDate).ToString("m");
                    }
                    else {
                        string selectedTabName = "";
                        if (WorkWithCalendar.CalendatInstance.SecondWayStartDay != DateTime.MinValue.Date)
                            selectedTabName += "От: " + WorkWithCalendar.CalendatInstance.SecondWayStartDay.ToString("m") + "   ";
                        if (WorkWithCalendar.CalendatInstance.SecondWayFinishDay != DateTime.MaxValue.Date)
                            selectedTabName += "До: " + WorkWithCalendar.CalendatInstance.SecondWayFinishDay.ToString("m");
                        WorkWithTabControl.InstanceTabControl.SelectedTabName = selectedTabName;
                    }
                }

                if (numb >= 0 && numb <= 4 || numb == 5) {
                    WorkWithTabControl.InstanceTabControl.ChangeTabItemMethod();

                    WorkWithChart.AllConditions[0].CountCondition = WorkWithChart.GetCountActive(TreeViewModels.Root) - 1;
                    WorkWithChart.AllConditions[1].CountCondition = WorkWithChart.GetCountInActive(TreeViewModels.Root);
                }
                WorkWithTabControl.InstanceTabControl.OnTasksEmpty();
            }
        }

        private void CalendarShowButton_Click(object sender, RoutedEventArgs e) {
            CalendarFlyouts.IsOpen = false;
            if ((DateTime)MainCalendar.SelectedDate != null) {
                WorkWithCalendar.CalendatInstance.calendarWay = 1;
                WorkWithCalendar.CalendatInstance.FirstWayDay = (DateTime)MainCalendar.SelectedDate;
                if (MainTabControl.SelectedIndex == 5) {
                    Dispatcher.BeginInvoke((Action)(() => MainTabControl.SelectedIndex = 0));
                }
                Dispatcher.BeginInvoke((Action)(() => MainTabControl.SelectedIndex = 5));
            }
        }

        private void PeriodCalendarShowButton_Click(object sender, RoutedEventArgs e) {
            CalendarFlyouts.IsOpen = false;
            if (StartPeriodDatePicker.SelectedDate != null || FinishPeriodDatePicker.SelectedDate != null) {
                WorkWithCalendar.CalendatInstance.calendarWay = 2;
                WorkWithCalendar.CalendatInstance.SecondWayStartDay = (StartPeriodDatePicker.SelectedDate == null)? DateTime.MinValue.Date : (DateTime)StartPeriodDatePicker.SelectedDate;
                WorkWithCalendar.CalendatInstance.SecondWayFinishDay = (FinishPeriodDatePicker.SelectedDate == null)? DateTime.MaxValue.Date : (DateTime)FinishPeriodDatePicker.SelectedDate;
                if (MainTabControl.SelectedIndex == 5) {
                    Dispatcher.BeginInvoke((Action)(() => MainTabControl.SelectedIndex = 0));
                }
                Dispatcher.BeginInvoke((Action)(() => MainTabControl.SelectedIndex = 5));
            }
            StartPeriodDatePicker.Text = "";
            FinishPeriodDatePicker.Text = "";
        }
    }
}
