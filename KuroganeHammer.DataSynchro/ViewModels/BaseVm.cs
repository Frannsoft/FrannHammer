using System.ComponentModel;
using System.Runtime.CompilerServices;
using KuroganeHammer.Data.Core;
using KuroganeHammer.DataSynchro.Annotations;
using KuroganeHammer.DataSynchro.Models;

namespace KuroganeHammer.DataSynchro.ViewModels
{
    public class BaseVm : INotifyPropertyChanged
    {
        protected readonly UserModel LoggedInUser;

        protected BaseVm(UserModel user)
        {
            Guard.VerifyObjectNotNull(user, nameof(user));

            LoggedInUser = user;
        }

        protected BaseVm()
        { }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}