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
using System.ComponentModel;
using System.Diagnostics;
using BLAPI;

namespace PL
{
    /// <summary>
    /// Interaction logic for RefuelAndTreat.xaml
    /// </summary>
    public partial class RefuelAndTreat : Window
    {
        IBL bl;
        //BO.Bus bus;
        BackgroundWorker worker;
        public RefuelAndTreat(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            RefreshAllBusesList();
        }
        public void RefreshAllBusesList()
        {
            List<BO.Bus> buses = bl.GetAllBuses().ToList();
            LBBuses.DataContext = buses;
        }  
        private void refuel_MouseDown_Button(object sender, MouseButtonEventArgs e)
        {       
            try
            {
                BO.Bus select = (sender as Image).DataContext as BO.Bus;
                if (select.FuelRemain == 1200)
                {
                    MessageBox.Show("The fuel remain of the bus ia already full", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (select.Status != BO.BusStatus.Available)
                {
                    MessageBox.Show("The bus does not avaliable", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                select.Status = BO.BusStatus.REFUELING;//change status
                bl.UpdateBusDetails(select);//update
                worker = new BackgroundWorker();
                worker.DoWork += Worker_DoWork;
                worker.ProgressChanged += Worker_ProgressChanged_fuel;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted_Refuel;
                worker.WorkerReportsProgress = true;//יכול לדווח על שינויים למסך
                pbRefuel.Visibility = Visibility.Visible;
                worker.RunWorkerAsync(12);// Start the thread.
            }
            catch (BO.BadLicenseNumException ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
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
        private void treatment_MouseDown_Button(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BO.Bus select = (sender as Image).DataContext as BO.Bus;
                if (select.DateLastTreat.ToShortDateString() == DateTime.Now.ToShortDateString())//if the bus is already treated
                {
                    MessageBox.Show("The bus is already treated today", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (select.Status != BO.BusStatus.Available)
                {
                    MessageBox.Show("The bus does not avaliable", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                select.Status = BO.BusStatus.TREATMENT;//change status
                bl.UpdateBusDetails(select);//update with the new status
                worker = new BackgroundWorker();
                worker.DoWork += Worker_DoWork;
                worker.ProgressChanged += Worker_ProgressChanged_treat;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted_Treat;
                worker.WorkerReportsProgress = true;
                pbTreat.Visibility = Visibility.Visible;
                worker.RunWorkerAsync(12); // Start the thread.
            }
            catch (BO.BadLicenseNumException ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
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
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int length = (int)e.Argument;//how many seconds the thread is active
            for (int i = 1; i <= length; i++)
            {
                if (worker.CancellationPending == true)//לא כל כך צריך שעון לפרוייקט שלנו
                {
                    e.Cancel = true;
                    e.Result = stopwatch.ElapsedMilliseconds; // Unnecessary
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(500);
                    worker.ReportProgress(i * 100 / length);
                }
            }

            e.Result = stopwatch.ElapsedMilliseconds;
        }
        private void Worker_ProgressChanged_fuel(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            pbRefuel.Value = progress;           
        }
        private void Worker_ProgressChanged_treat(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            pbTreat.Value = progress;
        }
        private void Worker_RunWorkerCompleted_Refuel(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                BO.Bus select = (sender as Image).DataContext as BO.Bus;
                pbRefuel.Visibility = Visibility.Hidden;
                bl.RefuelBus(select);//refuel
                select = bl.GetBus(select.LicenseNum);
                //busDetailsGrid.DataContext = bus;
                //statusComboBox.Text = select.Status.ToString();//to show the current bus status
                MessageBox.Show("The bus was refuled successfuly", "success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (BO.BadLicenseNumException ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
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
        private void Worker_RunWorkerCompleted_Treat(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                BO.Bus select = (sender as Image).DataContext as BO.Bus;
                pbTreat.Visibility = Visibility.Hidden;
                bl.TreatmentBus(select);//treat the bus
                select = bl.GetBus(select.LicenseNum);
               //busDetailsGrid.DataContext = bus;//update the grid with the details of the bus after the treatment
               //statusComboBox.Text = bus.Status.ToString();//to show the current bus status
                MessageBox.Show("The bus was treated successfuly", "success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (BO.BadLicenseNumException ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}

