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
            MissionsList.DataContext = TreeViewModels.Root.ChildrenView;
            WorksWithFlyouts.CloseNewTaskFlyout += WorksWithFlyouts_CloseNewTaskFlyout;
            NeedToNotifySelectedItem.Instance.OpenNewTaskFlyout += Instance_OpenNewTaskFlyout;
            WorkWithTabControl.ChangeTabItemEvent += WorkWithTabControl_ChangeTabItemEvent;
            WorksWithFlyouts.CloseAllTaskFlyouts += WorksWithFlyouts_CloseAllTaskFlyouts;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            string _searchText = ((TextBox)sender).Text;
            ApplyFilterText(_searchText);
        }

        private void ApplyFilterText(string filterText) {
            Predicate<Mission> Filter = item => item.Name.Contains(filterText);
            ApplyFilterText(TreeViewModels.Root, Filter);
        }

        private static void ApplyFilterText(Mission item, Predicate<Mission> filter) {
            if (item == null)
                return;

            foreach (Mission child in item.Children)
                ApplyFilterText(child, filter);

            item.Filter = filter;

            item.ChildrenView.Refresh();
        }

        private void WorksWithFlyouts_CloseAllTaskFlyouts() {
            AddNewMission.IsOpen = false;
            EditingSelectedMission.IsOpen = false;
        }

        private void WorkWithTabControl_ChangeTabItemEvent() {
            /*int numb = WorkWithTabControl.SelectedTabItem;
            WorksWithFlyouts.CloseAllFlyouts();
            WorksWithFlyouts.ClearBlackoutsDate();
            if (numb == 0) {
                MissionsList.DataContext = TreeViewModels.AllMissionCollection;
            } else if (numb == 1) {
                MissionsList.DataContext = TreeViewModels.TodayMissionCollection;
            }*/
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
            EditingSelectedMission.IsOpen = true;
            NeedToNotifySelectedItem.Instance.UpdateBlackoutsDate();

        }
    }
}
