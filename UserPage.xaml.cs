using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;
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
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Window
    {
        User user;
        public UserPage(User _user)
        {
            user = _user;
            InitializeComponent();
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            if (search_Box.Text == "") { search_Box.Focus(); return; }
            else
            {
                string command = "select from Users where name = '" + search_Box.Text + "' or Writer = '" + search_Box.Text + "'";
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\DataSql\data.mdf;Integrated Security=True;Connect Timeout=30");
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable data = new DataTable();
                adapter.Fill(data);
            }
        }

        private void Books_Butt_Click(object sender, RoutedEventArgs e)
        {
            List<Book> books = user.Books;
        }

        private void Change_info_Click(object sender, RoutedEventArgs e)
        {
            //if (Tabs.Visibility == Visibility.Visible)
            //    Tabs.Visibility = Visibility.Collapsed;
             
        }

        private void Change_info_Butt_Click(object sender, RoutedEventArgs e)
        {
            Name_error.Visibility = Visibility.Collapsed;
            Empty_error.Visibility = Visibility.Collapsed;
            Email_error.Visibility = Visibility.Collapsed;
            Phone_error.Visibility = Visibility.Collapsed;
            Same_error.Visibility = Visibility.Collapsed;
            if (fname.Text == "" && lname.Text == "" && pnum.Text == "" && email.Text == "" && pass.Text == "")
            {
                Empty_error.Visibility = Visibility.Visible;
                return;
            }
            if (Regex.IsMatch(fname.Text, @"^[a-zA-Z]{3,32}$"))
            {
                if (fname.Text == user.First_Name)
                {
                    Same_error.Visibility = Visibility.Visible; return;
                }
                user.First_Name = fname.Text;
            }
            else { if (fname.Text == "") ; else { Name_error.Visibility = Visibility.Visible; return; } }
            if (Regex.IsMatch(lname.Text, @"^[a-zA-Z]{3,32}$"))
            {
                if (lname.Text == user.Last_Name)
                {
                    Same_error.Visibility = Visibility.Visible; return;
                }
                user.Last_Name = lname.Text;
            }
            else { if (lname.Text == "") ; else { Name_error.Visibility = Visibility.Visible; return; } }
            if (Regex.IsMatch(pnum.Text, @"^09[0-9]{9}$"))
            {
                if (pnum.Text == user.Phone)
                {
                    Same_error.Visibility = Visibility.Visible; return;
                }
                user.Phone = pnum.Text;
            }
            else { if (pnum.Text == "") ; else{ Phone_error.Visibility = Visibility.Visible; return; } }
            if (Regex.IsMatch(email.Text, @"^.{1,32}@.{1,32}\..{1,32}$"))
            {
                if (email.Text == user.Email)
                {
                    Same_error.Visibility = Visibility.Visible; return;
                }
                user.Email = email.Text;
            }
            else { if (email.Text == "") ; else { Email_error.Visibility = Visibility.Visible; return; } }
            if (Regex.IsMatch(pass.Text, @"^(?=.*[a-z])(?=.*[A-Z]).{8,40}$") || pass.Text == "")
            {
                if (pass.Text == user.Password)
                {
                    Same_error.Visibility = Visibility.Visible; return;
                }
                user.Password = pass.Text;
            }
            else { if (pass.Text == "") ; else { Pass_error.Visibility = Visibility.Visible; return; } }

        }

        private void Profile_Butt_Click(object sender, RoutedEventArgs e)
        {
            if (Tabs.Visibility == Visibility.Visible)
                Tabs.Visibility = Visibility.Collapsed;
            else
            {
                Tabs.Visibility = Visibility.Visible; Tabs.TabIndex = 0;
            }
        }
    }
}
