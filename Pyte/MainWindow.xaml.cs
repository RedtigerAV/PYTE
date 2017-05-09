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
            int numb = MainTabControl.SelectedIndex;
            WorkWithTabControl.SelectedTabItem = numb;
            if (numb == 0 || numb == 1) {
                WorkWithTabControl.ChangeTabItemMethod();
            }
        }
    }
}
