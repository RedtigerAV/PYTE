﻿#pragma checksum "..\..\..\Pages\AllTasks.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F5A5FFCEFF8BA36819C0B6A532AF2C48"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.Controls;
using Pyte.Models;
using Pyte.Pages;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Pyte.Pages {
    
    
    /// <summary>
    /// AllTasks
    /// </summary>
    public partial class AllTasks : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\Pages\AllTasks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BorderFirst;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Pages\AllTasks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MissionsList;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Pages\AllTasks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Add_Mission;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Pages\AllTasks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.Flyout AddNewMission;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Pages\AllTasks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.Flyout EditingSelectedMission;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Pyte;component/pages/alltasks.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\AllTasks.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.BorderFirst = ((System.Windows.Controls.Border)(target));
            return;
            case 2:
            this.MissionsList = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            
            #line 20 "..\..\..\Pages\AllTasks.xaml"
            ((System.Windows.Controls.TreeView)(target)).SelectedItemChanged += new System.Windows.RoutedPropertyChangedEventHandler<object>(this.TreeView_SelectedItemChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Add_Mission = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\Pages\AllTasks.xaml"
            this.Add_Mission.Click += new System.Windows.RoutedEventHandler(this.Add_Mission_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.AddNewMission = ((MahApps.Metro.Controls.Flyout)(target));
            return;
            case 6:
            this.EditingSelectedMission = ((MahApps.Metro.Controls.Flyout)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

