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
using System.Windows.Shapes;

using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Data.SqlClient;
using System.Data;

namespace Library_Management
{
    /// <summary>
    /// Interaction logic for BookPending.xaml
    /// </summary>
    public partial class BookPending : Window
    {
        public BookPending()
        {
            InitializeComponent();
            GetPending();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-OS3TOHI;Initial Catalog=LibraryDatabase;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        DataSet ds;
        public void GetPending()
        {

            cmd = new SqlCommand("Select  RegistorID ,BookID, BookName, ReturningDate, CASE WHEN DATEDIFF(DAY, ReturningDate, GETDATE()) > 0 THEN DATEDIFF(DAY, ReturningDate, GETDATE()) ELSE 0 END AS DaysPassed, CASE WHEN DATEDIFF(DAY, ReturningDate, GETDATE()) > 0 THEN DATEDIFF(DAY, ReturningDate, GETDATE()) * 10 ELSE 0 END AS Fines, Membermail from BooksLending, MemberDetails WHERE MemberDetails.RegistrationID = BooksLending.RegistorID AND DATEDIFF(DAY, ReturningDate, GETDATE()) > 0", con);

            con.Open();

            dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            list_data.ItemsSource = dt.AsDataView();

            con.Close();
        }

        private void btn_send_Click(object sender, RoutedEventArgs e)
        {
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();

            da.Fill(ds, "BooksLending");

            dt = ds.Tables["BooksLending"];

            string fromMail = "Your Library Mail";
            string fromPassword = "Password";

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    string emailAddress = dt.Rows[i].ItemArray[6].ToString();
                    string fines = dt.Rows[i].ItemArray[5].ToString();
                    string bookTitle = dt.Rows[i].ItemArray[2].ToString();

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(fromMail);
                    message.Subject = "Book Return";
                    message.To.Add(new MailAddress(emailAddress));
                    message.Body = "<html><body>This is to Inform You that You have to return '" + bookTitle + "'the Book with a fine of Rs. '"+fines+"'</body></html>";

                    message.IsBodyHtml = true;

                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(fromMail, fromPassword),
                        EnableSsl = true,
                    };
                    smtpClient.Send(message);

                }
                MessageBox.Show("Mail Sent Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                MessageBox.Show("No internet connection is available.", "Internet Status", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            
            LibrarianMenu obj = new LibrarianMenu();
            obj.Show();
            this.Close();
        }


    }
}
