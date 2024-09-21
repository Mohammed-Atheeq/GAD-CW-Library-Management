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
    /// Interaction logic for LibrarianMenu.xaml
    /// </summary>
    public partial class LibrarianMenu : Window
    {
        public LibrarianMenu()
        {
            InitializeComponent();
        }

        private void btn_registor_Click(object sender, RoutedEventArgs e)
        {
            BookRegisterMenu obj = new BookRegisterMenu();
            obj.Show();
            this.Close();
        }

        private void btn_lending_Click(object sender, RoutedEventArgs e)
        {
            BookLendingMenu obj = new BookLendingMenu();
            obj.Show();
            this.Close();
        }

        private void btn_returning_Click(object sender, RoutedEventArgs e)
        {
            BookReturningMenu obj = new BookReturningMenu();
            obj.Show();
            this.Close();
        }

        private void btn_pending_Click(object sender, RoutedEventArgs e)
        {
            BookPending obj = new BookPending();
            obj.Show();
            this.Close();
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

        private void btn_deletemember_Click(object sender, RoutedEventArgs e)
        {
            RemoveMemberMenu obj = new RemoveMemberMenu();
            obj.Show();
            this.Close();
        }
    }
}
