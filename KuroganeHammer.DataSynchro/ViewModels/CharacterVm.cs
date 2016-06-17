using System.Collections.ObjectModel;
using KuroganeHammer.Data.Core;
using KuroganeHammer.Data.Core.Models;
using KuroganeHammer.Data.Core.Requests;
using KuroganeHammer.DataSynchro.Models;

namespace KuroganeHammer.DataSynchro.ViewModels
{
    public class CharacterVm : BaseVm
    {
        private Character _character;
        public Character Character
        {
            get { return _character; }
            private set
            {
                _character = value;
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

        public CharacterVm(Character character, UserModel user)
            : base(user)
        {
            Guard.VerifyObjectNotNull(character, nameof(character));

            _character = character;
        }


        public void RefreshCharacter()
        {
            Character = new CharacterRequest(LoggedInUser.LoggedInClient).GetCharacter(_character.Id);
        }

        public void RefreshMoves()
        {
            var moves = new MovesRequest(LoggedInUser.LoggedInClient).GetMovesForCharacter(_character.Id);
            Moves = new ObservableCollection<Move>(moves);
        }
    }
}
