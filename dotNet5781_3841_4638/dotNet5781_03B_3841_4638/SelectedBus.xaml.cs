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

namespace dotNet5781_03B_3841_4638
{
    /// <summary>
    /// Interaction logic for SelectedBus.xaml
    /// </summary>
    public partial class SelectedBus : Window
    {
        public Bus BusCurrent { get; set; }
        public ProgressBar ProgressBar { get; set; }
        public Label Lable { get; set; }
        public Button ButtonDriving { get; set; }
        public SelectedBus(Bus bus, ProgressBar bar, Label label,Button button)
        {
            InitializeComponent();
            BusCurrent = bus;
            BusTextBlock.Text = BusCurrent.ToString();
            ProgressBar = bar;
            Lable = label;
            ButtonDriving = button;
        } //const
        private void Button_Click(object sender, RoutedEventArgs e) //treatment button
        {
            if (BusCurrent.BusStatus != Status.Available)
            {
                MessageBox.Show("Sorry the bus is not available right now", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (BusCurrent.LastTreatment == DateTime.Now && BusCurrent.LastKmTreat == BusCurrent.TotalKm)
            {
                MessageBox.Show("The bus was treatmented", "GREAT!", MessageBoxButton.OK);
                return;
            }
            BusCurrent.BusStatus = Status.Treat;
            BackgroundWorker workerTreatment = new BackgroundWorker();
            workerTreatment.DoWork += Worker_DoWork;
            workerTreatment.ProgressChanged += Worker_ProgressChanged;
            workerTreatment.RunWorkerCompleted += Worker_RunWorkerCompleted_Treatment;
            workerTreatment.WorkerReportsProgress = true;
            DataTread thread = new DataTread(ProgressBar, Lable,ButtonDriving, 144, BusCurrent);
            thread.UpdateDetails(BusCurrent.BusStatus);
            ProgressBarTreatment.Visibility = Visibility.Visible;          
            ProgressBarTreatment.Foreground = Brushes.LightGreen;
            BusTextBlock.Text = BusCurrent.ToString();
            workerTreatment.RunWorkerAsync(thread);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e) //refuel button
        {
            if (BusCurrent.BusStatus != Status.Available)
            {
                MessageBox.Show("Sorry the bus is not available right now", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (BusCurrent.Fuel == 1200)
            {
                MessageBox.Show("The bus has a full fuel tank", "GREAT!", MessageBoxButton.OK);
                return;
            }
            BusCurrent.BusStatus = Status.Fuel;
            BackgroundWorker workerRefuel = new BackgroundWorker();
            workerRefuel.DoWork += Worker_DoWork;
            workerRefuel.ProgressChanged += Worker_ProgressChanged;
            workerRefuel.RunWorkerCompleted += Worker_RunWorkerCompleted_Fuel;
            workerRefuel.WorkerReportsProgress = true;
            DataTread thread = new DataTread(ProgressBar, Lable, ButtonDriving, 12, BusCurrent);
            thread.UpdateDetails(BusCurrent.BusStatus);
            ProgressBar.Visibility = Visibility.Visible;            
            ProgressBarTreatment.Foreground = Brushes.BlueViolet;
            BusTextBlock.Text = BusCurrent.ToString();
            workerRefuel.RunWorkerAsync(thread);
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTread data = (DataTread)e.Argument;
            int length = data.Seconds;
            for (int i = 1; i <= length; i++)
            {
                System.Threading.Thread.Sleep(1000);
                (sender as BackgroundWorker).ReportProgress(i, data);
            }
            e.Result = data;
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = (int)e.ProgressPercentage;
            DataTread data = (DataTread)e.UserState;
            int result = data.Seconds - progress;
            data.Label.Content = result;
            data.ProgressBar.Value = (progress * 100) / data.Seconds;
            ProgressBarTreatment.Value = data.ProgressBar.Value;
            ProgressBarTreatment.Visibility = Visibility.Visible;
        }
        private void Worker_RunWorkerCompleted_Treatment(object sender, RunWorkerCompletedEventArgs e)
        {
            DataTread data = ((DataTread)(e.Result));
            data.ProgressBar.Visibility = Visibility.Hidden;
            data.Label.Visibility = Visibility.Hidden;
            data.Bus.BusStatus = Status.Available;
            BusCurrent.Treatment();
            BusCurrent.Refuel();
            BusTextBlock.Text = BusCurrent.ToString();
            MessageBox.Show("The bus finished the treatment", "GREAT!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void Worker_RunWorkerCompleted_Fuel(object sender, RunWorkerCompletedEventArgs e)
        {
            DataTread data = ((DataTread)(e.Result));
            data.ProgressBar.Visibility = Visibility.Hidden;
            data.Label.Visibility = Visibility.Hidden;
            data.Bus.BusStatus = Status.Available;          
            BusCurrent.Refuel();
            BusTextBlock.Text = BusCurrent.ToString();
            MessageBox.Show( "The bus finished the refuel", "GREAT!", MessageBoxButton.OK, MessageBoxImage.Information);
        }       
    }
}
