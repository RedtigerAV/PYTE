using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Pyte.Models {
    public static class Methods {
        public static bool TextIsEmpty(string text) {
            bool flag = true;
            for (int i = 0; i < text.Length; i++) {
                if (text[i] != ' ') {
                    flag = false;
                }
            }
            return flag;
        }

        public static Dictionary<long, Mission> idToMission = new Dictionary<long, Mission>();

        public static void MakeConnectWithDict(Mission parametr) {
            idToMission[parametr.ID] = parametr;

            for (int i = 0; i < parametr.Children.Count; i++) {
                MakeConnectWithDict(parametr.Children[i]);
            }
        }

        public static void GetAllId(Mission item, ref List<long> Ids) {
            Ids.Add(item.ID);

            for (int i = 0; i < item.Children.Count; i++) {
                GetAllId(item.Children[i], ref Ids);
            }
        }

        public static void RemoveMissionFromDict(List<long> Ids) {
            for (int i = 0; i < Ids.Count; i++) {
                idToMission.Remove(Ids[i]);
            }
        }

        public static void MakeChildrenFinished(Mission item) {
            for (int i = 0; i < item.Children.Count; i++) {
                item.Children[i].IsFinished = item.IsFinished;
                item.Children[i].IsFatherFinished = item.IsFinished;
                MakeChildrenFinished(item.Children[i]);
            }
        }
    }
}
