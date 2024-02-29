using Microsoft.CodeAnalysis.Editing;
using Microsoft.Win32;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Interaction logic for ImportCar.xaml
    /// </summary>
    public partial class ImportCar : Window
    {
        private bool confirm = false;
        private Prn3dContext _context;
        private string filePath;
        public event EventHandler ChildFormClosed;
        public ImportCar(Prn3dContext context)
        {
            _context = context;
            InitializeComponent();
            Create_Save.IsEnabled = false;

        }
        private void OnChildFormClosed(EventArgs e)
        {
            ChildFormClosed?.Invoke(this, e);
        }
        protected override void OnClosed(EventArgs e)
        {
            // Raise the custom event when the form is closed
            OnChildFormClosed(e);
            base.OnClosed(e);
        }
        private void Create_Confirm_Click(object sender, RoutedEventArgs e)
        {
            confirm = true;
            Create_Save.IsEnabled = true;
        }

        private void Choose_3d_file(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                File3d.Text = filePath;
            }
        }


        private void Create_Save_Click(object sender, RoutedEventArgs e)
        {
            // missing import file 3D

            try
            {
                Position? pos = _context.Positions.FirstOrDefault(u => u.Available == true);
                if (pos == null)
                {
                    MessageBox.Show("No available position found!");
                    return;
                }
                pos.Available = false;
                _context.Positions.Update(pos);

                Car import = new Car()
                {
                    //CarId = int.Parse(CarID.Text.ToString()),
                    CreatedBy = Created_By.Text.ToString(),
                    CheckBy = Check_By.Text.ToString(),
                    PositionId = pos.PositionId,
                    BrandName= Brand_Name.Text.ToString(),
                    ModelName = Model_Name.Text.ToString(),
                    Scale = Scale.Text.ToString(),
                    Material =Material.Text.ToString(),
                    Color = Color.Text.ToString(),
                    CarAdvanceFeature = AdvanceFeature.Text.ToString(),
                    Packaging = Packaging.Text.ToString(),
                    AgeRange = AgeRange.Text.ToString(),
                    Price = decimal.Parse(Price.Text.ToString()),
                    CarStatus = Car_status.Text.ToString(),
                    MonthlyCheck = DateTime.Now , 
                    StatusCheck = StatusCheck.Text.ToString(),
                    File3D = File3d.Text.ToString(),
                    OtherInfo = OtherInfo.Text.ToString()
                };
                _context.Cars.Add(import);
                
                _context.SaveChanges();

                MessageBox.Show("Import new car successfull!");
                Close();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
