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
    /// Interaction logic for AddNewLine.xaml
    /// </summary>
    public partial class AddNewLine : Window
    {
        IBL bl;
        //BO.Line line = new BO.Line(); 
        public AddNewLine(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Area));
            //areaComboBox.SelectedIndex = 0;
            CbFirstStation.DisplayMemberPath = "Name";
            //CbFirstStation.SelectedValuePath = "Code";
            CbFirstStation.SelectedIndex = 0;
            CbFirstStation.DataContext = bl.GetAllStations().ToList();
            CbLastStation.DisplayMemberPath = "Name";
            CbLastStation.SelectedIndex = 0;
            CbLastStation.DataContext = bl.GetAllStations().ToList();
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Station first = (CbFirstStation.SelectedItem) as BO.Station;
                BO.Station second = (CbLastStation.SelectedItem) as BO.Station;
                int lineNum = int.Parse(lineNumTextBox.Text);
                BO.Area area = (BO.Area)Enum.Parse(typeof(BO.Area), areaComboBox.SelectedItem.ToString());
                BO.Line newLine = new BO.Line() { LineId = -1, LineNum = lineNum, Area = area };
                //if (bl.IsExistAdjacentStations(first.Code, second.Code))
                //{
                //    BO.StationInLine station1 = new BO.StationInLine() { DisabledAccess = first.DisabledAccess, StationCode = first.Code, Name = first.Name, LineStationIndex = 1 };
                //    newLine.stations.Add(station1);
                //    BO.StationInLine station2 = new BO.StationInLine() { DisabledAccess = second.DisabledAccess, StationCode = second.Code, Name = second.Name, LineStationIndex = 2 };
                //    newLine.stations.Add(station2);
                //    bl.AddLine(newLine);
                //    Close();
                //}

                //GrTimeDist.Visibility = Visibility.Visible;
                //TimeSpan time = TimeSpan.Parse(TbTime.Text);
                //double dis = double.Parse(TbDistance.Text);
                BO.StationInLine station1 = new BO.StationInLine() { DisabledAccess = first.DisabledAccess, StationCode = first.Code, Name = first.Name, LineStationIndex = 1, };
                newLine.stations = new List<BO.StationInLine>();
                newLine.stations.Add(station1);
                BO.StationInLine station2 = new BO.StationInLine() { DisabledAccess = second.DisabledAccess, StationCode = second.Code, Name = second.Name, LineStationIndex = 2 };
                newLine.stations.Add(station2);
                bl.AddLine(newLine);
                Close();
                
            }
            catch (BO.BadLineIdException ex)
            {
                MessageBox.Show(ex.ToString(), "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
