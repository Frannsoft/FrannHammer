using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using FrannHammer.AccountRegistrationTool.Models;
using FrannHammer.AccountRegistrationTool.Properties;
using FrannHammer.Core;
using FrannHammer.Core.Models;

namespace FrannHammer.AccountRegistrationTool.ViewModels
{
    public class MainVm : INotifyPropertyChanged
    {
        private const string KeyServiceLocation = "servicelocation";

        #region Props

        private RegisterUserModel _model;
        public RegisterUserModel Model
        {
            get { return _model; }
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoggedIn;
        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set
            {
                _isLoggedIn = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private ServiceHandler _serviceHandler;

        public void LoginAs(string username, string password)
        {
            Guard.VerifyStringIsNotNullOrEmpty(username, nameof(username));
            Guard.VerifyStringIsNotNullOrEmpty(password, nameof(password));

            var serviceLocation = GetServiceLocationFromAppConfig();

            if (string.IsNullOrEmpty(serviceLocation))
            { throw new Exception("Service Location must be present"); }

            _serviceHandler = new ServiceHandler(serviceLocation);
            IsLoggedIn = _serviceHandler.LoginAs(username, password);
        }

        public void Register(string username, string email, string confirmEmail, string password, string confirmpassword, Dictionary<string, bool> roles)
        {
            Guard.VerifyStringIsNotNullOrEmpty(username, nameof(username));
            Guard.VerifyStringIsNotNullOrEmpty(email, nameof(email));
            Guard.VerifyStringIsNotNullOrEmpty(confirmEmail, nameof(confirmEmail));
            Guard.VerifyStringIsNotNullOrEmpty(password, nameof(password));
            Guard.VerifyStringIsNotNullOrEmpty(confirmpassword, nameof(confirmpassword));
            Guard.VerifyObjectNotNull(roles, nameof(roles));

            var isValid = VerifyNewUserCreds(username, email, confirmEmail, password, confirmpassword);

            if (isValid)
            {
                var registerUserModel = new RegisterUserModel
                {
                    BindingModel = new RegisterBindingModel
                    {
                        UserName = username,
                        Email = email,
                        Password = password,
                        ConfirmPassword = confirmpassword
                    },
                    Roles = new Roles(roles)
                };

                RegisterUser(registerUserModel);
            }
        }

        private void RegisterUser(RegisterUserModel newUserModel)
        {
            _serviceHandler.RegisterNewUser(newUserModel);
        }

        private string GetServiceLocationFromAppConfig()
        {
            return ConfigurationManager.AppSettings[KeyServiceLocation];
        }

        private bool VerifyNewUserCreds(string username, string email, string confirmEmail, string password, string confirmpassword)
        {
            if (!email.Equals(confirmEmail))
            {
                throw new Exception("Email must match confirm email field");
            }

            if (!password.Equals(confirmpassword))
            {
                throw new Exception("Password much match confirm password field");
            }

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
