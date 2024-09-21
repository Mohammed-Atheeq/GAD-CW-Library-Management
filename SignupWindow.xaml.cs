using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for SignupWindow.xaml
    /// </summary>
    public partial class SignupWindow : Window
    {
        public SignupWindow()
        {
            InitializeComponent();
            GetRegistrationID();
            txt_password.Visibility = Visibility.Hidden;
        }

        private void btn_signup_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (String.IsNullOrEmpty(txt_name.Text))
                {
                    lbl_id.Visibility = Visibility.Hidden;
                    lbl_name1.Content = "Name Cannot be blank";
                    txt_name.Focus();
                }
                else if (String.IsNullOrEmpty(txt_mail.Text))
                {
                    lbl_name1.Visibility = Visibility.Hidden;
                    lbl_mail.Content = "Email cannot be blank";
                    txt_mail.Focus();
                }
                else if (String.IsNullOrEmpty(txt_telephone.Text))
                {
                    lbl_mail.Visibility = Visibility.Hidden;
                    lbl_telephone.Content = "Telephone Number cannot be blank";
                    txt_telephone.Focus();
                } 
                
                else if (String.IsNullOrEmpty(pwd_password.Password))
                {
                    lbl_telephone.Visibility = Visibility.Hidden;
                    lbl_password1.Content = "Password Cnnot be blank";
                    pwd_password.Focus();
                }
                else if (!Regex.IsMatch(pwd_password.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$"))
                {
                    lbl_telephone.Visibility = Visibility.Hidden;
                    lbl_password1.Content = "Password must contain Minimum length of 8 characters with at least one lowercase and uppercase letter, one digit, one special character";
                    pwd_password.Focus();
                }
                else if (pwd_password.Password != pwd_confirm.Password)
                {
                    lbl_password1.Visibility = Visibility.Hidden;
                    lbl_confirm.Content = "Password doesn't matchplease check again";
                    pwd_confirm.Focus();
                }
                else
                {
                    lbl_confirm.Visibility = Visibility.Hidden;

                    SqlConnection con = new SqlConnection("Data Source=DESKTOP-OS3TOHI;Initial Catalog=LibraryDatabase;Integrated Security=True");
                    SqlCommand cmd;
                    con.Open();
                    cmd = new SqlCommand("Insert into MemberDetails values ('" + txt_registrationid.Text + "', '" + txt_name.Text + "', '" + txt_mail.Text + "', '" + txt_telephone.Text + "', '" + pwd_confirm.Password.ToString() + "' )", con);
                    //cmd = new SqlCommand("insert into MemberDetails (RegistrationID, MemberName, Membermail, Telephone, UserPassword) values ('" + txt_registrationid.Text + "', '" + txt_name.Text + "', '" + txt_mail.Text + "', '" + txt_telephone.Text + "', '" + pwd_confirm.Password.ToString() + "' )", con);

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
        public void GetRegistrationID()
        {
            string regid;
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-OS3TOHI;Initial Catalog=LibraryDatabase;Integrated Security=True");
            SqlCommand cmd;
            cmd = new SqlCommand("Select RegistrationID from MemberDetails order by RegistrationID Desc", con);
            con.Open();

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

            txt_registrationid.Text = regid.ToString();

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
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            
            LoginWindow obj = new LoginWindow();
            obj.Show();
            this.Close();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btn_close_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txt_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_name.Text.Any(char.IsDigit))
            {
                lbl_name1.Content = "Name Cannot have Numbers";
                txt_name.Focus();
            }
            else
            {
                lbl_name1.Content = "";
            }
        }

        private void txt_mail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(txt_mail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                lbl_mail.Content = "Enter a valid email. Ex: name@gmail.com";
                txt_mail.Focus();
            }
            else
            {
                lbl_mail.Content = "";
            }
        }

        private void txt_telephone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_telephone.Text.Any(char.IsLetter))
            {
                lbl_telephone.Content = "Telephone Number cannot have characters";
                txt_telephone.Focus();
            }
            else if (!Regex.IsMatch(txt_telephone.Text, @"^(?:7|0|(?:\+94))[0-9]{9,10}$"))
            {
                lbl_telephone.Content = "Enter valid Number";
                txt_telephone.Focus();
            }
            else
            {
                lbl_telephone.Content = "";
            }
        }
    }
}

