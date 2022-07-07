using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

namespace project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Payment payment = new Payment(20);
            InitializeComponent();
            payment.Show();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            UserLogin userlog = new UserLogin();
            userlog.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Admin a = new Admin();
            a.Show();
            this.Close();
        }
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            UserSignup usersignup = new UserSignup();
            usersignup.Show();
            this.Close();
        }
    }
    public enum Type
    {
        normal, VIP
    }
    public class User
    {
        public static List<User> users = new List<User>(); 
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public float Wallet { get; set; }
        public string Password { get; set; }
        public Type type { get; set; }
        public ObservableCollection<Book> ShoppingCart = new ObservableCollection<Book>();
        public ObservableCollection<Book> BookMarks = new ObservableCollection<Book>();
        public ObservableCollection<Book> Books = new ObservableCollection<Book>();
        public User(string fname, string lname, string pnum, string email, string pass)
        {
            First_Name = fname;
            Last_Name = lname;
            Phone = pnum;
            Email = email;
            Password = pass;
            type = Type.normal;
        }
    }
    public class Card
    {
        public string Id { get; set; }
        public int CVV { get; set; }
        public string Password { get; set; }
        public DateOnly Expiration { get; set; }
        public Card(string id, int cvv, string pass, DateOnly date)
        {
            Id = id;
            CVV = cvv;
            Password = pass;
            Expiration = date;

        }
        public static int digitsum(int n)
        {
            int m = 0;
            while (n > 0)
            {
                m += n % 10;
                n /= 10;
            }
            return m;
        }
        public static bool checkid(string id)
        {
            string[] vs = id.Split(' ');
            string ID = "";
            for (int i = 0; i < vs.Length; i++)
            {
                ID += vs[i];
            }
            int sum = 0;
            for (int i = ID.Length - 1; i >= 0; i--)
            {
                int j = 0;
                try
                {
                    j = int.Parse(ID[i].ToString());
                }
                catch { return false; }
                if (i % 2 == 0)
                {
                    sum += digitsum(j * 2);
                }
                else
                    sum += j;
            }
            if (sum % 10 == 0)
            {
                return true;
            }
            else return false;
        }
    }
    public class Book
    {
        public static List<Book> books = new List<Book>();
        public int id { get;}
        public string Name { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }
        public int Published_Year { get; set; }
        public string Summary { get; set; }
        public string Cover_Path { get; set; }
        public string Pdf_Path { get; set; }
        public float discount_value { get; set; }
        public TimeOnly discount_Time { get; set; }
        public ObservableCollection<int> Ratings = new ObservableCollection<int>();
        public float Rate { get; set; }
        public int Total_Sale { get; set; }
        public float Total_Income { get; set; }
        public Type type { get; set; }
        public Book(string _name,string _author,float _price,int _published,string _summary,string cover,string pdf)
        {
            if (books.Count == 0)
            {
                id = 0;
            }
            else
            id = books[books.Count - 1].id + 1;
            Name = _name;
            Author = _author;
            Price = _price;
            Published_Year = _published;
            Summary = _summary;
            Cover_Path = cover;
            Pdf_Path = pdf;
            type = Type.normal;
            if (Ratings != null)
            {
                Rate = Ratings.Sum(x => x/Ratings.Count);
            }
        }
    }
    class Management
    {

    }
    static class ExtentionMetodes
    {
        //public static List<Book> search(this IEnumerable<Book> data,string _name)
        //{
        //    List<Book> list = data.Where(x => x.name.Contains(_name)||x.Writer.Contains(_name)).Select(x => x).ToList();
        //    return list;
        //}
        public static void AddTotable(this User user)
        {
            float wallet = 0;
            string Books = "";
            foreach (Book book in user.Books)
            {
                Books += book.id.ToString()+",";
            }
            string BookMarks = "";
            foreach (Book book in user.BookMarks)
            {
                BookMarks += book.id.ToString() + ",";
            }
            string Cart = "";
            foreach (Book book in user.ShoppingCart)
            {
                Cart += book.id.ToString() + ",";
            }
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\Database.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string Command = "insert into Users values('" + user.Email.Trim() + "','" + user.First_Name.Trim() + "','" + user.Last_Name.Trim() + "','" + user.Phone.Trim() + "','" + user.Password.Trim() + "','" + user.Wallet + "','"+Books+ "','"+BookMarks+"','"+0+"','"+Cart+"')";
            SqlCommand cmd = new SqlCommand(Command, connection);
            cmd.ExecuteNonQueryAsync();
            connection.Close();
        }
        public static void AddTotable(this Book book)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\DataSql\data.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string Command = "insert into Users(Name,Author,Published Year,Price,Summary,Cover,Pdf Path,Type,vipfee) values('" + book.Name.Trim() + "','" + book.Author.Trim() + "','" + book.Published_Year + "','" + book.Price + " ','" + book.Summary.Trim() + "','" + book.Cover_Path.Trim() + "','" + book.Pdf_Path.Trim() + "','" + "normal" + "','" + 0 + "'";
            SqlCommand cmd = new SqlCommand(Command, connection);
            cmd.BeginExecuteNonQuery();
            connection.Close();
        }
    }
    class Helper : IMultiValueConverter
    {

        public object Convert(object[] values, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)

        {

            if (values[0] is bool && values[1] is bool)

            {

                bool hasText = !(bool)values[0];

                bool hasFocus = (bool)values[1];



                if (hasFocus || hasText)

                    return Visibility.Collapsed;

            }

            return Visibility.Visible;

        }
        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
