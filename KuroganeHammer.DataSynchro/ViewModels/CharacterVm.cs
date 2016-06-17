using System.Collections.ObjectModel;
using System.Threading.Tasks;
using KuroganeHammer.Data.Core;
using KuroganeHammer.Data.Core.DTOs;
using KuroganeHammer.Data.Core.Models;
using KuroganeHammer.Data.Core.Requests;
using KuroganeHammer.DataSynchro.Models;

namespace KuroganeHammer.DataSynchro.ViewModels
{
    public class CharacterVm : BaseVm
    {
        #region Props
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

        private ObservableCollection<Movement> _movements;
        public ObservableCollection<Movement> Movements
        {
            get { return _movements; }
            private set
            {
                _movements = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CharacterAttributeDto> _characterAttributes;
        public ObservableCollection<CharacterAttributeDto> CharacterAttributes
        {
            get { return _characterAttributes; }
            private set
            {
                _characterAttributes = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private readonly bool _isNewCharacter;
        private const string NewCharacterName = "New Character";

        public CharacterVm(Character character, UserModel user, bool isNewCharacter = false)
            : base(user)
        {
            Guard.VerifyObjectNotNull(character, nameof(character));

            _isNewCharacter = isNewCharacter;
            if (_isNewCharacter)
            { character.Name = NewCharacterName; }

            _character = character;
        }


        public async Task RefreshCharacter()
        {
            if (!_isNewCharacter)
            {
                Character = await
                    ExecuteAsync(
                        async () =>
                            await
                                new CharacterRequest(LoggedInUser.LoggedInClient)
                                    .GetCharacter(_character.Id));
            }
        }

        public async Task RefreshMoves()
        {
            if (!_isNewCharacter)
            {
                var moves = await
                    ExecuteAsync(
                        async () =>
                            await
                                new MovesRequest(LoggedInUser.LoggedInClient)
                                    .GetMovesForCharacter(_character.Id));

                Moves = new ObservableCollection<Move>(moves);
            }
        }

        public async Task RefreshMovements()
        {
            if (!_isNewCharacter)
            {
                var movements = await
                    ExecuteAsync(
                        async () =>
                            await
                                new MovementsRequest(LoggedInUser.LoggedInClient)
                                    .GetMovementsForCharacter(_character.Id));

                Movements = new ObservableCollection<Movement>(movements);
            }
        }

        public async Task RefreshCharacterAttributes()
        {
            if (!_isNewCharacter)
            {
                var attributes = await
                    ExecuteAsync(
                        async () =>
                            await
                                new CharacterRequest(LoggedInUser.LoggedInClient)
                                    .GetCharacterAttributesOfCharacter(_character.Id));

                CharacterAttributes = new ObservableCollection<CharacterAttributeDto>(attributes);
            }
        }
    }
}
