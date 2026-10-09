using GymSwipe.Domain;
using GymSwipe.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GymSwipe.UserTests
{
    public class UserApiFixture : WebApplicationFactory<GymSwipe.API.Program>
    {
        private bool _init = false;

        //Inital build
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("TestEnv");

            builder.ConfigureServices(services =>
            {
                services.RemoveAll<DbContextOptions<GymAppDbContext>>();
                services.RemoveAll<IDbContextOptionsConfiguration<GymAppDbContext>>();
                services.RemoveAll<GymAppDbContext>();

                services.AddDbContext<GymAppDbContext>(options =>
                    options.UseInMemoryDatabase("GymSwipeTestDb"));
            });

        }


        //Initalize db once for client use
        public HttpClient GetClient()
        {
            if (!_init)
            {
                InitalizeDb();
                _init = true;
            }
            return CreateClient();
        }

        //Get db context & db initazier
        private void InitalizeDb()
        {
            using (var scope = Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GymAppDbContext>();
                dbContext.Database.EnsureCreatedAsync().GetAwaiter().GetResult();

                var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitalizer>();
                initializer.InitalizeAsync().GetAwaiter().GetResult();
            }
        }
    }
}