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
    /// Логика взаимодействия для NewTaskFlyoutContent.xaml
    /// </summary>
    public partial class NewTaskFlyoutContent : Page {

        public NewTaskFlyoutContent() {
            InitializeComponent();
            NewMarkTextBox.DataContext = this;
            AddMarkCommand_InNewTaskFlyout = new DelegateCommand<string>((text) => {
                if (Methods.TextIsEmpty(text))
                    return;
                NeedToNotifySelectedItem.Instance.NewTaskMarks.Insert(0, new MiniMark(text));
                NewMarkTextBox.Text = "";

            });
            StartDateTimePicker.DisplayDate = DateTime.Today;
            FinishDateTimePicker.DisplayDate = DateTime.Today;
            NeedToNotifySelectedItem.Instance.OpenNewTaskFlyout += DatePickerRange_OpenNewTaskFlyout;
            WorksWithFlyouts.ClearBlackouts += WorksWithFlyouts_ClearBlackouts;
        }

        public DelegateCommand<string> AddMarkCommand_InNewTaskFlyout { get; set; }

        private void WorksWithFlyouts_ClearBlackouts() {
            StartDateTimePicker.BlackoutDates.Clear();
            FinishDateTimePicker.BlackoutDates.Clear();
        }

        //Ограничиваем подходящие даты для подзадачи относительно задачи-отца
        private void DatePickerRange_OpenNewTaskFlyout() {
            StartDateTimePicker.BlackoutDates.Clear();
            FinishDateTimePicker.BlackoutDates.Clear();
            if (Methods.idToMission[NeedToNotifySelectedItem.Instance.NotifyParentID].StartDate != DateTime.MinValue.Date) {
                DateTime minDate = Methods.idToMission[NeedToNotifySelectedItem.Instance.NotifyParentID].StartDate.AddDays(-1);
                CalendarDateRange rangeMin = new CalendarDateRange(DateTime.MinValue.Date.AddDays(1), minDate);
                StartDateTimePicker.BlackoutDates.Add(rangeMin);
                FinishDateTimePicker.BlackoutDates.Add(rangeMin);
            }
            if (Methods.idToMission[NeedToNotifySelectedItem.Instance.NotifyParentID].FinishDate != DateTime.MaxValue.Date) {
                DateTime maxDate = Methods.idToMission[NeedToNotifySelectedItem.Instance.NotifyParentID].FinishDate.AddDays(1);
                CalendarDateRange rangeMax = new CalendarDateRange(maxDate, DateTime.MaxValue.Date.AddDays(-1));
                StartDateTimePicker.BlackoutDates.Add(rangeMax);
                FinishDateTimePicker.BlackoutDates.Add(rangeMax);
            }
        }

        public void ClearFlyout() {
            MissionNameTextBox.Text = "";
            ToggleSwitchIsImportant.IsChecked = false;
            StartDateTimePicker.DisplayDate = DateTime.Today;
            StartDateTimePicker.Text = "";
            FinishDateTimePicker.Text = "";
            FinishDateTimePicker.DisplayDate = DateTime.Today;
        }

        private void SaveNewMissionButton_Click(object sender, RoutedEventArgs e) {
            Mission newMission;

            string name = (Methods.TextIsEmpty(MissionNameTextBox.Text)) ? "Без названия" : MissionNameTextBox.Text;

            newMission = new Mission(name, 0);
            newMission.IsImportant = (bool)ToggleSwitchIsImportant.IsChecked;

            DateTime start, finish;

            if (StartDateTimePicker.SelectedDate == null || StartDateTimePicker.SelectedDate == DateTime.MinValue.Date) {
                start = DateTime.MinValue.Date;
            } else {
                start = (DateTime)StartDateTimePicker.SelectedDate;
            }

            if (FinishDateTimePicker.SelectedDate == null || FinishDateTimePicker.SelectedDate == DateTime.MaxValue.Date) {
                finish = DateTime.MaxValue.Date;
            } else {
                finish = (DateTime)FinishDateTimePicker.SelectedDate;
            }

            DateTime helpDateTime;
            if (DateTime.Compare(finish, start) <= 0) {
                helpDateTime = finish;
                finish = start;
                start = helpDateTime;
            }

            newMission.StartDate = start;
            newMission.FinishDate = finish;

            for (int i = 0; i < NeedToNotifySelectedItem.Instance.NewTaskMarks.Count; i++) {
                newMission.Marks.Insert(0, new MiniMark(NeedToNotifySelectedItem.Instance.NewTaskMarks[i].MarkText));
            }

            long id = (long)((Button)e.OriginalSource).Tag;

            if (NeedToNotifySelectedItem.Instance.NotifyOpenFlyout) {
                NeedToNotifySelectedItem.Instance.NotifyOpenFlyout = false;
                newMission.FatherID = id;
            }
            else {
                newMission.FatherID = 0;
            }
            Methods.idToMission[newMission.FatherID].Add(newMission);
            Methods.idToMission[newMission.ID] = newMission;
            WorksWithFlyouts.CloseFlyout();
            ClearFlyout();
            WorkWithFilters.Filters.OnOtherFilters();
            WorkWithTabControl.InstanceTabControl.OnTasksEmpty();

        }

        private void MarksListBoxNewTask_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ListBox markList = (ListBox)(sender);
            if (markList == null)
                return;
            MiniMark selectedMark = (MiniMark)markList.SelectedItem;
            //MessageBox.Show((selectedMark == null) ? "Че за хрень2?" : selectedMark.MarkText + "2");
            NeedToNotifySelectedItem.Instance.SelectedMark = selectedMark;

        }
    }
}
