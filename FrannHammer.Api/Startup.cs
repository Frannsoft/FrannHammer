using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using FrannHammer.Api;
using FrannHammer.Models;
using FrannHammer.Models.DTOs;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace FrannHammer.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            app.UseCors(CorsOptions.AllowAll);
            ConfigureAuth(app, Container.Instance.AutoFacContainer);

            ConfigureAutoMapping();

        }

        internal static void ConfigureAutoMapping()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Autocancel, AutocancelDto>();
                cfg.CreateMap<AutocancelDto, Autocancel>();
                cfg.CreateMap<LandingLag, LandingLagDto>();
                cfg.CreateMap<LandingLagDto, LandingLag>();
                cfg.CreateMap<FirstActionableFrame, FirstActionableFrameDto>();
                cfg.CreateMap<FirstActionableFrameDto, FirstActionableFrame>();
                cfg.CreateMap<Throw, ThrowDto>();
                cfg.CreateMap<ThrowDto, Throw>();
                cfg.CreateMap<ThrowType, ThrowTypeDto>();
                cfg.CreateMap<ThrowTypeDto, ThrowType>();
                cfg.CreateMap<Notation, NotationDto>();
                cfg.CreateMap<Angle, AngleDto>();
                cfg.CreateMap<AngleDto, Angle>();
                cfg.CreateMap<BaseDamage, BaseDamageDto>();
                cfg.CreateMap<BaseDamageDto, BaseDamage>();
                cfg.CreateMap<KnockbackGrowth, KnockbackGrowthDto>();
                cfg.CreateMap<KnockbackGrowthDto, KnockbackGrowth>();
                cfg.CreateMap<Hitbox, HitboxDto>();
                cfg.CreateMap<HitboxDto, Hitbox>();
                cfg.CreateMap<Character, CharacterDto>();
                cfg.CreateMap<CharacterDto, Character>();
                cfg.CreateMap<Movement, MovementDto>();
                cfg.CreateMap<MovementDto, Movement>();
                cfg.CreateMap<Move, MoveDto>();
                cfg.CreateMap<MoveDto, Move>();
                cfg.CreateMap<Move, MoveSearchDto>();
                cfg.CreateMap<MoveSearchDto, Move>();
                cfg.CreateMap<SmashAttributeType, SmashAttributeTypeDto>();
                cfg.CreateMap<SmashAttributeTypeDto, SmashAttributeType>();
                cfg.CreateMap<CharacterAttributeType, CharacterAttributeTypeDto>();
                cfg.CreateMap<CharacterAttributeTypeDto, CharacterAttributeType>();
                cfg.CreateMap<CharacterAttribute, CharacterAttributeDto>();
                cfg.CreateMap<CharacterAttributeDto, CharacterAttribute>();
                cfg.CreateMap<BaseKnockback, BaseKnockbackDto>();
                cfg.CreateMap<BaseKnockbackDto, BaseKnockback>();
                cfg.CreateMap<SetKnockback, SetKnockbackDto>();
                cfg.CreateMap<SetKnockbackDto, SetKnockback>();
            });
        }
    }
}
