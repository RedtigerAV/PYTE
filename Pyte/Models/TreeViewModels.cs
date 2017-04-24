using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Pyte.Models {
    public static class Methods {
        public static Dictionary<long, Mission> idToMission= new Dictionary<long, Mission>();
        public static void MakeConnectWithDict(ObservableCollection<Mission> parametr) {
            if (parametr.Count == 0) {
                return;
            }

            for (int i = 0; i < parametr.Count; i++) {
                idToMission[parametr[i].ID] = parametr[i];
                MakeConnectWithDict(parametr[i].Children);
            }

        }
    }

    public static class TreeViewModels {
        public static ObservableCollection<Mission> mis = new ObservableCollection<Mission> {
            new Mission("1"), new Mission("2")
        };
        public static ObservableCollection<Mission> dis = new ObservableCollection<Mission> {
                new Mission("1.1"), new Mission("1.2"), new Mission("1.3"),
        };
        static TreeViewModels() {
            mis[0].Children = dis;
            ObservableCollection<Mission> kis = new ObservableCollection<Mission> {
                new Mission("1.1.1"), new Mission("1.1.2"),
            };
            mis[0].Children[1].Children = kis;
            Methods.MakeConnectWithDict(mis);
        }
    }
}
