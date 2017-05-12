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
    public partial class ListBoxForMarksStyle {
        public ListBoxForMarksStyle() {
            InitializeComponent();
        }

        private void DelMarkButton_Click(object sender, RoutedEventArgs e) {
            Mission selectedTreeViewItem = NeedToNotifySelectedItem.Instance.NeedToNotify;

            MiniMark selectedMiniMark = NeedToNotifySelectedItem.Instance.SelectedMark;

            if (selectedMiniMark == null)
                return;
            
            if (NeedToNotifySelectedItem.Instance.NewTaskFlyoutIsOpen) {
                NeedToNotifySelectedItem.Instance.NewTaskMarks.Remove(selectedMiniMark);
            }

            if (selectedTreeViewItem == null)
                return;
            selectedTreeViewItem.RemoveMark(selectedMiniMark);
        }
    }
}
