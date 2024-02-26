using Microsoft.EntityFrameworkCore;
using NuGet.Frameworks;
using Repository.Models;
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
        int selectedID = 0;
        
        public MainWindow()
        {
            InitializeComponent();
            _context = new Prn3dContext();
            LoadGridView();
        }

        public void HandleChildWindowClosed()
        {
            DataTotal.ItemsSource = null;

            LoadGridView();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ImportCar Import = new ImportCar(_context);
            Import.Show();
        }

        private void DataTotal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LoadGridView()
        {
            DataTotal.ItemsSource = _context.Cars.ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

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
                _context.Cars.Remove(selected);
                MessageBox.Show("Delete ok!");
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
    }
}