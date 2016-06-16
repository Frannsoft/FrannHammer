using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KuroganeHammer.Data.Core.Models;
using KuroganeHammer.DataSynchro.Models;
using KuroganeHammer.DataSynchro.ViewModels;

namespace KuroganeHammer.DataSynchro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly MainVm _mainVm;

        public MainWindow(UserModel user)
        {
            InitializeComponent();

            _mainVm = new MainVm(user);

            DataContext = _mainVm;
        }

        private void MetroWindow_Closing(object sender, CancelEventArgs e) => Execute(() => _mainVm.Logout());

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
            => Execute(() => _mainVm.RefreshCharacters());

        private void CharactersDataGrid_Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CharactersDataGrid.SelectedIndex != -1)
            {
                var character = CharactersDataGrid.SelectedItem as Character;
                if (character == null) return;

                //TODO: Load character into new window to update
            }
        }

        private void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, "Error");
            }
        }
    }
}
