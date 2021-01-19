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
using BLAPI;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl;
       // IBL bl = BLFactory.GetBL("1");
        BO.User adminUser;
        public MainWindow(IBL _bl, BO.User user)
        {
            InitializeComponent();
            bl = _bl;
            adminUser = user;             
        }
        private void Button_Click_Buses(object sender, RoutedEventArgs e)
        {
            Buses win = new Buses(bl);
            win.Show();
        }

        private void Button_Click_Stations(object sender, RoutedEventArgs e)
        {
            Stations win = new Stations(bl);
            win.Show();
        }

        private void Button_Click_Lines(object sender, RoutedEventArgs e)
        {
            Lines win = new Lines(bl);
            win.Show();
        }
    }
}
