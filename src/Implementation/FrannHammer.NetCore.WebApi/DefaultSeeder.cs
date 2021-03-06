﻿using AutoMapper;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.Domain;
using FrannHammer.Seeding.Contracts;
using FrannHammer.Utility;
using FrannHammer.WebScraping.Contracts.Character;
using FrannHammer.WebScraping.Domain.Contracts;

namespace FrannHammer.NetCore.WebApi
{
    //public class DefaultSeeder : ISeeder
    //{
    //    private readonly ICharacterDataScraper _characterDataScraper;

    //    public DefaultSeeder(ICharacterDataScraper characterDataScraper)
    //    {
    //        Guard.VerifyObjectNotNull(characterDataScraper, nameof(characterDataScraper));
    //        _characterDataScraper = characterDataScraper;
    //        //Mapper.Initialize(config =>
    //        //{
    //        //    config.CreateMap<WebCharacter, Character>();
    //        //});
    //    }

    //    public void SeedCharacterData(WebCharacter character,
    //        ICharacterService characterService,
    //        IMovementService movementService,
    //        IMoveService moveService,
    //        ICharacterAttributeRowService characterAttributeRowService,
    //        IUniqueDataService uniqueDataService)
    //    {
    //        Guard.VerifyObjectNotNull(character, nameof(character));
    //        Guard.VerifyObjectNotNull(characterService, nameof(characterService));
    //        Guard.VerifyObjectNotNull(movementService, nameof(movementService));
    //        Guard.VerifyObjectNotNull(moveService, nameof(moveService));
    //        Guard.VerifyObjectNotNull(characterAttributeRowService, nameof(characterAttributeRowService));
    //        Guard.VerifyObjectNotNull(uniqueDataService, nameof(uniqueDataService));

    //        //_characterDataScraper.PopulateCharacterFromWeb(character);

    //        var entityCharacter = Mapper.Map<Character>(character);
    //        characterService.Add(entityCharacter);
    //        movementService.AddMany(character.Movements);
    //        moveService.AddMany(character.Moves);
    //        characterAttributeRowService.AddMany(character.AttributeRows);
    //        uniqueDataService.AddMany(character.UniqueProperties); 
    //    }
    //}
}
