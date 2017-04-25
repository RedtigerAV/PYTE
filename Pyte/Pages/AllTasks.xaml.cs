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
            MissionsList.DataContext = TreeViewModels.mis;
        }

        private void Add_Mission_Click(object sender, RoutedEventArgs e) {
            if (EditingSelectedMission.IsOpen) {
                EditingSelectedMission.IsOpen = false;
            }
            AddNewMission.IsOpen = true;
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            if (AddNewMission.IsOpen) {
                AddNewMission.IsOpen = false;
            }

            TreeView tr = (TreeView)sender;
            Mission SelectedMission = (Mission)tr.SelectedItem;
            NeedToNotifySelectedItem.Instance.NeedToNotify = SelectedMission;
            EditingSelectedMission.IsOpen = true;

        }
    }
}
