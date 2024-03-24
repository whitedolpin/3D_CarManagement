using Microsoft.Win32;
using Repository.Models;
using System.ComponentModel;
using System.Security.Policy;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace _3DCarManagement
{
    /// <summary>
    /// Interaction logic for CarDetail.xaml
    /// </summary>
    public partial class CarDetail : Window
    {
        private string filePath;
        public Model3DGroup ModelInstance { get; set; } = new Model3DGroup(); 
        private double _rotationAngle;
        private Car CarData;
        private double vertical_rotate;
        private double horizontal_rotate;
        private double camera_zoom;
        public CarDetail( Car input)
        {
            CarData = input;
            InitializeComponent();
            Set3dObject();
         //   MoveObjectToLeft();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Set3dObject()
        {
            try
            {
                if (string.IsNullOrEmpty(CarData.File3D))
                {
                    Close();
                    MessageBox.Show("File 3d is empty! Please update it!");
                    return;
                }

                var reader = new HelixToolkit.Wpf.ObjReader();
                var model3D = reader.Read(CarData.File3D);
                var modelVisual3D = new ModelVisual3D { Content = model3D };

                if (model3D == null)
                {
                    Close();
                    MessageBox.Show("File not exist! Please check again!");
                    return;
                }

                GeometryModel3D geometryModel = new GeometryModel3D();
                ScaleTransform3D scaleTransform = new ScaleTransform3D(0.2, 0.2, 0.2);
                modelVisual3D.Transform = new Transform3DGroup
                {
                    Children = new Transform3DCollection
                    {
                        scaleTransform
                    }
                };

                RotateTransform3D rotateTransform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 45));

                modelVisual3D.Transform = new Transform3DGroup
                {
                    Children = new Transform3DCollection
                        {
                            rotateTransform
                        }
                };

                ModelInstance.Children.Add(modelVisual3D.Content as Model3D);

                DirectionalLight light1 = new DirectionalLight(Colors.White, new Vector3D(-1, -1, -1));
                DirectionalLight light2 = new DirectionalLight(Colors.White, new Vector3D(1, -1, -1));

                // Add lights to the model group
                ModelInstance.Children.Add(light1);
                ModelInstance.Children.Add(light2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void UpdateModel()
        {
            Set3dObject();

            OnPropertyChanged(nameof(ModelInstance));

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            double angle = 100;
            RotateTransform3D rotateTransform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), angle));
            ModelInstance.Transform = rotateTransform;
        }


        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            vertical_rotate = Rotate_Slider.Value;
            set_Horizontal();
            set_vertical();
        }

        private void Horizontal_Rotate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            horizontal_rotate = Horizontal_Rotate.Value;
            set_vertical();
            set_Horizontal();
        }


        private void MoveObjectToLeft()
        {
            // Create a DoubleAnimation for moving the object along the X-axis
            DoubleAnimation animation = new DoubleAnimation
            {
                From = 0,
                To = 20,
                Duration = TimeSpan.FromSeconds(5),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(2)
            };

            // Apply the animation to the TranslateTransform3D
            TranslateTransform3D translateTransform = new TranslateTransform3D();
            ModelInstance.Transform = translateTransform;
            translateTransform.BeginAnimation(TranslateTransform3D.OffsetXProperty, animation);
        }

        private void Zoom_Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            camera_zoom = Zoom_Slider.Value;
            set_zoom();
        }


        private void set_Horizontal()
        {
            RotateTransform3D rotateTransform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), horizontal_rotate));
            ModelInstance.Transform = rotateTransform;
        }
        private void set_vertical()
        {
            RotateTransform3D rotateTransform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), vertical_rotate));
            ModelInstance.Transform = rotateTransform;
        }
        private void set_zoom()
        {
            camMain.Position = new Point3D(0, 0, camera_zoom);
        }
    }


}
