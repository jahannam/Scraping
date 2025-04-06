using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace SearchEngineScraper.Models
{
    public class IndexModel
    {
        [BindProperty]
        public string? Keywords { get; set; }

        [BindProperty]
        public string? Url { get; set; }

        public bool HasSearched { get; set; }

        public string? Result { get; set; }
    }
}
