using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace project
{
    /// <summary>
    /// Interaction logic for UserLogin.xaml
    /// </summary>
    public partial class UserLogin : Window
    {
        public UserLogin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Name_error.Visibility = Visibility.Collapsed;
            Empty_error.Visibility = Visibility.Collapsed;
            Email_error.Visibility = Visibility.Collapsed;
            Phone_error.Visibility = Visibility.Collapsed;
            if (fname.Text == "" || lname.Text == "" || pnum.Text == "" || email.Text == "" || pass.Text == "")
            {
                Empty_error.Visibility = Visibility.Visible;
            }
            if (Regex.IsMatch(email.Text, @"^.{1,32}@.{1,32}\..{1,32}$"))
            {
                SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\DataSql\data.mdf;Integrated Security=True;Connect Timeout=30");
                _connection.Open();
                string command = "select * from Users where email ='"+email.Text.Trim()+"' ";
                SqlDataAdapter adapter = new SqlDataAdapter(command,_connection);
                DataTable table = new DataTable(); 
                adapter.Fill(table);
                SqlCommand cmd = new SqlCommand(command, _connection);
                cmd.BeginExecuteNonQuery();
                if (table.Rows.Count > 0)
                {
                    Repeated_error.Visibility = Visibility.Visible; return;
                }

                if (Regex.IsMatch(fname.Text, @"^[a-zA-Z]{3,32}$"))
                {
                    if (Regex.IsMatch(lname.Text, @"^[a-zA-Z]{3,32}$"))
                    {
                        if (Regex.IsMatch(pnum.Text, @"^09[0-9]{9}$"))
                        {
                            if (Regex.IsMatch(pass.Text, @"^(?=.*[a-z])(?=.*[A-Z]).{8,40}$"))
                            {
                                User user = new User(fname.Text, lname.Text, pnum.Text, email.Text, pass.Text);
                                user.AddTotable();
                                UserPage userPage = new UserPage(user);
                                this.Close();
                                userPage.Show();
                            }
                            else { Pass_error.Visibility = Visibility.Visible; }
                        }
                        else { Phone_error.Visibility = Visibility.Visible; }
                    }
                    else { Name_error.Visibility = Visibility.Visible; }
                }
                else { Name_error.Visibility = Visibility.Visible; }
            }
            else { Email_error.Visibility = Visibility.Visible; }
        }
    }
}
