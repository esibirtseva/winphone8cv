﻿#pragma checksum "C:\code\winphone8cv\wp8videocapture\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "472EE83CA716A17D567DA403DA84264F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace wp8videocapture {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Canvas LayoutRoot;
        
        internal System.Windows.Shapes.Rectangle viewfinderRectangle;
        
        internal System.Windows.Controls.MediaElement VideoPlayer;
        
        internal System.Windows.Controls.TextBlock txtDebug;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/wp8videocapture;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Canvas)(this.FindName("LayoutRoot")));
            this.viewfinderRectangle = ((System.Windows.Shapes.Rectangle)(this.FindName("viewfinderRectangle")));
            this.VideoPlayer = ((System.Windows.Controls.MediaElement)(this.FindName("VideoPlayer")));
            this.txtDebug = ((System.Windows.Controls.TextBlock)(this.FindName("txtDebug")));
        }
    }
}

