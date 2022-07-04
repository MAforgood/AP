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
using System.IO;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace project
{
    /// <summary>
    /// Interaction logic for AdminMain.xaml
    /// </summary>

    public partial class AdminMain : Window
    {
        public AdminMain()
        {
            InitializeComponent();
        }



        private static void Uploader(string filename, Stream Data)

        {

            BinaryReader reader = new BinaryReader(Data);

            string path = @"C:\new\file";

            FileStream fstream = new FileStream(path, FileMode.CreateNew);

            BinaryWriter wr = new BinaryWriter(fstream);

            wr.Write(reader.ReadBytes((int)Data.Length));

            wr.Close();

            fstream.Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Multiselect = false;

            open.Filter = "AllFiles|*.*";

            if ((bool)open.ShowDialog())

            {

                Uploader(open.FileName, open.OpenFile());

                textBlock1.Text = "File Uploaded";

            }

            else

            {

                textBlock1.Text = " No files selected!";

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Multiselect = false;

            open.Filter = "AllFiles|*.*";

            if ((bool)open.ShowDialog())

            {

                Uploader(open.FileName, open.OpenFile());

                textBlock2.Text = "Image Uploaded";

            }

            else

            {

                textBlock2.Text = " No Image selected!";

            }
        }

        private void Editbut_Click(object sender, RoutedEventArgs e)
        {
            if (Books.allbooksname.Contains(editnamebox.Text))
            {
                Editbook edit = new Editbook();
                edit.Show();
            }
            else
            {
                MessageBox.Show("Not Existed!");
            }
        }

        private void Vippricebut_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Succesfully Set!", "Done!");
        }

        private void Vipbookbut_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Mentioned Book Was Added To VIP Section!", "Done!");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Off Was Set Succesfully!", "Done!");
        }

        private void Walletincomebut_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Total Income : ", "Income");
        }

        private void Walletdepositbut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Statsbooknamebut_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sold Numbers:  \nIncome: ", "Stats");
        }

        private void Statsbookratebut_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Average Rate: ", "Rating");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (addbooknamebox.Text == "" || addbookauthorbox.Text == "" || addbookyearbox.Text == "" || addbookpricebox.Text == "" || addbooksummarybox.Text == "")
            {
                MessageBox.Show("None Of Fields Can Be Empty!", "Empty Input");
            }
            if (Regex.IsMatch(addbooknamebox.Text, @"^[a-zA-Z]{3,32}$"))
            {
                SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\DataSql\data.mdf;Integrated Security=True;Connect Timeout=30");
                _connection.Open();
                string command = "select * from Users where Name ='" + addbooknamebox.Text.Trim() + "' ";
                SqlDataAdapter adapter = new SqlDataAdapter(command, _connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                SqlCommand cmd = new SqlCommand(command, _connection);
                cmd.BeginExecuteNonQuery();
                if (table.Rows.Count > 0)
                {
                    MessageBox.Show("This Book Already Existed", "Repetition"); ; return;
                }


                if (Regex.IsMatch(addbookauthorbox.Text, @"^[a-zA-Z]{3,32}$"))
                {
                    if (int.Parse(addbookyearbox.Text) > 0 && int.Parse(addbookyearbox.Text) < 2022)
                    {
                        if (int.Parse(addbookpricebox.Text) > 0 && int.Parse(addbookpricebox.Text) < 10000000)
                        {
                            if (addbooksummarybox.Text.Length > 0 && addbooksummarybox.Text.Length < 300)
                            {
                                Books book = new Books(addbooknamebox.Text, addbookauthorbox.Text, int.Parse(addbookyearbox.Text), int.Parse(addbookpricebox.Text), addbooksummarybox.Text);
                                book.AddTotable();
                                this.Close();

                            }
                            else { MessageBox.Show("It Is Not In Correct Format","Summary Format!"); }
                        }
                        else { MessageBox.Show("It Is Not In Correct Format", "Price Format!"); }
                    }
                    else { MessageBox.Show("It Is Not In Correct Format", "Year Format!"); }
                }
                else { MessageBox.Show("It Is Not In Correct Format", "Author Format!"); }
            }
            else { MessageBox.Show("It Is Not In Correct Format", "Name Format!"); }
        }


        private void Userlistsearchbut_Click(object sender, RoutedEventArgs e)
        {
            if (usersearchbox.Text == "") { MessageBox.Show("Cnat Be Empty"); }
            else
            {
                string command = "select from Users where Name = '" + usersearchbox.Text + "' or Email = '" + usersearchbox.Text + "'";
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\DataSql\data.mdf;Integrated Security=True;Connect Timeout=30");
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable data = new DataTable();
                adapter.Fill(data);
            }
        }

        private void Booklostsearchbut_Click(object sender, RoutedEventArgs e)
        {
            if (booksearchbox.Text == "") { MessageBox.Show("Cnat Be Empty"); }
            else
            {
                string command = "select from Users where Name = '" + booksearchbox.Text + "' or Author = '" + booksearchbox.Text + "'";
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\DataSql\data.mdf;Integrated Security=True;Connect Timeout=30");
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable data = new DataTable();
                adapter.Fill(data);
            }
        }
    }
}
 
