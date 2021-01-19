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
        //List<BO.LineInStation> lines;
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
            //linesDataGrid.DataContext=stations.
            //lines=bl.get

        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{

        //    System.Windows.Data.CollectionViewSource stationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stationViewSource")));
        //    // Load data by setting the CollectionViewSource.Source property:
        //    // stationViewSource.Source = [generic data source]
        //}

        private void LbStation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            grid1.Visibility = Visibility.Visible;
            linesDataGrid.Visibility = Visibility.Visible;
            BO.Station s = (sender as ListBox).SelectedItem as BO.Station;
            if (s == null)
                return;
            grid1.DataContext = s;
            linesDataGrid.DataContext = s.Lines;
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("?האם אתה בטוח שאתה רוצה למחוק את התחנה", "...רגע", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;
            try
            {
                int code = int.Parse(codeTextBlock.Text);
                if(codeTextBlock!=null)
                {
                    bl.DeleteStation(code);
                    //foreach (BO.Station item in stations)
                    //{
                    //    if (item.Code == code)
                    //        stations.Remove(item);
                    //}
                     RefreshListBoxStations();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "הפעולה נכשלה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            try
            {
                string address = addressTextBox.Text;
                int code = int.Parse(codeTextBlock.Text);
                bool disAccess = (disabledAccessCheckBox.IsChecked == true);
                string name = nameTextBox.Text;
                double longitude = double.Parse(longitudeTextBlock.Text);
                double latitude = double.Parse(latitudeTextBlock.Text);
                BO.Station stat = new BO.Station() { Address = address, Code = code, Latitude = latitude, Longitude = longitude, Name = name, DisabledAccess = disAccess };
                bl.UpdateStation(stat);
                RefreshListBoxStations();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "הפעולה נכשלה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            AddNewStation win = new AddNewStation(bl);
            win.ShowDialog();
            RefreshListBoxStations();
        }
    }
}
