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
using FrannHammer.WebScraping;
using static FrannHammer.Domain.PropertyParsers.MoveDataNameConstants;

namespace FrannHammer.Api.Services
{
    public class DefaultMoveService : OwnerBasedApiService<IMove>, IMoveService
    {
        private readonly IQueryMappingService _queryMappingService;

        public DefaultMoveService(IRepository<IMove> repository, IQueryMappingService queryMappingService)
            : base(repository)
        {
            Guard.VerifyObjectNotNull(queryMappingService, nameof(queryMappingService));

            _queryMappingService = queryMappingService;
        }

        public IEnumerable<IDictionary<string, string>> GetAllPropertyDataWhereName(string name, string property)
        {
            Guard.VerifyStringIsNotNullOrEmpty(name, nameof(name));
            Guard.VerifyStringIsNotNullOrEmpty(property, nameof(property));

            //case-insensitive check for matching property name
            var propertyInfo = FindPropertyInfo(property);

            //TODO - check if the parsed data exists in redis cache before starting to parse it
            //TODO - if it is present, just pull it from the cache and return it.  No need to parse in this case.

            //TODO - if not, parse the data and cache it then return 
            //it from this method (after handling 'fields' parameter data if not empty)'
            //parse into strongly typed result...
            var moves = GetAllWhereName(name).ToList();

            if (moves.Count == 0)
            {
                return null;
            }

            var requestedRawPropertyForAllMatchingMoves = moves.Select(move => new { RawValue = propertyInfo.GetValue(move)?.ToString() });

            var parserType = ExtractParserType(propertyInfo, property);

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

        public IEnumerable<ParsedMove> GetAllMovePropertyDataForCharacter(ICharacter character)
        {
            var allMovesForCharacter = GetAllWhere(move => move.OwnerId == character.OwnerId).ToList();

            var parsedMoves = new List<ParsedMove>();

            foreach (var move in allMovesForCharacter)
            {
                var parsedMove = new ParsedMove
                {
                    MoveName = move.Name,
                    Owner = move.Owner,
                    OwnerId = move.OwnerId
                };

                var properties = move.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(prop => prop.GetCustomAttributes<PropertyParserAttribute>().Any());

                foreach (var property in properties)
                {
                    var parsedMoveDataProperty = new ParsedMoveDataProperty
                    {
                        Name = property.Name
                    };

                    var parserType = default(Type);
                    string rawValue = property.GetValue(move)?.ToString();

                    //in order to properly handle the two properties combined here, special logic is 
                    //required.  Not ideal, but neither is the way these are listed on the site imho.
                    //I can't separate them while seeding the db because it would break backwards
                    //compatibility.
                    if (property.Name == nameof(IMove.BaseKnockBackSetKnockback))
                    {
                        parserType = ExtractParserType(property, FriendlyNameMoveCommonConstants.BaseKnockbackName) ??
                                     ExtractParserType(property, FriendlyNameMoveCommonConstants.SetKnockbackName);

                        ParseData(parserType, rawValue, parsedMove, parsedMoveDataProperty, move);
                    }
                    else
                    {
                        parserType = ExtractParserType(property, property.Name);
                        ParseData(parserType, rawValue, parsedMove, parsedMoveDataProperty, move);
                    }
                }
                parsedMoves.Add(parsedMove);
            }

            return parsedMoves;
        }

        private static void ParseData(Type parserType, string rawValue, ParsedMove parsedMove, ParsedMoveDataProperty parsedMoveDataProperty, IMove move)
        {
            if (parserType == null)
            {
                parsedMove.MoveProperties.Add(parsedMoveDataProperty);
            }
            else
            {
                var parser = (PropertyParser)Activator.CreateInstance(parserType);
                var parsedData = parser.Parse(rawValue);
                foreach (var parsed in parsedData)
                {
                    parsedMoveDataProperty.AddParsedMoveAttribute(
                        new ParsedMoveAttribute
                        {
                            Name = parsed.Key,
                            Value = parsed.Value
                        });
                }

                parsedMove.MoveProperties.Add(parsedMoveDataProperty);
            }
        }

        public IDictionary<string, string> GetPropertyDataWhereId(string id, string property)
        {
            Guard.VerifyStringIsNotNullOrEmpty(id, nameof(id));
            Guard.VerifyStringIsNotNullOrEmpty(property, nameof(property));

            //case-insensitive check for matching property name
            var propertyInfo = FindPropertyInfo(property);

            //TODO - check if the parsed data exists in redis cache before starting to parse it
            //TODO - if it is present, just pull it from the cache and return it.  No need to parse in this case.

            //TODO - if not, parse the data and cache it then return 
            //it from this method (after handling 'fields' parameter data if not empty)'
            //parse into strongly typed result...
            var move = GetSingleByInstanceId(id);


            if (move == null)
            {
                return null;
            }

            var requestedRawPropertyForMatchingMove = propertyInfo.GetValue(move)?.ToString();

            var parserType = ExtractParserType(propertyInfo, property);

            //if parserType is null, just return raw properties instead of throwing an exception.  The consumer doesn't necessarily care.
            if (parserType == null)
            {
                return new Dictionary<string, string>
                {
                    { RawValueKey, requestedRawPropertyForMatchingMove },
                    { MoveNameKey, move.Name }
                };
            }

            //create an instance of the parser
            var parser = (PropertyParser)Activator.CreateInstance(parserType);

            var parsedData = parser.Parse(requestedRawPropertyForMatchingMove);
            parsedData[MoveNameKey] = move.Name;
            return parsedData;
        }

        private static PropertyInfo FindPropertyInfo(string property)
        {
            var registeredMoveType = MoveParseClassMap.GetRegisteredImplementationTypeFor<IMove>();

            //case-insensitive check for matching property name
            var propertyInfo = registeredMoveType.GetProperties(BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(p => p.Name.IndexOf(property, 0, StringComparison.CurrentCultureIgnoreCase) != -1);

            if (propertyInfo == null)
            { throw new ArgumentException($"Move property name of '{property}' does not exist on {registeredMoveType.Name}."); }

            return propertyInfo;
        }

        private static Type ExtractParserType(MemberInfo propertyInfo, string property)
        {
            //Pull back all parser type attributes for the property being parsed.
            //If there are more than one assigned, pick the one that matches the property 
            //name value passed into this method.  
            //This allows for multiple types of parsing on single properties - 
            //something that is necessary for base/set knockback.
            var propertyParserAttributes = propertyInfo.GetCustomAttributes<PropertyParserAttribute>().ToList();

            var parserType = default(Type);
            if (propertyParserAttributes.Count > 1)
            {
                parserType = propertyParserAttributes.FirstOrDefault(p => p.MatchingPropertyName == property)?.ParserType;
            }
            else if (propertyParserAttributes.Count > 0)
            {
                parserType = propertyParserAttributes[0].ParserType;
            }

            return parserType;
        }

        public IEnumerable<IMove> GetAllThrowsWhereCharacterNameIs(string name)
        {
            Guard.VerifyStringIsNotNullOrEmpty(name, nameof(name));

            var throwMoves = GetAllWhere(move => move.Owner.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                                                 move.MoveType == MoveType.Throw.GetEnumDescription());

            return throwMoves;
        }

        public IEnumerable<IMove> GetAllThrowsWhereCharacterOwnerIdIs(int ownerId)
        {
            var throwMoves = GetAllWhere(move => move.OwnerId == ownerId &&
                                                 move.MoveType == MoveType.Throw.GetEnumDescription());

            return throwMoves;
        }

        public IEnumerable<IMove> GetAllWhere(IMoveFilterResourceQuery query)
        {
            var queryFilterParameters = _queryMappingService.MapResourceQueryToDictionary(query, BindingFlags.Public | BindingFlags.Instance);

            return GetAllWhere(queryFilterParameters);
        }

        public IEnumerable<IMove> GetAllThrowsForCharacter(ICharacter character)
        {
            Guard.VerifyObjectNotNull(character, nameof(character));
            Guard.VerifyStringIsNotNullOrEmpty(character.Name, nameof(character.Name));

            var throwMoves = GetAllWhere(move => move.Owner.Equals(character.Name) &&
                                                 move.MoveType == MoveType.Throw.GetEnumDescription());

            return throwMoves;
        }

        public IEnumerable<IMove> GetAllMovesForCharacter(ICharacter character)
        {
            var moves = GetAllWhere(move => move.OwnerId == character.OwnerId);
            return moves;
        }
    }
}
