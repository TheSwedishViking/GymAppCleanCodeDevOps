using GymSwipe.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Infrastructure.Factory
{
    public class GymAppDbContextFactory : IDesignTimeDbContextFactory<GymAppDbContext>
    {
        public GymAppDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder().AddUserSecrets<GymAppDbContextFactory>().Build();

            var connstring = config["Connections:LocalConnection"] ?? throw new InvalidOperationException("Uh oh, no connection string found!");
            var opts = new DbContextOptionsBuilder<GymAppDbContext>().UseSqlServer(connstring).Options;

            return new GymAppDbContext(opts);
        }
    }
}
