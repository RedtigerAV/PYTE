using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using System.Windows;

namespace Pyte.Models {

    
    public static class TreeViewModels {

        #region GetSelectedItem
        public static NeedToNotifySelectedItem HelpSelectedItem;

        public static Mission SelectedItemFromTreeView { get; set; }

        public static long ParentID { get; set; }

        public static bool OpenFlyout { get; set; } = false;
        #endregion

        public static Mission RootFirst, Root;

        static TreeViewModels() {
            try {
                XmlSerializer formatter = new XmlSerializer(typeof(Mission));
                using (FileStream fs = new FileStream("Root.xml", FileMode.OpenOrCreate)) {
                    RootFirst = (Mission)formatter.Deserialize(fs);
                }

                long max_id = SetMaxId(RootFirst);

                Mission.id_counter = max_id + 1;

                Root = new Mission(RootFirst);

                Methods.MakeConnectWithDict(Root);
            } catch {  }
        }

        public static bool IsRootChildEmpty {
            get {
                return Root.ChildrenView.IsEmpty;
            }
        }

        private static long SetMaxId(Mission item) {
            long id_max = -5;
            for (int i = 0; i < item.Children.Count; i++) {
                id_max = Math.Max(id_max, SetMaxId(item.Children[i]));
            }
            return Math.Max(id_max, item.ID);
        }
    }
}
