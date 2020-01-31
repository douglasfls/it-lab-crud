using ItLab.CrudApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ItLab.CrudApp.Data
{
    public class ItLabCrudAppContext : DbContext
    {
        public ItLabCrudAppContext(DbContextOptions<ItLabCrudAppContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductTag>().HasOne(p => p.Product).WithMany(p => p.ProductTags).HasForeignKey(p => p.ProductId);
            modelBuilder.Entity<ProductTag>().HasOne(p => p.Tag).WithMany(p => p.ProductTags).HasForeignKey(p => p.TagId);

            modelBuilder.Entity<Tag>().HasData(new Tag { Id = 1, Name = "Tag1" }, new Tag { Id = 2, Name = "Tag2" });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTag { get; set; }
    }
}
