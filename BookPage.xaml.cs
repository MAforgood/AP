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
        private int clickcounter = 0;
        public BookPage(Book book, User _user)
        {
            if (clickcounter == 1)
            {
                Star_1.Visibility = Visibility.Collapsed;
                goldStar_1.Visibility = Visibility.Visible;
            }
            else if (clickcounter == 2)
            {
                Star_1.Visibility = Visibility.Collapsed;
                goldStar_1.Visibility = Visibility.Visible;
                Star_2.Visibility = Visibility.Collapsed;
                goldStar_2.Visibility = Visibility.Visible;
            }
            else if (clickcounter == 3)
            {
                Star_1.Visibility = Visibility.Collapsed;
                goldStar_1.Visibility = Visibility.Visible;
                Star_2.Visibility = Visibility.Collapsed;
                goldStar_2.Visibility = Visibility.Visible;
                Star_3.Visibility = Visibility.Collapsed;
                goldStar_3.Visibility = Visibility.Visible;
            }
            else if (clickcounter == 4)
            {
                Star_1.Visibility = Visibility.Collapsed;
                goldStar_1.Visibility = Visibility.Visible;
                Star_2.Visibility = Visibility.Collapsed;
                goldStar_2.Visibility = Visibility.Visible;
                Star_3.Visibility = Visibility.Collapsed;
                goldStar_3.Visibility = Visibility.Visible;
                Star_4.Visibility = Visibility.Collapsed;
                goldStar_4.Visibility = Visibility.Visible;
            }
            else if (clickcounter == 5)
            {
                Star_1.Visibility = Visibility.Collapsed;
                goldStar_1.Visibility = Visibility.Visible;
                Star_2.Visibility = Visibility.Collapsed;
                goldStar_2.Visibility = Visibility.Visible;
                Star_3.Visibility = Visibility.Collapsed;
                goldStar_3.Visibility = Visibility.Visible;
                Star_4.Visibility = Visibility.Collapsed;
                goldStar_4.Visibility = Visibility.Visible;
                Star_5.Visibility = Visibility.Collapsed;
                goldStar_5.Visibility = Visibility.Visible;
            }
            user = _user;
            bookofpage = book;
            DataContext = this;
            InitializeComponent();
        }

        private void Book_Pdf_Click(object sender, RoutedEventArgs e)
        {
            if (bookofpage.Pdf_Path == "")
            {
                MessageBox.Show("sorry this book doesn't have PDF");
            }
            else
            {
                Process process = new Process();
                process.StartInfo.UseShellExecute = true;
                string pdf = @"" + bookofpage.Pdf_Path;
                process.StartInfo.FileName = pdf;
                //process.StartInfo.Arguments = pdf;
                process.Start();
            }
        }
        private int click_counter = 0;
        private void Book_Buy_Click(object sender, RoutedEventArgs e)
        {
            if (clickcounter == 0)
            {
                //int previous = user.ShoppingCart.Count;
                if (user.Books.Where(x => x.id == bookofpage.id).Count() > 0) { MessageBox.Show("you already have this book"); return; }
                if (user.ShoppingCart.Where(x => x.id == bookofpage.id).Count() > 0) { MessageBox.Show("you have added this book"); return; }
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
        public int Rating { get; set; }
        private void Star_1_Click(object sender, RoutedEventArgs e)
        {
            clickcounter = 1;
            //if (clickcounter > 0)
            //{
            //    MessageBox.Show("you've voted so far");
            //}
            //else
            bookofpage.Ratings.Add(1);
            Star_1.Visibility = Visibility.Collapsed;
            goldStar_1.Visibility = Visibility.Visible;
            string New_rate = "";
            for (int i = 0; i < bookofpage.Ratings.Count; i++)
            {
                New_rate += bookofpage.Ratings[i] + ",";
            }
            int Ratingsum = bookofpage.Ratings.Sum();
            if (bookofpage.Ratings.Count > 0)
                Rating = Ratingsum / bookofpage.Ratings.Count();
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
            clickcounter = 2;
            //if (clickcounter > 0)
            //{
            //    MessageBox.Show("you've voted so far");
            //}
            //else
            bookofpage.Ratings.Add(2);
            Star_1.Visibility = Visibility.Collapsed;
            goldStar_1.Visibility = Visibility.Visible;
            Star_2.Visibility = Visibility.Collapsed;
            goldStar_2.Visibility = Visibility.Visible;
            string New_rate = "";
            for (int i = 0; i < bookofpage.Ratings.Count; i++)
            {
                New_rate += bookofpage.Ratings[i] + ",";
            }
            int Ratingsum = bookofpage.Ratings.Sum();
            if (bookofpage.Ratings.Count > 0)
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
            clickcounter = 3;
            //if (clickcounter > 0)
            //{
            //    MessageBox.Show("you've voted so far");
            //}
            //else
            bookofpage.Ratings.Add(3);
            Star_1.Visibility = Visibility.Collapsed;
            goldStar_1.Visibility = Visibility.Visible;
            Star_2.Visibility = Visibility.Collapsed;
            goldStar_2.Visibility = Visibility.Visible;
            Star_3.Visibility = Visibility.Collapsed;
            goldStar_3.Visibility = Visibility.Visible;
            string New_rate = "";
            for (int i = 0; i < bookofpage.Ratings.Count; i++)
            {
                New_rate += bookofpage.Ratings[i] + ",";
            }
            int Ratingsum = bookofpage.Ratings.Sum();
            if (bookofpage.Ratings.Count > 0)
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
            clickcounter = 4;
            //if (clickcounter > 0)
            //{
            //    MessageBox.Show("you've voted so far");
            //}
            //else
            bookofpage.Ratings.Add(4);
            Star_1.Visibility = Visibility.Collapsed;
            goldStar_1.Visibility = Visibility.Visible;
            Star_2.Visibility = Visibility.Collapsed;
            goldStar_2.Visibility = Visibility.Visible;
            Star_3.Visibility = Visibility.Collapsed;
            goldStar_3.Visibility = Visibility.Visible;
            Star_4.Visibility = Visibility.Collapsed;
            goldStar_4.Visibility = Visibility.Visible;
            string New_rate = "";
            for (int i = 0; i < bookofpage.Ratings.Count; i++)
            {
                New_rate += bookofpage.Ratings[i] + ",";
            }
            int Ratingsum = bookofpage.Ratings.Sum();
            if (bookofpage.Ratings.Count > 0)
                Rating = Ratingsum / bookofpage.Ratings.Count;
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
            clickcounter = 5;
            //if (clickcounter > 0)
            //{
            //    MessageBox.Show("you've voted so far");
            //}
            //else
            bookofpage.Ratings.Add(5);
            Star_1.Visibility = Visibility.Collapsed;
            goldStar_1.Visibility = Visibility.Visible;
            Star_2.Visibility = Visibility.Collapsed;
            goldStar_2.Visibility = Visibility.Visible;
            Star_3.Visibility = Visibility.Collapsed;
            goldStar_3.Visibility = Visibility.Visible;
            Star_4.Visibility = Visibility.Collapsed;
            goldStar_4.Visibility = Visibility.Visible;
            Star_5.Visibility = Visibility.Collapsed;
            goldStar_5.Visibility = Visibility.Visible;
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
                user.BookMarks.Remove(bookofpage);
                Add_Bookmark_Butt.Visibility = Visibility.Visible;
                Remove_Bookmark_Butt.Visibility = Visibility.Collapsed;
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
                user.BookMarks.Add(bookofpage);
                Add_Bookmark_Butt.Visibility = Visibility.Collapsed;
                Remove_Bookmark_Butt.Visibility = Visibility.Visible;
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

        private void Remove_Bookmark_Butt_Click(object sender, RoutedEventArgs e)
        {
            if (user.BookMarks.Where(x => x.id == bookofpage.id).Count() > 0)
            {
                user.BookMarks.Remove(bookofpage);
                Remove_Bookmark_Butt.Visibility = Visibility.Collapsed;
                Add_Bookmark_Butt.Visibility = Visibility.Visible;
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
                user.BookMarks.Add(bookofpage);
                Add_Bookmark_Butt.Visibility = Visibility.Collapsed;
                Remove_Bookmark_Butt.Visibility = Visibility.Visible;
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

        private void goldStar_1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("you've voted so far");
        }

        private void goldStar_2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("you've voted so far");
        }

        private void goldStar_3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("you've voted so far");
        }

        private void Star_4_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("you've voted so far");
        }

        private void goldStar_5_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("you've voted so far");
        }

        private void Pay_Directly_Click(object sender, RoutedEventArgs e)
        {
            if (user.Books.Where(x => x.id == bookofpage.id).Count() > 0) { MessageBox.Show("you already have this book"); return; }
            float price = bookofpage.Price * (1 - bookofpage.discount_value / 100);
            Payment payment = new Payment(price);
            payment.Show();
            if (payment.Boolean)
            {
                user.Wallet -= price;
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                connection.Open();
                string command = "Update Users Set Wallet = '" + user.Wallet + "' where Email = '" + user.Email + "'";
                SqlCommand command1 = new SqlCommand(command, connection);
                command1.ExecuteNonQuery();
                connection.Close();
                bookofpage.Total_Sale++;
                bookofpage.Total_Income += bookofpage.Price * (1 - bookofpage.discount_value / 100);

            }
        }

        private void Book_Voice_Click(object sender, RoutedEventArgs e)
        {
            if (bookofpage.audio_Path == "")
            {
                MessageBox.Show("sorry this book doesn't have PDF");
            }
            else
            {
                Process process = new Process();
                process.StartInfo.UseShellExecute = true;
                string audio = @"" + bookofpage.audio_Path;
                process.StartInfo.FileName = audio;
                //process.StartInfo.Arguments = pdf;
                process.Start();
            }
        }
    }
}
