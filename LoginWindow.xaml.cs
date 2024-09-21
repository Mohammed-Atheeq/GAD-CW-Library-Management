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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static LoginWindow Instance;
        public LoginWindow()
        {
            InitializeComponent();
            Instance = this;
            txt_password.Visibility = Visibility.Hidden;
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-OS3TOHI;Initial Catalog=LibraryDatabase;Integrated Security=True");
        SqlCommand cmd;

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((String.IsNullOrEmpty(txt_username.Text)) || (String.IsNullOrEmpty(pwd_password.Password)))
                {
                    lbl_password.Content = "UserName or Password Cannot be blank";
                    txt_username.Focus();
                }
                else
                {
                    con.Open();

                    cmd = new SqlCommand("Select * from StaffAccount WHERE UserName = '" + txt_username.Text + "' AND UserPassword = '" + pwd_password.Password.ToString() + "' ", con);
                    //cmd = new SqlCommand("Select * from StaffAccount WHERE UserName = '" + txt_username.Text + "' AND UserPassword IN ('" + pwd_password.Password.ToString() + "' , '"+txt_password.Text+"') ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd = new SqlCommand("Select * from MemberDetails WHERE RegistrationID = '" + txt_username.Text + "' AND UserPassword = '" + pwd_password.Password.ToString() + "' ", con);
                    //cmd = new SqlCommand("Select * from MemberDetails WHERE RegistrationID = '" + txt_username.Text + "' AND UserPassword IN ('" + pwd_password.Password.ToString() + "' , '" + txt_password.Text + "') ", con);

                    SqlDataAdapter da1 = new SqlDataAdapter(cmd);


                    DataTable dtable = new DataTable();
                    DataTable dtable1 = new DataTable();

                    da.Fill(dtable);
                    da1.Fill(dtable1);


                    if (dtable.Rows.Count > 0)
                    {
                        LibrarianMenu obj = new LibrarianMenu();
                        this.Close();
                        obj.Show();
                    }
                    else if (dtable1.Rows.Count > 0)
                    {
                        MemberMenu obj = new MemberMenu();
                        this.Close();
                        obj.Show();

                        cmd = new SqlCommand("Select MemberName from MemberDetails Where RegistrationID = '" + txt_username.Text + "' ", con);
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            MemberMenu.Instance.lbl1.Content = "Welcome " + dr.GetValue(0).ToString();
                        }

                    }
                    else
                    {
                        lbl_password.Content = "Invalid User Name or Password";
                        txt_username.Focus();
                        pwd_password.Focus();
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

        private void btn_signup_Click(object sender, RoutedEventArgs e)
        {
            SignupWindow obj = new SignupWindow();
            obj.Show();
            this.Close();
        }

        private void chck_show_Checked(object sender, RoutedEventArgs e)
        {
            pwd_password.Visibility = Visibility.Hidden;
            txt_password.Text = pwd_password.Password;
            txt_password.Visibility = Visibility.Visible;
        }
        private void chck_show_Unchecked(object sender, RoutedEventArgs e)
        {
            txt_password.Visibility = Visibility.Hidden;
            pwd_password.Password = txt_password.Text;
            pwd_password.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
