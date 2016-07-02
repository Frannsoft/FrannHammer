using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FrannHammer.Models;

namespace FrannHammer.Core.Requests
{
    public class MovementsRequest : Request
    {
        public MovementsRequest(HttpClient client)
            : base(client)
        { }

        public async Task<Movement> GetMovement(int id)
        {
            var response = await ExecuteAsync(async() => await Client.GetAsync(
                $"{Client.BaseAddress.AbsoluteUri}/movements/{id}"));

            var movement = await response.Content.ReadAsAsync<Movement>();

            return movement;
        }

        public async Task<IEnumerable<Movement>> GetMovementsForCharacter(int id)
        {
            Guard.VerifyObjectNotNull(id, nameof(id));

            var response = await ExecuteAsync(async () => await Client.GetAsync($"{Client.BaseAddress.AbsoluteUri}/characters/{id}/movements"));

            var movements = await response.Content.ReadAsAsync<List<Movement>>();

            return movements;
        }
    }
}
