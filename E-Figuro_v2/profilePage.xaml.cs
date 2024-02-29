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
using System.Windows.Shapes;

namespace E_Figuro
{
    /// <summary>
    /// Interaction logic for profilePage.xaml
    /// </summary>
    public partial class profilePage : Window
    {

        private String userID;

        static SqlConnection con;
        static SqlCommand cmd;
        public profilePage()
        {
            InitializeComponent();
        }

        public profilePage(String userID)
        {
            InitializeComponent();
            this.userID = userID;

            string connectionString = "Data Source=EricWlaptop;Initial Catalog=master;Integrated Security=True";
            con = new SqlConnection(connectionString);
            con.Open();

            if (userID.Substring(0, 1) == "A")
            {
                string query = "select * from Administrator where admin_id=@ID";
                cmd = new SqlCommand(query, con);

                try
                {
                    cmd.Parameters.AddWithValue("@ID", userID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    try
                    {
                        if (dt.Rows != null)
                        {
                            DateTime hireDate_convert = (DateTime)dt.Rows[0]["admin_hire_date"];      //to pull DateTime from
                            DateTime birthDate_convert = (DateTime)dt.Rows[0]["admin_birth_date"];    //the database

                            id_box.Text = dt.Rows[0]["admin_id"].ToString();
                            name_box.Text = dt.Rows[0]["admin_name"].ToString();
                            email_box.Text = dt.Rows[0]["admin_email"].ToString();
                            position_box.Text = dt.Rows[0]["admin_position"].ToString();
                            department_box.Text = dt.Rows[0]["admin_department"].ToString();
                            hire_date_box.Text = hireDate_convert.ToShortDateString();              //to remove the time portion
                            birth_date_box.Text = birthDate_convert.ToShortDateString();
                            address_box.Text = dt.Rows[0]["admin_address"].ToString();
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Administrator with the current ID does not exist");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Please input the proper format for the ID");
                }
            }

            if (userID.Substring(0, 1) == "E")
            {
                string query = "select * from Employee where emp_id=@ID";
                cmd = new SqlCommand(query, con);

                try
                {
                    cmd.Parameters.AddWithValue("@ID", userID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    try
                    {
                        if (dt.Rows != null)
                        {
                            DateTime hireDate_convert = (DateTime)dt.Rows[0]["emp_hire_date"];      //to pull DateTime from
                            DateTime birthDate_convert = (DateTime)dt.Rows[0]["emp_birth_date"];    //the database

                            id_box.Text = dt.Rows[0]["emp_id"].ToString();
                            name_box.Text = dt.Rows[0]["emp_name"].ToString();
                            email_box.Text = dt.Rows[0]["emp_email"].ToString();
                            position_box.Text = dt.Rows[0]["emp_position"].ToString();
                            department_box.Text = dt.Rows[0]["emp_department"].ToString();
                            hire_date_box.Text = hireDate_convert.ToShortDateString();              //to remove the time portion
                            birth_date_box.Text = birthDate_convert.ToShortDateString();
                            address_box.Text = dt.Rows[0]["emp_address"].ToString();
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Employee with the current ID does not exist");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Please input the proper format for the ID");
                }
            }
            con.Close();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            FeaturesPage window1 = new FeaturesPage(userID);
            window1.Show();
            this.Close();
        }
    }
}
