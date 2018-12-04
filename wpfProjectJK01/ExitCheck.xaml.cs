﻿using System;
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
using System.Windows.Shapes;

namespace wpfProjectJK01
{
    /// <summary>
    /// Interaction logic for ExitCheck.xaml
    /// </summary>
    public partial class ExitCheck : Window
    {
        public ExitCheck()
        {
            InitializeComponent();
        }

        private void Btn_Click_Save(object sender, RoutedEventArgs e)
        {
            DataStore.SaveFile();
            Application.Current.Shutdown();

        }

        private void Btn_Click_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }
    }
}
