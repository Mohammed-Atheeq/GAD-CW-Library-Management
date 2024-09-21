using System;
using System.Collections.Generic;
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
    /// Interaction logic for BookReturningMenu.xaml
    /// </summary>
    public partial class BookReturningMenu : Window
    {
        public BookReturningMenu()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-OS3TOHI;Initial Catalog=LibraryDatabase;Integrated Security=True");
        SqlCommand cmd;

        private void txt_bookid_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT COUNT(*) FROM BooksLending WHERE BookID = '" + txt_bookid.Text + "' ", con);

                int bookCount = (int)cmd.ExecuteScalar();

                if ((bookCount == 1))
                {
                    cmd = new SqlCommand("SELECT RegistorID, BookName, CASE WHEN DATEDIFF(DAY, ReturningDate, GETDATE()) > 0 THEN DATEDIFF(DAY, ReturningDate, GETDATE()) ELSE 0 END AS DaysPassed, CASE WHEN DATEDIFF(DAY, ReturningDate, GETDATE()) > 0 THEN DATEDIFF(DAY, ReturningDate, GETDATE()) * 10 ELSE 0 END AS Fines FROM BooksLending WHERE BookID = '" + txt_bookid.Text + "'", con);

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        txt_memberid.Text = dr.GetValue(0).ToString();
                        txt_bookname.Text = dr.GetValue(1).ToString();
                        txt_daypassed.Text = dr.GetValue(2).ToString();
                        txt_fine.Text = dr.GetValue(3).ToString();
                    }

                    lbl_vbookid.Content = "";
                }
                else
                {
                    lbl_vbookid.Content = "Invalid Book ID";
                }
                   
                con.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Book Name Database Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Book Name Error Found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txt_bookid.Text))
                {
                    lbl_vbookid.Content = "Name Cannot be blank";
                    txt_bookid.Focus();
                }
                else
                {
                    con.Open();

                    cmd = new SqlCommand("Update BooksLending set BookStatus = 0 where BookID = '" + txt_bookid.Text + "' ", con);

                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        MessageBox.Show("Saved Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cannot Save check Again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    con.Close();
                }
                
            }
            catch (SqlException)
            {
                MessageBox.Show("Database Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            LibrarianMenu obj = new LibrarianMenu();    
            obj.Show();
            this.Close();
        }

        private void btn_clr_Click(object sender, RoutedEventArgs e)
        {
            lbl_vbookid.Content = "";
            txt_bookid.Clear();
            txt_bookname.Clear();
            txt_bookname.Clear();
            txt_daypassed.Clear();
            txt_fine.Clear();
        }
    }
}
