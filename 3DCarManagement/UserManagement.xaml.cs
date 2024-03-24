using Repository.Models;
using Repository.Util;
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
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Window
    {
        private Prn3dContext _context;
        public UserManagement( Prn3dContext context)
        {
            _context = context;
            InitializeComponent();
            LoadGrid();
        }

        private void LoadGrid()
        {
            List<User> users = _context.Users.ToList();
            foreach (User user in users)
            {
                user.UserPass  = "***";
            }
            UserDataGrid.ItemsSource = users;
        }

        private void AddNewUserBTN_Click(object sender, RoutedEventArgs e)
        {
            User add = new User()
            {
                UserName = TxtUserName.Text,
                UserPass = TxtPassword.Text,
                Roles = SD.Check_role(TxtRole.Text),
            };
            _context.Users.Add(add);
            _context.SaveChanges();
            MessageBox.Show("Create " + add.Roles +" user successfully!");
            LoadGrid();
        }

        private void DeleteUserBTN_Click(object sender, RoutedEventArgs e)
        {
            User? selected = UserDataGrid.SelectedItem as User;
            if (selected != null)
            {
                _context.Users.Remove(selected);
                _context.SaveChanges();
                MessageBox.Show("Remove user successfully!");
            }
            else
            {
                MessageBox.Show(" Please choose a car!");
            }
            LoadGrid();
        }

        private void UpdateUserBTN_Click(object sender, RoutedEventArgs e)
        {
            User? selected = UserDataGrid.SelectedItem as User;
            selected.Roles = SD.Check_role(selected.Roles);
            if (selected != null)
            {
                _context.Users.Remove(selected);
                _context.SaveChanges();
                MessageBox.Show("Remove user successfully!");
            }
            else
            {
                MessageBox.Show(" Please choose a car!");
            }
            LoadGrid();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }



    }
}
