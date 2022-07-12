﻿using PFlight.viewmodel;
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

namespace PFlight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public VM CurrentVM { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            CurrentVM = new VM();
            this.DataContext = CurrentVM;

        }

        private void inList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void outList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
