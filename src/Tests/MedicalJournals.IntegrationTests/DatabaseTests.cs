using System.Net.Http;
using MedicalJournals.Entities;
using MedicalJournals.Entities.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace MedicalJournals.IntegrationTests
{
    [TestClass]
    public class DatabaseTests
    {
        private static IWebHostBuilder _builder;
        private static TestServer _server;
        private static HttpClient _client;



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

        [TestMethod]
        public void Migrations_CheckSeededDataWorkingForCategoriesTable()
        {
            // All contexts that share the same service provider will share the same InMemory database
            var options = CreateNewContextOptions();

            // Run the test against one instance of the context
            using (var context = new JournalContext(options))
            {
                var logger = new Mock<ILogger>();

                // Seed the database with data
                SeedDataExtensions.SeedData(context, logger.Object);
                var categoriesExist = context.Categories.AnyAsync().Result;

                Assert.IsTrue(categoriesExist);
            }
        }

        [TestMethod, TestCategory("Integration")]
        public void Database_AllMigrationsCanBeApplied_WithoutErrors()
        {

//            _builder = new WebHostBuilder()
//              .UseEnvironment("Development")
//              .UseStartup<TestStartup>();
//
//            // anything else you might need?....
//            _server = new TestServer(_builder);
//            _client = _server.CreateClient();
        }
    }
}
