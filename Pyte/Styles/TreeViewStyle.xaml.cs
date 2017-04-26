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
using Pyte.Pages;
using System.Collections.ObjectModel;

namespace Pyte.Styles {
    public partial class TreeViewStyle {
        public TreeViewStyle() {
            InitializeComponent();

        }

        private void MakeMissionImportant_Click(object sender, RoutedEventArgs e) {
            long id = (long)((Button)e.OriginalSource).Tag;
            Methods.idToMission[id].IsImportant = !Methods.idToMission[id].IsImportant;
        }

        private void AddSubMission_Click(object sender, RoutedEventArgs e) {
            long id = (long)((Button)e.OriginalSource).Tag;
            NeedToNotifySelectedItem.Instance.NotifyParentID = id;
            NeedToNotifySelectedItem.Instance.NotifyOpenFlyout = true;
        }
    }
}
