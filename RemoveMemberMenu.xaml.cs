using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for RemoveMemberMenu.xaml
    /// </summary>
    public partial class RemoveMemberMenu : Window
    {
        public RemoveMemberMenu()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-OS3TOHI;Initial Catalog=LibraryDatabase;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            da = new SqlDataAdapter("select RegistrationID, MemberName,Telephone, UserPassword, BookID, BookName, BorrowedDate, ReturningDate,  CASE WHEN DATEDIFF(DAY, ReturningDate, GETDATE()) > 0 THEN DATEDIFF(DAY, ReturningDate, GETDATE()) * 10 ELSE 0 END AS Fines from BooksLending, MemberDetails WHERE MemberDetails.RegistrationID = BooksLending.RegistorID AND RegistorID = '"+txt_search.Text+"' ", con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            list_remove.ItemsSource = dt.AsDataView();

            con.Close();
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txt_search.Text))
                {
                    lbl_search.Content = "Search Box Cannot be blank";
                    txt_search.Focus();
                }
                else
                {
                    con.Open();
                    cmd = new SqlCommand("delete from BooksLending where RegistorID = '" + txt_search.Text + "' delete from MemberDetails where RegistrationID = '" + txt_search.Text + "' ", con);

                    int i = cmd.ExecuteNonQuery();

                    if (i == 1)
                    {
                        MessageBox.Show("Data Cannot Delete Successfully", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Data Delete Successfully", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    con.Close();
                }
                
            }
            catch(SqlException)
            {
                MessageBox.Show("Database Errors", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                if (String.IsNullOrEmpty(txt_search.Text))
                {
                    lbl_search.Content = "Search Box Cannot be blank";
                    txt_search.Focus();
                }
                else
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT COUNT(*) FROM MemberDetails WHERE RegistrationID = '" + txt_search.Text + "' ", con);
                    int bookCount = (int)cmd.ExecuteScalar();

                    if (bookCount == 1)
                    {
                        lbl_search.Content = "";
                    }
                    else
                    {
                        lbl_search.Content = "Invalid Member Id";
                    }
                    con.Close();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Database Errors", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            LibrarianMenu obj = new LibrarianMenu();
            obj.Show();
            this.Close();
        }
    }
}
