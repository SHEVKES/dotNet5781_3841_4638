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
    /// Interaction logic for AddNewStationToLine.xaml
    /// </summary>
    public partial class AddNewStationToLine : Window
    {
        IBL bl;
        BO.Line line;
        public AddNewStationToLine(IBL _bl, BO.Line _line)
        {
            InitializeComponent();
            bl = _bl;
            line = _line;
            CbNew.DisplayMemberPath = "Name";
            CbNew.SelectedIndex = 0;
            CbPrev.SelectedIndex = 0;
            CbPrev.DataContext = line.stations.ToList();
            CbNew.DataContext = bl.GetAllStations();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Check.IsChecked == true) //this is the first station
            {
                BO.Station station = (CbNew.SelectedItem) as BO.Station;
                BO.LineStation lineStation = new BO.LineStation() { LineId = line.LineId, LineStationIndex = 1, StationCode = station.Code };
                try
                {
                    bl.AddLineStation(lineStation);
                }
                catch (BO.BadLineStationException ex)
                {
                    MessageBox.Show(ex.Message + ": " + ex.lineId + " " + ex.stationCode, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (BO.BadAdjacentStationsException ex)
                {
                    MessageBox.Show(ex.Message + ": " + ex.stationCode1 + " " + ex.stationCode2, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                }              
            }
            else
            {
                BO.StationInLine prev = (CbPrev.SelectedItem) as BO.StationInLine;
                if(prev==null)
                {
                    MessageBox.Show("לא נבחרה תחנה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                BO.Station station = (CbNew.SelectedItem) as BO.Station;
                BO.LineStation lineStation = new BO.LineStation() { LineId = line.LineId, LineStationIndex = prev.LineStationIndex + 1, StationCode = station.Code };
                try
                {
                    bl.AddLineStation(lineStation);
                }
                catch (BO.BadLineStationException ex)
                {
                    MessageBox.Show(ex.Message + ": " + ex.lineId + " " + ex.stationCode, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (BO.BadAdjacentStationsException ex)
                {
                    MessageBox.Show(ex.Message + ": " + ex.stationCode1 + " " + ex.stationCode2, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            Close();
             
        }
    }
}
