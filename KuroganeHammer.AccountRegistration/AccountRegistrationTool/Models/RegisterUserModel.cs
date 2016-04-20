﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using AccountRegistrationTool.Annotations;
using KuroganeHammer.Data.Api.Models;

namespace AccountRegistrationTool.Models
{
    public class RegisterUserModel : INotifyPropertyChanged
    {

        #region Props

        private RegisterBindingModel _bindingModel;
        public RegisterBindingModel BindingModel
        {
            get { return _bindingModel; }
            set
            {
                _bindingModel = value;
                OnPropertyChanged();
            }
        }

        //private string _username;
        //public string Username
        //{
        //    get { return _username; }
        //    set
        //    {
        //        _username = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private string _password;
        //public string Password
        //{
        //    get { return _password; }
        //    set
        //    {
        //        _password = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private string _email;
        //public string Email
        //{
        //    get { return _email; }
        //    set
        //    {
        //        _email = value;
        //        OnPropertyChanged();
        //    }
        //}

        private Roles _roles;
        public Roles Roles
        {
            get { return _roles; }
            set
            {
                _roles = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}