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

namespace Pyte.Pages {
    /// <summary>
    /// Логика взаимодействия для AllTasks.xaml
    /// </summary>
    public partial class AllTasks : Page {
        public static ObservableCollection<Mission> mis = new ObservableCollection<Mission> {
            new Mission("1"), new Mission("2")
        };

        public AllTasks() {
            InitializeComponent();
            ObservableCollection<Mission> dis = new ObservableCollection<Mission> {
                new Mission("1.1"), new Mission("1.2"), new Mission("1.3"),
            };
            mis[0].Children = dis;
            ObservableCollection<Mission> kis = new ObservableCollection<Mission> {
                new Mission("1.1.1"), new Mission("1.1.2"),
            };
            mis[0].Children[1].Children = kis;
            MissionsList.DataContext = mis;
        }

        private void Add_Mission_Click(object sender, RoutedEventArgs e) {
            AddNewMission.Header = "Новая задача";
            AddNewMission.IsOpen = true;
        }
    }
}
