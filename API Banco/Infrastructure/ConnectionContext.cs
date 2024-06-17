using API_Banco.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Banco.Infrastructure
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Movimentacao> Movimentacao { get; set; }

        // Aqui está contida a string de conexão com o banco.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(
                "Server=localhost;" +
                "Port=5432;" +
                "Database=postgres;" +
                "User Id=postgres;" +
                "Password=180223;");
    }
}
