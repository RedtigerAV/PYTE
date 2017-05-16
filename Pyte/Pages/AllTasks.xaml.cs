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
using System.ComponentModel;

namespace Pyte.Pages {
    /// <summary>
    /// Логика взаимодействия для AllTasks.xaml
    /// </summary>
    public partial class AllTasks : Page {

        #region INotifyProperty
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion


        public AllTasks() {
            InitializeComponent();
            ChartCondition.ItemsSource = WorkWithChart.AllConditions;
            NeedToNotifySelectedItem.Instance.NewTaskFlyoutIsOpen = false;
            MissionsList.DataContext = TreeViewModels.Root.ChildrenView;
            WorksWithFlyouts.CloseNewTaskFlyout += WorksWithFlyouts_CloseNewTaskFlyout;
            NeedToNotifySelectedItem.Instance.OpenNewTaskFlyout += Instance_OpenNewTaskFlyout;
            WorkWithTabControl.InstanceTabControl.ChangeTabItemEvent += WorkWithTabControl_ChangeTabItemEvent;
            WorksWithFlyouts.CloseAllTaskFlyouts += WorksWithFlyouts_CloseAllTaskFlyouts;
            //WorkWithTabControl.InstanceTabControl.TasksEmptyEvent += InstanceTabControl_TasksEmptyEvent;
           
        }

        private void InstanceTabControl_TasksEmptyEvent() {
            if (TreeViewModels.Root.ChildrenView != null && TreeViewModels.Root.ChildrenView.IsEmpty)
                TasksEmptyTextBlock.Visibility = Visibility.Visible;
            else
                TasksEmptyTextBlock.Visibility = Visibility.Collapsed;
        }

        private void WorksWithFlyouts_CloseAllTaskFlyouts() {
            AddNewMission.IsOpen = false;
            EditingSelectedMission.IsOpen = false;
        }

        private void WorkWithTabControl_ChangeTabItemEvent() {

            WorksWithFlyouts.CloseAllFlyouts();
            WorksWithFlyouts.ClearBlackoutsDate();

            WorkWithFilters.Filters.OnOtherFilters();
        }

        private void WorksWithFlyouts_CloseNewTaskFlyout() {
            AddNewMission.IsOpen = false;
        }

        private void Instance_OpenNewTaskFlyout() {
            NeedToNotifySelectedItem.Instance.NewTaskMarks.Clear();
            AddNewMission.IsOpen = true;
            EditingSelectedMission.IsOpen = false;
        }

        private void Add_Mission_Click(object sender, RoutedEventArgs e) {
            if (EditingSelectedMission.IsOpen) {
                EditingSelectedMission.IsOpen = false;
            }
            WorksWithFlyouts.ClearBlackoutsDate();
            NeedToNotifySelectedItem.Instance.NewTaskMarks.Clear();
            AddNewMission.IsOpen = true;

        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            WorksWithFlyouts.ClearBlackoutsDate();

            if (AddNewMission.IsOpen) {
                AddNewMission.IsOpen = false;
            }

            TreeView tr = (TreeView)sender;
            if (tr.SelectedItem == null)
                return;
            Mission SelectedMission = (Mission)tr.SelectedItem;
            //MessageBox.Show($"SelectedMission: {SelectedMission.Name} and {SelectedMission.ID}\n StartDate: {SelectedMission.StartDate.ToString()}\n FinishDate: {SelectedMission.FinishDate.ToString()}\n FatherId: {SelectedMission.FatherID}");
            if (SelectedMission.IsFinished)
                return;
            NeedToNotifySelectedItem.Instance.NeedToNotify = SelectedMission;
            EditingSelectedMission.IsOpen = true;
            NeedToNotifySelectedItem.Instance.UpdateBlackoutsDate();

        }
    }
}
