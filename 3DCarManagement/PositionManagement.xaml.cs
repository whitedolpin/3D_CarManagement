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
    /// Interaction logic for PositionManagement.xaml
    /// </summary>
    public partial class PositionManagement : Window
    {
        public int _total { get; private set; }
        public int _available { get; private set; }
        private Prn3dContext _context;
        public PositionManagement( Prn3dContext context)
        {
            _context = context;
            Set_Available();
            InitializeComponent();
            LoadGrid();
        }
        private void Set_Available()
        {
            _total = _context.Positions.Count();
            _available = _context.Positions.Count( u => u.Available == true);
        }

        private void LoadGrid()
        {
            DataGrid dataGrid = new DataGrid();

            // Add the DataGrid to your container (e.g., a StackPanel)
            List<Position> ListSource = _context.Positions.OrderBy(u => u.PositionId).ToList();
            int? shelf = 0;
            int? level = 0;
            
            foreach (Position position in ListSource)
            {
                if(position.ShelfNumber != shelf)
                {
                    shelf = position.ShelfNumber;
                    level = 0;
                }
                else
                {
                    position.ShelfNumber = null;
                }

                if (position.Levels != level)
                {
                    level = position.Levels;
                }
                else
                {
                    position.Levels = null;
                }
            }


            dataGrid.ItemsSource = ListSource;
            Grid_Total.Children.Add(dataGrid);

            double avai = _context.Positions.Count( u => u.Available == false);
            double total = _context.Positions.Count();
            if(total != 0)
            {
                PositionAvailable(avai / total);
            }
            else
            {
                PositionAvailable(0);
            }
            
        }

        private void PositionAvailable(double percent)
        {
            percent = 1 - percent;

            TxtAvailable.Content = "Position available: " + percent*100 +" %";
            gradientStop.Offset = percent;
            gradientStop2.Offset = percent;


        }


        private void RemoveSheftBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int? sheft = int.Parse(TxtSheftIdToRemove.Text);
                if (sheft.HasValue)
                {
                    IEnumerable<Position> removeRange = _context.Positions.
                                Where(u => u.ShelfNumber == sheft).ToList();

                    _context.Positions.RemoveRange(removeRange);
                    _context.SaveChanges();
                    MessageBox.Show("Remove permanently successfully!");
                    LoadGrid();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void addPositionBTN_Click(object sender, RoutedEventArgs e)
        {
            int? levels = int.Parse(txtNumberOfLevels.Text);
            int? positions = int.Parse(TxtNumberOfPosition.Text);
            if (!levels.HasValue)
            {
                MessageBox.Show("Please enter number of level");
                return;
            }else if (!positions.HasValue)
            {
                MessageBox.Show("Please enter number of position");
                return;
            }

            int? MaxSheftID = _context.Positions.Max( u => u.ShelfNumber);
            if (!MaxSheftID.HasValue)
            {
                MaxSheftID = 0;
            }

            for (int i = 1; i <= levels.Value; i++)
            {
                for (int j = 1; j <= positions.Value; j++)
                {
                    Position add = new Position()
                    {
                        ShelfNumber = MaxSheftID +1,
                        Levels = i,
                        Position1 = j,
                        Available = true,
                    };

                    _context.Positions.Add(add);
                }
            }
            _context.SaveChanges();
            MessageBox.Show("Create new sheft successfully! ");
            LoadGrid();
            
        }
    }
}
