using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using KuroganeHammer.Data.Core.Models;

namespace KuroganeHammer.Data.Core.Requests
{
    public class MovementsRequest : Request
    {
        public MovementsRequest(HttpClient client)
            : base(client)
        { }

        public async Task<Movement> GetMovement(int id)
        {
            var response = await Client.GetAsync(Client.BaseAddress.AbsoluteUri + "/movements/" + id);
            response.EnsureSuccessStatusCode();

            var movement = await response.Content.ReadAsAsync<Movement>();

            return movement;
        }

        public async Task<IEnumerable<Movement>> GetMovementsForCharacter(int id)
        {
            Guard.VerifyObjectNotNull(id, nameof(id));

            var response = await Client.GetAsync($"{Client.BaseAddress.AbsoluteUri}/characters/{id}/movements");
            response.EnsureSuccessStatusCode();

            var movements = await response.Content.ReadAsAsync<List<Movement>>();

            return movements;
        }
    }
}
