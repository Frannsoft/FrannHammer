using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FrannHammer.Core.Models;

namespace FrannHammer.Core.Requests
{
    public class MovesRequest : Request
    {
        public MovesRequest(HttpClient client)
            : base(client)
        { }

        public async Task<IEnumerable<Move>> GetMoves()
        {
            var response = await ExecuteAsync(async () => await Client.GetAsync(Client.BaseAddress.AbsoluteUri + "/moves"));

            var moves = await response.Content.ReadAsAsync<List<Move>>();

            return moves;
        }

        public async Task<IEnumerable<Move>> GetMovesForCharacter(int id)
        {
            Guard.VerifyObjectNotNull(id, nameof(id));

            var response = await ExecuteAsync(async () => await Client.GetAsync($"{Client.BaseAddress.AbsoluteUri}/characters/{id}/moves"));
            response.EnsureSuccessStatusCode();

            var moves = await response.Content.ReadAsAsync<List<Move>>();

            return moves;
        }
    }
}
