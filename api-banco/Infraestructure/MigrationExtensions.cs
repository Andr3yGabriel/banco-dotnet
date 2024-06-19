using Microsoft.EntityFrameworkCore;

namespace api_banco.Infraestructure
{
    public static class MigrationExtensions
    {
        public static void ApplyMigration(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ConnectionContext dbContext =
                scope.ServiceProvider.GetRequiredService<ConnectionContext>();

            dbContext.Database.Migrate();
        }
    }
}
