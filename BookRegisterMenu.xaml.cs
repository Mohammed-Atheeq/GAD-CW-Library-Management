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
    /// Interaction logic for BookRegisterMenu.xaml
    /// </summary>
    public partial class BookRegisterMenu : Window
    {
        public BookRegisterMenu()
        {
            InitializeComponent();
            GetBookRegID();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-OS3TOHI;Initial Catalog=LibraryDatabase;Integrated Security=True");
        SqlCommand cmd;

        public void GetBookRegID()
        {
            string regid;

            con.Open();
            cmd = new SqlCommand("Select BookID from Books order by BookID Desc", con);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int id = int.Parse(dr[0].ToString()) + 1;
                regid = id.ToString("000");
            }
            else if (Convert.IsDBNull(dr))
            {
                regid = ("001");
            }
            else
            {
                regid = ("001");
            }

            con.Close();

            txt_bookid.Text = regid.ToString();

        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(String.IsNullOrEmpty(txt_bookname.Text))
                {
                    lbl_bname.Content = "Book Name Cannot be Blank";
                    txt_bookname.Focus();
                }
                else if(String.IsNullOrEmpty(txt_author.Text))
                {
                    lbl_bname.Content = "";
                    lbl_author2.Content = "Author Name Cannot be Blank";
                    txt_author.Focus();
                }
                
                else
                {
                    lbl_author2.Content = "";
                    con.Open();
                    cmd = new SqlCommand("Insert into Books values ('" + txt_bookid.Text + "', '" + txt_bookname.Text + "', '" + txt_author.Text + "')", con);

                    int i = cmd.ExecuteNonQuery();

                    if (i == 1)
                    {
                        MessageBox.Show("Data Saved Successfully", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Data Cannot Save Successfully", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    con.Close();
                    GetBookRegID();
                    txt_bookname.Clear();
                    txt_author.Clear();
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

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            LibrarianMenu obj = new LibrarianMenu();
            obj.Show();
            this.Close();
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            txt_bookname.Clear();
            txt_author.Clear();
        }

        private void txt_author_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_author.Text.Any(char.IsDigit))
            {
                lbl_author2.Content = "Author Name Cannot have Numbers";
                txt_author.Focus();
            }
            else
            {
                lbl_author2.Content = "";
            }
        }
    }
}
