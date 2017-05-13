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
using MahApps.Metro.Controls;
using Pyte.Models;

namespace Pyte {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {
        public MainWindow() {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("ru-RU");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            InitializeComponent();
        }

        private void Open_Calendar(object sender, RoutedEventArgs e) {
            CalendarFlyouts.IsOpen = true;
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var fatherOfEvent = e.OriginalSource as TabControl;
            if (fatherOfEvent != null) {
                int numb = MainTabControl.SelectedIndex;
                string name = (string)((TabItem)MainTabControl.SelectedItem).Header;
                WorkWithTabControl.InstanceTabControl.SelectedTabItem = numb;
                WorkWithTabControl.InstanceTabControl.SelectedTabName = name;

                if (numb == 5) {
                    if (WorkWithCalendar.CalendatInstance.calendarWay == 1) {
                        WorkWithTabControl.InstanceTabControl.SelectedTabName = ((DateTime)MainCalendar.SelectedDate).ToString("m");
                    }
                    else {
                        string selectedTabName = "";
                        if (WorkWithCalendar.CalendatInstance.SecondWayStartDay != DateTime.MinValue.Date)
                            selectedTabName += "От: " + WorkWithCalendar.CalendatInstance.SecondWayStartDay.ToString("m") + "   ";
                        if (WorkWithCalendar.CalendatInstance.SecondWayFinishDay != DateTime.MaxValue.Date)
                            selectedTabName += "До: " + WorkWithCalendar.CalendatInstance.SecondWayFinishDay.ToString("m");
                        WorkWithTabControl.InstanceTabControl.SelectedTabName = selectedTabName;
                    }
                }

                if (numb >= 0 && numb <= 4 || numb == 5) {
                    WorkWithTabControl.InstanceTabControl.ChangeTabItemMethod();
                }
                WorkWithTabControl.InstanceTabControl.OnTasksEmpty();
            }
        }

        private void CalendarShowButton_Click(object sender, RoutedEventArgs e) {
            CalendarFlyouts.IsOpen = false;
            if ((DateTime)MainCalendar.SelectedDate != null) {
                WorkWithCalendar.CalendatInstance.calendarWay = 1;
                WorkWithCalendar.CalendatInstance.FirstWayDay = (DateTime)MainCalendar.SelectedDate;
                if (MainTabControl.SelectedIndex == 5) {
                    Dispatcher.BeginInvoke((Action)(() => MainTabControl.SelectedIndex = 0));
                }
                Dispatcher.BeginInvoke((Action)(() => MainTabControl.SelectedIndex = 5));
            }
        }

        private void PeriodCalendarShowButton_Click(object sender, RoutedEventArgs e) {
            CalendarFlyouts.IsOpen = false;
            if (StartPeriodDatePicker.SelectedDate != null || FinishPeriodDatePicker.SelectedDate != null) {
                WorkWithCalendar.CalendatInstance.calendarWay = 2;
                WorkWithCalendar.CalendatInstance.SecondWayStartDay = (StartPeriodDatePicker.SelectedDate == null)? DateTime.MinValue.Date : (DateTime)StartPeriodDatePicker.SelectedDate;
                WorkWithCalendar.CalendatInstance.SecondWayFinishDay = (FinishPeriodDatePicker.SelectedDate == null)? DateTime.MaxValue.Date : (DateTime)FinishPeriodDatePicker.SelectedDate;
                if (MainTabControl.SelectedIndex == 5) {
                    Dispatcher.BeginInvoke((Action)(() => MainTabControl.SelectedIndex = 0));
                }
                Dispatcher.BeginInvoke((Action)(() => MainTabControl.SelectedIndex = 5));
            }
            StartPeriodDatePicker.Text = "";
            FinishPeriodDatePicker.Text = "";
        }
    }
}
