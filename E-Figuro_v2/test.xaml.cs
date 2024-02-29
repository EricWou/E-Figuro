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

namespace E_Figuro
{
    /// <summary>
    /// Interaction logic for test.xaml
    /// </summary>
    public partial class test : Window
    {
        public test()
        {
            InitializeComponent();
        }

        private void calculateDays_button_Click(object sender, RoutedEventArgs e)
        {
            DateTime start = start_date.SelectedDate.Value.Date;
            DateTime end = end_date.SelectedDate.Value.Date;

            TimeSpan difference = end - start;
            int sumHours = (int)difference.TotalHours;
            NoDays_Label.Content = difference.TotalDays.ToString();
            
        }
    }
}
