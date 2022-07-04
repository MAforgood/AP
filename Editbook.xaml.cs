using System;
using System.Collections.Generic;
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
    /// Interaction logic for Editbook.xaml
    /// </summary>
    public partial class Editbook : Window
    {
        
        public Editbook()
        {
            //this.book = book;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(neweditname.Text, @"^[a-zA-Z]{3,32}$"))
            {
                if (Regex.IsMatch(neweditauthor.Text, @"^[a-zA-Z]{3,32}$"))
                {
                    if (int.Parse(newedityear.Text) > 0 && int.Parse(newedityear.Text) < 2022)
                    {
                        if (int.Parse(neweditprice.Text) > 0 && int.Parse(neweditprice.Text) < 10000000)
                        {
                            if (neweditsummary.Text.Length > 0 && neweditsummary.Text.Length < 300)
                            {
                                SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\DataSql\data.mdf;Integrated Security=True;Connect Timeout=30");
                                _connection.Open();
                                string command2 = "Update Books SET Name='" + neweditname + "',Author='" + neweditauthor + "',Publishe Year='" + newedityear + "',Price='" + neweditprice + "',Summary='" + neweditsummary + "',  ";
                                SqlCommand cmd2 = new SqlCommand(command2, _connection);
                                try
                                {
                                    cmd2.ExecuteNonQuery();
                                    MessageBox.Show("Edited Successfully");
                                }
                                catch (SqlException ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Wrong Format!", "Summary Format!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Wrong Format!", "Price Format!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Wrong Format!", "Publishe Year Format!");
                    }
                }
                else
                {
                    MessageBox.Show("Wrong Format!", "Author Format!");
                }
            }
            else
            {
                MessageBox.Show("Wrong Format!", "Name Format!");
            }
        }
    }
}
            /*
                                if (neweditname.Text == book.Name)
                {
                    MessageBox.Show("It Is Reppetetive!", "Reppetetive Name!");
                }
                else
                {
                    book.Name = neweditname.Text;
                }
            }
            else
            {
                MessageBox.Show("Wrong Format!", "Nmae Format!");
            }

            if (Regex.IsMatch(neweditauthor.Text, @"^[a-zA-Z]{3,32}$"))
            {
                if (neweditauthor.Text == book.Author)
                {
                    MessageBox.Show("It Is Reppetetive!", "Reppetetive Author!");
                }
                book.Author = neweditauthor.Text;
            }
            else
            {
                MessageBox.Show("Wrong Format!", "Author Format!");
            }

            if (int.Parse(newedityear.Text)>0&&int.Parse(newedityear.Text)<2022)
            {
                if (int.Parse(newedityear.Text) == book.PublishedYear)
                {
                    MessageBox.Show("It Is Reppetetive!", "Reppetetive Year!");
                }
                book.PublishedYear = int.Parse(newedityear.Text);
            }
            else
            {
                MessageBox.Show("Wrong Format!", "Yaer Format!");
            }

            if (int.Parse(neweditprice.Text) > 0 && int.Parse(neweditprice.Text) <10000000)
            {
                if (int.Parse(neweditprice.Text) == book.Price)
                {
                    MessageBox.Show("It Is Reppetetive!", "Reppetetive Price!");
                }
                book.Price = int.Parse(neweditprice.Text);
            }
            else
            {
                MessageBox.Show("Wrong Format!", "Price Format!");
            }
            if (neweditsummary.Text.Length>0&& neweditsummary.Text.Length<300)
            {
                if (neweditsummary.Text == book.Summary)
                {
                    MessageBox.Show("It Is Reppetetive!", "Reppetetive Summary!");
                }
                book.Summary = neweditsummary.Text;
            }
            else
            {
                MessageBox.Show("Wrong Format!", "Summary Format!");
            }
        }

       */

