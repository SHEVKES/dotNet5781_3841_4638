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
    /// Interaction logic for UpdateLine.xaml
    /// </summary>
    public partial class UpdateLine : Window
    {
        IBL bl;
        BO.Line line;
        public UpdateLine(IBL _bl,BO.Line _line)
        {
            InitializeComponent();
            bl = _bl;
            line = _line;
            grid1.DataContext = bl.GetLine(line.LineId);
            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Area));
            areaComboBox.Text = line.Area.ToString();
            LbStationsInLine.DataContext = line.stations;

        }
        public void RefreshListLines()
        {
            line = bl.GetLine(line.LineId);
            LbStationsInLine.DataContext = line.stations;
        }
        //IBL bl;
        //BO.Line line;
        //public LineDetails(IBL _bl, BO.Line _line)
        //{
        //    InitializeComponent();
        //    bl = _bl;
        //    line = _line;
        //    grid1.DataContext = bl.GetLine(line.LineId);
        //    areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Area));
        //    LBStations.DataContext = line.Stations;
        //    areaComboBox.Text = line.Area.ToString();
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource lineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("lineViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // lineViewSource.Source = [generic data source]
        }
        private void UpdateTimeDis_Button_Click(object sender, RoutedEventArgs e)
        {
            BO.StationInLine select = (sender as Button).DataContext as BO.StationInLine;
            if(select.StationCode==line.stations[line.stations.Count-1].StationCode)
            {
                MessageBox.Show("אין אפשרות לעדכן זמן או מרחק של תחנה אחרונה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            BO.StationInLine next = line.stations[select.LineStationIndex];
            UpdateTD win = new UpdateTD(bl, select, next);
            win.Show();
        }
        private void Delete_Station_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.StationInLine select = (sender as Button).DataContext as BO.StationInLine;
                bl.DeleteLineStation(line.LineId, select.StationCode);
                RefreshListLines();
            }
            catch (BO.BadLineIdException ex)
            {
                MessageBox.Show(ex.Message + ": " + ex.ID, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BadAdjacentStationsException ex)
            {
                MessageBox.Show(ex.Message + ": " + ex.stationCode1 + " " + ex.stationCode2, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BadLineStationException ex)
            {
                MessageBox.Show(ex.Message + ": " + ex.lineId + " " + ex.stationCode, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BadInputException ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            AddNewStationToLine win = new AddNewStationToLine(bl, line);
            win.Show();
        }
    }
}
