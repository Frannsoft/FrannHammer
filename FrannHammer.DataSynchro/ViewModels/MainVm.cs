using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FrannHammer.Core.Requests;
using FrannHammer.DataSynchro.Models;
using FrannHammer.Models;

namespace FrannHammer.DataSynchro.ViewModels
{
    public class MainVm : BaseVm
    {

        #region Props

        public string Username => LoggedInUser.Username;

        private ObservableCollection<Character> _characters;
        public ObservableCollection<Character> Characters
        {
            get { return _characters; }
            private set
            {
                _characters = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public MainVm(UserModel user)
            : base(user)
        { }

        public async Task RefreshCharacters()
        {
            var characters = await ExecuteAsync(async() => await new CharacterRequest(LoggedInUser.LoggedInClient).GetCharacters());
            Characters = new ObservableCollection<Character>(characters);
        }

        public void Logout()
        {
            LoggedInUser.Logout();
        }


    }
}
