﻿using AutoMapper;
using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.NetCore.WebApi.HypermediaServices
{
    public class EnrichmentProvider : IEnrichmentProvider
    {
        private readonly ILinkProvider _linkProvider;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly HttpContext _httpContext;

        public EnrichmentProvider(ILinkProvider linkProvider, IMapper mapper, LinkGenerator linkGenerator, HttpContext httpContext)
        {
            _linkProvider = linkProvider;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _httpContext = httpContext;
        }

        public CharacterResource EnrichCharacter(ICharacter character)
        {
            var enricher = new CharacterResourceEnricher(_linkProvider, _mapper, _linkGenerator, _httpContext);
            return enricher.Enrich(character);
        }

        public IEnumerable<CharacterResource> EnrichManyCharacters(IEnumerable<ICharacter> characters)
        {
            return characters.Select(EnrichCharacter);
        }

        public CharacterAttributeNameResource EnrichCharacterAttributeName(ICharacterAttributeName characterAttributeName)
        {
            var enricher = new CharacterAttributeNameResourceEnricher(_linkProvider, _mapper, _linkGenerator, _httpContext);
            return enricher.Enrich(characterAttributeName);
        }

        public IEnumerable<CharacterAttributeNameResource> EnrichManyCharacterAttributeNames(IEnumerable<ICharacterAttributeName> characterAttributeNames)
        {
            return characterAttributeNames.Select(EnrichCharacterAttributeName);
        }

        public CharacterAttributeRowResource EnrichCharacterAttributeRow(ICharacterAttributeRow characterAttributeRow)
        {
            var enricher = new CharacterAttributeRowResourceEnricher(_linkProvider, _mapper, _linkGenerator, _httpContext);
            return enricher.Enrich(characterAttributeRow);
        }

        public IEnumerable<CharacterAttributeRowResource> EnrichManyCharacterAttributeRowResources(IEnumerable<ICharacterAttributeRow> characterAttributeRows)
        {
            return characterAttributeRows.Select(EnrichCharacterAttributeRow);
        }

        public IMoveResource EnrichMove(IMove move, bool expand = false)
        {
            var enricher = new MoveResourceEnricher(_linkProvider, _mapper, _linkGenerator, _httpContext);
            return enricher.Enrich(move, expand);
        }

        public IEnumerable<IMoveResource> EnrichManyMoves(IEnumerable<IMove> moves, bool expand = false)
        {
            return moves.Select(m => EnrichMove(m, expand));
        }

        public MovementResource EnrichMovement(IMovement movement)
        {
            var enricher = new MovementResourceEnricher(_linkProvider, _mapper, _linkGenerator, _httpContext);
            return enricher.Enrich(movement);
        }

        public IEnumerable<MovementResource> EnrichManyMovements(IEnumerable<IMovement> movements)
        {
            return movements.Select(EnrichMovement);
        }

        public dynamic EnrichUniqueData(IUniqueData uniqueData)
        {
            var enricher = new UniqueDataResourceEnricher(_linkProvider, _mapper, _linkGenerator, _httpContext);
            return enricher.Enrich(uniqueData);
        }

        public IEnumerable<dynamic> EnrichManyUniqueDatas(IEnumerable<IUniqueData> uniqueDatas)
        {
            return uniqueDatas.Select(EnrichUniqueData);
        }
    }
}
