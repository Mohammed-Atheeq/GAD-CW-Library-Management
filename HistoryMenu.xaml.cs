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
    /// Interaction logic for HistoryMenu.xaml
    /// </summary>
    public partial class HistoryMenu : Window
    {
        public HistoryMenu()
        {
            InitializeComponent();
            GetHistory();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-OS3TOHI;Initial Catalog=LibraryDatabase;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        public void GetHistory()
        {

            cmd = new SqlCommand("Select BookName, BorrowedDate, ReturningDate, CASE WHEN DATEDIFF(DAY, ReturningDate, GETDATE()) > 0 THEN DATEDIFF(DAY, ReturningDate, GETDATE()) ELSE 0 END AS DaysPassed, CASE WHEN DATEDIFF(DAY, ReturningDate, GETDATE()) > 0 THEN DATEDIFF(DAY, ReturningDate, GETDATE()) * 10 ELSE 0 END AS Fines from BooksLending where RegistorId = '" + LoginWindow.Instance.txt_username.Text + "' ", con);

            con.Open();

            dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            list_history.ItemsSource = dt.AsDataView();

            con.Close();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MemberMenu obj = new MemberMenu();
            obj.Show();
        }
    }
}
