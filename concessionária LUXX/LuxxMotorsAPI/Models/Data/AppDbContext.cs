using Microsoft.EntityFrameworkCore;
using LuxxMotorsAPI.Models;

namespace LuxxMotorsAPI.Data
{
    // Contexto principal do banco de dados da aplicação
    // DOC: Classe AppDbContext usada no backend para representar esta entidade ou controller.
public class AppDbContext : DbContext
    {
        // Construtor padrão utilizado pelo Entity Framework
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Representam as tabelas do banco
        // DOC: Conjunto de entidades mapeado para o banco de dados.
        public DbSet<Veiculo> VEICULO { get; set; }
        // DOC: Conjunto de entidades mapeado para o banco de dados.
        public DbSet<Cliente> CLIENTE { get; set; }
        // DOC: Conjunto de entidades mapeado para o banco de dados.
        public DbSet<TestDrive> TESTDRIVE { get; set; }
        // DOC: Conjunto de entidades mapeado para o banco de dados.
        public DbSet<Agendamento> AGENDAMENTOS { get; set; }
        // DOC: Conjunto de entidades mapeado para o banco de dados.
        public DbSet<Financiamento> FINANCIAMENTO { get; set; }
        // DOC: Conjunto de entidades mapeado para o banco de dados.
        public DbSet<Vendedor> VENDEDOR { get; set; }
        // DOC: Conjunto de entidades mapeado para o banco de dados.
        public DbSet<Revisao> REVISAO { get; set; }
        // DOC: Conjunto de entidades mapeado para o banco de dados.
        public DbSet<Venda> VENDA { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define o tipo decimal com duas casas para o preço do veículo
            modelBuilder.Entity<Veiculo>()
                .Property(v => v.Preco)
                .HasColumnType("decimal(10,2)");

            // Define o tipo decimal com duas casas para a renda do cliente
            modelBuilder.Entity<Cliente>()
                .Property(c => c.Renda_aproximada)
                .HasColumnType("decimal(10,2)");
        }
    }
}