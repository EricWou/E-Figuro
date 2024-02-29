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
    /// Interaction logic for TimeSheet.xaml
    /// </summary>
    public partial class TimeSheet : Window
    {
        private String userID;

        static SqlConnection con;
        static SqlCommand cmd;

        public TimeSheet()
        {
            InitializeComponent();
        }

        public TimeSheet(string userID)
        {
            InitializeComponent();

            this.userID = userID;

            string connectionString = "Data Source=EricWlaptop;Initial Catalog=master;Integrated Security=True";
            con = new SqlConnection(connectionString);
            con.Open();

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

        private void fill_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();

                DateTime startDate = start_date.SelectedDate.Value.Date;
                DateTime endDate = end_date.SelectedDate.Value.Date;

                if (userID.Substring(0,1)=="A")
                {
                    string query = "select clock_date, punchIn, mealOut, mealIn, punchOut, totalHours from TimeSheet where admin_id=@ID and clock_date>=@start and clock_date<=@end";

                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", userID);
                    cmd.Parameters.AddWithValue("@start", startDate);
                    cmd.Parameters.AddWithValue("@end", endDate);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    /* change column names, try and change clock_date to just show date
                    dt.Columns["product_name"].ColumnName = "Product Name";
                    dt.Columns["product_id"].ColumnName = "Product ID";
                    dt.Columns["amount"].ColumnName = "Amount in inventory(kg)";
                    dt.Columns["price"].ColumnName = "Price(CA$/kg)";
                    */
                    timesheet_grid.ItemsSource = dt.AsDataView();
                    DataContext = da;
                }
                if (userID.Substring(0,1)=="E")
                {
                    string query = "select * from TimeSheet where emp_id=@ID and clock_date>=@start and clock_date<=@end";

                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", userID);
                    cmd.Parameters.AddWithValue("@start", startDate);
                    cmd.Parameters.AddWithValue("@end", endDate);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    timesheet_grid.ItemsSource = dt.AsDataView();
                    DataContext = da;
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
