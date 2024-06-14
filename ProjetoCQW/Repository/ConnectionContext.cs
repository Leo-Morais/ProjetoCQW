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
        public DbSet<ModeloCarro> ModeloCarros { get; set; }
        public DbSet<ModeloSiteDetalhe> ModeloSiteDetalhes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Mapeamento de Montadora
            modelBuilder.Entity<Montadora>().HasKey(k => k.id);
            modelBuilder.Entity<Montadora>().Property(e => e.id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Montadora>().ToTable("Montadora");
            modelBuilder.Entity<Montadora>().Property(x => x.Nome).HasColumnName("Nome");
            modelBuilder.Entity<Montadora>().Property(x => x.UrlSite).HasColumnName("UrlSite");
            modelBuilder.Entity<Montadora>().Property(x => x.DataAtualizacao).HasColumnName("DataAtualizacao");
            modelBuilder.Entity<Montadora>().Property(x => x.DataCriacao).HasColumnName("DataCriacao");


            //Mapeamento de ModeloSiteDetalhes
            modelBuilder.Entity<ModeloSiteDetalhe>().HasKey(k => k.id);
            modelBuilder.Entity<ModeloSiteDetalhe>().Property(e => e.id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ModeloSiteDetalhe>().ToTable("ModeloSiteDetalhe");
            modelBuilder.Entity<ModeloSiteDetalhe>().Property(x => x.UrlSite).HasColumnName("UrlSite");
            modelBuilder.Entity<ModeloSiteDetalhe>().Property(x => x.XpathNome).HasColumnName("XpathNome");
            modelBuilder.Entity<ModeloSiteDetalhe>().Property(x => x.XpathModelo).HasColumnName("XpathModelo");
            modelBuilder.Entity<ModeloSiteDetalhe>().Property(x => x.XpathCor).HasColumnName("XpathCor");
            modelBuilder.Entity<ModeloSiteDetalhe>().Property(x => x.XpathImg).HasColumnName("XpathImg");
            modelBuilder.Entity<ModeloSiteDetalhe>().Property(x => x.XpathValor).HasColumnName("XpathValor");
            modelBuilder.Entity<ModeloSiteDetalhe>().Property(x => x.DataCriacao).HasColumnName("DataCriacao");
            modelBuilder.Entity<ModeloSiteDetalhe>().Property(x => x.DataAtualizacao).HasColumnName("DataAtualizacao");



            //Mapeamento de ModeloCarros
            modelBuilder.Entity<ModeloCarro>().HasKey(k => k.id);
            modelBuilder.Entity<ModeloCarro>().Property(e => e.id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ModeloCarro>().ToTable("ModeloCarro");
            modelBuilder.Entity<ModeloCarro>().Property(x => x.Nome).HasColumnName("Nome");
            modelBuilder.Entity<ModeloCarro>().Property(x => x.Ano).HasColumnName("Ano");
            modelBuilder.Entity<ModeloCarro>().Property(x => x.Imagem).HasColumnName("Imagem");
            modelBuilder.Entity<ModeloCarro>().Property(x => x.Cor).HasColumnName("Cor");
            modelBuilder.Entity<ModeloCarro>().Property(x => x.Valor).HasColumnName("Valor").HasColumnType("float"); ;
            modelBuilder.Entity<ModeloCarro>().Property(x => x.Versao).HasColumnName("Versao");
            modelBuilder.Entity<ModeloCarro>().Property(x => x.DataCriacao).HasColumnName("DataCriacao");
            modelBuilder.Entity<ModeloCarro>().Property(x => x.DataAtualizacao).HasColumnName("DataAtualizacao");
            modelBuilder.Entity<ModeloCarro>().Property(x => x.Montadora_Id).HasColumnName("Montadora_Id");
            modelBuilder.Entity<ModeloCarro>().Property(x => x.ModeloSite_Id).HasColumnName("ModeloSite_Id");


            modelBuilder.Entity<ModeloCarro>()
                .HasOne(x => x.Montadora)
                .WithOne()
                .HasForeignKey<Montadora>(e => e.id)
                .HasPrincipalKey<ModeloCarro>(c => c.Montadora_Id)
                .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<ModeloCarro>()
                .HasOne(x => x.ModeloSiteDetalhe)
                .WithOne()
                .HasForeignKey<ModeloSiteDetalhe>(e => e.id)
                .HasPrincipalKey<ModeloCarro>(c => c.ModeloSite_Id)
                .OnDelete(DeleteBehavior.ClientNoAction);


            modelBuilder.Entity<ModeloCarro>().Navigation(x => x.Montadora).AutoInclude();
            modelBuilder.Entity<ModeloCarro>().Navigation(x => x.ModeloSiteDetalhe).AutoInclude();

        }

    }
}