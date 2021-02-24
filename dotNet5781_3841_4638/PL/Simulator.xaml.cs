using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class Simulator : Window
    {
        IBL bl;
        BO.Station currentStation;
        ObservableCollection<BO.LineTiming> lineTimingList;
        Stopwatch stopwatch;
        BackgroundWorker timerworker;
        TimeSpan tsStartTime;
        bool isTimerRun;
        public Simulator(IBL _bl, BO.Station station)
        {
            InitializeComponent();
            bl = _bl;
            currentStation = station;//current station
            grid1.DataContext = currentStation;
            stopwatch = new Stopwatch();
            timerworker = new BackgroundWorker();
            timerworker.DoWork += Worker_DoWork;
            timerworker.ProgressChanged += Worker_ProgressChanged;
            timerworker.WorkerReportsProgress = true;//מדווח על שינויים למסך
            tsStartTime = DateTime.Now.TimeOfDay;//השעה של עכשיו
            stopwatch.Restart();
            isTimerRun = true;//if the stopwatch run

            LBLineTiming.DataContext = lineTimingList;// הכנסת פרטים לדאטה גריד בחלון של הפרטים של הקו
            this.Closing += Window_Closing;
            timerworker.RunWorkerAsync();//the thread start
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            stopwatch.Stop();
            isTimerRun = false;
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TimeSpan tsCurrentTime = tsStartTime + stopwatch.Elapsed;//the current time
            string timmerText = tsCurrentTime.ToString().Substring(0, 8);//string of the current time
            this.TBTimer.Text = timmerText;//update the label of the the current clock
            //get all the lines that pass in this station in the closer hour
            LBLineTiming.DataContext = bl.GetLineTimingPerStation(currentStation, tsCurrentTime).ToList();
            lineTimingList = new ObservableCollection<BO.LineTiming>(bl.GetLineTimingPerStation(currentStation, tsCurrentTime)); //התצוגה תתעדכן כי זה אובזרוובל קוללקשיין
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                timerworker.ReportProgress(231);
                Thread.Sleep(1000);
            }
        }

    }
}
