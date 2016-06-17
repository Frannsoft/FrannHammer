using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KuroganeHammer.Data.Core;
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
        private readonly UserModel _user;

        public MainWindow(UserModel user)
        {
            Guard.VerifyObjectNotNull(user, nameof(user));

            InitializeComponent();

            _user = user;
            _mainVm = new MainVm(user);

            DataContext = _mainVm;
        }

        private void MetroWindow_Closing(object sender, CancelEventArgs e) => Execute(() => _mainVm.Logout());

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            const int characters = 0;
            const int moves = 1;
            switch (MainTabControl.SelectedIndex)
            {
                case characters:
                    {
                        Execute(() => _mainVm.RefreshCharacters());
                        break;
                    }
                //case moves:
                //    {
                //        Execute(() => _mainVm.RefreshMoves());
                //        break;
                //    }
            }

        }

        private void CharactersDataGrid_Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CharactersDataGrid.SelectedIndex != -1)
            {
                var character = CharactersDataGrid.SelectedItem as Character;
                if (character == null) return;

                // Load character into new window to update
                //var editWindow = new EditWindow(character);
                //editWindow.ShowDialog();

                var characterWindow = new CharacterWindow(character, _user);
                characterWindow.ShowDialog();
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

        private void MovesDataGrid_Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
