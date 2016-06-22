using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FrannHammer.Core;
using FrannHammer.DataSynchro.Models;
using FrannHammer.DataSynchro.Properties;

namespace FrannHammer.DataSynchro.ViewModels
{
    public class BaseVm : INotifyPropertyChanged
    {
        public UserModel LoggedInUser { get; private set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            private set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        protected BaseVm(UserModel user)
        {
            Guard.VerifyObjectNotNull(user, nameof(user));

            LoggedInUser = user;
        }

        protected BaseVm()
        { }

        protected void IsNowBusy() => IsBusy = true;
        protected void NotBusy() => IsBusy = false;

        //protected async Task ExecuteAsync(Action action)
        //{
        //    IsNowBusy();
        //    action();
        //    NotBusy();
        //}

        protected async Task<T> ExecuteAsync<T>(Func<Task<T>> action)
        {
            IsNowBusy();
            var retVal = await action();
            NotBusy();

            return retVal;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}