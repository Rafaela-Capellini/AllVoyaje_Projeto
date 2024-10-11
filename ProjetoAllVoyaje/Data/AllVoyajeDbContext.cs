using Microsoft.EntityFrameworkCore;
using ProjetoAllVoyaje.Models;

namespace ProjetoAllVoyaje.Data
{
    public class AllVoyajeDbContext : DbContext
    {
        public AllVoyajeDbContext(DbContextOptions<AllVoyajeDbContext> options) : base(options) { }


        public DbSet<TipoPacote> TiposPacotes { get; set; }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<PacoteCliente> PacoteClientes { get; set; }

        
        public DbSet<PacoteViagem> PacoteViagens { get; set; }
        public DbSet<OpcoesDatas> OpcoesDatas { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoPacote>().ToTable("tbTiposPacotes");
            modelBuilder.Entity<Cliente>().ToTable("tbClientes");
            modelBuilder.Entity<PacoteCliente>().ToTable("tbPacoteClientes");
          
            modelBuilder.Entity<PacoteViagem>().ToTable("tbPacoteViagens");
            modelBuilder.Entity<OpcoesDatas>().ToTable("tbOpcoesDatas");
        }
    }
}
