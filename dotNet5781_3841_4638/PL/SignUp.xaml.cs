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
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        IBL bl;
        //BO.User NewUser;
        public SignUp(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{

        //    System.Windows.Data.CollectionViewSource userViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("userViewSource")));
        //    // Load data by setting the CollectionViewSource.Source property:
        //    // userViewSource.Source = [generic data source]
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string firstName = firstNameTextBox.Text;
                string lastName = lastNameTextBox.Text;
                string userName = userNameTextBox.Text;
                string password = passwordTextBox.Text;
                bool adminAccess = (adminAccessCheckBox.IsChecked == true);
                BO.User newUser = new BO.User() { FirstName = firstName, LastName = lastName, UserName = userName, Password = password, AdminAccess = adminAccess };
                bl.AddUser(newUser);
                MessageBox.Show("שם המשתמש התווסף בהצלחה למערכת", "מזל טוב", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (BO.BadUserNameException ex)
            {
                MessageBox.Show(ex.Message + ":" + ex.userName, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
