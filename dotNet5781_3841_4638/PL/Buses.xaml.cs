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
    /// Interaction logic for Buses.xaml
    /// </summary>
    public partial class Buses : Window
    {
        IBL bl;
        List<BO.Bus> buses;
        public Buses(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;          
            //buses = bl.GetAllBuses().ToList();
            //LbBuses.DataContext = buses;
            RefreshListBoxBuses();
        }
        public void RefreshListBoxBuses()
        {
            buses = bl.GetAllBuses().ToList();
            LbBuses.DataContext = buses;
        }
        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{

        //    System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
        //    // Load data by setting the CollectionViewSource.Source property:
        //    // busViewSource.Source = [generic data source]
        //}

        private void LbBuses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            grid2.Visibility = Visibility.Visible;
            BO.Bus b = (sender as ListBox).SelectedItem as BO.Bus;
            if (b == null)
                return;
            grid2.DataContext = b;
            statusComboBox.ItemsSource = Enum.GetValues(typeof(BO.BusStatus));
            //statusComboBox.SelectedIndex = 0;
            statusComboBox.Text = b.Status.ToString();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddNewBus win = new AddNewBus(bl);
            win.ShowDialog();
            RefreshListBoxBuses();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int licenum = int.Parse(licenseNumTextBlock.Text);
                double fuel = double.Parse(fuelRemainTextBox.Text);
                DateTime fromDate = DateTime.Parse(fromDateDatePicker.Text);
                DateTime lastDate = DateTime.Parse(dateLastTreatDatePicker.Text);
                double kmLastTreat = double.Parse(kmLastTreatTextBox.Text);
                BO.BusStatus st = (BO.BusStatus)Enum.Parse(typeof(BO.BusStatus), statusComboBox.SelectedItem.ToString());
                double totalKm = double.Parse(totalTripTextBox.Text);
                BO.Bus bus = new BO.Bus() { LicenseNum = licenum, FuelRemain = fuel, FromDate = fromDate, DateLastTreat = lastDate, Status = st, TotalTrip = totalKm, KmLastTreat = kmLastTreat };
                bl.UpdateBusDetails(bus);
            }           
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "הפעולה נכשלה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("?האם אתה בטוח שאתה רוצה למחוק את האוטובוס", "...רגע", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;
            try
            { 
                int ln = int.Parse(licenseNumTextBlock.Text);                              
            if (licenseNumTextBlock != null)
                {
                    bl.DeleteBus(ln);
                    buses = bl.GetAllBuses().ToList();
                    LbBuses.DataContext = buses;
                }                   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "הפעולה נכשלה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
