using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Pyte.Models {

    public delegate void CloseNewTaskFlyoutEventHadler();

    public static partial class WorksWithFlyouts {
        public static event CloseNewTaskFlyoutEventHadler CloseNewTaskFlyout;

        public static void CloseFlyout() {
            CloseNewTaskFlyout();
        }

    }
}
