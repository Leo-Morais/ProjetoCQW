using Microsoft.EntityFrameworkCore;
using ProjetoCQW.Model;

namespace ProjetoCQW.Repository
{
    public class ConnectionContext : DbContext
    {
        public ConnectionContext(DbContextOptions<ConnectionContext> options)
           : base(options)
        {
        }

        public DbSet<Montadora> Montadoras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Montadora>().HasKey(k => k.id);
            modelBuilder.Entity<Montadora>().ToTable("Montadora");
            modelBuilder.Entity<Montadora>().Property(x => x.Nome).HasColumnName("Nome");
            modelBuilder.Entity<Montadora>().Property(x => x.UrlSite).HasColumnName("UrlSite");
            modelBuilder.Entity<Montadora>().Property(x => x.DataAtualizacao).HasColumnName("DataAtualizacao");
            modelBuilder.Entity<Montadora>().Property(x => x.DataCriacao).HasColumnName("DataCriacao");
        }

    }
}
