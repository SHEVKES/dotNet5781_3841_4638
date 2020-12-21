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

namespace dotNet5781_03B_3841_4638
{
    /// <summary>
    /// Interaction logic for DataTread.xaml
    /// </summary>
    public partial class DataTread : Window
    {
        public ProgressBar ProgressBar { get; set; }
        public Label Label { get; set; }
        public Button ButtonStartDriving { get; set; }
        public int Seconds { get; set; }
        public Bus Bus { get; set; }
        public double DistanceDriving { get; set; }
        public DataTread(ProgressBar bar,Label label, Button button, int sec, Bus bus)
        {
            InitializeComponent();
            ProgressBar = bar;
            Label = label;
            Seconds = sec;
            Bus = bus;
        }
        public DataTread(ProgressBar bar, Label label,Button button ,int sec, Bus bus,double dis)
        {
            InitializeComponent();
            ProgressBar = bar;
            Label = label;
            ButtonStartDriving = button;
            Seconds = sec;
            Bus = bus;
            DistanceDriving = dis;
            
        }
        public void UpdateDetails(Status st)
        {
            switch (st)
            {
                case Status.Available:                   
                    ProgressBar.Visibility = Visibility.Hidden;
                    Label.Visibility = Visibility.Hidden;
                    ProgressBar.Value = 0;
                    break;
                case Status.Middle:                 
                    ProgressBar.Visibility = Visibility.Visible;
                    Label.Visibility = Visibility.Visible;
                    ProgressBar.Foreground = Brushes.Red;
                    break;
                case Status.Fuel:                   
                    Label.Visibility = Visibility.Visible;
                    ProgressBar.Visibility = Visibility.Visible;
                    ProgressBar.Foreground = Brushes.BlueViolet;
                    break;
                case Status.Treat:                  
                    Label.Visibility = Visibility.Visible;
                    ProgressBar.Visibility = Visibility.Visible;
                    ProgressBar.Foreground = Brushes.LightGreen;                
                    break;
                default:
                    break;
            }
        }
    }
}
