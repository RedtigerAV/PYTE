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
        }

        public void ClearFlyout() {
            MissionNameTextBox.Text = "";
            ToggleSwitchIsImportant.IsChecked = false;
            StartDateTimePicker.SelectedDate = DateTime.MinValue;
            FinishDateTimePicker.SelectedDate = DateTime.MinValue;
        }

        private void SaveNewMissionButton_Click(object sender, RoutedEventArgs e) {
            Mission newMission;

            newMission = new Mission(MissionNameTextBox.Text);
            newMission.IsImportant = (bool)ToggleSwitchIsImportant.IsChecked;

            DateTime start, finish;

            if (StartDateTimePicker.SelectedDate == null) {
                start = DateTime.MinValue;
            } else {
                start = (DateTime)StartDateTimePicker.SelectedDate;
            }

            if (FinishDateTimePicker.SelectedDate == null) {
                finish = DateTime.MinValue;
            } else {
                finish = (DateTime)FinishDateTimePicker.SelectedDate;
            }

            newMission.StartDate = start;
            newMission.FinishDate = finish;

            if (NeedToNotifySelectedItem.Instance.NotifyOpenFlyout) {
                NeedToNotifySelectedItem.Instance.NotifyOpenFlyout = false;
                long id = (long)((Button)e.OriginalSource).Tag;

                Methods.idToMission[id].Children.Add(newMission);
            }
            else {
                TreeViewModels.AllMissionCollection.Add(newMission);
            }
            Methods.idToMission[newMission.ID] = newMission;
            ClearFlyout();
            WorksWithFlyouts.CloseFlyout();
        }
    }
}
