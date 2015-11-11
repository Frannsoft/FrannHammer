using KuroganeHammer.Data.Core.Model.Characters;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using KuroganeHammer.Data.Core.Model.Stats;
using System.Net;
using System;
using Kurogane.Data.RestApi.DTOs;
using Newtonsoft.Json;

namespace KurograneTransferDBTool
{
    public class Smoke : BaseTest
    {


        [Test]
        public async Task GetCharacter()
        {
            var getResult = await client.GetAsync(BASEURL + "characters/" + 1);
            string content = await getResult.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);
        }

        [Test]
        public async Task GetMoves()
        {
            var getResult = await client.GetAsync(BASEURL + "moves");
            string content = await getResult.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);
        }

        [Test]
        public async Task GetMoveById()
        {
            var getResult = await client.GetAsync(BASEURL + "moves/" + 20);
            string content = await getResult.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);
        }

        [Test]
        public async Task GetMovesOfType()
        {
            var getResult = await client.GetAsync(BASEURL + "movesoftype/" + MoveType.Ground);
            string content = await getResult.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);
        }

        [Test]
        public void GetCharacterFromKurogane()
        {
            var chara = Character.FromId(7);
        }


      
       
    }
}
