using System.Collections.ObjectModel;
using KuroganeHammer.Data.Core.Models;
using KuroganeHammer.Data.Core.Requests;
using KuroganeHammer.DataSynchro.Models;

namespace KuroganeHammer.DataSynchro.ViewModels
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

        private ObservableCollection<Move> _moves;
        public ObservableCollection<Move> Moves
        {
            get { return _moves; }
            private set
            {
                _moves = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public MainVm(UserModel user)
            : base(user)
        { }

        public void RefreshCharacters()
        {
            var characters = new CharacterRequest(LoggedInUser.LoggedInClient).GetCharacters();
            Characters = new ObservableCollection<Character>(characters);
        }

        public void RefreshMoves()
        {
            var moves = new MovesRequest(LoggedInUser.LoggedInClient).GetMoves();
            Moves = new ObservableCollection<Move>(moves);
        }

        public void Logout()
        {
            LoggedInUser.Logout();
        }


    }
}
