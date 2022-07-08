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
using System.Collections.ObjectModel;

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
    public  class Book:IEnumerable
    {
        public static List<Book> books = new List<Book>();
        public static List<string> allbooksname = new List<string>();
        public string Name { get; set; }
        public string Author { get; set; }
        public int Published_Year { get; set; }
        public float Price { get; set; }
        public string Summary { get; set; }
        public string Cover_Path { get; set; }
        public string Pdf_Path { get; set; }
        public int Discount_Time { get; set; }
        public float Discount_Value { get; set; }
        public string audio_Path { get; set; }
        public Type type { get; set; }
        public int id { get; set; }
        public ObservableCollection<int> Rating = new ObservableCollection<int>();

        public float Rate { get;  }
        public float Total_Income { get; }
        public int Total_Sale { get; }


        public Book(string Name, string Author, float Price, int PublishedYear, string Summary,string Cover_Path,string Pdf_Path, string audio_Path)
        {
            this.Name = Name;
            this.Author = Author;
            this.Published_Year = PublishedYear;
            this.Price = Price;
            this.Summary = Summary;
            this.Cover_Path = Cover_Path;
            this.Pdf_Path = Pdf_Path;
            this.audio_Path = audio_Path;
            this.Discount_Value = Discount_Value;
            type = Type.normal;
            
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
        public static void AddTotable(this Book book)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=I:\data.mdf;Integrated Security=True;Connect Timeout=30");
            connection.Open();
            string Command = "insert into Books(Name,Author,Published Year,Price,Summary,Cover Path,Pdf Path,Type) values('" + book.Name.Trim() + "','" + book.Author.Trim() + "','" + book.Published_Year + "','" + book.Price + " ','"+book.Summary.Trim()+ "','" + book.Cover_Path.Trim() + "','" + book.Pdf_Path.Trim() + "','"+"normal"+"'";
            SqlCommand cmd = new SqlCommand(Command, connection);
            cmd.BeginExecuteNonQuery();
            connection.Close();
        }
    }


}
