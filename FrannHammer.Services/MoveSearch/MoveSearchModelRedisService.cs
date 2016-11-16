using FrannHammer.Models;
using Newtonsoft.Json;

namespace FrannHammer.Services.MoveSearch
{
    public class MoveSearchModelRedisService
    {
        public string MoveSearchModelToRedisKey(MoveSearchModel moveSearchModel)
        {
            return moveSearchModel == null ? string.Empty : JsonConvert.SerializeObject(moveSearchModel);

            //return $"{nameof(moveSearchModel.Name)}:{moveSearchModel.Name};" +
            //       $"{nameof(MoveSearchModel.CharacterName)}:{moveSearchModel.CharacterName};" +
            //       $"{nameof(MoveSearchModel.Angle)}:{RangeModelToRedisKey(moveSearchModel.Angle)}" +
            //       $"{nameof(MoveSearchModel.AutoCancel)}:{RangeModelToRedisKey(moveSearchModel.AutoCancel)}" +
            //       $"{nameof(MoveSearchModel.BaseDamage)}:{RangeModelToRedisKey(moveSearchModel.BaseDamage)}" +
            //       $"{nameof(MoveSearchModel.BaseKnockback)}:{RangeModelToRedisKey(moveSearchModel.BaseKnockback)}" +
            //       $"{nameof(MoveSearchModel.FirstActionableFrame)}:{RangeModelToRedisKey(moveSearchModel.FirstActionableFrame)}" +
            //       $"{nameof(MoveSearchModel.HitboxActiveLength)}:{RangeModelToRedisKey(moveSearchModel.HitboxActiveLength)}" +
            //       $"{nameof(MoveSearchModel.HitboxActiveOnFrame)}:{RangeModelToRedisKey(moveSearchModel.HitboxActiveOnFrame)}" +
            //       $"{nameof(MoveSearchModel.HitboxStartupFrame)}:{RangeModelToRedisKey(moveSearchModel.HitboxStartupFrame)}" +
            //       $"{nameof(MoveSearchModel.KnockbackGrowth)}:{RangeModelToRedisKey(moveSearchModel.KnockbackGrowth)}" +
            //       $"{nameof(MoveSearchModel.LandingLag)}:{RangeModelToRedisKey(moveSearchModel.LandingLag)}" +
            //       $"{nameof(MoveSearchModel.SetKnockback)}:{RangeModelToRedisKey(moveSearchModel.SetKnockback)};";
        }

        private string RangeModelToRedisKey(RangeModel rangeModel)
        {
            if (rangeModel == null)
            {
                return string.Empty;
            }

            return $"{nameof(RangeModel.StartValue)}:{rangeModel.StartValue},{nameof(RangeModel.RangeQuantifier)}:{rangeModel.RangeQuantifier}," +
                   $"{nameof(RangeModel.EndValue)}:{rangeModel.EndValue};";
        }
    }
}
