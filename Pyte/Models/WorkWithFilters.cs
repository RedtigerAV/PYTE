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
using Pyte.Pages;
using System.ComponentModel;

namespace Pyte.Models {

    public class WorkWithFilters : INotifyPropertyChanged {

        public static WorkWithFilters Filters = new WorkWithFilters();

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string filterText;
        public string FilterText {
            get {
                return filterText;
            }
            set {
                filterText = value;
                ApplyFilterText(value);
                OnPropertyChanged(nameof(FilterText));
            }
        }

        #region FilteringText

        private void ApplyFilterText(string filterText) {
            Predicate<Mission> Filter = item => item.Name.ToUpper().Contains(filterText.ToUpper());
            ApplyFilterText(TreeViewModels.Root, Filter);
        }

        private static void ApplyFilterText(Mission item, Predicate<Mission> filter) {
            if (item == null)
                return;

            foreach (Mission child in item.Children)
                ApplyFilterText(child, filter);

            item.Filter = filter;

            item.IsAccepted = true;

            item.ChildrenView.Refresh();
        }

        #endregion

        #region OtherFiltering

        public void OnOtherFilters() {
            Predicate<Mission> Filter;
            int numb = WorkWithTabControl.SelectedTabItem;
            if (numb == 0)
                Filter = null;
            else if (numb == 1) {
                Filter = (item) => {
                    return (DateTime.Compare(item.StartDate, DateTime.Today) <= 0 &&
                    DateTime.Compare(DateTime.Today, item.FinishDate) <= 0);
                };
            }
            else if (numb == 2) {
                Filter = (item) => {
                    return (DateTime.Compare(item.StartDate, DateTime.Today.AddDays(1)) <= 0 &&
                    DateTime.Compare(DateTime.Today.AddDays(1), item.FinishDate) <= 0);
                };
            }

            //week filter

            else {
                Filter = item => item.IsImportant;
            }

            ApplyOtherFilters(TreeViewModels.Root, Filter);
        }

        private void ApplyOtherFilters(Mission item, Predicate<Mission> filter) {
            if (item == null) {
                return;
            }

            item.TabControlFilter = filter;

            foreach (Mission child in item.Children) {
                ApplyOtherFilters(child, filter);
            }

            item.ChildrenView.Refresh();
        }

        #endregion

    }
}
