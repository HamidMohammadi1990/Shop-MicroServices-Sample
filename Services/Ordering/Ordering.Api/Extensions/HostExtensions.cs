using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ordering.Api.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host,            
            int? retry = 0) where TContext : DbContext
        {
            int retryForAvailability = retry!.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("migrating started for sql server");
                    InvokeSeeder(context, services);
                    logger.LogInformation("migrating has been done for sql server");
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "an error occurred while migrating database");

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);                        
                        MigrateDatabase<TContext>(host, retryForAvailability);
                    }
                    throw;
                }
            }

            return host;
        }

        private static void InvokeSeeder<TContext>(            
            TContext context,
            IServiceProvider services)
            where TContext : DbContext
        {
            context.Database.Migrate();
            //seeder(context, services);
        }
    }
}