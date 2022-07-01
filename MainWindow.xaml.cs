using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            InitializeComponent();
            Payment payment = new Payment();
            UserLogin userLogin = new UserLogin();
            User user = new User("", "", "", "", "");
            UserPage userPage = new UserPage(user);
            userPage.Show();
            //userLogin.Show();
            //payment.Show();
        }
    }
    public enum Type
    {
        normal, VIP
    }
    public class User
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public double Wallet { get; set; }
        public string Password { get; set; }
        public Type type = Type.normal;
        public List<Book> ShoppingCart = new List<Book>();
        public List<Book> Books = new List<Book>();
        public User(string fname, string lname, string pnum, string email, string pass)
        {
            First_Name = fname;
            Last_Name = lname;
            Phone = pnum;
            Email = email;
            Password = pass;
        }
    }
    public class Card
    {
        public string Id { get; set; }
        public int CVV { get; set; }
        public string Password { get; set; }
        public DateOnly Expiration { get; set; }
        public Card(string id,int cvv,string pass,DateOnly date)
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
    public class Book : IEnumerable
    {
        public string name { get; set; }
        public string Writer { get; set; }
        public IEnumerator GetEnumerator()
        {
            foreach (var item in this)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
    class Management
    {

    }
    static class ExtentionMetodes
    {
        public static List<Book> search(this IEnumerable<Book> data,string _name)
        {
            List<Book> list = data.Where(x => x.name.Contains(_name)||x.Writer.Contains(_name)).Select(x => x).ToList();
            return list;
        }
        public static void AddTotable(this User user)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\DataSql\data.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string Command = "insert into Users values('"+user.Email.Trim()+"','"+user.First_Name.Trim()+"','"+user.Last_Name.Trim()+"','"+user.Phone.Trim()+",'"+0+"'')";
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
