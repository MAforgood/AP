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
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Window
    {
        public User user { get; set; }
        public float Uprice { get; set; }
        public UserPage(User _user)
        {
            user = _user;
            for (int i = 0; i < user.ShoppingCart.Count; i++)
            {
                Uprice += user.ShoppingCart[i].Price * (1 - user.ShoppingCart[i].discount_value / 100);
            }
            DataContext = this;
            InitializeComponent();
            UserPage_Tab.SelectedIndex = 0;
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string command = "select * from Books";
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                Book book = new Book(data.Rows[i][1].ToString(), data.Rows[i][2].ToString(), float.Parse(data.Rows[i][4].ToString()), int.Parse(data.Rows[i][3].ToString()), data.Rows[i][6].ToString(), data.Rows[i][14].ToString(), data.Rows[i][10].ToString());
                Book.books.Add(book);
            }
            connection.Close();
            SqlConnection connection1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
            connection1.Open();
            string command1 = "select * from Users where Email = '" + user.Email.Trim() + "'";
            SqlDataAdapter adapter1 = new SqlDataAdapter(command1, connection1);
            DataTable data1 = new DataTable();
            adapter1.Fill(data1);
            connection1.Close();
            user.Books.Clear();
            user.BookMarks.Clear();
            user.ShoppingCart.Clear();
            List<string> book_ids_string = data1.Rows[0][6].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            for (int i = 0; i < book_ids_string.Count; i++)
            {
                _user.Books.Add(Book.books.Where(x => x.id == int.Parse(book_ids_string[i])).First());
            }

            List<string> bookmark_ids_string = data1.Rows[0][7].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            for (int i = 0; i < bookmark_ids_string.Count; i++)
            {
                if (bookmark_ids_string[i] != null)
                {
                    _user.BookMarks.Add(Book.books.Where(x => x.id == int.Parse(bookmark_ids_string[i])).First());
                }
            }

            List<string> Cart_ids_string = data1.Rows[0][9].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            for (int i = 0; i < Cart_ids_string.Count; i++)
            {
                _user.ShoppingCart.Add(Book.books.Where(x => x.id == int.Parse(Cart_ids_string[i])).First());
            }

        }
        ObservableCollection<Book> Searched_Books = new ObservableCollection<Book>();
        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            if (search_Box.Text == "") { search_Box.Focus(); Search_ListBox.Visibility = Visibility.Collapsed; return; }
            else
            {
                List<Book> books = Book.books.Where(x => x.Name == search_Box.Text || x.Author == search_Box.Text).ToList();
                if (books.Count == 0)
                {
                    Search_ListBox.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Search_ListBox.Visibility = Visibility.Visible;
                    Searched_Books.Clear();
                    for (int i = 0; i < books.Count; i++)
                    {
                        Searched_Books.Add(books[i]);
                    }
                    Search_ListBox.ItemsSource = Searched_Books;
                }
            }
        }

        private void Books_Butt_Click(object sender, RoutedEventArgs e)
        {
            Mybooks_ListBox.ItemsSource = user.Books;
            Prof_Tabs.Visibility = Visibility.Visible;
            Prof_Tabs.SelectedIndex=0;
        }

        private void Change_info_Click(object sender, RoutedEventArgs e)
        {
            if (Prof_Tabs.Visibility == Visibility.Visible)
                Prof_Tabs.Visibility = Visibility.Collapsed;
            else { Prof_Tabs.SelectedIndex = 1; Prof_Tabs.Visibility = Visibility.Visible; }

        }

        private void Change_info_Butt_Click(object sender, RoutedEventArgs e)
        {
            Name_error.Visibility = Visibility.Collapsed;
            Empty_error.Visibility = Visibility.Collapsed;
            Email_error.Visibility = Visibility.Collapsed;
            Phone_error.Visibility = Visibility.Collapsed;
            Same_error.Visibility = Visibility.Collapsed;
            null_error.Visibility = Visibility.Collapsed;
            wrong_error.Visibility = Visibility.Collapsed;
            Pass_error.Visibility = Visibility.Collapsed;
            if (fname.Text == "" && lname.Text == "" && pnum.Text == "" && email.Text == "" && pass.Text == "" && new_pass.Text == "")
            {
                fname.Focus();
                //Empty_error.Visibility = Visibility.Visible;
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
            else { if (pnum.Text == "") ; else { Phone_error.Visibility = Visibility.Visible; return; } }
            if (Regex.IsMatch(email.Text, @"^.{1,32}@.{1,32}\..{1,32}$"))
            {
                if (email.Text == user.Email)
                {
                    Same_error.Visibility = Visibility.Visible; return;
                }
                user.Email = email.Text;
            }
            else { if (email.Text == "") ; else { Email_error.Visibility = Visibility.Visible; return; } }
            if (new_pass.Text != "")
            {
                if (pass.Text == "")
                {
                    null_error.Visibility = Visibility.Visible; return;
                }
                else
                {
                    if (Regex.IsMatch(pass.Text, @"^(?=.*[a-z])(?=.*[A-Z]).{8,40}$") || pass.Text == "")
                    {
                        if (pass.Text == user.Password)
                        {
                            if (Regex.IsMatch(new_pass.Text, @"^(?=.*[a-z])(?=.*[A-Z]).{8,40}$") || pass.Text == "")
                            {
                                user.Password = new_pass.Text;
                            }
                            else { Pass_error.Visibility = Visibility.Visible; return; }
                        }
                        else { wrong_error.Visibility = Visibility.Visible; return; }
                    }
                    else { Pass_error.Visibility = Visibility.Visible; return; }
                }
            }
        }

        private void Profile_Butt_Click(object sender, RoutedEventArgs e)
        {
            if (Tabs.Visibility == Visibility.Visible)
                Tabs.Visibility = Visibility.Collapsed;
            else
            {
                Tabs.SelectedIndex = 0;
                Tabs.Visibility = Visibility.Visible;
            }
        }

        private void Back_Butt_Click(object sender, RoutedEventArgs e)
        {
            Prof_Tabs.Visibility = Visibility.Collapsed;
            //Tabs.Visibility = Visibility.Collapsed;
        }
        //private void Back1_Butt_Click(object sender, RoutedEventArgs e)
        //{
        //    Prof_Tabs.Visibility = Visibility.Collapsed;
        //    //Tabs.Visibility = Visibility.Collapsed;
        //}

        private void Search_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Choosen_Book = Search_ListBox.SelectedIndex;
            if (Choosen_Book >= 0)
            {
                BookPage bookpage = new BookPage(Searched_Books[Choosen_Book],user);
                bookpage.Show();
            }
        }

        private void search_Box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (search_Box.Text == "") { search_Box.Focus(); Search_ListBox.Visibility = Visibility.Collapsed; return; }
            else
            {
                List<Book> books = Book.books.Where(x => x.Name.Contains(search_Box.Text) || x.Author.Contains(search_Box.Text)).ToList();
                if (books.Count == 0)
                {
                    Search_ListBox.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Search_ListBox.Visibility = Visibility.Visible;
                    Searched_Books.Clear();
                    for (int i = 0; i < books.Count; i++)
                    {
                        Searched_Books.Add(books[i]);
                    }
                    Search_ListBox.ItemsSource = Searched_Books;
                }
            }
        }

        private void Mybooks_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Choosen_Book = Mybooks_ListBox.SelectedIndex;
            if (Choosen_Book >= 0)
            {
                BookPage bookpage = new BookPage(user.BookMarks[Choosen_Book],user);
                bookpage.Show();
            }
        }

        private void fav_Butt_Click(object sender, RoutedEventArgs e)
        {
            Mybooks_ListBox.ItemsSource = user.BookMarks;
            Prof_Tabs.Visibility = Visibility.Visible;
            Prof_Tabs.SelectedIndex = 0;
        }

        private void MyCart_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int Choosen_Book = MyCart_ListBox.SelectedIndex;
            //if (Choosen_Book >= 0)
            //{
            //    BookPage bookpage = new BookPage(user.ShoppingCart[Choosen_Book],user);
            //    bookpage.Show();
            //}
        }
        private void Shopping_Cart_Click(object sender, RoutedEventArgs e)
        {
            //Cart = user.ShoppingCart;
            for (int i = 0; i < user.ShoppingCart.Count; i++)
            {
                Uprice += user.ShoppingCart[i].Price * (1 - user.ShoppingCart[i].discount_value / 100);
            }
            MyCart_ListBox.ItemsSource = user.ShoppingCart;
            UserPage_Tab.SelectedIndex = 1;

        }

        private void Back2_Butt_Click(object sender, RoutedEventArgs e)
        {
            UserPage_Tab.SelectedIndex = 0;
        }

        private void Remove_Book_Butt_Click(object sender, RoutedEventArgs e)
        {
            //int Choosen_id = user.ShoppingCart[Choosen_Book].id;
            //int Choosen_id = Choosen_Book.id;
            //user.ShoppingCart.Remove(user.ShoppingCart[Choosen_Book]);
            Book Choosen_Book = (Book)MyCart_ListBox.SelectedItem;
            user.ShoppingCart.Remove(Choosen_Book);
            string New_Cart = "";
            for (int i = 0;i < user.ShoppingCart.Count; i++)
            {
                New_Cart +=user.ShoppingCart[i].id+",";
            }
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string Command = "Update Users Set Shopping_Cart = '"+New_Cart+"'  where Email = '" + user.Email + "'";
            SqlCommand cmd = new SqlCommand(Command, connection);
            cmd.ExecuteNonQuery();
            connection.Close();

        }

        private void Pay_Butt_Click(object sender, RoutedEventArgs e)
        {
            Payment payment = new Payment(Uprice);


        }
    }
}
