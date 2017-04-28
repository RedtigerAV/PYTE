﻿using System;
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

        public static ObservableCollection<Mission> AllMissionCollection = new ObservableCollection<Mission> {
            new Mission("Сходить в магазин"), new Mission("Прибраться в комнате")
        };

        public static ObservableCollection<Mission> TodayMissionCollection = new ObservableCollection<Mission>();

        public static ObservableCollection<Mission> TomorrowMissionCollection = new ObservableCollection<Mission>();

        public static ObservableCollection<Mission> WeekCollection = new ObservableCollection<Mission>();

        public static ObservableCollection<Mission> ImportantCollection = new ObservableCollection<Mission>();


        public static ObservableCollection<Mission> dis = new ObservableCollection<Mission> {
                new Mission("Купить кефир"), new Mission("Купить молоко"), new Mission("Купить творог"),
        };

        static TreeViewModels() {
            HelpSelectedItem = new NeedToNotifySelectedItem();
            AllMissionCollection[0].Children = dis;
            ObservableCollection<Mission> kis = new ObservableCollection<Mission> {
                new Mission("Молоко топленое"), new Mission("Молоко обезжиренное"),
            };
            AllMissionCollection[0].Children[1].Children = kis;
            AllMissionCollection[0].IsSelected = true;
            Methods.MakeConnectWithDict(AllMissionCollection);
        }
    }
}
