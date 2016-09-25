using System;
using MedicalJournals.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MedicalJournals.Entities.Extensions;

namespace MedicalJournals.IntegrationTests
{
    public class TestStartup : IStartup
    {

        private static IConfigurationRoot _configuration;

        private static DbContextOptions<JournalContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<JournalContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return null;
        }

        public void Configure(IApplicationBuilder app)
        {
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var loggerFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();

            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<JournalContext>();

                // All contexts that share the same service provider will share the same InMemory database
                var options = CreateNewContextOptions();

                // Run the test against one instance of the context
                using (var context = new JournalContext(options))
                {
                    context.Database.Migrate();
                    serviceScopeFactory.SeedData(loggerFactory.CreateLogger("Seeding data"));

                }


                dbContext.Database.OpenConnection(); // see Resource #2 link why we do this
                dbContext.Database.EnsureCreated();
                // run Migrations
                dbContext.Database.Migrate();
            }
        }
    }
}
