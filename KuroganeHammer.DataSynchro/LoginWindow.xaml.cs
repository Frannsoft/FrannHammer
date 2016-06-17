using KuroganeHammer.DataSynchro.Models;
using KuroganeHammer.DataSynchro.ViewModels;
using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KuroganeHammer.DataSynchro
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow
    {
        private readonly LoginVm _loginVm;
        public LoginWindow()
        {
            InitializeComponent();
            _loginVm = new LoginVm();
            DataContext = _loginVm;
        }

        private void HideLoggingInUi()
        {
            LoggingInStackPanel.Visibility = Visibility.Hidden;
            CredentialsDockPanel.Visibility = Visibility.Visible;
            LoginCancelStackPanel.Visibility = Visibility.Visible;
        }

        private void ShowLoggingInUi()
        {
            LoggingInStackPanel.Visibility = Visibility.Visible;
            CredentialsDockPanel.Visibility = Visibility.Hidden;
            LoginCancelStackPanel.Visibility = Visibility.Hidden;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
#if DEBUG
       
#endif
            var username = UsernameTextBox.Text;
            var password = PasswordPasswordBox.Password;
            var baseUrl = ConfigurationManager.AppSettings["site"];

            try
            {
                ShowLoggingInUi();

                var user = default(UserModel);
                user = await _loginVm.LoginAs(username, password, baseUrl);

                var mainWindow = new MainWindow(user);
                mainWindow.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace); //TODO: Make an actual error dialog that outputs to file
                HideLoggingInUi();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
