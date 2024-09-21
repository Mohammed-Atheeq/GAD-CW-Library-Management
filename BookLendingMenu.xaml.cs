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
    /// Interaction logic for BookLendingMenu.xaml
    /// </summary>
    public partial class BookLendingMenu : Window
    {
        public BookLendingMenu()
        {
            InitializeComponent();
            date_borrow.Text = DateTime.Now.ToString();
            date_return.Text = DateTime.Now.AddMonths(1).ToString();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-OS3TOHI;Initial Catalog=LibraryDatabase;Integrated Security=True");
        SqlCommand cmd;

        private void txt_bookid_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                lbl_member.Content = "";
                con.Open();
                cmd = new SqlCommand("SELECT COUNT(*) FROM Books WHERE BookID = '" + txt_bookid.Text + "' ", con);

                int bookCount = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("SELECT COUNT(*) FROM BooksLending WHERE BookID = '" + txt_bookid.Text + "' ", con);
                int bookCount2 = (int)cmd.ExecuteScalar();
                
                if ((bookCount == 1) && (bookCount2 != 1))
                {

                    cmd = new SqlCommand("Select BookName from Books where BookID = '" + txt_bookid.Text + "'", con);

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        txt_bookname.Text = dr.GetValue(0).ToString();
                    }
                    lbl_book.Content = "";
                }
                else
                {
                    lbl_book.Content = "Invalid Book ID";
                    txt_bookname.Clear();
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
                con.Open();

                cmd = new SqlCommand("SELECT COUNT(*) FROM MemberDetails WHERE RegistrationID = '" + txt_memberid.Text + "'", con);
                int memberCount = (int)cmd.ExecuteScalar();


                if (String.IsNullOrEmpty(txt_memberid.Text))
                {
                    lbl_member.Content = "Member ID cannot be blank";
                    txt_memberid.Focus();
                }
                else if (txt_memberid.Text.Any(char.IsLetter))
                {
                    lbl_member.Content = "Member ID Cannot be have characters";
                    txt_memberid.Focus();
                }
                else if (memberCount != 1)
                {
                    lbl_member.Content = "Invalid Member ID";
                    txt_memberid.Focus();
                }
                else
                {
                    cmd = new SqlCommand("Insert into BooksLending values ('" + txt_memberid.Text + "', '" + txt_bookid.Text + "', '" + txt_bookname.Text + "', '" + DateTime.Now + "', '" + date_return.SelectedDate.Value + "', (1))", con);

                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        MessageBox.Show("Saved Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cannot Save check Again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                con.Close();

                txt_bookid.Clear();
                txt_bookname.Clear();
                date_return.Text = DateTime.Now.AddMonths(1).ToString();
            }

            catch (SqlException)
            {
                MessageBox.Show("Database Error From Saving", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            txt_memberid.Clear();
            txt_bookid.Clear();
            txt_bookname.Clear();
            lbl_book.Content = "";
            date_return.Text = DateTime.Now.AddMonths(1).ToString();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            LibrarianMenu obj = new LibrarianMenu();
            obj.Show();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_back_Click_1(object sender, RoutedEventArgs e)
        {
            LibrarianMenu obj = new LibrarianMenu();
            obj.Show();
            this.Close();
        }

        private void txt_memberid_TextChanged(object sender, TextChangedEventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("SELECT COUNT(*) FROM MemberDetails WHERE RegistrationID = '" + txt_memberid.Text + "' ", con);
            int member = (int)cmd.ExecuteScalar();

            if ((member != 1))
            {
                lbl_member.Content = "Invalid Member ID";
                txt_memberid.Focus();
            }
            else
            {
                lbl_member.Content = "";
            }
            con.Close();
        }
    }
}
