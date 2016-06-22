using System;
using System.Collections.Generic;
using System.Windows;
using FrannHammer.AccountRegistrationTool.ViewModels;
using MahApps.Metro.Controls;

namespace FrannHammer.AccountRegistrationTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly MainVm _mainVm;

        public MainWindow()
        {
            InitializeComponent();
            _mainVm = new MainVm();

            DataContext = _mainVm;
        }

        private void LoginAsButton_Click(object sender, RoutedEventArgs e)
        {
            var username = LoginAsUsernameTextBox.Text;
            var password = LoginAsPasswordTextBox.Password;

            try
            {
                _mainVm.LoginAs(username, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        private void RegisterNewUserButton_Click(object sender, RoutedEventArgs e)
        {
            var username = NewUserTextBox.Text;
            var email = NewUserEmailTextBox.Text;
            var emailConfirm = NewUserEmailConfirmTextBox.Text;
            var password = NewUserPasswordTextBox.Password;
            var passwordConfirm = NewUserConfirmPasswordTextBox.Password;

            var roles = new Dictionary<string, bool>();

            if (BasicRoleCheckBox.IsChecked.HasValue)
            {
                roles.Add("Basic", BasicRoleCheckBox.IsChecked.Value);
            }

            if (AdminRoleCheckBox.IsChecked.HasValue)
            {
                roles.Add("Admin", AdminRoleCheckBox.IsChecked.Value);
            }

            try
            {
                _mainVm.Register(username, email, emailConfirm, password, passwordConfirm, roles);
                MessageBox.Show($"{username} created.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
    }
}
