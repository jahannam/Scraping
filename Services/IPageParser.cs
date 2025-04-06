using Microsoft.Playwright;

namespace SearchEngineScraper.Services
{
    public interface IPageParser
    {
        Task<string> ParsePage(IPage page, string normalisedUrl);
    }
}
