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
    /// Логика взаимодействия для EditingTaskFlyoutContent.xaml
    /// </summary>
    public partial class EditingTaskFlyoutContent : Page {
        public EditingTaskFlyoutContent() {
            InitializeComponent();
            StartDateTimePicker.DisplayDate = DateTime.Today;
            FinishDateTimePicker.DisplayDate = DateTime.Today;
            NeedToNotifySelectedItem.Instance.UpdateDatePicker += Instance_UpdateDatePicker;
            WorksWithFlyouts.ClearBlackouts += WorksWithFlyouts_ClearBlackouts;
        }

        private void WorksWithFlyouts_ClearBlackouts() {
            StartDateTimePicker.BlackoutDates.Clear();
            FinishDateTimePicker.BlackoutDates.Clear();
        }

        //Для каждой задачи выставляем ограницения на установку дат начала и конца в зависимости от детей и отцов задачи
        private void Instance_UpdateDatePicker() {
            StartDateTimePicker.DisplayDate = DateTime.Today;
            FinishDateTimePicker.DisplayDate = DateTime.Today;
            Mission selectMission = NeedToNotifySelectedItem.Instance.NeedToNotify;
            if (selectMission == null) {
                WorksWithFlyouts.CloseAllFlyouts();
                return;
            }
            #region Ограничение задачи относительно подзадач
            DateTime minChildDate = DateTime.MaxValue.Date, maxChildDate = DateTime.MinValue.Date;
            DateTime minMiddleDate = DateTime.MaxValue.Date, maxMiddleDate = DateTime.MinValue.Date;
            for (int i = 0; i < selectMission.Children.Count; i++) {

                if (selectMission.Children[i].StartDate != DateTime.MinValue.Date &&
                    selectMission.Children[i].FinishDate == DateTime.MaxValue.Date) {

                    if (DateTime.Compare(selectMission.Children[i].StartDate, minMiddleDate) < 0) {
                        minMiddleDate = selectMission.Children[i].StartDate;
                    }
                    if (DateTime.Compare(maxMiddleDate, selectMission.Children[i].StartDate) < 0) {
                        maxMiddleDate = selectMission.Children[i].StartDate;
                    }

                }
                else if (selectMission.Children[i].StartDate == DateTime.MinValue.Date &&
                  selectMission.Children[i].FinishDate != DateTime.MaxValue.Date) {

                    if (DateTime.Compare(selectMission.Children[i].FinishDate, minMiddleDate) < 0) {
                        minMiddleDate = selectMission.Children[i].FinishDate;
                    }
                    if (DateTime.Compare(maxMiddleDate, selectMission.Children[i].FinishDate) < 0) {
                        maxMiddleDate = selectMission.Children[i].FinishDate;
                    }

                }
                    if (selectMission.Children[i].StartDate != DateTime.MinValue.Date &&
                    DateTime.Compare(selectMission.Children[i].StartDate, minChildDate) < 0) {
                        minChildDate = selectMission.Children[i].StartDate;
                    }
                    if (selectMission.Children[i].FinishDate != DateTime.MaxValue.Date &&
                       DateTime.Compare(maxChildDate, selectMission.Children[i].FinishDate) < 0) {
                        maxChildDate = selectMission.Children[i].FinishDate;
                    }
            }
            StartDateTimePicker.BlackoutDates.Clear();
            FinishDateTimePicker.BlackoutDates.Clear();
            if (minChildDate != DateTime.MaxValue.Date 
                && maxChildDate != DateTime.MinValue.Date) {
                CalendarDateRange childDateForStart = new CalendarDateRange(minChildDate.AddDays(1), DateTime.MaxValue.Date);
                CalendarDateRange childDateForFinish = new CalendarDateRange(DateTime.MinValue.Date, maxChildDate.AddDays(-1));
                StartDateTimePicker.BlackoutDates.Add(childDateForStart);
                FinishDateTimePicker.BlackoutDates.Add(childDateForFinish);
            } else if (minMiddleDate != DateTime.MaxValue.Date && maxMiddleDate != DateTime.MinValue.Date) {
                CalendarDateRange childDateForStart = new CalendarDateRange(minMiddleDate.AddDays(1), DateTime.MaxValue.Date);
                CalendarDateRange childDateForFinish = new CalendarDateRange(DateTime.MinValue.Date, maxMiddleDate.AddDays(-1));
                StartDateTimePicker.BlackoutDates.Add(childDateForStart);
                FinishDateTimePicker.BlackoutDates.Add(childDateForFinish);
            }
            #endregion

            #region Ограничение задачи относительно задачи-отца
            if (NeedToNotifySelectedItem.Instance.NeedToNotify.FatherID != -1 && Methods.idToMission[NeedToNotifySelectedItem.Instance.NeedToNotify.FatherID].StartDate != DateTime.MinValue.Date) {
                DateTime minDate = Methods.idToMission[NeedToNotifySelectedItem.Instance.NeedToNotify.FatherID].StartDate.AddDays(-1);
                CalendarDateRange rangeMin = new CalendarDateRange(DateTime.MinValue.Date.AddDays(1), minDate);
                StartDateTimePicker.BlackoutDates.Add(rangeMin);
                FinishDateTimePicker.BlackoutDates.Add(rangeMin);
            }
            if (NeedToNotifySelectedItem.Instance.NeedToNotify.FatherID != -1 && Methods.idToMission[NeedToNotifySelectedItem.Instance.NeedToNotify.FatherID].FinishDate != DateTime.MaxValue.Date) {
                DateTime maxDate = Methods.idToMission[NeedToNotifySelectedItem.Instance.NeedToNotify.FatherID].FinishDate.AddDays(1);
                CalendarDateRange rangeMax = new CalendarDateRange(maxDate, DateTime.MaxValue.Date.AddDays(-1));
                StartDateTimePicker.BlackoutDates.Add(rangeMax);
                FinishDateTimePicker.BlackoutDates.Add(rangeMax);
            }
            #endregion
        }

        private void EditMissionButton_Click(object sender, RoutedEventArgs e) {
            string name = (MissionNameTextBox.Text == "") ? "Без названия" : MissionNameTextBox.Text;
            bool isImportant = (bool)ToggleSwitchIsImportant.IsChecked;
            DateTime start, finish;

            if (StartDateTimePicker.SelectedDate == null || StartDateTimePicker.SelectedDate == DateTime.MinValue.Date) {
                start = DateTime.MinValue.Date;
            }
            else {
                start = (DateTime)StartDateTimePicker.SelectedDate;
            }

            if (FinishDateTimePicker.SelectedDate == null || FinishDateTimePicker.SelectedDate == DateTime.MaxValue.Date) {
                finish = DateTime.MaxValue.Date;
            }
            else {
                finish = (DateTime)FinishDateTimePicker.SelectedDate;
            }
            NeedToNotifySelectedItem.Instance.NeedToNotify.Name = name;
            NeedToNotifySelectedItem.Instance.NeedToNotify.IsImportant = isImportant;
            NeedToNotifySelectedItem.Instance.NeedToNotify.StartDate = start;
            NeedToNotifySelectedItem.Instance.NeedToNotify.FinishDate = finish;
        }
    }
}
