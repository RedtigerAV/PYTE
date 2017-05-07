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
using System.Collections.ObjectModel;
using Pyte.Pages;

namespace Pyte.Pages {
    /// <summary>
    /// Логика взаимодействия для AllTasks.xaml
    /// </summary>
    public partial class AllTasks : Page {

        public AllTasks() {
            InitializeComponent();
            MissionsList.DataContext = TreeViewModels.AllMissionCollection;
            WorksWithFlyouts.CloseNewTaskFlyout += WorksWithFlyouts_CloseNewTaskFlyout;
            NeedToNotifySelectedItem.Instance.OpenNewTaskFlyout += Instance_OpenNewTaskFlyout;
        }

        private void WorksWithFlyouts_CloseNewTaskFlyout() {
            AddNewMission.IsOpen = false;
        }

        private void Instance_OpenNewTaskFlyout() {
            AddNewMission.IsOpen = true;
            EditingSelectedMission.IsOpen = false;
        }

        private void Add_Mission_Click(object sender, RoutedEventArgs e) {
            if (EditingSelectedMission.IsOpen) {
                EditingSelectedMission.IsOpen = false;
            }
            WorksWithFlyouts.ClearBlackoutsDate();
            AddNewMission.IsOpen = true;

        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            WorksWithFlyouts.ClearBlackoutsDate();

            if (AddNewMission.IsOpen) {
                AddNewMission.IsOpen = false;
            }

            TreeView tr = (TreeView)sender;
            Mission SelectedMission = (Mission)tr.SelectedItem;
            //MessageBox.Show($"SelectedMission: {SelectedMission.Name} and {SelectedMission.ID}\n StartDate: {SelectedMission.StartDate.ToString()}\n FinishDate: {SelectedMission.FinishDate.ToString()}\n FatherId: {SelectedMission.FatherID}");

            NeedToNotifySelectedItem.Instance.NeedToNotify = SelectedMission;
            NeedToNotifySelectedItem.Instance.UpdateBlackoutsDate();
            EditingSelectedMission.IsOpen = true;

        }
    }
}
