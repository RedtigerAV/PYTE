using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Pyte.Models {

    
    public static class TreeViewModels {

        #region GetSelectedItem
        public static NeedToNotifySelectedItem HelpSelectedItem;

        public static Mission SelectedItemFromTreeView { get; set; } = new Mission("LolKekCheb");

        public static long ParentID { get; set; }

        public static bool OpenFlyout { get; set; } = false;
        #endregion

        public static ObservableCollection<Mission> mis = new ObservableCollection<Mission> {
            new Mission("LolTrol"), new Mission("2")
        };
        public static ObservableCollection<Mission> dis = new ObservableCollection<Mission> {
                new Mission("1.1"), new Mission("1.2"), new Mission("1.3"),
        };

        static TreeViewModels() {
            HelpSelectedItem = new NeedToNotifySelectedItem();
            mis[0].Children = dis;
            ObservableCollection<Mission> kis = new ObservableCollection<Mission> {
                new Mission("1.1.1"), new Mission("1.1.2"),
            };
            mis[0].Children[1].Children = kis;
            mis[0].IsSelected = true;
            Methods.MakeConnectWithDict(mis);
        }
    }
}
