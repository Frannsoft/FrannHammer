using System.Windows.Controls;
using KuroganeHammer.Data.Core;
using KuroganeHammer.Data.Core.Models;
using KuroganeHammer.DataSynchro.Controls;
using KuroganeHammer.DataSynchro.Models;
using KuroganeHammer.DataSynchro.ViewModels;

namespace KuroganeHammer.DataSynchro
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow
    {
        private readonly EditVm _editVm;

        public EditWindow(BaseModel model, UserModel user)
        {
            Guard.VerifyObjectNotNull(model, nameof(model));

            InitializeComponent();

            _editVm = new EditVm(model, user);
            DataContext = _editVm;
            InitEditorControl();
        }

        private void InitEditorControl()
        {
            var editorControl = new EditorControl(_editVm);
            PropertiesGrid.Children.Add(editorControl);
            //Grid.SetRow(editorControl, 1);
            //MainGrid.Children.Add(editorControl);
        }
    }
}
