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

namespace Library_Management
{
    /// <summary>
    /// Interaction logic for MemberMenu.xaml
    /// </summary>
    public partial class MemberMenu : Window
    {
        public static MemberMenu Instance;
        public Label lbl1;
        public MemberMenu()
        {
            InitializeComponent();
            Instance = this;
            lbl1 = lbl_userid;
        }

        private void btn_borrowedbook_Click(object sender, RoutedEventArgs e)
        {
            BorrowedBooksMenu obj = new BorrowedBooksMenu();
            obj.Show();
            this.Close();
        }

        private void btn_history_Click(object sender, RoutedEventArgs e)
        {
            HistoryMenu obj = new HistoryMenu();
            obj.Show();
            this.Close();
        }

        private void btn_bookbuy_Click(object sender, RoutedEventArgs e)
        {
            BuyBookMenu obj = new BuyBookMenu();
            obj.Show();
            this.Close();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            LoginWindow obj = new LoginWindow();
            obj.Show();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
