using CL.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CL.WebApi.Configuration
{
    public static class DataBaseConfig
    {
        public static void AddDatabaseConfigurantion(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CLContext>(options => options.UseSqlServer(configuration.GetConnectionString("CLConnection")));
        }

        public static void UseDatabaseConfigurantion(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<CLContext>();
            context.Database.Migrate();
            context.Database.EnsureCreated(); 
        }
    }
}
