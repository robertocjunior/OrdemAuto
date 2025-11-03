using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infra.Contexts
{
    public class AnalyzerDbContext : DbContext
    {
        public AnalyzerDbContext(DbContextOptions<AnalyzerDbContext> options) : base(options) { }
        public DbSet<CWParceiroNegocio> ParceiroNegocios { get; set; }
        public DbSet<CWPecas> Pecas { get; set; }
        public DbSet<CWVeiculo> Veiculo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region ENTITIES

            modelBuilder.Entity<CWParceiroNegocio>(entity =>
            {
                entity.ToTable("PARCEIRO_NEGOCIO");
                entity.HasKey(p => p.nCdParceiro);
                entity.Property(p => p.sNmParceiro).IsRequired();
                entity.Property(p => p.sEmail).IsRequired();
                entity.Property(p => p.sCpfCnpj).IsRequired();
                entity.Property(p => p.eTipo).IsRequired();
            });

            modelBuilder.Entity<CWPecas>(entity =>
            {
                entity.ToTable("PECAS");
                entity.HasKey(p => p.nCdPeca);
            });

            modelBuilder.Entity<CWVeiculo>(entity =>
            {
                entity.ToTable("VEICULOS");
                entity.HasKey(p => p.nCdVeiculo);
            });

            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
