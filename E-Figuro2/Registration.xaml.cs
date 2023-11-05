using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        static SqlConnection con;
        static SqlCommand cmd;

        private void connect_button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=EricWlaptop;Initial Catalog=master;Integrated Security=True";
            con = new SqlConnection(connectionString);
            con.Open();
            MessageBox.Show("Connection to database successfully established");
            con.Close();
        }

        private void create_button_Click(object sender, RoutedEventArgs e)
        {
            if (con != null)
            {
                try
                {
                    con.Open();
                    
                    if (selection_box.SelectedValue.Equals("Employee")) // FIX
                    {
                        MessageBox.Show("Inside if statement for employee");
                        string query = "insert into Employee values(@empID, @empName, @empEmail, @empPos, @empDep, @empHire, @empSalary, @empBirth, @empAddress)";
                        cmd = new SqlCommand(query, con);

                        try
                        {
                            cmd.Parameters.AddWithValue("empID", int.Parse(id_box.Text));
                            cmd.Parameters.AddWithValue("empName", name_box.Text);
                            cmd.Parameters.AddWithValue("empEmail", email_box.Text);
                            cmd.Parameters.AddWithValue("empPos", position_box.Text);
                            cmd.Parameters.AddWithValue("empDep", department_box.Text);
                            cmd.Parameters.AddWithValue("empHire", DateTime.Parse(hire_date_box.Text)); //FIX
                            cmd.Parameters.AddWithValue("empSalary", Math.Round(float.Parse(salary_box.Text)));
                            cmd.Parameters.AddWithValue("empBirth", DateTime.Parse(birth_date_box.Text));
                            cmd.Parameters.AddWithValue("empAddress", address_box.Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Please ensure all boxes are filled correctly");
                        }

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Employee profile created successfully");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Please connect to database first");
            }
        }
    }
 
}
