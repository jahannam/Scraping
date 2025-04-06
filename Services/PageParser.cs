using Microsoft.Playwright;

namespace SearchEngineScraper.Services
{
    public class PageParser : IPageParser
    {
        public async Task<string> ParsePage(IPage page, string url)
        {
            var results = await page.QuerySelectorAllAsync("div#search a");

            var position = new List<int>();
            int index = 0;
            foreach (var result in results)
            {
                var jsname = await result.GetAttributeAsync("jsname");
                if (jsname == null)
                    continue;
                index++;
                var href = await result.GetAttributeAsync("href");

                if (href != null && href.Contains(url))
                {
                    position.Add(index);
                }
            }

            var postionString = position.Count > 0 ? string.Join(", ", position) : "0";

            return postionString;
        }
    }
}
