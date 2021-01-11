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
using BLAPI;

namespace PL
{
    /// <summary>
    /// Interaction logic for Stations.xaml
    /// </summary> 
    public partial class Stations : Window
    {
        IBL bl;
        List<BO.Station> stations;
        public Stations(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            RefreshListBoxStations();
        }
        public void RefreshListBoxStations()
        {
            stations = bl.GetAllStations().ToList();
            LbStation.DataContext = stations;
        }
    }
}
