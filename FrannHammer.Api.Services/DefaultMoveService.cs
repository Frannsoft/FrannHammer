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

            //case-insensitive check for matching property name
            var propertyInfo = registeredMoveType.GetProperties(BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(p => p.Name.IndexOf(property, 0, StringComparison.CurrentCultureIgnoreCase) != -1);

            if (propertyInfo == null)
            { throw new ArgumentException($"Move property name of '{property}' does not exist on {registeredMoveType.Name}."); }

            //TODO - check if the parsed data exists in redis cache before starting to parse it
            //TODO - if it is present, just pull it from the cache and return it.  No need to parse in this case.

            //TODO - if not, parse the data and cache it then return 
            //it from this method (after handling 'fields' parameter data if not empty)'
            //parse into strongly typed result...
            var moves = GetAllWhereName(name);

            var requestedRawPropertyForAllMatchingMoves = moves.Select(move => new { RawValue = propertyInfo.GetValue(move)?.ToString() });

            //Pull back all parser type attributes for the property being parsed.
            //If there are more than one assigned, pick the one that matches the property 
            //name value passed into this method.  
            //This allows for multiple types of parsing on single properties - 
            //something that is necessary for base/set knockback.
            var propertyParserAttributes = propertyInfo.GetCustomAttributes<PropertyParserAttribute>().ToList();

            var parserType = default(Type);
            if (propertyParserAttributes.Count > 1)
            {
                parserType = propertyParserAttributes.First(p => p.MatchingPropertyName == property)?.ParserType;
            }
            else if (propertyParserAttributes.Count > 0)
            {
                parserType = propertyParserAttributes[0].ParserType;
            }

            //if parserType is null, just return raw properties instead of throwing an exception.  The consumer doesn't necessarily care.
            if (parserType == null)
            {
                return requestedRawPropertyForAllMatchingMoves.Select(p =>
                new Dictionary<string, string>
                {
                    { RawValueKey, p.RawValue },
                    { MoveNameKey, name }
                });
            }

            //create an instance of the parser
            var parser = (PropertyParser)Activator.CreateInstance(parserType);

            var strongTypedPropertyInEachMove = requestedRawPropertyForAllMatchingMoves
                .Select(rawProperty =>
                {
                    var parsedData = parser.Parse(rawProperty.RawValue);
                    parsedData[MoveNameKey] = name;
                    return parsedData;
                })
                .ToList();

            return strongTypedPropertyInEachMove;
        }
    }
}
