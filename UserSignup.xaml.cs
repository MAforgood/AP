using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace project
{
    /// <summary>
    /// Interaction logic for UserSignup.xaml
    /// </summary>
    public partial class UserSignup : Window
    {
        public UserSignup()
        {
            InitializeComponent();
        }

        private void Sign_in_Click(object sender, RoutedEventArgs e)
        {
            Email_Error.Visibility = Visibility.Collapsed;
            Wrong_Email.Visibility = Visibility.Collapsed;
            Wrong_Password.Visibility = Visibility.Collapsed;
            if (Email_Box.Text == "" || Password_Box.Text == "") { Email_Box.Focus(); return; }
            if (Regex.IsMatch(Email_Box.Text, @"^.{1,32}@.{1,32}\..{1,32}$"))
            {
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                connection.Open();
                string Command = "select * from Users where Email = '"+Email_Box.Text.Trim()+"' ";
                SqlDataAdapter adapter = new SqlDataAdapter(Command,connection);
                DataTable data = new DataTable();
                adapter.Fill(data);
                if (data.Rows.Count == 1)
                {
                    if (data.Rows[0][4].ToString() == Password_Box.Text)
                    {
                        User user = new User(data.Rows[0][1].ToString(), data.Rows[0][2].ToString(), data.Rows[0][3].ToString(), data.Rows[0][0].ToString(), data.Rows[0][4].ToString());
                        UserPage userPage = new UserPage(user);
                        this.Close();
                        userPage.Show();
                    }
                    else { Wrong_Password.Visibility = Visibility.Visible; return; }
                }
                else { Wrong_Email.Visibility = Visibility.Visible; return; }

            }
            else { Email_Error.Visibility = Visibility.Visible; return; }
        }

        private void Back_Butt_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
    }
}
