using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using KuroganeHammer.Data.Core;
using KuroganeHammer.Data.Core.Models;
using KuroganeHammer.DataSynchro.Models;
using KuroganeHammer.DataSynchro.ViewModels;

namespace KuroganeHammer.DataSynchro.Controls
{
    /// <summary>
    /// Interaction logic for EditorControl.xaml
    /// </summary>
    public partial class EditorControl
    {
        private readonly EditVm _editVm;

        public EditorControl(EditVm editVm)
        {
            Guard.VerifyObjectNotNull(editVm, nameof(editVm));

            InitializeComponent();
            _editVm = editVm;

            AssembleFields();
        }

        public EditorControl(BaseModel model, UserModel user, bool isNewObject)
        {
            Guard.VerifyObjectNotNull(model, nameof(model));
            Guard.VerifyObjectNotNull(user, nameof(user));


            InitializeComponent();
            _editVm = new EditVm(model, user, isNewObject);

            DataContext = _editVm;

            AssembleFields();
        }

        private void AssembleFields()
        {
            var rowIndex = 1;

            foreach (var prop in _editVm.Properties)
            {
                var margin = new Thickness(5);

                //Create a label for each property
                var propLabel = new Label { Content = $"{prop.Name}: ", Margin = margin };
                Grid.SetRow(propLabel, rowIndex);
                Grid.SetColumn(propLabel, 0);

                //create a textbox for each property
                //load existing data into the textbox
                var valueBinding = new Binding("Value") {Mode = BindingMode.TwoWay};
                var propValue = new TextBox
                {
                    Width = 150,
                    Margin = margin,
                    DataContext = prop
                };
                propValue.SetBinding(TextBox.TextProperty, valueBinding);
                Grid.SetRow(propValue, rowIndex);
                Grid.SetColumn(propValue, 1);

                MainControlGrid.Children.Add(propLabel);
                MainControlGrid.Children.Add(propValue);
                MainControlGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                rowIndex++;
            }
        }

        private async Task Execute(Func<Task<bool>> operation)
        {
            try
            {
                ShowWaitingUi();
                var result = await operation();

                if (result)
                {
                    MessageBox.Show("Object successfully updated.", "Task Complete.", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Unable to update object.  If this happens again contact the dev.", "Error", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, "Error");
            }
            finally
            {
                HideWaitingUi();
            }
        }

        private void HideWaitingUi()
        {
            WaitingStackPanel.Visibility = Visibility.Hidden;
            SaveDeleteStackPanel.Visibility = Visibility.Visible;
        }

        private void ShowWaitingUi()
        {
            WaitingStackPanel.Visibility = Visibility.Visible;
            SaveDeleteStackPanel.Visibility = Visibility.Hidden;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //Save teh item
            await Execute(async () => await _editVm.SaveModel());
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "Confirm Delete", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                await Execute(async () => await _editVm.DeleteModel());
            }
        }
    }
}
