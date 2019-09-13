using FrannHammer.Domain.Contracts;
using System;
using System.Globalization;

namespace FrannHammer.Api.Services
{
    public class GameParameterParserService : IGameParameterParserService
    {
        private readonly string _game;

        public GameParameterParserService(string game)
        {
            _game = game;
        }

        public Games ParseGame()
        {
            string adjustedCasingGame = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_game);
            Enum.TryParse(adjustedCasingGame, out Games _parsedGame);

            return _parsedGame;
        }
    }
}
