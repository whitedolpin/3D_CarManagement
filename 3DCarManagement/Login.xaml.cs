using Repository.Models;
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

namespace _3DCarManagement
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private Prn3dContext _context;
        public Login()
        {
            InitializeComponent();
            _context = new Prn3dContext();
        }

        private void LoginBTN_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtUsername.Text))
            {
                MessageBox.Show("Please enter username!");
            }
            if (string.IsNullOrEmpty(TxtPassword.Password))
            {
                MessageBox.Show("Please enter pass!");
            }

            User? login = _context.Users.FirstOrDefault( u => u.UserName == TxtUsername.Text);
            if (login != null)
            {
                if(login.UserPass == TxtPassword.Password)
                {
                    this.Hide();
                    MainWindow main = new MainWindow(login.Roles);
                    main.Mainclosed += ChildForm_Closed;
                    main.Show();
                    
                }
                else
                {
                    MessageBox.Show("Invalid username or password!");
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password!");
            }
        }
        private void ChildForm_Closed(object sender, EventArgs e)
        {
            this.Show();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
