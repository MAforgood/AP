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
    /// Interaction logic for Editbook.xaml
    /// </summary>
    public partial class Editbook : Window
    {
        public Book book { get; set; }
        public Editbook(Book _book)
        {
            this.book = _book;
            //this.book = book;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (neweditname.Text.Length>0&& neweditname.Text.Length<50)
            {
                if (Regex.IsMatch(neweditauthor.Text, @"^[a-zA-Z/ ]{3,32}$"))
                {
                    if (int.Parse(newedityear.Text) > 0 && int.Parse(newedityear.Text) < 2022)
                    {
                        if (int.Parse(neweditprice.Text) > 0 && int.Parse(neweditprice.Text) < 10000000)
                        {
                            if (neweditsummary.Text.Length > 0 && neweditsummary.Text.Length < 300)
                            {
                                SqlConnection _connection3 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                                _connection3.Open();
                                string command3 = "select id from Books where Name ='" + currentnamebox.Text.Trim() + "'";
                                SqlDataAdapter adapter3 = new SqlDataAdapter(command3, _connection3);
                                DataTable table3 = new DataTable();
                                adapter3.Fill(table3);
                                int hlp = int.Parse(table3.Rows[0][0].ToString());

                                SqlConnection _connection2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                                string command2 = "DELETE FROM Books WHERE Name ='" + currentnamebox.Text.Trim() + "'";
                                _connection2.Open();
                                SqlCommand cmd2 = new SqlCommand(command2, _connection2);

                                try
                                {
                                    cmd2.ExecuteNonQuery();
                                    _connection2.Close();
                                   
                                }
                                catch (SqlException ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                string Ratings = "";
                                for (int i = 0; i < book.Ratings.Count; i++)
                                {
                                    Ratings += book.Ratings[i]+",";
                                }
                                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30"); connection.Open();
                                string Command3 = "insert into [Books] values('" + hlp + "','" + neweditname.Text.Trim() + "','" + neweditauthor.Text.Trim() + "','" + int.Parse(newedityear.Text) + "','" + float.Parse(neweditprice.Text) + " ','"+neweditaudiobox.Text+"','" + neweditsummary.Text.Trim() + "','"+0+ "','" + 0 + "','"+null+"','" + neweditpdfpathbox.Text.Trim() + "','" +Ratings + "','" + 0 + "','" + 0 + "',,'" + neweditcoverpathbox.Text.Trim() + "','"+0+"')";
                                book.Name = neweditname.Text.Trim();
                                book.Author = neweditauthor.Text.Trim();
                                book.Published_Year = int.Parse(newedityear.Text.Trim());
                                book.Price = float.Parse(neweditprice.Text.Trim());
                                book.audio_Path = neweditaudiobox.Text;
                                book.Summary = neweditsummary.Text;
                                book.Pdf_Path = neweditpdfpathbox.Text;
                                book.Cover_Path = neweditcoverpathbox.Text;
                                SqlCommand cmd3 = new SqlCommand(Command3, connection);
                                cmd3.ExecuteNonQuery();
                                connection.Close();
                                MessageBox.Show("Edited Successfully!","Done");
                                this.Close();
                                /*
                                using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=I:\Project.mdf;Integrated Security=True;Connect Timeout=30"))
                                {
                                    conn.Open();
                                    using (SqlCommand cmd2 = new SqlCommand("UPDATE Books SET Name=@Name,Author=@Author,Published Year=@Published Year,Price=@Price,Summary=@Summary,Cover_Path=@Cover_Path,Pdf_Path=@Pdf_Path" + "WHERE Name=@Name", conn))
                                    {
                                        try
                                        {
                                            cmd2.Parameters.AddWithValue("@Name", neweditname.Text.Trim());
                                            cmd2.Parameters.AddWithValue("@Author", neweditauthor.Text.Trim());
                                            cmd2.Parameters.AddWithValue("@Published Year", int.Parse(newedityear.Text));
                                            cmd2.Parameters.AddWithValue("@Price", float.Parse(neweditprice.Text));
                                            cmd2.Parameters.AddWithValue("@Summary", neweditsummary.Text.Trim());
                                            cmd2.Parameters.AddWithValue("@Cover_Path", neweditcoverpathbox.Text.Trim());
                                            cmd2.Parameters.AddWithValue("@Pdf_Path", neweditpdfpathbox.Text.Trim());
                                            int rows = cmd2.ExecuteNonQuery();
                                            conn.Close();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                    }
                                }
                               
                                SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=I:\proj.mdf;Integrated Security=True;Connect Timeout=30");
                                SqlDataAdapter adapter = new SqlDataAdapter();
                                SqlCommand cmd2 = new SqlCommand();
                                cmd2.CommandText = "UPDATE Books SET Name=@Name,Author=@Author,Price=@Price,Summary=@Summary,Cover_Path=@Cover_Path,Pdf_Path=@Pdf_Path" + "WHERE Name=@currentnamebox.Text";
                                adapter.UpdateCommand = cmd2;
                                adapter.UpdateCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = neweditname.Text;
                                adapter.UpdateCommand.Parameters.Add("@Author", SqlDbType.NVarChar).Value = neweditauthor.Text;
                                //adapter.UpdateCommand.Parameters.Add("@Published Year", SqlDbType.Int).Value = int.Parse(newedityear.Text);
                                adapter.UpdateCommand.Parameters.Add("@Price", SqlDbType.Float).Value = float.Parse(neweditprice.Text);
                                adapter.UpdateCommand.Parameters.Add("@Summary", SqlDbType.NVarChar).Value = neweditsummary.Text;
                                adapter.UpdateCommand.Parameters.Add("@Cover_Path", SqlDbType.NVarChar).Value = neweditcoverpathbox.Text;
                                adapter.UpdateCommand.Parameters.Add("@Pdf_Path", SqlDbType.NVarChar).Value = neweditpdfpathbox.Text;
                                adapter.UpdateCommand.Connection = conn;
                                conn.Open();
                                adapter.UpdateCommand.ExecuteNonQuery();
                                conn.Dispose();
                                conn.Close();
                               /*
                                conn.Open();

                                string command2 = "Update Books SET Name='" + neweditname.Text.Trim() + "',Author='" + neweditauthor.Text.Trim() + "',Published Year='" + int.Parse(newedityear.Text) + "',Price='" + float.Parse(neweditprice.Text)+ "',Summary='" + neweditsummary.Text.Trim() + "',Cover_Path='" + neweditcoverpathbox.Text.Trim() + "',Pdf_Path='" + neweditpdfpathbox.Text.Trim() + "'where Name='"+currentnamebox.Text+"' ";
                                
                                try
                            {
                                cmd2.BeginExecuteNonQuery();
                                MessageBox.Show("Edited Successfully");
                           }
                            catch (SqlException ex)
                            {
                                MessageBox.Show(ex.Message);
                           }
                           */
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

