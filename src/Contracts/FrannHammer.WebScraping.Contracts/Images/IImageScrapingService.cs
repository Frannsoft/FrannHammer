using System.Threading.Tasks;

namespace FrannHammer.WebScraping.Contracts.Images
{
    public interface IColorScrapingService
    {
        Task<string> GetColorHexValue(string characterName);
    }
}
