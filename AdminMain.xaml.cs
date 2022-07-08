using System;
using System.Collections.Generic;
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
    /// Interaction logic for AdminMain.xaml
    /// </summary>

    public partial class AdminMain : Window
    {

        public AdminMain()
        {

            InitializeComponent();
            SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
            _connection.Open();
            string command = "select * from Books ";
            SqlDataAdapter adapter = new SqlDataAdapter(command, _connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Book book = new Book(table.Rows[i][1].ToString(), table.Rows[i][2].ToString(), float.Parse(table.Rows[i][4].ToString()), int.Parse(table.Rows[i][3].ToString()), table.Rows[i][6].ToString(), table.Rows[i][14].ToString(), table.Rows[i][10].ToString(), table.Rows[i][5].ToString());
                List<string> ratings = table.Rows[i][11].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                for (int j = 0; j < ratings.Count; j++)
                {
                    if (ratings[j] != null)
                        book.Ratings.Add(int.Parse(ratings[j].ToString()));
                }
                book.Total_Sale = int.Parse(table.Rows[i][12].ToString());
                book.Total_Income = float.Parse(table.Rows[i][13].ToString());
                book.Rate = book.Ratings.Sum(x => x / book.Ratings.Count);
                Book.books.Add(book);
            }
        }


/*
        private static void Uploader(string filename, Stream Data)

        {

            BinaryReader reader = new BinaryReader(Data);

            string path = @"C:";

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
        */
        private void Editbut_Click(object sender, RoutedEventArgs e)
        {
            if (editnamebox.Text.Length>0&&editnamebox.Text.Length<50)
            {
                SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                _connection.Open();
                string command = "select * from Books where Name ='" + editnamebox.Text.Trim() + "' ";
                SqlDataAdapter adapter = new SqlDataAdapter(command, _connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Book book = new Book(table.Rows[i][1].ToString(), table.Rows[i][2].ToString(), float.Parse(table.Rows[i][4].ToString()), int.Parse(table.Rows[i][3].ToString()), table.Rows[i][6].ToString(), table.Rows[i][14].ToString(), table.Rows[i][10].ToString(), table.Rows[i][5].ToString());
                    List<string> ratings = table.Rows[i][11].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                    for (int j = 0; j < ratings.Count; j++)
                    {
                        if (ratings[j] != null)
                            book.Ratings.Add(int.Parse(ratings[j].ToString()));
                    }
                    book.Total_Sale = int.Parse(table.Rows[i][12].ToString());
                    book.Total_Income = float.Parse(table.Rows[i][13].ToString());
                    book.Rate = book.Ratings.Sum(x => x / book.Ratings.Count);
                    Book.books.Add(book);
                    SqlCommand cmd = new SqlCommand(command, _connection);
                    cmd.BeginExecuteNonQuery();

                    if (table.Rows.Count != 1)
                    {
                        MessageBox.Show("This Book Doesnt Exists!", "Not Found"); ; return;
                    }

                    Editbook edit = new Editbook(book);
                    edit.Show();
                }
            }
            else
            {
                MessageBox.Show("Wrong Format!");
            }
            
        }

        private void Vippricebut_Click(object sender, RoutedEventArgs e)
        {
            if (double.Parse(vipprice.Text) < 0)
            {
                MessageBox.Show("Price Can Not Be Negatve!","Wrong Input");
            }
            else
            {
                SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                _connection.Open();
                string command2 = "Update Books SET vipfee='"+float.Parse(vipprice.Text)+"'where Name='"+vipbooksbox.Text+"'";
                SqlCommand cmd2 = new SqlCommand(command2, _connection);
                try
                {
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Vip Fee Set", "Done!");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Vipbookbut_Click(object sender, RoutedEventArgs e)
        {
            if (vipbooksbox.Text.Length>0)
            {
                
                SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                _connection.Open();
                string command = "select * from Books where Name ='" + vipbooksbox.Text.Trim() + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(command, _connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                SqlCommand cmd = new SqlCommand(command, _connection);
                cmd.BeginExecuteNonQuery();
                if (table.Rows.Count != 1)
                {
                    MessageBox.Show("This Book Doesnt Exists!", "Not Found"); ; return;
                }
                _connection.Close();
                SqlConnection _connection2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                _connection2.Open();
                //SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\DataSql\data.mdf;Integrated Security=True;Connect Timeout=30");
                // _connection.Open();
                string command2 = "Update Books SET Type='"+"1"+"'where Name='"+vipbooksbox.Text+"'";
                SqlCommand cmd2 = new SqlCommand(command2, _connection2);
                try
                {
                    cmd2.ExecuteNonQuery();
                    Book.books.Where(x => x.Name == vipbooksbox.Text).ToList().ForEach(x => x.type = Type.VIP);
                    MessageBox.Show("Mentioned Book Was Added To VIP Section!", "Done!");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
               
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (offnamebookbox.Text.Length > 0 && offnamebookbox.Text.Length < 50)
            {
                SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                _connection.Open();
                string command = "select * from Books where Name ='" + offnamebookbox.Text.Trim() + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(command, _connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                SqlCommand cmd = new SqlCommand(command, _connection);
                cmd.BeginExecuteNonQuery();
                if (table.Rows.Count != 1)
                {
                    MessageBox.Show("This Book Doesnt Exists!", "Not Found"); ; return;
                }
                _connection.Close();

                if (float.Parse(offbookpercentagebox.Text) < 100 && float.Parse(offbookpercentagebox.Text) > 0)
                {
                    SqlConnection _connection2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                    _connection2.Open();
                    string command2 = "Update Books SET [Discount Time]='" + int.Parse(offbooktimebox.Text) + "',[Discount Value]='"+float.Parse(offbookpercentagebox.Text)+"' WHERE Name='"+offnamebookbox.Text+"'";

                    SqlCommand cmd2 = new SqlCommand(command2, _connection2);
                    try
                    {   TimeSpan timeSpan = TimeSpan.FromMinutes(60 * int.Parse(offbooktimebox.Text));
                        TimeOnly time = TimeOnly.Parse(offbooktimebox.Text+":00:00");
                        Book.books.Where(x => x.Name == offnamebookbox.Text).ToList().ForEach(x => { x.discount_Time = time; x.discount_value = float.Parse(offbookpercentagebox.Text); });
                        cmd2.BeginExecuteNonQuery();
                         MessageBox.Show("Set Successfully");
                        offnamebookbox.Text = "";
                        offbooktimebox.Text = "";
                        offbookpercentagebox.Text = "";
                           
                        _connection2.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    /*
                    SqlConnection _connection4 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=I:\proj.mdf;Integrated Security=True;Connect Timeout=30");
                    _connection4.Open();
                    string command4 = "Update Books SET Discount Value='" + float.Parse(offbookpercentagebox.Text) + "'";
                    SqlCommand cmd4 = new SqlCommand(command4, _connection4);
                    try
                    {
                        cmd4.BeginExecuteNonQuery();
                        MessageBox.Show("Set Successfully");
                        _connection2.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    */
                }

            }
            else
            {
                MessageBox.Show("Wrong Format!");
            }
        }
        private void Walletincomebut_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection _connection2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
            string command2 = "select SUM([Totalincome]) FROM Books";
            _connection2.Open();
            SqlCommand cmd2 = new SqlCommand(command2, _connection2);
            double sum = Convert.ToInt32(cmd2.ExecuteScalar());

            MessageBox.Show("Total Income : "+sum, "Income");
        }

        private void Walletdepositbut_Click(object sender, RoutedEventArgs e)
        {
            Walletadminpasscheck wallet = new Walletadminpasscheck();
            wallet.Show();
        }
        /*
        private void Statsbooknamebut_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(statsbooknamebox.Text, @"^[a-zA-Z]{3,32}$"))
            {
                SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\DataSql\data.mdf;Integrated Security=True;Connect Timeout=30");
                _connection.Open();
                string command = "select * from Books where Name ='" + statsbooknamebox.Text.Trim() + "' ";
                SqlDataAdapter adapter = new SqlDataAdapter(command, _connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                SqlCommand cmd = new SqlCommand(command, _connection);
                cmd.BeginExecuteNonQuery();
                if (table.Rows.Count != 1)
                {
                    MessageBox.Show("This Book Doesnt Exists!", "Not Found"); ; return;
                }
               



                MessageBox.Show("Sold Numbers:  \nIncome: ", "Stats");
            }
        }
        
        private void Statsbookratebut_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(statsbookratebox.Text, @"^[a-zA-Z]{3,32}$"))
            {
                SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\DataSql\data.mdf;Integrated Security=True;Connect Timeout=30");
                _connection.Open();
                string command = "select * from Books where Name ='" + statsbookratebox.Text.Trim() + "' ";
                SqlDataAdapter adapter = new SqlDataAdapter(command, _connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                SqlCommand cmd = new SqlCommand(command, _connection);
                cmd.BeginExecuteNonQuery();
                if (table.Rows.Count != 1)
                {
                    MessageBox.Show("This Book Doesnt Exists!", "Not Found"); ; return;
                }
               

            }
        }
        */
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (addbooknamebox.Text == "" || addbookauthorbox.Text == "" || addbookyearbox.Text == "" || addbookpricebox.Text == "" || addbooksummarybox.Text == ""||addbookimagepathbox.Text==""||addbookpdfpathbox.Text==""||addbookidbox.Text==""||addbookaudiopathbox.Text=="")
            {
                MessageBox.Show("None Of Fields Can Be Empty!", "Empty Input");
            }
           // if (editnamebox.Text.ToString().Length>0&& editnamebox.Text.ToString().Length<50)
           // {
                SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
            _connection.Open();
                string command = "select * from Books where Name ='" + addbooknamebox.Text.ToString().Trim() + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(command, _connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                SqlCommand cmd = new SqlCommand(command, _connection);
                cmd.BeginExecuteNonQuery();
                if (table.Rows.Count > 0)
                {
                    MessageBox.Show("This Book Already Existed", "Repetition"); ; return;
                }
            _connection.Close();
            SqlConnection _connection9 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
            _connection9.Open();
            string command9 = "select * from Books where Id ='" + int.Parse(addbookidbox.Text)+ "'";
            SqlDataAdapter adapter9 = new SqlDataAdapter(command9, _connection9);
            DataTable table9 = new DataTable();
            adapter9.Fill(table9);
            SqlCommand cmd9 = new SqlCommand(command9, _connection9);
            cmd9.BeginExecuteNonQuery();
            if (table9.Rows.Count > 0)
            {
                MessageBox.Show("This Id Already Existed", "Repetition"); ; return;
            }
            _connection.Close();
            if (Regex.IsMatch(addbookauthorbox.Text, @"^[a-zA-Z/ ]{3,32}$"))
                {
                if (int.Parse(addbookidbox.Text) > 0 && int.Parse(addbookidbox.Text) < 10000)
                {
                    if (int.Parse(addbookyearbox.Text) > 0 && int.Parse(addbookyearbox.Text) < 2022)
                    {
                        if (int.Parse(addbookpricebox.Text) > 0 && int.Parse(addbookpricebox.Text) < 10000000)
                        {
                            if (addbooksummarybox.Text.Length >= 0)
                            {
                                Book book = new Book(addbooknamebox.Text, addbookauthorbox.Text, float.Parse(addbookpricebox.Text), int.Parse(addbookyearbox.Text), addbooksummarybox.Text, addbookimagepathbox.Text, addbookpdfpathbox.Text, addbookaudiopathbox.Text);
                                float a = 0;
                                int b = 0;
                                TimeOnly time = new TimeOnly();
                                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                                connection.Open();
                                string Command2 = "insert into [Books] values('" + int.Parse(addbookidbox.Text) + "','" + book.Name + "','" + book.Author + "','" + book.Published_Year + "','" + book.Price + " ','"+book.audio_Path+"','" + book.Summary + "','"+time+ "','" +a+ "','"+null+"','" + book.Pdf_Path + "','" +book.Ratings+ "','" + b + "','" + a + "','" + book.Cover_Path + "','"+b+"')";
                                SqlCommand cmd2 = new SqlCommand(Command2, connection);
                                cmd2.ExecuteNonQuery();
                                connection.Close();
                                MessageBox.Show("The Book Was Added!", "Successful Attempt");
                                addbooknamebox.Text = "";
                                addbookauthorbox.Text = "";
                                addbookpricebox.Text = "";
                                addbookyearbox.Text = "";
                                addbooksummarybox.Text = "";
                                addbookpdfpathbox.Text = "";
                                addbookimagepathbox.Text = "";
                                addbookidbox.Text = "";
                                addbookaudiopathbox.Text = "";


                            }
                            else { MessageBox.Show("It Is Not In Correct Format", "Summary Format!"); }
                        }
                        else { MessageBox.Show("It Is Not In Correct Format", "Price Format!"); }
                    }
                    else { MessageBox.Show("It Is Not In Correct Format", "Year Format!"); }
                }
                else { MessageBox.Show("It Is Not In Correct Format", "Id Format!"); }
                }
                else { MessageBox.Show("It Is Not In Correct Format", "Author Format!"); }
           // }
           // else { MessageBox.Show("It Is Not In Correct Format", "Name Format!"); }
        }


        private void Userlistsearchbut_Click(object sender, RoutedEventArgs e)
        {
            if (usersearchbox.Text == "") { MessageBox.Show("Cnat Be Empty"); }
            else
            {
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                try
                {
                    connection.Open();
                    string command = "select * from Users where (Lastname = '" + usersearchbox.Text + "' or Email = '" + usersearchbox.Text + "') And Type='" +0+ "'";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable data = new DataTable("Users");
                    adapter.Fill(data);
                    userlistdatagrid.ItemsSource = data.DefaultView;
                    adapter.Update(data);
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "exception");
                }
            }
        }

        private void Booklostsearchbut_Click(object sender, RoutedEventArgs e)
        {
            if (booksearchbox.Text == "") { MessageBox.Show("Cant Be Empty"); }
            else
            {
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                try
                {
                    connection.Open();
                    string command = "select * from Books where Name = '" + booksearchbox.Text + "' or Author = '" + booksearchbox.Text + "'";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable data = new DataTable("Books");
                    adapter.Fill(data);
                    booklisstdatagrid.ItemsSource = data.DefaultView;
                    adapter.Update(data);
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "exception");
                }
            }
        }

        private void Delbut_Click(object sender, RoutedEventArgs e)
        {
            if (delnamebox.Text.Length>0&& delnamebox.Text.Length<50)
            {
                SqlConnection _connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                _connection.Open();
                string command = "select * from Books where Name ='" + delnamebox.Text.Trim() + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(command, _connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                SqlCommand cmd = new SqlCommand(command, _connection);
                cmd.BeginExecuteNonQuery();
                if (table.Rows.Count != 1)
                {
                    MessageBox.Show("This Book Doesnt Exists!", "Not Found"); ; return;
                }
                _connection.Close();
                SqlConnection _connection2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                string command2= "DELETE FROM Books WHERE Name ='"+ delnamebox.Text.Trim() + "'";
                _connection2.Open();
                SqlCommand cmd2 = new SqlCommand(command2,_connection2);
                try
                {
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Delete successful");
                    delnamebox.Text = "";
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Wrong Format!");
            }
        }

        private void Vipusersearchbut_Click(object sender, RoutedEventArgs e)
        {
            if (vipuserssearchbox.Text == "") { MessageBox.Show("Cant Be Empty"); }
            else
            {
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                try
                {
                    connection.Open();
                    string command = "select * from Users where (Lastname = '" + vipuserssearchbox.Text + "' or Email = '" + vipuserssearchbox.Text + " ') And Type='"+"1"+"'";
                    SqlCommand cmd = new SqlCommand(command, connection);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable data = new DataTable("Users");
                    adapter.Fill(data);
                    userlistdatagrid.ItemsSource = data.DefaultView;
                    adapter.Update(data);
                    connection.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message,"exception");
                }
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            addbooknamebox.Text = "";
            addbookauthorbox.Text = "";
            addbookpricebox.Text = "";
            addbookyearbox.Text = "";
            addbooksummarybox.Text = "";
            addbookpdfpathbox.Text = "";
            addbookimagepathbox.Text = "";
        }

        private void Showallbooksbut_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
            try
            {
                connection.Open();
                string command = "select * from Books";
                SqlCommand cmd = new SqlCommand(command, connection);
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable data = new DataTable("Books");
                adapter.Fill(data);
                booklisstdatagrid.ItemsSource = data.DefaultView;
                adapter.Update(data);
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "exception");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
            try
            {
                connection.Open();
                string command = "select * from Users ";
                SqlCommand cmd = new SqlCommand(command, connection);
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable data = new DataTable("Users");
                adapter.Fill(data);
                userlistdatagrid.ItemsSource = data.DefaultView;
                adapter.Update(data);
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "exception");
            }
        }
    }
}
 
