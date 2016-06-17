using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KuroganeHammer.Data.Core;
using KuroganeHammer.Data.Core.Models;
using KuroganeHammer.DataSynchro.Controls;
using KuroganeHammer.DataSynchro.Models;
using KuroganeHammer.DataSynchro.ViewModels;

namespace KuroganeHammer.DataSynchro
{
    /// <summary>
    /// Interaction logic for CharacterWindow.xaml
    /// </summary>
    public partial class CharacterWindow
    {
        private readonly CharacterVm _characterVm;
        private readonly UserModel _user;

        public CharacterWindow(Character character, UserModel user)
        {
            Guard.VerifyObjectNotNull(character, nameof(character));
            Guard.VerifyObjectNotNull(user, nameof(user));

            InitializeComponent();

            _user = user;
            _characterVm = new CharacterVm(character, user);
            DataContext = _characterVm;
            InitEditorControl(character, user);
        }

        private void InitEditorControl(BaseModel model, UserModel user)
        {
            var editorControl = new EditorControl(model, user);
            Grid.SetRow(editorControl, 0);
            Grid.SetColumn(editorControl, 0);
            Grid.SetColumnSpan(editorControl, 1);
            CharacterPropertiesGrid.Children.Add(editorControl);
        }

        public CharacterWindow()
        {
            InitializeComponent();
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

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                const int characters = 0;
                const int moves = 1;
                switch (MainTabControl.SelectedIndex)
                {
                    case characters:
                        {
                            Execute(() => _characterVm.RefreshCharacter());
                            break;
                        }
                    case moves:
                        {
                            Execute(() => _characterVm.RefreshMoves());
                            break;
                        }
                }
            }
        }

        private void MovesDataGrid_Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MovesDataGrid.SelectedIndex != -1)
            {
                var move = MovesDataGrid.SelectedItem as Move;
                if (move == null) return;

                // Load character into new window to update
                var editWindow = new EditWindow(move, _user);
                editWindow.ShowDialog();
            }
        }
    }
}
