using System.Collections.Generic;
using System.Net.Http;
using KuroganeHammer.Data.Core.Models;

namespace KuroganeHammer.Data.Core.Requests
{
    public class MovesRequest : Request
    {
        public MovesRequest(HttpClient client)
            : base(client)
        { }

        public IEnumerable<Move> GetMoves()
        {
            var response = Client.GetAsync(Client.BaseAddress.AbsoluteUri + "/moves").Result;

            response.EnsureSuccessStatusCode();

            var moves = response.Content.ReadAsAsync<List<Move>>().Result;

            return moves;
        }

        public IEnumerable<Move> GetMovesForCharacter(int id)
        {
            Guard.VerifyObjectNotNull(id, nameof(id));

            var response = Client.GetAsync($"{Client.BaseAddress.AbsoluteUri}/characters/{id}/moves").Result;
            response.EnsureSuccessStatusCode();

            var moves = response.Content.ReadAsAsync<List<Move>>().Result;

            return moves;
        }
    }
}
