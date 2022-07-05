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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Collections;
using System.Data.SqlClient;

namespace project
{
    public enum Type {normal,vip }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
                NormalUser n = new NormalUser();
                n.Show();
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
            NormalSignin normsign = new NormalSignin();
            normsign.Show();
            this.Close();
        }
    }
    public  class Books:IEnumerable
    {
        public static List<Books> allbooks = new List<Books>();
        public static List<string> allbooksname = new List<string>();
        public string Name { get; set; }
        public string Author { get; set; }
        public int PublishedYear { get; set; }
        public float Price { get; set; }
        public string Summary { get; set; }
        public string Cover_Path { get; set; }
        public string Pdf_Path { get; set; }
        public int Offtime { get; set; }
        public float Discount_Value { get; set; }
       public Type type { get; set; }
        public int id { get; set; }
        public List<int> Rating { get; set; }
        public float Rate { get;  }
        public float Total_Income { get; }
        public int Total_Sale { get; }


        public Books(string Name, string Author, float Price, int PublishedYear, string Summary,string Cover_Path,string Pdf_Path, int Offtime, float Discount_Value)
        {
            this.Name = Name;
            this.Author = Author;
            this.PublishedYear = PublishedYear;
            this.Price = Price;
            this.Summary = Summary;
            this.Cover_Path = Cover_Path;
            this.Pdf_Path = Pdf_Path;
            this.Offtime = Offtime;
            this.Discount_Value = Discount_Value;
            type = Type.normal;
            if (Rating != null)
            {
                Rate = Rating.Sum(x => x / Rating.Count);
            }
        }
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

    static class ExtentionMetodes
    {
        public static void AddTotable(this Books book)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\programms\c#\project\DataSql\data.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string Command = "insert into Users(Name,Author,Published Year,Price,Summary,Cover,Pdf Path,Type,vipfee) values('" + book.Name.Trim() + "','" + book.Author.Trim() + "','" + book.PublishedYear + "','" + book.Price + " ','"+book.Summary.Trim()+ "','" + book.Cover_Path.Trim() + "','" + book.Pdf_Path.Trim() + "','"+"normal"+"','"+0+"'";
            SqlCommand cmd = new SqlCommand(Command, connection);
            cmd.BeginExecuteNonQuery();
            connection.Close();
        }
    }


}
