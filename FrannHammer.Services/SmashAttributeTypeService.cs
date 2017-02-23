using System;
using System.Collections.Generic;
using System.Linq;
using FrannHammer.Models;
using FrannHammer.Models.DTOs;
using FrannHammer.Services.MoveSearch;

namespace FrannHammer.Services
{
    public interface ISmashAttributeTypeService : IMetadataService
    {
        /// <summary>
        /// Get all <see cref="CharacterAttribute"/> data for a <see cref="SmashAttributeType"/> by <paramref name="id"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        IEnumerable<dynamic> GetAllCharacterAttributeOfSmashAttributeType(int id, string fields = "");

        /// <summary>
        /// Get all <see cref="CharacterAttribute"/> data for a <see cref="SmashAttributeType"/> by <see cref="name"/>.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        IEnumerable<dynamic> GetAllCharacterAttributeOfSmashAttributeType(string name, string fields = "");
    }

    public class SmashAttributeTypeService : MetadataService, ISmashAttributeTypeService
    {
        public SmashAttributeTypeService(IApplicationDbContext db, IResultValidationService resultValidationService)
            : base(db, resultValidationService)
        { }

        public IEnumerable<dynamic> GetAllCharacterAttributeOfSmashAttributeType(int id, string fields = "")
        {
            SmashAttributeType smashAttributeType = Db.SmashAttributeTypes.Find(id);

            return smashAttributeType != null ?
                    GetAllCharacterAttributeOfSmashAttributeTypeCore(smashAttributeType, fields) :
                    null;
        }

        public IEnumerable<dynamic> GetAllCharacterAttributeOfSmashAttributeType(string name, string fields = "")
        {
            SmashAttributeType smashAttributeType =
                Db.SmashAttributeTypes.FirstOrDefault(
                    t => t.Name.Equals(name.ToUpper(), StringComparison.OrdinalIgnoreCase));

            return smashAttributeType != null ?
                    GetAllCharacterAttributeOfSmashAttributeTypeCore(smashAttributeType, fields) :
                    null;
        }

        private IEnumerable<dynamic> GetAllCharacterAttributeOfSmashAttributeTypeCore(SmashAttributeType smashAttributeType, string fields)
        {
            //create a 'row' from each pulled back characterattribute since a characterattribute only represents
            //a single cell of a row in the existing KH site.
            var characterAttributeRows =
                Db.CharacterAttributes.Where(c => c.SmashAttributeType.Id == smashAttributeType.Id)
                    .ToList() //execute query and bring into memory so I can continue to query against the data below
                    .GroupBy(a => a.OwnerId)
                    .Select(g => new CharacterAttributeRowDto(g.First().Rank, smashAttributeType.Name, smashAttributeType.Id,
                        g.Key, g.Select(at => new CharacterAttributeKeyValuePair()
                        {
                            KeyName = at.Name,
                            ValueCharacterAttribute = new CharacterAttributeDto
                            {
                                Id = at.Id,
                                Name = at.Name,
                                OwnerId = at.OwnerId,
                                Rank = at.Rank,
                                SmashAttributeTypeId = at.SmashAttributeTypeId,
                                Value = at.Value
                            }
                        }).ToList(),
                        Db.Characters.Find(g.Key).Name, Db.Characters.Find(g.Key).ThumbnailUrl))
                        .ToList();

            ResultValidationService.ValidateMultipleResult<CharacterAttributeRowDto, CharacterAttributeRowDto>(
                characterAttributeRows, smashAttributeType.Id);

            return BuildContentResponseMultiple<CharacterAttributeRowDto, CharacterAttributeRowDto>(characterAttributeRows, fields);
        }
    }
}
