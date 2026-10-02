using GymSwipe.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace GymSwipe.UserTests.FixtureFolder
{
    public class UserApiFixture : WebApplicationFactory<API.Program> //startup class for the api
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<DbContextOptions<GymAppDbContext>>();
                services.RemoveAll<IDbContextOptionsConfiguration<GymAppDbContext>>();
                services.RemoveAll<GymAppDbContext>();

                services.AddDbContext<GymAppDbContext>(options =>
                    options.UseInMemoryDatabase("GymSwipeTestDb"));
            });
        }


    }
}