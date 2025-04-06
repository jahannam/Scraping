using Microsoft.AspNetCore.Mvc;
using SearchEngineScraper.Models;
using SearchEngineScraper.Services;
using System.Diagnostics;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace SearchEngineScraper.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScraper _scraper;

        public HomeController(IScraper scraper, ILogger<HomeController> logger)
        {
            _logger = logger;
            _scraper = scraper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new IndexModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(IndexModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Keywords) && !string.IsNullOrWhiteSpace(model.Url))
            {
                model.HasSearched = true;
                model.Result = await _scraper.Scrape(model.Keywords, model.Url);
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
