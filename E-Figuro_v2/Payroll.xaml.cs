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
    /// Interaction logic for Payroll.xaml
    /// </summary>

    public partial class Payroll : Window
    {
        private String userID;
        const double taxes = 0.25;

        static SqlConnection con;
        static SqlCommand cmd;

        public Payroll()
        {
            InitializeComponent();
        }

        public Payroll(string userID)
        {
            InitializeComponent();

            this.userID = userID;

            string connectionString = "Data Source=EricWlaptop;Initial Catalog=master;Integrated Security=True";
            con = new SqlConnection(connectionString);
            con.Open();

            salary_label.Content = getSalary().ToString();
            hours_label.Content = getTotalHours().ToString();
            earnings_label.Content = calculateEarnings().ToString();
            taxes_label.Content = calculateTaxes().ToString();
            netPay_label.Content = calculateNetPay().ToString();

            if (userID.Substring(0, 1) == "A")
            {
                string query = "select admin_name from Administrator where admin_id=@ID";

                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", userID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows != null)
                {
                    name_label.Content = dt.Rows[0]["admin_name"].ToString();
                }
            }
            if (userID.Substring(0, 1) == "E")
            {
                string query = "select admin_name from Employee where emp_id=@ID";

                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", userID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows != null)
                {
                    name_label.Content = dt.Rows[0]["emp_name"].ToString();
                }
            }

            con.Close();
        }

        private double getSalary()
        {
            double salary = 0;

            if (userID.Substring(0, 1) == "A")
            {
                string query = "select * from Administrator where admin_id=@ID";
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ID", userID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                try
                {
                    if (dt.Rows != null)
                    {
                        salary = double.Parse(dt.Rows[0]["admin_salary"].ToString());
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Administrator with the current ID does not exist");
                }
            }
            else if (userID.Substring(0, 1) == "E")
            {
                string query = "select * from Employee where emp_id=@ID";
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ID", userID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                try
                {
                    if (dt.Rows != null)
                    {
                        salary = double.Parse(dt.Rows[0]["emp_salary"].ToString());
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Employee with the current ID does not exist");
                }
            }
            return salary;
        }

        private int getTotalHours()
        {
            int totalHours = 0;

            if (userID.Substring(0, 1) == "A")
            {
                string query = "select * from TimeSheet where admin_id=@ID";
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ID", userID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        totalHours += int.Parse(dt.Rows[i]["totalHours"].ToString());
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Administrator with the current ID does not exist");
                }
            }
            else if (userID.Substring(0, 1) == "E")
            {
                string query = "select * from TimeSheet where emp_id=@ID";
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ID", userID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        totalHours += int.Parse(dt.Rows[i]["totalHours"].ToString());
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Employee with the current ID does not exist");
                }
            }
            return totalHours;
        }

        private double calculateEarnings()
        {
            double salary = getSalary();
            int totalHours = getTotalHours();

            double earnings = salary * totalHours;

            return earnings;
        }

        private double calculateTaxes()
        {
            double salary = getSalary() * getTotalHours();

            double taxedSalary = salary * taxes;

            return taxedSalary;
        }

        private double calculateNetPay()
        {
            double netPay = calculateEarnings() - calculateTaxes();

            return netPay;
        }

        private void print_button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                dialog.PrintVisual(this, "My Canvas");
            }
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            FeaturesPage window1 = new FeaturesPage(userID);
            window1.Show();
            this.Close();
        }

    }
}
