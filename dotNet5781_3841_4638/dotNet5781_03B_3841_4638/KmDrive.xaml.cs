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
    /// Interaction logic for KmDrive.xaml
    /// </summary>
    public partial class KmDrive : Window
    {
        public Bus Bus { get; set; }
        public double Distance { get; set; }
        public KmDrive()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        //These functions are designed for the Enter key
        private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            {
                TextBox text = sender as TextBox;
                if (text == null) return;
                if (e == null) return;

                //allow get out of the text box
                if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                    return;

                //allow list of system keys (add other key here if you want to allow)
                if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                    e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
                 || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right || e.Key == Key.OemPeriod)
                    return;

                char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

                //allow control system keys
                if (Char.IsControl(c)) return;

                //allow digits (without Shift or Alt)
                if (Char.IsDigit(c))
                    if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                        return; //let this key be written inside the textbox

                //forbid letters and signs (#,$, %, ...)
                e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
                return;
            }
        }
        private void Enter_Distance(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    this.Distance = Double.Parse((sender as TextBox).Text);
                    Bus.DoRide(Distance);
                    Bus.BusStatus = Status.Fuel;
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
}
