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
    public partial class EmployeeFeaturesPage : Window
    {
        public EmployeeFeaturesPage()
        {
            InitializeComponent();
        }

        private void registration_button_Click(object sender, RoutedEventArgs e)
        {
            Registration window1 = new Registration();
            window1.Show();
            this.Close();
        }

        private void logout_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window2 = new MainWindow();
            window2.Show();
            this.Close();
        }
    }
}
