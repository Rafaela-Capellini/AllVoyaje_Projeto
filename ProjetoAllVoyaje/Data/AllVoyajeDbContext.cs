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
        public DbSet<ImagemPacote> ImagemPacote { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoPacote>().ToTable("tbTiposPacotes");
            modelBuilder.Entity<Cliente>().ToTable("tbClientes");
            modelBuilder.Entity<PacoteCliente>().ToTable("tbPacoteClientes");
          
            modelBuilder.Entity<PacoteViagem>().ToTable("tbPacoteViagens");
            modelBuilder.Entity<ImagemPacote>().ToTable("tbImagemPacote");            
        }

        /*public void CreateTrigger()
        {
            var sql = @"
        IF NOT EXISTS (SELECT * FROM sys.triggers WHERE name = 'trg_UsuarioClienteAfterUserInsert')
            BEGIN
                CREATE TRIGGER trg_UsuarioClienteAfterUserInsert
                ON AspNetUsers
                FOR INSERT
                AS
                BEGIN
                    DECLARE @UserId NVARCHAR(450), @UserName NVARCHAR(256);
                    
                    SELECT @UserId = Id, @UserName = UserName FROM INSERTED;

                    INSERT INTO tbClientes (ClienteId, Nome, CPF, Telefone, Cargo, AspNetUserId)
                    VALUES (NEWID(), @UserName, '', '', '', @UserId);
                END;
            END;
        ";

            this.Database.ExecuteSqlRaw(sql); // Executa o comando SQL
        }*/

    }
}
