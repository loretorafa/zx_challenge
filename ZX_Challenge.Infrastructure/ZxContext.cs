using Microsoft.EntityFrameworkCore;
using ZX_Challenge.Domain.Models;

namespace ZX_Challenge.Infrastructure
{
    public class ZxContext : DbContext
    {

        public ZxContext() : base()
        {
            Database.EnsureCreated();
        }

        public ZxContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Pdv> Pdvs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = ZX_Challenge; Integrated Security = True;", x => x.UseNetTopologySuite());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)        {

            modelBuilder.Entity<Pdv>(e =>
            {
                e.ToTable("PDV", schema: "ZX_Challenge");

                e.Property(p => p.TradingName).IsRequired();
                e.Property(p => p.OwnerName).IsRequired();
                e.Property(p => p.Document).IsRequired();
                e.Property(p => p.CoverageArea).IsRequired();
                e.Property(p => p.Address).IsRequired();

                e.HasIndex(p => p.Document).IsUnique();
            });
        }

    }
}
