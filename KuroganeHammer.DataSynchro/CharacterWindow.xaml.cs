using System;
using System.Threading.Tasks;
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
        private readonly bool _isNewCharacter;

        public CharacterWindow(Character character, UserModel user, bool isNewCharacter = false)
        {
            Guard.VerifyObjectNotNull(character, nameof(character));
            Guard.VerifyObjectNotNull(user, nameof(user));

            InitializeComponent();

            _isNewCharacter = isNewCharacter;
            _user = user;
            _characterVm = new CharacterVm(character, user, isNewCharacter);
            DataContext = _characterVm;
            InitEditorControl(character, user);
        }

        private void InitEditorControl(BaseModel model, UserModel user)
        {
            var editorControl = new EditorControl(model, user, _isNewCharacter);
            Grid.SetRow(editorControl, 0);
            Grid.SetColumn(editorControl, 0);
            Grid.SetColumnSpan(editorControl, 1);
            CharacterPropertiesGrid.Children.Add(editorControl);
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
                const int movements = 2;
                const int attributes = 3;

                switch (MainTabControl.SelectedIndex)
                {
                    case characters:
                        {
                            Execute(async () => await _characterVm.RefreshCharacter());
                            break;
                        }
                    case moves:
                        {
                            Execute(async () => await _characterVm.RefreshMoves());
                            break;
                        }
                    case movements:
                        {
                            Execute(async () => await _characterVm.RefreshMovements());
                            break;
                        }
                    case attributes:
                        {
                            Execute(async () => await _characterVm.RefreshCharacterAttributes());
                            break;
                        }
                }
            }
        }
    }
}
