using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Pyte.Models {
    public static class Methods {
        public static Dictionary<long, Mission> idToMission = new Dictionary<long, Mission>();

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
}
