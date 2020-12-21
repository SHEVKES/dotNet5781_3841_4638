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
using System.Collections.ObjectModel;

namespace dotNet5781_03B_3841_4638
{
    /// <summary>
    /// Interaction logic for AddNewBus.xaml
    /// </summary>
    public partial class AddNewBus : Window
    {
        public ObservableCollection<Bus> Buses { get; set; }
        public AddNewBus()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
        }
        /// <summary>
        /// The function checks if the line number already exists in the system
        /// </summary>
        /// <param name="licenseNumber">Check if the added license number is equal to an existing license number</param>
        /// <returns>true or false </returns>
        private bool IsExist(string licenseNumber) 
        {
            foreach (Bus item in Buses)
            {
                if (item.LicenseNumber == licenseNumber)
                    return true;
            }
            return false;
           
        }
        private void bSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Bus b = new Bus(licNumTextBox.Text, startDatePicker.DisplayDate, double.Parse(totalKmTextBox.Text), double.Parse(fuelTextBox.Text), lastTreatmentDatePicker.DisplayDate, double.Parse(lastKmTreatTextBox.Text));
                if (IsExist(b.LicenseNumber))
                {
                    MessageBox.Show("The bus already exists in the system", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    Buses.Add(b);
            }
            catch (BusException ex)
            {
                MessageBox.Show("ERROR, " + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Close();
        }

    }
}
