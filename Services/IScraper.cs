namespace SearchEngineScraper.Services
{
    public interface IScraper
    {
        Task<string> Scrape(string keywords, string url);
    }
}
