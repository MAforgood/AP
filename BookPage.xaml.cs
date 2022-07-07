using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace project
{
    /// <summary>
    /// Interaction logic for BookPage.xaml
    /// </summary>
    public partial class BookPage : Window
    {
        public Book bookofpage { get; set; }
        public User user { get; set; }
        public BookPage(Book book,User _user)
        {
            user = _user;
            bookofpage = book;
            DataContext = this;
            InitializeComponent();
        }

        private void Book_Pdf_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "pdf files (*.pdf) |*.pdf;";
            dialog.ShowDialog();
            if (dialog.FileName != null)
            {
               
            }
            //Process process = new Process();
            //string pdf =@""+bookofpage.Pdf_Path;
            //process.StartInfo.FileName = pdf;
            //process.StartInfo.Arguments = pdf;
            //process.Start();
        }
        private int click_counter = 0;
        private void Book_Buy_Click(object sender, RoutedEventArgs e)
        {
            if (clickcounter == 0)
            {
                //int previous = user.ShoppingCart.Count;
                if (user.ShoppingCart.Where(x => x.id == bookofpage.id).Count() > 0) { MessageBox.Show("you already have this book"); return; }
                user.ShoppingCart.Add(bookofpage);
                string newcart = "";
                for (int i = 0; i < user.ShoppingCart.Count; i++)
                {
                    newcart += user.ShoppingCart[i].id + ",";
                }
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                connection.Open();
                string Command = "Update Users Set Shopping_Cart = '" + newcart + "' where Email = '" + user.Email + "'";
                SqlCommand command = new SqlCommand(Command, connection);
                command.ExecuteNonQueryAsync();
                connection.Close();
                if (user.ShoppingCart.Count > 1)
                {
                    MessageBox.Show("you'he added this seccesfully");
                }
            }
            else { MessageBox.Show("you'he added this so far"); }
            clickcounter++;
        }
        private int clickcounter=0;
        public int Rating { get; set; }
        private void Star_1_Click(object sender, RoutedEventArgs e)
        {
            clickcounter++;
            if (clickcounter > 1)
            {
                MessageBox.Show("you've voted so far");
            }
            else
                bookofpage.Ratings.Add(1);
            string New_rate = "";
            for (int i = 0; i < bookofpage.Ratings.Count; i++)
            {
                New_rate += bookofpage.Ratings[i] + ",";
            }
            int Ratingsum = bookofpage.Ratings.Sum();
            Rating = Ratingsum/ bookofpage.Ratings.Count();
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string Command = "Update Books Set Ratings = '" + New_rate + "'  where id = '" + bookofpage.id + "'";
            SqlCommand cmd = new SqlCommand(Command, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show($"you voted for 1 star");
        }

        private void Star_2_Click(object sender, RoutedEventArgs e)
        {
            clickcounter++;
            if (clickcounter > 1)
            {
                MessageBox.Show("you've voted so far");
            }
            else
                bookofpage.Ratings.Add(2);
            string New_rate = "";
            for (int i = 0; i < bookofpage.Ratings.Count; i++)
            {
                New_rate += bookofpage.Ratings[i] + ",";
            }
            int Ratingsum = bookofpage.Ratings.Sum();
            Rating = Ratingsum / bookofpage.Ratings.Count();
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string Command = "Update Books Set Ratings = '" + New_rate + "'  where id = '" + bookofpage.id + "'";
            SqlCommand cmd = new SqlCommand(Command, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show($"you voted for 2 star");
        }

        private void Star_3_Click(object sender, RoutedEventArgs e)
        {
            clickcounter++;
            if (clickcounter > 1)
            {
                MessageBox.Show("you've voted so far");
            }
            else
                bookofpage.Ratings.Add(3);
            string New_rate = "";
            for (int i = 0; i < bookofpage.Ratings.Count; i++)
            {
                New_rate += bookofpage.Ratings[i] + ",";
            }
            int Ratingsum = bookofpage.Ratings.Sum();
            Rating = Ratingsum / bookofpage.Ratings.Count();
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string Command = "Update Books Set Ratings = '" + New_rate + "'  where id = '" + bookofpage.id + "'";
            SqlCommand cmd = new SqlCommand(Command, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show($"you voted for 3 star");
        }

        private void Star_4_Click(object sender, RoutedEventArgs e)
        {
            clickcounter++;
            if (clickcounter > 1)
            {
                MessageBox.Show("you've voted so far");
            }
            else
            bookofpage.Ratings.Add(4);
            string New_rate = "";
            for (int i = 0; i < bookofpage.Ratings.Count; i++)
            {
                New_rate += bookofpage.Ratings[i] + ",";
            }
            int Ratingsum = bookofpage.Ratings.Sum();
            Rating = Ratingsum / bookofpage.Ratings.Count();
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string Command = "Update Books Set Ratings = '" + New_rate + "'  where id = '" + bookofpage.id + "'";
            SqlCommand cmd = new SqlCommand(Command, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show($"you voted for 4 star");
        }

        private void Star_5_Click(object sender, RoutedEventArgs e)
        {
            clickcounter++;
            if (clickcounter > 1)
            {
                MessageBox.Show("you've voted so far");
            }
            else
                bookofpage.Ratings.Add(5);
            string New_rate = "";
            for (int i = 0; i < bookofpage.Ratings.Count; i++)
            {
                New_rate += bookofpage.Ratings[i] + ",";
            }
            int Ratingsum = bookofpage.Ratings.Sum();
            Rating = Ratingsum / bookofpage.Ratings.Count();
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string Command = "Update Books Set Ratings = '" + New_rate + "'  where id = '" + bookofpage.id + "'";
            SqlCommand cmd = new SqlCommand(Command, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show($"you voted for 5 star");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public string sourcee { get; set; }
        private void Add_Bookmark_Butt_Click(object sender, RoutedEventArgs e)
        {
            if (user.BookMarks.Where(x => x.id == bookofpage.id).Count() > 0)
            {
                sourcee ="/add+bookmark-1319964827107158553.png";
                user.BookMarks.Remove(bookofpage);
                string NewBookmarks = "";
                for (int i = 0; i < user.BookMarks.Count; i++)
                {
                    NewBookmarks += user.BookMarks[i].id + ",";
                }
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                connection.Open();
                string Command = "Update Users Set BookMarks = '" + NewBookmarks + "' where Email = '" + user.Email + "'";
                SqlCommand command = new SqlCommand(Command, connection);
                command.ExecuteNonQueryAsync();
                connection.Close();
            }
            else
            {
               sourcee="/1200px-OOjs_UI_icon_bookmark.svg.png";
                user.BookMarks.Add(bookofpage);
                string New_Bookmarks = "";
                for (int i = 0; i < user.BookMarks.Count; i++)
                {
                    New_Bookmarks += user.BookMarks[i].id + ",";
                }
                SqlConnection connection1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                connection1.Open();
                string Command1 = "Update Users Set BookMarks = '" + New_Bookmarks + "' where Email = '" + user.Email + "'";
                SqlCommand command1 = new SqlCommand(Command1, connection1);
                command1.ExecuteNonQueryAsync();
                connection1.Close();
            }
        }
    }
}
