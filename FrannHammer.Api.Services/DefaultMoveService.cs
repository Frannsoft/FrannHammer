using System;
using System.Collections.Generic;
using FrannHammer.DataAccess.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.Utility;
using System.Linq;
using System.Reflection;
using FrannHammer.Domain;
using FrannHammer.Domain.PropertyParsers;
using static FrannHammer.Domain.PropertyParsers.MoveDataNameConstants;

namespace FrannHammer.Api.Services
{
    public class DefaultMoveService : BaseApiService<IMove>, IMoveService
    {
        public DefaultMoveService(IRepository<IMove> repository)
            : base(repository)
        { }

        public IEnumerable<IDictionary<string, string>> GetAllPropertyDataWhereName(string name, string property, string fields = "")
        {
            Guard.VerifyStringIsNotNullOrEmpty(name, nameof(name));
            Guard.VerifyStringIsNotNullOrEmpty(property, nameof(property));

            var registeredMoveType = MoveParseClassMap.GetRegisteredImplementationTypeFor<IMove>();
            var propertyInfo = registeredMoveType.GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);

            if (propertyInfo == null)
            { throw new ArgumentException($"Move property name of '{property}' does not exist on {registeredMoveType.Name}."); }

            //TODO - check if the parsed data exists in redis cache before starting to parse it
            //TODO - if it is present, just pull it from the cache and return it.  No need to parse in this case.

            //TODO - if not, parse the data and cache it then return 
            //it from this method (after handling 'fields' parameter data if not empty)'
            //parse into strongly typed result...
            var moves = GetAllWhereName(name);

            var requestedRawPropertyForAllMatchingMoves = moves.Select(move => new { Property = propertyInfo.GetValue(move).ToString() });

            var parserType = propertyInfo?.GetCustomAttribute<PropertyParserAttribute>()?.ParserType;

            //if parserType is null, just return raw properties instead of throwing an exception.  The consumer doesn't necessarily care.
            if (parserType == null)
            {
                return requestedRawPropertyForAllMatchingMoves.Select(p =>
                new Dictionary<string, string>
                {
                    { "Property", p.Property },
                    { MoveNameKey, name }
                });
            }

            //create an instance of the parser
            var parser = (PropertyParser)Activator.CreateInstance(parserType);

            var strongTypedPropertyInEachMove = requestedRawPropertyForAllMatchingMoves
                .Select(rawProperty =>
                {
                    var parsedData = parser.Parse(rawProperty.Property);
                    parsedData[MoveNameKey] = name;
                    return parsedData;
                })
                .ToList();

            return strongTypedPropertyInEachMove;
        }
    }
}
