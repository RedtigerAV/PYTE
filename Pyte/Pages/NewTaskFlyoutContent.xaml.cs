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

        private void SaveNewMissionButton_Click(object sender, RoutedEventArgs e) {
            Mission newMission;
            if (NeedToNotifySelectedItem.Instance.NotifyOpenFlyout) {
                NeedToNotifySelectedItem.Instance.NotifyOpenFlyout = false;
                long id = (long)((Button)e.OriginalSource).Tag;
                newMission = new Mission(MissionNameTextBox.Text);
                newMission.IsImportant = (bool)ToggleSwitchIsImportant.IsChecked;
                Methods.idToMission[newMission.ID] = newMission;
                Methods.idToMission[id].Children.Add(newMission);
                WorksWithFlyouts.CloseFlyout();
                return;
            }
            newMission = new Mission(MissionNameTextBox.Text);
            newMission.IsImportant = (bool)ToggleSwitchIsImportant.IsChecked;
            TreeViewModels.mis.Add(newMission);
            Methods.idToMission[newMission.ID] = newMission;
            WorksWithFlyouts.CloseFlyout();
        }
    }
}
