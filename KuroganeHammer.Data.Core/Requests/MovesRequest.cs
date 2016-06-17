using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using KuroganeHammer.Data.Core.Models;

namespace KuroganeHammer.Data.Core.Requests
{
    public class MovesRequest : Request
    {
        public MovesRequest(HttpClient client)
            : base(client)
        { }

        public async Task<IEnumerable<Move>> GetMoves()
        {
            var response = await Client.GetAsync(Client.BaseAddress.AbsoluteUri + "/moves");

            response.EnsureSuccessStatusCode();

            var moves = await response.Content.ReadAsAsync<List<Move>>();

            return moves;
        }

        public async Task<IEnumerable<Move>> GetMovesForCharacter(int id)
        {
            Guard.VerifyObjectNotNull(id, nameof(id));

            var response = await Client.GetAsync($"{Client.BaseAddress.AbsoluteUri}/characters/{id}/moves");
            response.EnsureSuccessStatusCode();

            var moves = await response.Content.ReadAsAsync<List<Move>>();

            return moves;
        }
    }
}
