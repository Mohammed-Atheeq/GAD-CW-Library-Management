using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace Library_Management
{
    /// <summary>
    /// Interaction logic for BorrowedBooksMenu.xaml
    /// </summary>
    public partial class BorrowedBooksMenu : Window
    {
        public BorrowedBooksMenu()
        {
            InitializeComponent();
            GetBorrowed();
        }

        public void GetBorrowed()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-OS3TOHI;Initial Catalog=LibraryDatabase;Integrated Security=True");
            SqlDataAdapter da;
            con.Open();

            da = new SqlDataAdapter("Select BookName, ReturningDate from BooksLending where RegistorId = '" + LoginWindow.Instance.txt_username.Text + "' ", con);


            DataTable dt = new DataTable();
            da.Fill(dt);

            list_borrowed.ItemsSource = dt.AsDataView();

            con.Close();
        }
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            
            MemberMenu obj = new MemberMenu();
            obj.Show();
            this.Close();
        }
    }
}
