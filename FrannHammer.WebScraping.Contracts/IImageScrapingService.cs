using System.Threading.Tasks;

namespace FrannHammer.WebScraping.Contracts
{
    public interface IImageScrapingService
    {
        Task<string> GetColorHexValue(string imageSourceUrl);
    }
}
