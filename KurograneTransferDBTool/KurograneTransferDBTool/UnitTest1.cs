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
    public class UnitTest1
    {
        [Test]
        public async Task UpdateCharacter()
        {
            HttpClient client = new HttpClient();

            var get = await client.GetAsync("http://localhost:53410/api/" + (int)Characters.BOWSER + "/character");

            CharacterDTO bowser = JsonConvert.DeserializeObject<CharacterDTO>(await get.Content.ReadAsStringAsync());

            bowser.Name = "NEWNAME";

            var result = await client.PutAsJsonAsync("http://localhost:53410/api/" + (int)Characters.BOWSER + "/character", bowser);
        }


        [Test]
        [TestCase((int)Characters.KIRBY)]
        [TestCase((int)Characters.MIIBRAWLER)]
        [TestCase((int)Characters.MIIGUNNER)]
        public async Task LoadData(int id)
        {
            Character character = new Character((Characters)id);

            HttpClient client = new HttpClient();

            CharacterDTO model = new CharacterDTO()
            {
                FrameDataVersion = character.FrameDataVersion,
                FullUrl = character.FullUrl,
                Name = character.Name,
                OwnerId = character.OwnerId
            };

            var result = await client.PostAsJsonAsync("http://localhost:53410/api/" + id + "/character", model);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            foreach (var movementStat in character.FrameData.Values.OfType<MovementStat>())
            {
                var resultMovement = await client.PostAsJsonAsync("http://localhost:53410/api/" + id + "/movement", movementStat);
                Assert.AreEqual(HttpStatusCode.OK, resultMovement.StatusCode);
            }

            foreach (var groundStat in character.FrameData.Values.OfType<GroundStat>())
            {
                var resultGround = await client.PostAsJsonAsync("http://localhost:53410/api/" + id + "/ground", groundStat);
                Assert.AreEqual(HttpStatusCode.OK, resultGround.StatusCode);
            }


            foreach (var aerialStat in character.FrameData.Values.OfType<AerialStat>())
            {
                var resultAerial = await client.PostAsJsonAsync("http://localhost:53410/api/" + id + "/aerial", aerialStat);
                Assert.AreEqual(HttpStatusCode.OK, resultAerial.StatusCode);
            }

            foreach (var specialStat in character.FrameData.Values.OfType<SpecialStat>())
            {
                var resultSpecial = await client.PostAsJsonAsync("http://localhost:53410/api/" + id + "/special", specialStat);
                Assert.AreEqual(HttpStatusCode.OK, resultSpecial.StatusCode);
            }
        }

        [Test]
        public async Task LoadCharacterData()
        {
            int[] characters = (int[])Enum.GetValues(typeof(Characters));

            foreach (int c in characters)
            {
                Character character = Character.FromId(c);

                HttpClient client = new HttpClient();

                CharacterDTO model = new CharacterDTO()
                {
                    FrameDataVersion = character.FrameDataVersion,
                    FullUrl = character.FullUrl,
                    Name = character.Name,
                    OwnerId = character.OwnerId
                };

                var result = await client.PostAsJsonAsync("http://localhost:53410/api/" + c + "/character", model);
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            }
        }

        [Test]
        public async Task LoadMovementData()
        {
            int[] characters = (int[])Enum.GetValues(typeof(Characters));

            foreach (int c in characters)
            {
                Character character = Character.FromId(c);

                HttpClient client = new HttpClient();

                foreach (var movementStat in character.FrameData.Values.OfType<MovementStat>())
                {
                    var result = await client.PostAsJsonAsync("http://localhost:53410/api/movement", movementStat);
                    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
                }
            }
        }

        [Test]
        public async Task LoadGroundData()
        {
            int[] characters = (int[])Enum.GetValues(typeof(Characters));

            foreach (int c in characters)
            {
                Character character = Character.FromId(c);

                HttpClient client = new HttpClient();

                foreach (var groundStat in character.FrameData.Values.OfType<GroundStat>())
                {
                    var result = await client.PostAsJsonAsync("http://localhost:53410/api/ground", groundStat);
                    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
                }
            }
        }

        [Test]
        public async Task LoadAerialData()
        {
            int[] characters = (int[])Enum.GetValues(typeof(Characters));

            foreach (int c in characters)
            {
                Character character = Character.FromId(c);

                HttpClient client = new HttpClient();

                foreach (var aerialStat in character.FrameData.Values.OfType<AerialStat>())
                {
                    var result = await client.PostAsJsonAsync("http://localhost:53410/api/aerial", aerialStat);
                    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
                }
            }
        }

        [Test]
        public async Task LoadSpecialData()
        {
            int[] characters = (int[])Enum.GetValues(typeof(Characters));

            foreach (int c in characters)
            {
                Character character = Character.FromId(c);

                HttpClient client = new HttpClient();

                foreach (var specialStat in character.FrameData.Values.OfType<SpecialStat>())
                {
                    var result = await client.PostAsJsonAsync("http://localhost:53410/api/special", specialStat);
                    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
                }
            }
        }

        [Test]
        public void GetCharacterFromKurogane()
        {
            var chara = Character.FromId(7);
        }
    }
}
