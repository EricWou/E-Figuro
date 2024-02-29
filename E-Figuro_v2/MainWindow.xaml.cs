using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace E_Figuro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static SqlConnection con;
        static SqlCommand cmd;

        public MainWindow()
        {
            InitializeComponent();

            string connectionString = "Data Source=EricWlaptop;Initial Catalog=master;Integrated Security=True";
            con = new SqlConnection(connectionString);
            con.Open();
            con.Close();
        }

        private void login_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();

                string query = "";
                if (employee_radio.IsChecked == true) 
                {
                    query = "select * from Employee where emp_email=@email";
                }
                else if (admin_radio.IsChecked == true)
                {
                    query = "select * from Administrator where admin_email=@email";
                }

                cmd = new SqlCommand(query, con);                       //takes value from user input and places
                cmd.Parameters.AddWithValue("@email", email_box.Text);  //into the query to be sent to SQL

                SqlDataAdapter da = new SqlDataAdapter(cmd);    //retrieves information from database using cmd
                DataTable dt = new DataTable();                 //creates an object dt to hold the information
                da.Fill(dt);                                    //places info from database into object dt

                try
                {
                    String userID = "";
                    String comparisonPassword = "";
                    if (employee_radio.IsChecked == true) //need to create employee PageFeatures
                    {
                        userID = dt.Rows[0]["emp_id"].ToString();
                        comparisonPassword = (string)dt.Rows[0]["emp_password"];    //retrieves password from the row
                    }                                                               //where the email matches
                    else if (admin_radio.IsChecked == true)
                    {
                        userID = dt.Rows[0]["admin_id"].ToString();
                        comparisonPassword = (string)dt.Rows[0]["admin_password"];
                    }
                                              
                    if (comparisonPassword.Equals(password_box.Password))
                    {
                        FeaturesPage window1 = new FeaturesPage(userID);  //go to employee PageFeatures
                        window1.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid password or email");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid email, please recheck your input");
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


        /*
        private void login_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();

                if (employee_radio.IsChecked == true) //need to create employee PageFeatures
                {
                    string query = "select emp_password from Employee where emp_email=@email";

                    cmd = new SqlCommand(query, con);                       //takes value from user input and places
                    cmd.Parameters.AddWithValue("@email", email_box.Text);  //into the query to be sent to SQL

                    SqlDataAdapter da = new SqlDataAdapter(cmd);    //retrieves information from database using cmd
                    DataTable dt = new DataTable();                 //creates an object dt to hold the information
                    da.Fill(dt);                                    //places info from database into object dt

                    try
                    {
                        string comparisonPassword = (string)dt.Rows[0]["emp_password"]; //retrieves password from the row
                                                                                        //where the email matches
                        if (comparisonPassword.Equals(password_box.Password))
                        {
                            FeaturesPage window1 = new FeaturesPage();  //go to employee PageFeatures
                            window1.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid password or email");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid email, please recheck your input");
                    }
                }
                if (admin_radio.IsChecked == true)
                {
                    string query = "select * from Administrator where admin_email=@email";

                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@email", email_box.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    try
                    {
                        String userID = dt.Rows[0]["admin_id"].ToString();
                        string comparisonPassword = (string)dt.Rows[0]["admin_password"];

                        if (comparisonPassword.Equals(password_box.Password))
                        {
                            FeaturesPage window1 = new FeaturesPage(userID);  //go to admin PageFeatures
                            window1.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid password or email");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid email, please recheck your input");
                    }
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
        */




    }
}
