using CRUD_CodeFirst_Ef.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD_CodeFirst_Ef.Data
{
    public class DataDbContext :DbContext 
    {
        public DataDbContext()
        {

        }
        public DataDbContext(DbContextOptions<DataDbContext> options) :base(options)
        {
        }


        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .OnDelete(DeleteBehavior.Cascade);
        }

       
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Server=WIN-HRJ8B1ML5AG;Database=AdminPanel;Trusted_Connection=true;TrustServerCertificate=True;Encrypt=True;");
        //}
    }
}
