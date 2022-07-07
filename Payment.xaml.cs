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
    /// Interaction logic for Payment.xaml
    /// </summary>
    public partial class Payment : Window
    {
        public bool Boolean { get; set; }
        public float price { get; set; }
        public Payment(float _price)
        {
            price = _price;
            InitializeComponent();
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Pay_Butt_Click(object sender, RoutedEventArgs e)
        {
            Expire_error.Visibility = Visibility.Collapsed;
            Empty_error.Visibility = Visibility.Collapsed;
            Cvv_error.Visibility = Visibility.Collapsed;
            No_error.Visibility = Visibility.Collapsed;
            Date_error.Visibility = Visibility.Collapsed;
            if (Card_No.Text == "" || CVV.Text == "" || Year.Text == "" || Month.Text == "")
            {
                Empty_error.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                string Card_no = Card_No.Text;
                int Cvv = 0;
                try
                {
                    Cvv = int.Parse(CVV.Text);
                }
                catch { Cvv_error.Visibility = Visibility.Visible; return; }
                string pass = password.Text;
                if (Card.checkid(Card_no))
                {
                    if (CVV.Text.Length < 5 && CVV.Text.Length > 2)
                    {
                        int year = 0;
                        int month = 0;
                        try
                        {
                            year = int.Parse(Year.Text);
                        }
                        catch { Date_error.Visibility = Visibility.Visible; }
                        try
                        {
                            month = int.Parse(Month.Text);
                        }
                        catch { Date_error.Visibility = Visibility.Visible; }
                        if (year < DateTime.Now.Year)
                        {
                            Expire_error.Visibility = Visibility.Visible;
                            return;
                        }
                        else
                        {
                            if (year == DateTime.Now.Year && month <= DateTime.Now.Month)
                            {
                                Expire_error.Visibility = Visibility.Visible;
                                return;
                            }
                            else
                            {
                                DateOnly date = new DateOnly(year, month, 1);
                                Card card = new Card(Card_no, Cvv, pass, date);
                                Boolean = true;
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        Cvv_error.Visibility = Visibility.Visible; return;
                    }
                }
                else
                {
                    No_error.Visibility = Visibility.Visible; return;
                }

            }

        }
    }
}
