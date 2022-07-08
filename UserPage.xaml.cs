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
        static public TimeSpan Time_Left { get; set; }
        public static User staticuser { get; set; }
        public User user { get; set; }
        public static float Uprice { get; set; }
        public UserPage(User _user)
        {
            user = _user;
            UserPage.staticuser = _user;
            DataContext = this;
            if (UserPage.staticuser.ShoppingCart.Count > 0)
                Binding_Total_Price.Text = Uprice + "$";
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
                Book book = new Book(data.Rows[i][1].ToString(), data.Rows[i][2].ToString(), float.Parse(data.Rows[i][4].ToString()), int.Parse(data.Rows[i][3].ToString()), data.Rows[i][6].ToString(), data.Rows[i][14].ToString(), data.Rows[i][10].ToString(), data.Rows[i][5].ToString());
                List<string> ratings = data.Rows[i][11].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                for (int j = 0; j < ratings.Count; j++)
                {
                    if (ratings[j] != null)
                        book.Ratings.Add(int.Parse(ratings[j].ToString()));
                }
                book.Total_Sale = int.Parse(data.Rows[i][12].ToString());
                book.Total_Income = float.Parse(data.Rows[i][13].ToString());
                book.Rate = book.Ratings.Sum(x => x / book.Ratings.Count);
                Book.books.Add(book);
            }
            connection.Close();
            SqlConnection connection1 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
            connection1.Open();
            string command1 = "select * from Users where Email = '" + UserPage.staticuser.Email.Trim() + "'";
            SqlDataAdapter adapter1 = new SqlDataAdapter(command1, connection1);
            DataTable data1 = new DataTable();
            adapter1.Fill(data1);
            connection1.Close();
            Time_Left = DateTime.Now - user.Vip_Begining;
            for (int i = 0; i < UserPage.staticuser.ShoppingCart.Count; i++)
            {
                Uprice += UserPage.staticuser.ShoppingCart[i].Price * (1 - UserPage.staticuser.ShoppingCart[i].discount_value / 100);
            }
            UserPage.staticuser.Books.Clear();
            UserPage.staticuser.BookMarks.Clear();
            UserPage.staticuser.ShoppingCart.Clear();
            List<string> book_ids_string = data1.Rows[0][6].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            for (int i = 0; i < book_ids_string.Count; i++)
            {
                staticuser.Books.Add(Book.books.Where(x => x.id == int.Parse(book_ids_string[i])).First());
            }

            List<string> bookmark_ids_string = data1.Rows[0][7].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            for (int i = 0; i < bookmark_ids_string.Count; i++)
            {
                if (bookmark_ids_string[i] != null)
                {
                    staticuser.BookMarks.Add(Book.books.Where(x => x.id == int.Parse(bookmark_ids_string[i])).First());
                }
            }

            List<string> Cart_ids_string = data1.Rows[0][9].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            for (int i = 0; i < Cart_ids_string.Count; i++)
            {
                staticuser.ShoppingCart.Add(Book.books.Where(x => x.id == int.Parse(Cart_ids_string[i])).First());
            }
            UserPage.staticuser.Wallet = int.Parse(data1.Rows[0][5].ToString());
            string usertype = data1.Rows[0][8].ToString();
            TimeSpan time = DateTime.Now - user.Vip_Begining;
            if (usertype == "0" || time.Days >= 30)
            {
                UserPage.staticuser.type = Type.normal;
            }
            else if (usertype == "1" ||time.Days <= 30) { UserPage.staticuser.type = Type.VIP; }
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
            No_Books.Visibility = Visibility.Collapsed;
            if (staticuser.Books.Count == 0)
            {
                No_Books.Visibility = Visibility.Visible;
            }
            Mybooks_ListBox.ItemsSource = staticuser.Books;
            Prof_Tabs.Visibility = Visibility.Visible;
            Prof_Tabs.SelectedIndex = 0;
        }

        private void Change_info_Click(object sender, RoutedEventArgs e)
        {
            if (Prof_Tabs.Visibility == Visibility.Visible)
                Prof_Tabs.Visibility = Visibility.Collapsed;
            else { Prof_Tabs.SelectedIndex = 2; Prof_Tabs.Visibility = Visibility.Visible; }

        }

        private void Change_info_Butt_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
            Name_error.Visibility = Visibility.Collapsed;
            Empty_error.Visibility = Visibility.Collapsed;
            Email_error.Visibility = Visibility.Collapsed;
            Phone_error.Visibility = Visibility.Collapsed;
            Same_error.Visibility = Visibility.Collapsed;
            null_error.Visibility = Visibility.Collapsed;
            wrong_error.Visibility = Visibility.Collapsed;
            Pass_error.Visibility = Visibility.Collapsed;
            connection.Open();
            if (fname.Text == "" && lname.Text == "" && pnum.Text == "" && email.Text == "" && pass.Text == "" && new_pass.Text == "")
            {
                fname.Focus();
                //Empty_error.Visibility = Visibility.Visible;
                return;
            }
            if (Regex.IsMatch(fname.Text, @"^[a-zA-Z]{3,32}$"))
            {
                if (fname.Text == staticuser.First_Name)
                {
                    Same_error.Visibility = Visibility.Visible; return;
                }
                staticuser.First_Name = fname.Text;
                string command = "Update Users Set Firstname = '" + staticuser.First_Name + "' where Email = '" + staticuser.Email + "' ";
                SqlCommand command1 = new SqlCommand(command, connection);
                command1.ExecuteNonQuery();
                MessageBox.Show("Information Chaned");
            }
            else { if (fname.Text == "") ; else { Name_error.Visibility = Visibility.Visible; return; } }
            if (Regex.IsMatch(lname.Text, @"^[a-zA-Z]{3,32}$"))
            {
                if (lname.Text == staticuser.Last_Name)
                {
                    Same_error.Visibility = Visibility.Visible; return;
                }
                staticuser.Last_Name = lname.Text;
                string command = "Update Users Set Lastname = '" + staticuser.Last_Name + "' where Email = '" + staticuser.Email + "' ";
                SqlCommand command1 = new SqlCommand(command, connection);
                command1.ExecuteNonQuery();
                MessageBox.Show("Information Chaned");
            }
            else { if (lname.Text == "") ; else { Name_error.Visibility = Visibility.Visible; return; } }
            if (Regex.IsMatch(pnum.Text, @"^09[0-9]{9}$"))
            {
                if (pnum.Text == staticuser.Phone)
                {
                    Same_error.Visibility = Visibility.Visible; return;
                }
                staticuser.Phone = pnum.Text;
                string command = "Update Users Set Phone = '" + staticuser.Phone + "' where Email = '" + staticuser.Email + "' ";
                SqlCommand command1 = new SqlCommand(command, connection);
                command1.ExecuteNonQuery();
                MessageBox.Show("Information Chaned");
            }
            else { if (pnum.Text == "") ; else { Phone_error.Visibility = Visibility.Visible; return; } }
            if (Regex.IsMatch(email.Text, @"^.{1,32}@.{1,32}\..{1,32}$"))
            {
                if (email.Text == staticuser.Email)
                {
                    Same_error.Visibility = Visibility.Visible; return;
                }
                if (User.users.Select(x => x.Email).Contains(email.Text.Trim()))
                {
                    wrong_email_error.Visibility = Visibility.Visible;
                    return;
                }
                staticuser.Email = email.Text;
                string command = "Update Users Set Email = '" + staticuser.Email + "' where Email = '" + staticuser.Email + "' ";
                SqlCommand command1 = new SqlCommand(command, connection);
                command1.ExecuteNonQuery();
                MessageBox.Show("Information Chaned");
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
                        if (pass.Text == staticuser.Password)
                        {
                            if (Regex.IsMatch(new_pass.Text, @"^(?=.*[a-z])(?=.*[A-Z]).{8,40}$") || pass.Text == "")
                            {
                                staticuser.Password = new_pass.Text;
                                string command = "Update Users Set Password = '" + staticuser.Password + "' where Email = '" + staticuser.Email + "' ";
                                SqlCommand command1 = new SqlCommand(command, connection);
                                command1.ExecuteNonQuery();
                                MessageBox.Show("Information Chaned");
                                connection.Close();
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
                BookPage bookpage = new BookPage(Searched_Books[Choosen_Book], staticuser);
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
                BookPage bookpage = new BookPage(staticuser.Books[Choosen_Book], staticuser);
                bookpage.Show();
            }
        }

        private void fav_Butt_Click(object sender, RoutedEventArgs e)
        {
            No_Bookmarks.Visibility = Visibility.Collapsed;
            if (staticuser.Books.Count == 0)
            {
                No_Bookmarks.Visibility = Visibility.Visible;
            }
            Mybookmarks_ListBox.ItemsSource = staticuser.BookMarks;
            Prof_Tabs.SelectedIndex = 1;
            Prof_Tabs.Visibility = Visibility.Visible;
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
            for (int i = 0; i < staticuser.ShoppingCart.Count; i++)
            {
                Uprice += staticuser.ShoppingCart[i].Price * (1 - staticuser.ShoppingCart[i].discount_value / 100);
            }
            MyCart_ListBox.ItemsSource = staticuser.ShoppingCart;
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
            staticuser.ShoppingCart.Remove(Choosen_Book);
            string New_Cart = "";
            for (int i = 0; i < staticuser.ShoppingCart.Count; i++)
            {
                New_Cart += staticuser.ShoppingCart[i].id + ",";
            }
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string Command = "Update Users Set Shopping_Cart = '" + New_Cart + "'  where Email = '" + staticuser.Email + "'";
            SqlCommand cmd = new SqlCommand(Command, connection);
            cmd.ExecuteNonQuery();
            connection.Close();

        }

        private void Pay_Butt_Click(object sender, RoutedEventArgs e)
        {
            if (staticuser.Wallet >= Uprice)
            {
                SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                sqlConnection.Open();
                for (int i = 0; i < staticuser.ShoppingCart.Count; i++)
                {
                    Book.books.Where(x => x.id == staticuser.ShoppingCart[i].id).ToList().ForEach(x =>
                    {
                        staticuser.ShoppingCart.Remove(x);
                        staticuser.Books.Add(x);
                        x.Total_Sale++; staticuser.Wallet -= x.Price * (1 - x.discount_value); x.Total_Income += x.Price * (1 - x.discount_value);
                        string Command = "Update Books Set Total_Sale = '" + x.Total_Sale + "',Totalincome = '" + x.Total_Income + "' ";
                        SqlCommand sqlCommand = new SqlCommand(Command, sqlConnection);
                        sqlCommand.ExecuteNonQuery();
                    });
                }
                string New_Cart = "";
                string New_Books = "";
                for (int i = 0; i < staticuser.ShoppingCart.Count; i++) { New_Cart += staticuser.ShoppingCart[i].id + ","; }
                for (int i = 0; i < staticuser.Books.Count; i++) { New_Books += staticuser.Books[i].id + ","; }
                sqlConnection.Close();
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                connection.Open();
                string command = "Update Users Set Wallet = '" + staticuser.Wallet + "',Books = '" + New_Books + "',Shopping_Cart = '" + New_Cart + "' where Email = '" + staticuser.Email + "'";
                SqlCommand command1 = new SqlCommand(command, connection);
                command1.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("You bought it succesfully from your Wallet");
            }
            else
            {
                Payment payment = new Payment(Uprice);
                if (payment.Boolean)
                {
                    for (int i = 0; i < staticuser.ShoppingCart.Count; i++)
                    {
                        Book.books.Where(x => x.id == staticuser.ShoppingCart[i].id).ToList().ForEach(x => { x.Total_Sale++; x.Total_Income += x.Price * (1 - x.discount_value); });
                    }
                }
                payment.Boolean = false;
            }
        }

        private void Back3_Butt_Click(object sender, RoutedEventArgs e)
        {
            Prof_Tabs.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int price = 0;
            try { price = int.Parse(Charge_Box.Text); }
            catch { MessageBox.Show("Invalid amount ! "); return; }
            Payment payment = new Payment(price);
            payment.Show();
            if (payment.Boolean)
            {
                staticuser.Wallet += price;
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
                connection.Open();
                string command = "Update Users Set Wallet = '" + staticuser.Wallet + "' where Email = '" + staticuser.Email + "'";
                SqlCommand command1 = new SqlCommand(command, connection);
                command1.ExecuteNonQuery();
                connection.Close();

            }
        }

        private void Wallet_Butt_Click(object sender, RoutedEventArgs e)
        {
            Prof_Tabs.SelectedIndex = 3; Prof_Tabs.Visibility = Visibility.Visible;
        }

        private void Mybookmarks_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Choosen_Book = Mybookmarks_ListBox.SelectedIndex;
            if (Choosen_Book >= 0)
            {
                BookPage bookpage = new BookPage(staticuser.BookMarks[Choosen_Book], staticuser);
                bookpage.Remove_Bookmark_Butt.Visibility = Visibility.Visible;
                bookpage.Add_Bookmark_Butt.Visibility = Visibility.Collapsed;
                bookpage.Show();
            }
        }
        private void Vip_Click(object sender, RoutedEventArgs e)
        {
            if (user.type == Type.normal)
            {
                not_Vip_message.Visibility = Visibility.Visible;
                Vip.Visibility = Visibility.Visible;
                VipTime_Left.Visibility = Visibility.Collapsed;
            }
            else
            {
                VipTime_Left.Visibility = Visibility.Visible;
                not_Vip_message.Visibility = Visibility.Collapsed;
                Vip.Visibility = Visibility.Collapsed;
            }
            UserPage_Tab.SelectedIndex = 2;
        }
        private void Join_Vip_Click(object sender, RoutedEventArgs e)
        {
            if (user.type == Type.VIP)
            {
                MessageBox.Show("you still have charge");
                return;
            }
            if (user.Wallet >= 50)
            {
                user.Wallet -= 50;
                user.type = Type.VIP;
                user.Vip_Begining = DateTime.Now;
                Tabs.SelectedIndex = 0;
                MessageBox.Show("Wlcome");
            }
            else
            {
                Payment payment = new Payment(50);
                if (payment.Boolean)
                {
                    user.Wallet -= 50;
                    user.type = Type.VIP;
                    user.Vip_Begining = DateTime.Now;
                    Tabs.SelectedIndex = 0;
                    MessageBox.Show("Wlcome");
                }
            }
            Book.books.Where(book => book.type == Type.VIP).ToList().ForEach(book => staticuser.Books.Add(book));
            string NEWBOOKS = "";
            for (int i = 0; i < staticuser.Books.Count; i++)
            {
                NEWBOOKS += staticuser.Books[i].id+",";
            }
            int typeint = 0;
            if (user.type == Type.VIP)
            {
                typeint = 1;
            }
            else if (user.type == Type.normal) { typeint = 0; }
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
            sqlConnection.Open();
            string Comm = "Update Users Set Books = '"+NEWBOOKS+"', type = '"+typeint+"' where Email = '"+staticuser.Email+"'";
            SqlCommand sqlCommand = new SqlCommand(Comm, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserPage_Tab.SelectedIndex = 0;
        }
    }
}
