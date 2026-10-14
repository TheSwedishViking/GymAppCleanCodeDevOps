using GymSwipe.ApplicationLayer.Interfaces.Controls;
using GymSwipe.ApplicationLayer.Interfaces.Mappers;
using GymSwipe.ApplicationLayer.Interfaces.Repositories;
using GymSwipe.ApplicationLayer.Interfaces.Services;
using GymSwipe.ApplicationLayer.Interfaces.TimeDateFormat;
using GymSwipe.ApplicationLayer.Interfaces.Validators;
using GymSwipe.ApplicationLayer.Services.Controls;
using GymSwipe.ApplicationLayer.Services.Mappers;
using GymSwipe.ApplicationLayer.Services.Services;
using GymSwipe.ApplicationLayer.Services.TimeDateFormat;
using GymSwipe.ApplicationLayer.Services.Validators;
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
            //*******SERVICES*******
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITraningAreaService, TrainingAreaService>();
            builder.Services.AddScoped<IExerciseService, ExerciseService>();
            builder.Services.AddScoped<IGymPlaylistService, GymPlaylistService>();
            builder.Services.AddScoped<IDateHandler, DateHandler>();
            builder.Services.AddScoped<ITimeSetter, TimeSetter>();
            builder.Services.AddScoped<IRecordsValidator, RecordsValidator>();
            builder.Services.AddScoped<IAdminCreatorValidator, AdminCreatorValidator>();
            builder.Services.AddScoped<IExerciseRecordsService, ExerciseRecordsService>();
            builder.Services.AddScoped<ICalorieCountingService, CalorieCountingService>();
            builder.Services.AddScoped<IPlaylistControls, PlaylistControls>();

            //********MAPPING SERVICES************
            builder.Services.AddScoped<IMapDomainService, MapDomainService>();
            builder.Services.AddScoped<IMapDTOService, MapDTOService>();

            //*******REPOSITORIES*******
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IExerciseRepository, ExerciseRepo>();
            builder.Services.AddScoped<ITraningAreaRepo, TrainingAreaRepo>();
            builder.Services.AddScoped<IGymPlaylistRepository, GymPlaylistRepo>();
            builder.Services.AddScoped<IExerciseRecordsRepository, ExerciseRecordsRepository>();

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
