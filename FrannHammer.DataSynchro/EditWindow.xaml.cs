using FrannHammer.Core;
using FrannHammer.Core.Models;
using FrannHammer.DataSynchro.Controls;
using FrannHammer.DataSynchro.Models;
using FrannHammer.DataSynchro.ViewModels;

namespace FrannHammer.DataSynchro
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
