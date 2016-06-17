using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KuroganeHammer.Data.Core.Models;
using KuroganeHammer.DataSynchro.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using KuroganeHammer.DataSynchro.Models;

namespace KuroganeHammer.DataSynchro.ViewModels
{
    public class EditVm : BaseVm
    {
        private readonly BaseModel _model;

        private ObservableCollection<PropertyEssentials> _properties;
        public ObservableCollection<PropertyEssentials> Properties
        {
            get { return _properties; }
            private set
            {
                _properties = value;
                OnPropertyChanged();
            }
        }

        public EditVm(BaseModel model, UserModel user)
            : base(user)
        {
            _model = model;
            Properties = new ObservableCollection<PropertyEssentials>(GetModelProperties());
        }

        private List<PropertyEssentials> GetModelProperties()
        {
            var props = _model.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var propEssentials = props.Select(p => new PropertyEssentials
            {
                Name = p.Name,
                Value = p.GetValue(_model)
            }).ToList();

            return propEssentials;
        }

        private void UpdateModelFromProperties()
        {
            //get live property values from model
            var props = _model.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToList();

            //iterate through live values and update them on the local model with the changed Properties values
            props.ForEach(prop =>
            {
                var propEssentialValue = Properties.SingleOrDefault(pe => pe.Name.Equals(prop.Name));
                if (propEssentialValue != null && prop.CanWrite)
                {
                    prop.SetValue(_model, propEssentialValue.Value);
                }
            });
        }

        public async Task<bool> SaveModel()
        {
            //update the local model instance
            UpdateModelFromProperties();

            //now push the update
            var response = await _model.Update(LoggedInUser.LoggedInClient);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteModel()
        {
            var response = await _model.Delete(LoggedInUser.LoggedInClient);

            return response.IsSuccessStatusCode;
        }
    }
}
