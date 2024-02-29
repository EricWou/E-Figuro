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
using System.Windows.Shapes;

namespace E_Figuro
{
    /// <summary>
    /// Interaction logic for PageFeatures.xaml
    /// </summary>
    public partial class FeaturesPage : Window
    {
        private String userID;
        
        public FeaturesPage()
        {
            InitializeComponent();
        }

        public FeaturesPage(String userID)
        {
            InitializeComponent();
            this.userID = userID;
        }

        private void registration_button_Click(object sender, RoutedEventArgs e)
        {
            if (userID.Substring(0, 1) == "A")
            {
                Registration window1 = new Registration(userID);
                window1.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("You do not have access to this portal as an employee");
            }
        }

        private void logout_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window2 = new MainWindow();
            window2.Show();
            this.Close();
        }

        private void profile_button_Click(object sender, RoutedEventArgs e)
        {
            profilePage window2 = new profilePage(userID);
            window2.Show();
            this.Close();
        }

        private void payroll_button_Click(object sender, RoutedEventArgs e)
        {
            Payroll window3 = new Payroll(userID);
            window3.Show();
            this.Close();
        }

        private void timesheet_button_Click(object sender, RoutedEventArgs e)
        {
            TimeSheet window4 = new TimeSheet(userID);
            window4.Show();
            this.Close();
        }
    }
}
