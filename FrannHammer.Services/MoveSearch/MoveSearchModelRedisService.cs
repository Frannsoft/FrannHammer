using FrannHammer.Models;
using Newtonsoft.Json;

namespace FrannHammer.Services.MoveSearch
{
    public class MoveSearchModelRedisService
    {
        public string MoveSearchModelToRedisKey(MoveSearchModel moveSearchModel, string fields = "")
        {
            if (moveSearchModel == null)
            {
                return string.Empty;
            }
            else
            {
                string results = JsonConvert.SerializeObject(moveSearchModel);

                if (!string.IsNullOrEmpty(fields))
                {
                    results += JsonConvert.SerializeObject(fields);
                }
                return results;
            }
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
