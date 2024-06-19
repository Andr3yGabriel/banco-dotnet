using api_banco.Models;
using Microsoft.EntityFrameworkCore;

namespace api_banco.Infraestructure
{
    public sealed class ConnectionContext : DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Movimentacao> Movimentacao { get; set; }

        // Aqui está contida a string de conexão com o banco.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(
                "Server=localhost;" +
                "Port=5433;" +
                "Database=postgres;" +
                "User Id=postgres;" +
                "Password=180223;");
    }
}
