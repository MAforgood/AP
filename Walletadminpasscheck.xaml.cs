using System;
using System.Collections.Generic;
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

namespace project
{
    /// <summary>
    /// Interaction logic for Walletadminpasscheck.xaml
    /// </summary>
    public partial class Walletadminpasscheck : Window
    {
        public Walletadminpasscheck()
        {
            InitializeComponent();
        }

        private void Walletadminpassbut_Click(object sender, RoutedEventArgs e)
        {
            if (walletadminpassbox.Password == "12345")
            {
                float tot = Book.books.Sum(x => x.Total_Income);
                Payment Pay = new Payment(tot);
                Pay.Show();
            }
            else
            {
                MessageBox.Show("Access Denied!","Error");
            }
        }
    }
}
