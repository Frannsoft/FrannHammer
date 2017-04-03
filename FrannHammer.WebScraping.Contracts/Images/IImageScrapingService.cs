using System.Threading.Tasks;

namespace FrannHammer.WebScraping.Contracts.Images
{
    public interface IImageScrapingService
    {
        Task<string> GetColorHexValue(string imageSourceUrl);
    }
}
