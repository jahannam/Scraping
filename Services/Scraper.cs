using Microsoft.Playwright;

namespace SearchEngineScraper.Services
{
    public class Scraper : IScraper
    {
        private readonly ILogger _logger;
        private readonly Task<IBrowser> _browser;
        private readonly IPageParser _pageParser;
        
        public Scraper(ILogger<Scraper> logger, Task<IBrowser> browser, IPageParser pageParser)
        {
            _logger = logger;
            _browser = browser;
            _pageParser = pageParser;
        }

        public async Task<string> Scrape(string keywords, string url)
        {
            var normalisedUrl = UrlHelper.NormalizeUrl(url);

            var browser = await _browser;

            var context = await browser.NewContextAsync(new()
            {
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36",
                ViewportSize = new() { Width = 1366, Height = 768 }
            });

            await context.AddInitScriptAsync(@"Object.defineProperty(navigator, 'webdriver', { get: () => undefined });");

            var page = await context.NewPageAsync();

            await page.GotoAsync($"https://www.google.co.uk/search?num=100&q={keywords}", new()
            {
                WaitUntil = WaitUntilState.NetworkIdle
            });

            var accept = await page.QuerySelectorAsync("button:has-text('Accept all')");
            if (accept != null)
            {
                await accept.ClickAsync();
                await page.WaitForTimeoutAsync(2000);
            }

            await page.Mouse.MoveAsync(100, 100);
            await page.EvaluateAsync("window.scrollBy(0, 300);");
            await page.WaitForTimeoutAsync(1500);

            return await _pageParser.ParsePage(page, normalisedUrl);
        }
    }
}
