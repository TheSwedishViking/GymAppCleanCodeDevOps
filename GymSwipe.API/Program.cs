
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services;
using GymSwipe.Domain;
using GymSwipe.Infrastructure.Data;
using GymSwipe.Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace GymSwipe.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            //******DB CONTEXT*******
            if (!builder.Environment.IsEnvironment("TestEnv"))
            {
                var connectionString = builder.Configuration.GetConnectionString("MyConnectionString")
                                 ?? throw new InvalidOperationException("ConnectionStrings:MyConnectionString is missing.");


                builder.Services.AddDbContext<GymAppDbContext>(options =>
                    options.UseSqlServer(connectionString));
            }
            else
            {
                //For tests with in-memory, fixture sets up this
                builder.Services.AddScoped<GymAppDbContext>();
            }



            builder.Services.AddScoped<IDatabaseInitalizer, DatabaseInitalizer>();

            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IDateHandler, DateHandler>();
            builder.Services.AddScoped<IExerciseService, ExerciseService>();
            builder.Services.AddScoped<IGymPlaylistService, GymPlaylistService>();

            //*******REPOSITORIES*******
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IExerciseRepository, ExerciseRepo>();
            builder.Services.AddScoped<ITraningAreaRepo, TrainingAreaRepo>();
            builder.Services.AddScoped<IGymPlaylistRepository, GymPlaylistRepo>();






            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddEndpointsApiExplorer();










            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
