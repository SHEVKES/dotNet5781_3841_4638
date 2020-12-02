//Ayala Cohen 212224638
//Shira Shevkes 212433841
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotNet5781_002_3841_4638;

namespace dotNet5781_03A0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BusLine currentDisplayBusLine;
        private BusLineCollection bus;
        public MainWindow()
        {
            InitializeComponent();
            List<BusStation> busStations = new List<BusStation>();
            List<BusLine> busLines = new List<BusLine>();
            bus = new BusLineCollection(busLines);
            BuildLines.createLists(ref busStations, bus);
            cbBusLines.ItemsSource = bus;
            cbBusLines.DisplayMemberPath = "BusNumber";
            cbBusLines.SelectedIndex = 0;
        }
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).BusNumber);
        }
        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = bus[index].First();
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.busStations;
        }

    }
}
