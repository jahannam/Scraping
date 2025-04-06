using Microsoft.Playwright;
using SearchEngineScraper.Services;
using System.Net;

namespace SearchEngineScraper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<Task<IPlaywright>>(Playwright.CreateAsync());

            builder.Services.AddSingleton(async provider =>
            {
                var playwright = await provider.GetRequiredService<Task<IPlaywright>>();
                return await playwright.Chromium.LaunchAsync(new()
                {
                    Headless = true,
                    Args = new[] { "--disable-blink-features=AutomationControlled" }
                });
            });
            builder.Services.AddScoped<IScraper, Scraper>();
            builder.Services.AddScoped<IPageParser, PageParser>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
