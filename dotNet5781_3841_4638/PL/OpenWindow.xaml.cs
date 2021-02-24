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
    /// Interaction logic for OpenWindow.xaml
    /// </summary>
    public partial class OpenWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        //IBL bl;
        public OpenWindow()
        {
            InitializeComponent();
            //bl = _bl;
        }

      

        private void Button_Click_SignIn(object sender, RoutedEventArgs e)
        {
            try
            {
                string userName = userNameTextBox.Text;
                string password = PBPassword.Password;
                BO.User user = bl.SignIn(userName, password);
                if (user.AdminAccess)
                {
                    MainWindow win = new MainWindow(bl, user);
                    win.ShowDialog();
                }
                else
                {
                    MainWindowUser win = new MainWindowUser(bl, user);
                    win.ShowDialog();
                }
            }
            catch (BO.BadUserNameException)
            {
                MessageBox.Show("שם המשתמש או הסיסמא שהקשת אינו תקין", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Button_Click_SignUp(object sender, RoutedEventArgs e)
        {
            SignUp win = new SignUp(bl);
            win.ShowDialog();
        }
    }
}
