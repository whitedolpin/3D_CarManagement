using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using NuGet.Frameworks;
using Repository.Models;
using Repository.Util;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _3DCarManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Prn3dContext _context;
        private string _role;
        public event EventHandler Mainclosed;

        int selectedID = 0;
        
        public MainWindow(string role)
        {
            _role = role;
            InitializeComponent();
            _context = new Prn3dContext();
            LoadGridView();
        }
        private void OnMainclosed(EventArgs e)
        {
            Mainclosed?.Invoke(this, e);
        }
        protected override void OnClosed(EventArgs e)
        {
            // Raise the custom event when the form is closed
            OnMainclosed(e);
            base.OnClosed(e);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ImportCar Import = new ImportCar(_context);
            Import.ChildFormClosed += ChildForm_Closed;
            Import.Show();
        }

        private void ChildForm_Closed(object sender, EventArgs e)
        {
            DataTotal.ItemsSource = null;
            LoadGridView();
        }
        private void DataTotal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LoadGridView()
        {
            DataTotal.ItemsSource = _context.Cars.ToList();
            if(_role != SD.ROLE_ADMIN)
            {
                UpdateCarColumn.Visibility = Visibility.Hidden;
                DeleteCarColumn.Visibility = Visibility.Hidden;
                UpdateFileColumn.Visibility = Visibility.Hidden;

                ImportBTN.Visibility = Visibility.Hidden;
                UserManangeBTN.Visibility = Visibility.Hidden;

            }
        }
        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PositionManagement management = new PositionManagement(_context);
            management.Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Car? selected = DataTotal.SelectedItem as Car;
            if (selected != null)
            {
                _context.Cars.Update(selected);
                MessageBox.Show("Update ok!");
                _context.SaveChanges();
            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Car? selected = DataTotal.SelectedItem as Car;
            if (selected != null)
            {
                Position? pos = _context.Positions.FirstOrDefault(x => x.PositionId == selected.PositionId);
                if(pos != null)
                {
                    pos.Available = true;
                    _context.Positions.Update(pos);
                }
                _context.Cars.Remove(selected);
                MessageBox.Show("Delete ok!");
                _context.SaveChanges();
            }
            DataTotal.ItemsSource = null;
            LoadGridView();
        }

        private string Choose_3d_file(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return "";
        }


        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            Car? selected = DataTotal.SelectedItem as Car;
            if (selected != null)
            {
                string path = Choose_3d_file(sender, e);
                if (path != "")
                {
                    selected.File3D = path;
                    _context.Cars.Update(selected);
                }
                else
                {
                    return;
                }
                MessageBox.Show("Update ok!");
                _context.SaveChanges();
            }
            DataTotal.ItemsSource = null;
            LoadGridView();
        }

        private void CarDetailBTN_Click(object sender, RoutedEventArgs e)
        {
            Car? selected = DataTotal.SelectedItem as Car;
            if (selected != null)
            {
                CarDetail car = new CarDetail(selected);
                car.Show();
            }
            else
            {
                MessageBox.Show(" Please choose a car!");
            }
        }

        private void UserManangeBTN_Click(object sender, RoutedEventArgs e)
        {
            UserManagement user = new UserManagement(_context);
            user.Show();    
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
            Close();
        }
    }
}