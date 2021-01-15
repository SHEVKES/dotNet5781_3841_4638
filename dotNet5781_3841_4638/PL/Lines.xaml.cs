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
    /// Interaction logic for Lines.xaml
    /// </summary>
    public partial class Lines : Window
    {
        IBL bl;
        List<BO.Line> lines;
        //BO.Line line1;
        public Lines(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            RefreshListBoxLines();
        }
        public void RefreshListBoxLines()
        {
            lines = bl.GetAllLines().ToList();
            LbLines.DataContext = lines;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource stationInLineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stationInLineViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // stationInLineViewSource.Source = [generic data source]
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            BO.Line tempLine = LbLines.SelectedItem as BO.Line;
            UpdateLine win = new UpdateLine(bl,tempLine);
            win.Show();
        }

        private void LbLines_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Line line = (sender as ListBox).SelectedItem as BO.Line;
            if (line == null)
                return;
            stationInLineDataGrid.DataContext = line.stations;
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {           
            MessageBoxResult result = MessageBox.Show("?האם אתה בטוח שאתה רוצה למחוק את הקו", "...רגע", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;
            try
            {
                BO.Line tempLine = LbLines.SelectedItem as BO.Line;
                bl.DeleteLine(tempLine.LineId);
                RefreshListBoxLines();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "הפעולה נכשלה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
