using System.Collections.Generic;
using System.Linq;
using FrannHammer.Models;
using FrannHammer.Models.DTOs;

namespace FrannHammer.Services
{
    public interface ISmashAttributeTypeService : IMetadataService
    {
        IEnumerable<dynamic> GetAllCharacterAttributeOfSmashAttributeType(int id, string fields = "");
    }

    public class SmashAttributeTypeService : MetadataService, ISmashAttributeTypeService
    {
        public SmashAttributeTypeService(IApplicationDbContext db)
            : base(db)
        { }

        public IEnumerable<dynamic> GetAllCharacterAttributeOfSmashAttributeType(int id, string fields = "")
        {
            SmashAttributeType smashAttributeType = Db.SmashAttributeTypes.Find(id);

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
                        .AsQueryable();

            return BuildContentResponseMultiple<CharacterAttributeRowDto, CharacterAttributeRowDto>(characterAttributeRows, fields);
        }
    }
}
