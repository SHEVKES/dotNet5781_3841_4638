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
    /// Interaction logic for UpdateTD.xaml
    /// </summary>
    public partial class UpdateTD : Window
    {
        IBL bl;
        BO.StationInLine firstStation;
        BO.StationInLine secondStation;
        public UpdateTD(IBL _bl, BO.StationInLine first, BO.StationInLine second)
        {
            InitializeComponent();
            bl = _bl;
            firstStation = first;
            secondStation = second;
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeSpan time = TimeSpan.Parse(TbTime.Text);
                double dis = double.Parse(TbDistance.Text);
                if(dis < 0)
                {
                    MessageBox.Show("המרחק שהזנת אינו תקין", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                firstStation.Distance = dis;
                firstStation.Time = time;
                bl.UpdateTimeAndDistance(firstStation, secondStation);
                Close();
            }
            catch (BO.BadAdjacentStationsException ex)
            {
                MessageBox.Show(ex.Message + ": " + ex.stationCode1 + " " + ex.stationCode2, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("?האם הינך בטוח שאינך רוצה לשמור את השינויים", "...רגע", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result==MessageBoxResult.Yes)               
                Close();
        }
    }
}
