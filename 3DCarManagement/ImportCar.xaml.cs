﻿using Microsoft.CodeAnalysis.Editing;
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
        public ImportCar(Prn3dContext context)
        {
            _context = context;
            InitializeComponent();
            Create_Save.IsEnabled = false;

            Closed += CarDetail_Closed;
        }
        private void CarDetail_Closed(object sender, EventArgs e)
        {
            // Notify the parent window that the child window is closed
            MessageBox.Show("Create success! Close form");
            // You can use an event or any other mechanism for this purpose
            (Owner as MainWindow)?.HandleChildWindowClosed();
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
                Car import = new Car()
                {
                    //CarId = int.Parse(CarID.Text.ToString()),
                    CreatedBy = Created_By.Text.ToString(),
                    CheckBy = Check_By.Text.ToString(),
                    PositionId = int.Parse(PositionID.Text.ToString()),
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
                
                Close();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
