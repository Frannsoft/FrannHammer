using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FrannHammer.Core;
using FrannHammer.Core.Requests;
using FrannHammer.DataSynchro.Models;
using FrannHammer.Models;
using FrannHammer.Models.DTOs;

namespace FrannHammer.DataSynchro.ViewModels
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

        private CharacterMetadataTypes _selectedType;
        public CharacterMetadataTypes SelectedType
        {
            get { return _selectedType; }
            private set
            {
                _selectedType = value;
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

        public void SetSelectedDataType(CharacterMetadataTypes type) => SelectedType = type;

        public BaseModel CreateNewType()
        {
            var model = default(BaseModel);
            switch (SelectedType)
            {
                case CharacterMetadataTypes.CharacterAttributes:
                    {
                        model = new CharacterAttributeDto();
                        break;
                    }
                case CharacterMetadataTypes.Movements:
                    {
                        model = new Movement();
                        break;
                    }
                case CharacterMetadataTypes.Moves:
                    {
                        model = new Move();
                        break;
                    }
                default:
                {
                    throw new InvalidOperationException("Unable to determine the type of object to be created.");
                }
            }

            return model;
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
