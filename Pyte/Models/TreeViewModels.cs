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

        public static Mission SelectedItemFromTreeView { get; set; }

        public static long ParentID { get; set; }

        public static bool OpenFlyout { get; set; } = false;
        #endregion

        public static Mission Root = new Mission("Root", -1);

        static TreeViewModels() {
            Root.Add(new Mission("Сходить в магазин", -1));
            Root.Add(new Mission("Прибраться в комнате", -1));
            Root.Children[0].Add(new Mission("Lolo", Root.Children[0].ID));
            Methods.MakeConnectWithDict(Root.Children);
        }
    }
}
