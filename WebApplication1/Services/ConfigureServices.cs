using HotelDBLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Use routing and map controllers
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            // Default route (you may have other routes if needed)
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}