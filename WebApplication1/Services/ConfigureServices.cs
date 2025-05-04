using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<HotelDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("HotelDb")));
    }
}