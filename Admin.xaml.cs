using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string DefaultEmail = "admin@gmail.com";
            string DefaultPassword = "12345";
            if (adminemail.Text.ToString() == DefaultEmail && adminpass.Password.ToString() == DefaultPassword)
            {
               
                AdminMain main = new AdminMain();
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Input","Error");
            }
        }
    }
}
