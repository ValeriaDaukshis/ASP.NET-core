using Microsoft.EntityFrameworkCore;

namespace WebApiCommon.DataModel
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Hive> Hives { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    CategoryName = "Name 1",
                    Code = "12c25"
                },

                new Category
                {
                    Id = 2,
                    CategoryName = "Name 2",
                    Code = "4255kg"
                },

                new Category
                {
                    Id = 3,
                    CategoryName = "Name 3",
                    Code = "82ds5"
                }
            );

            modelBuilder.Entity<Hive>().HasData(
                new Hive
                {
                    Id = 1,
                    Address = "Address 1"
                },

                new Hive
                {
                    Id = 2,
                    Address = "Address 2"
                },

                new Hive
                {
                    Id = 3,
                    Address = "Address 3"
                }
            );
        }
    }
}
