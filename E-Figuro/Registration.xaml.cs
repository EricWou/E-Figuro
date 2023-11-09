using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
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
                    
                    if (employee_radio.IsChecked==true)
                    {
                        string query = "insert into Employee values(@ID, @name, @email, @password, @position, @dep, @hire, @salary, @birthdate, @address)";
                        cmd = new SqlCommand(query, con);

                        try
                        {
                            cmd.Parameters.AddWithValue("ID", int.Parse(id_box.Text));
                            cmd.Parameters.AddWithValue("name", name_box.Text);
                            cmd.Parameters.AddWithValue("email", email_box.Text);
                            cmd.Parameters.AddWithValue("password", password_box.Text);
                            cmd.Parameters.AddWithValue("position", position_box.Text);
                            cmd.Parameters.AddWithValue("dep", department_box.Text);
                            cmd.Parameters.AddWithValue("hire", DateTime.Parse(hire_date_box.Text));
                            cmd.Parameters.AddWithValue("salary", Math.Round(float.Parse(salary_box.Text),2));
                            cmd.Parameters.AddWithValue("birthdate", DateTime.Parse(birth_date_box.Text));
                            cmd.Parameters.AddWithValue("address", address_box.Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Please ensure all boxes are filled correctly");
                        }

                        cmd.ExecuteNonQuery();  //sends the query that is stored in cmd to SQL database
                        MessageBox.Show("Employee profile created successfully");
                    }

                    if (admin_radio.IsChecked == true)
                    {
                        string query = "insert into Administrator values(@ID, @name, @email, @password, @position, @dep, @hire, @salary, @birthdate, @address)";
                        cmd = new SqlCommand(query, con);

                        try
                        {
                            cmd.Parameters.AddWithValue("ID", int.Parse(id_box.Text));
                            cmd.Parameters.AddWithValue("name", name_box.Text);
                            cmd.Parameters.AddWithValue("email", email_box.Text);
                            cmd.Parameters.AddWithValue("password", password_box.Text);
                            cmd.Parameters.AddWithValue("position", position_box.Text);
                            cmd.Parameters.AddWithValue("dep", department_box.Text);
                            cmd.Parameters.AddWithValue("hire", DateTime.Parse(hire_date_box.Text));
                            cmd.Parameters.AddWithValue("salary", Math.Round(float.Parse(salary_box.Text),2));
                            cmd.Parameters.AddWithValue("birthdate", DateTime.Parse(birth_date_box.Text));
                            cmd.Parameters.AddWithValue("address", address_box.Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Please ensure all boxes are filled correctly");
                        }

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Administrator profile created successfully");
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



        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            EmployeeFeaturesPage window1 = new EmployeeFeaturesPage();
            window1.Show();
            this.Close();
        }

        private void select_button_Click(object sender, RoutedEventArgs e)
        {
            if(con != null)
            {
                try
                {
                    con.Open();

                    if (employee_radio.IsChecked == true)
                    {
                        string query = "select * from Employee where emp_id=@ID";
                        cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue("@ID", int.Parse(id_box.Text));

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        try
                        {
                            if (dt.Rows != null)
                            {
                                name_box.Text = dt.Rows[0]["emp_name"].ToString();
                                email_box.Text = dt.Rows[0]["emp_email"].ToString();
                                password_box.Text = dt.Rows[0]["emp_password"].ToString();
                                position_box.Text = dt.Rows[0]["emp_position"].ToString();
                                department_box.Text = dt.Rows[0]["emp_department"].ToString();
                                hire_date_box.Text = dt.Rows[0]["emp_hire_date"].ToString();
                                salary_box.Text = dt.Rows[0]["emp_salary"].ToString();
                                birth_date_box.Text = dt.Rows[0]["emp_birth_date"].ToString();
                                address_box.Text = dt.Rows[0]["emp_address"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Employee with the current ID does not exist");
                        }
                    }


                    if (admin_radio.IsChecked == true)
                    {
                        string query = "select * from Administrator where admin_id=@ID";
                        cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue("@ID", int.Parse(id_box.Text));

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        try
                        {
                            if (dt.Rows != null)
                            {
                                name_box.Text = dt.Rows[0]["admin_name"].ToString();
                                email_box.Text = dt.Rows[0]["admin_email"].ToString();
                                password_box.Text = dt.Rows[0]["admin_password"].ToString();
                                position_box.Text = dt.Rows[0]["admin_position"].ToString();
                                department_box.Text = dt.Rows[0]["admin_department"].ToString();
                                hire_date_box.Text = dt.Rows[0]["admin_hire_date"].ToString();
                                salary_box.Text = dt.Rows[0]["admin_salary"].ToString();
                                birth_date_box.Text = dt.Rows[0]["admin_birth_date"].ToString();
                                address_box.Text = dt.Rows[0]["admin_address"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Administrator with the current ID does not exist");
                        }
                    }
                }
                catch(SqlException ex)
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

        private void update_button_Click(object sender, RoutedEventArgs e)
        {
            if (con != null)
            {
                try
                {
                    con.Open();

                    if (employee_radio.IsChecked == true)
                    {
                        string query = "Update Employee set emp_name=@name, emp_email=@email, emp_password=@password, " +
                            "emp_position=@position,emp_department=@department, emp_hire_date=@hireDate, emp_salary=@salary, " +
                            "emp_birth_date=@birthDate, emp_address=@address where emp_id=@ID";

                        cmd = new SqlCommand(query, con);

                        try
                        {
                            cmd.Parameters.AddWithValue("name", name_box.Text);
                            cmd.Parameters.AddWithValue("email", email_box.Text);
                            cmd.Parameters.AddWithValue("password", password_box.Text);
                            cmd.Parameters.AddWithValue("position", position_box.Text);
                            cmd.Parameters.AddWithValue("department", department_box.Text);
                            cmd.Parameters.AddWithValue("hireDate", DateTime.Parse(hire_date_box.Text));
                            cmd.Parameters.AddWithValue("salary", Math.Round(float.Parse(salary_box.Text), 2));
                            cmd.Parameters.AddWithValue("birthDate", DateTime.Parse(birth_date_box.Text));
                            cmd.Parameters.AddWithValue("address", address_box.Text);
                            cmd.Parameters.AddWithValue("ID", int.Parse(id_box.Text));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Please make sure that all the boxes are filled properly");
                        }

                        int i = cmd.ExecuteNonQuery();
                        if (i == 1)
                        {
                            MessageBox.Show("Employee information updated");
                        }
                    }

                    if (admin_radio.IsChecked == true)
                    {
                        string query = "Update Administrator set admin_name=@name, admin_email=@email, admin_password=@password, " +
                            "admin_position=@position,admin_department=@department, admin_hire_date=@hireDate, admin_salary=@salary, " +
                            "admin_birth_date=@birthDate, admin_address=@address where admin_id=@ID";

                        cmd = new SqlCommand(query, con);

                        try
                        {
                            cmd.Parameters.AddWithValue("name", name_box.Text);
                            cmd.Parameters.AddWithValue("email", email_box.Text);
                            cmd.Parameters.AddWithValue("password", password_box.Text);
                            cmd.Parameters.AddWithValue("position", position_box.Text);
                            cmd.Parameters.AddWithValue("department", department_box.Text);
                            cmd.Parameters.AddWithValue("hireDate", DateTime.Parse(hire_date_box.Text));
                            cmd.Parameters.AddWithValue("salary", Math.Round(float.Parse(salary_box.Text), 2));
                            cmd.Parameters.AddWithValue("birthDate", DateTime.Parse(birth_date_box.Text));
                            cmd.Parameters.AddWithValue("address", address_box.Text);
                            cmd.Parameters.AddWithValue("ID", int.Parse(id_box.Text));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Please make sure that all the boxes are filled properly");
                        }

                        int i = cmd.ExecuteNonQuery();
                        if (i == 1)
                        {
                            MessageBox.Show("Administrator profile updated");
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
            else
            {
                MessageBox.Show("Please connect to database first");
            }
        }

        private void delete_button_Click(object sender, RoutedEventArgs e)
        {
            if (con != null)
            {
                try
                {
                    con.Open();

                    if (employee_radio.IsChecked == true)
                    {
                        string query = "delete from Employee where emp_id=@ID";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@ID", id_box.Text);

                        int i = cmd.ExecuteNonQuery();

                        if (i == 1)
                        {
                            MessageBox.Show("Employee profile deleted");
                        }
                        else
                        {
                            MessageBox.Show("Please check if you have entered the proper ID");
                        }
                    }

                    if (admin_radio.IsChecked == true)
                    {
                        string query = "delete from Administrator where admin_id=@ID";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@ID", id_box.Text);

                        int i = cmd.ExecuteNonQuery();

                        if (i == 1)
                        {
                            MessageBox.Show("Administrator profile deleted");
                        }
                        else
                        {
                            MessageBox.Show("Please check if you have entered the proper ID");
                        }
                    }
                }
                catch (Exception ex)
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
