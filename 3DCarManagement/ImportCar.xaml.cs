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
        public ImportCar(Prn3dContext context)
        {
            _context = context;
            InitializeComponent();
            Create_Save.IsEnabled = false;
        }

        private void Create_Confirm_Click(object sender, RoutedEventArgs e)
        {
            confirm = true;
            Create_Save.IsEnabled = true;
        }

        private void Create_Save_Click(object sender, RoutedEventArgs e)
        {
            // missing import file 3D

            try
            {
                Car import = new Car()
                {
                    CarId = int.Parse(CarID.ToString()),
                    CreatedBy = Created_By.ToString(),
                    CheckBy = Check_By.ToString(),
                    PositionId = int.Parse(PositionID.ToString()),
                    BrandName= Brand_Name.ToString(),
                    ModelName = Model_Name.ToString(),
                    Scale = Scale.ToString(),
                    Material =Material.ToString(),
                    Color = Color.ToString(),
                    CarAdvanceFeature = AdvanceFeature.ToString(),
                    Packaging = Packaging.ToString(),
                    AgeRange = AgeRange.ToString(),
                    Price = decimal.Parse(Price.ToString()),
                    CarStatus = Car_status.ToString(),
                    MonthlyCheck = DateTime.Parse(MonthlyCheck.ToString()) , 
                    StatusCheck = StatusCheck.ToString(),
                    File3D = File3D.ToString(),
                    OtherInfo = OtherInfo.ToString()
                };
                _context.Cars.Add(import);
                _context.SaveChanges();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
