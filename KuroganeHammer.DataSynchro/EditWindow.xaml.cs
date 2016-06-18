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
        private readonly UserModel _user;
        private readonly bool _isNewObject;
        private readonly BaseModel _model;

        public EditWindow(BaseModel model, UserModel user, bool isNewObject = false)
        {
            Guard.VerifyObjectNotNull(model, nameof(model));
            Guard.VerifyObjectNotNull(user, nameof(user));

            InitializeComponent();

            _user = user;
            _isNewObject = isNewObject;
            _model = model;
            DataContext = new EditVm(model, _user, _isNewObject);
            InitEditorControl();
        }

        private void InitEditorControl()
        {
            var editorControl = new EditorControl(_model, _user, _isNewObject);
            PropertiesGrid.Children.Add(editorControl);
        }
    }
}
