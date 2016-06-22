using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using FrannHammer.Core;
using Newtonsoft.Json;

namespace FrannHammer.WebScraper
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Character
    {
        private readonly Page _page;
        private const string UrlBase = "http://kuroganehammer.com/Smash4/";
        private readonly string _urlTail;

        [JsonProperty]
        public string FullUrl => UrlBase + _urlTail;

        [JsonProperty]
        public string MainImageUrl { get; private set; }

        [JsonProperty]
        public string ThumbnailUrl { get; set; }

        [JsonProperty]
        public string Name { get; private set; }

        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty]
        public string ColorHex { get; private set; }

        public string Description { get; private set; }
        public string FrameDataVersion { get; private set; }
        public string Style { get; private set; }

        [JsonProperty]
        public Dictionary<string, Stat> FrameData { get; private set; }

        public Character(Characters character)
        {
            Name = character.ToString();
            _urlTail = CharacterUtility.GetEnumDescription(character);
            Id = (int)character;
            _page = new Page(UrlBase + _urlTail, Id);
            GetData();
        }

        public Character(string characterName)
        {
            Name = characterName;
            var enumValue = (Characters)Enum.Parse(typeof(Characters), characterName, true);
            _urlTail = CharacterUtility.GetEnumDescription(enumValue);
            Id = (int)enumValue;
            _page = new Page(UrlBase + _urlTail, Id);
            GetData();
        }

        private void GetData()
        {
            FrameDataVersion = _page.GetVersion();
            MainImageUrl = _page.GetImageUrl();
            FrameData = _page.GetStats();
            ColorHex = GetColorHex().Result;
        }

        private async Task<string> GetColorHex()
        {
            var bitmapImage = default(Bitmap);
            //download image

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(MainImageUrl))
                {
                    response.EnsureSuccessStatusCode();

                    using (var inputStream = new MemoryStream())
                    {
                        await response.Content.ReadAsStreamAsync().Result.CopyToAsync(inputStream);
                        bitmapImage = new Bitmap(inputStream);
                    }
                }
            }
            //get color via bitmap call

            var color = bitmapImage.GetPixel(110, 90);
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }
    }
}
