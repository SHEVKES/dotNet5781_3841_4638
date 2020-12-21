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
using dotNet5781_01_3841_4638;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace dotNet5781_03B_3841_4638
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Random rand = new Random();
        public ObservableCollection<Bus> buses;
        public MainWindow()
        {
            InitializeComponent();
            buses=new ObservableCollection<Bus>();
            buildBusCollection.RestartBuses(buses);
            lbBuses.ItemsSource = buses;
        }
        private void bAddBus_Click(object sender, RoutedEventArgs e)
        {
            AddNewBus win = new AddNewBus();
            win.Buses = buses;
            win.ShowDialog();
        } //open new window when you click the button
        private void Start_Driving_Button_Click(object sender, RoutedEventArgs e)
        {
            Bus b = (sender as Button).DataContext as Bus;
            if(b.IsBusy())
            {
                MessageBox.Show("Sorry the bus is not available right now", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            KmDrive win = new KmDrive();
            win.Bus = b;
            win.ShowDialog();
            if (win.Bus.BusStatus == Status.Available)
                return;         
            BackgroundWorker workerStartD = new BackgroundWorker();
            workerStartD.DoWork += Worker_DoWork;
            workerStartD.ProgressChanged += Worker_ProgressChanged;
            workerStartD.RunWorkerCompleted += Worker_RunWorkerCompleted_StartD;
            workerStartD.WorkerReportsProgress = true;
            int speed = rand.Next(20, 50);
            int travelTime = (int)((win.Distance / speed) * 6);
            DataTread thread = new DataTread(Finditem<ProgressBar>((sender as Button).DataContext, "pbTread"), Finditem<Label>((sender as Button).DataContext, "seconds"), sender as Button, travelTime, b, win.Distance);
            thread.UpdateDetails(b.BusStatus);
            workerStartD.RunWorkerAsync(thread);
        }
        private void Refuel(object sender, RoutedEventArgs e)
        {
            Bus b= (sender as Button).DataContext as Bus;
            if (b.BusStatus != Status.Available)
            {
                MessageBox.Show("Sorry the bus is not available right now", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(b.Fuel==1200)
            {
                MessageBox.Show("The bus has a full fuel tank", "GREAT!", MessageBoxButton.OK);
                return;
            }
            b.BusStatus = Status.Fuel;
            BackgroundWorker workerRefuel = new BackgroundWorker();
            workerRefuel.DoWork += Worker_DoWork;
            workerRefuel.ProgressChanged += Worker_ProgressChanged;
            workerRefuel.RunWorkerCompleted += Worker_RunWorkerCompleted_Refuel;
            workerRefuel.WorkerReportsProgress = true;
            DataTread thread = new DataTread(Finditem<ProgressBar>((sender as Button).DataContext, "pbTread"), Finditem<Label>((sender as Button).DataContext, "seconds"), Finditem<Button>((sender as Button).DataContext, "StartDrivingButton"), 12, b);
            thread.UpdateDetails(b.BusStatus);
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
        }
        private void Worker_RunWorkerCompleted_Refuel(object sender, RunWorkerCompletedEventArgs e)
        {
            DataTread data = ((DataTread)(e.Result));
            data.Bus.BusStatus = Status.Available;
            data.UpdateDetails(data.Bus.BusStatus);
            data.Bus.Refuel();
            MessageBox.Show("The bus finished the refuel", "GREAT!", MessageBoxButton.OK, MessageBoxImage.Information);        
        }
        private void Worker_RunWorkerCompleted_StartD(object sender, RunWorkerCompletedEventArgs e)
        {
            DataTread data = ((DataTread)(e.Result));
            data.Bus.DoRide(data.DistanceDriving);
            data.Bus.BusStatus = Status.Available;
            data.UpdateDetails(data.Bus.BusStatus);
            MessageBox.Show("The bus finished the drive", "GREAT!", MessageBoxButton.OK, MessageBoxImage.Information);          
        }     
        public A Finditem<A>(object item, string str)
        {

            ListBoxItem myListBoxItem = (ListBoxItem)(lbBuses.ItemContainerGenerator.ContainerFromItem(item));

            // Getting the ContentPresenter of myListBoxItem
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);

            // Finding textBlock from the DataTemplate that is set on that ContentPresenter
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            A myLabel = (A)myDataTemplate.FindName(str, myContentPresenter);
            return myLabel;
        } //help function
        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        } //help function
        private void lbBuses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Bus b = (sender as ListBox).SelectedItem as Bus;
            if (b == null)
                return;
            ProgressBar bar = Finditem<ProgressBar>((sender as ListBox).SelectedItem, "pbTread");
            Label l = Finditem<Label>((sender as ListBox).SelectedItem, "seconds");
            Button button = Finditem<Button>((sender as ListBox).SelectedItem, "StartDrivingButton");
            SelectedBus win = new SelectedBus(b, bar, l,button);
            win.ShowDialog();
        }//open new window when you double click
       
    }
}
