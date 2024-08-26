using CatalogService.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CatalogService.Infrastructure;
public class StoreDbContext : DbContext //: IdentityDbContext<User>
{
    public StoreDbContext() : base()
    {
    }
    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Color>? Colors { get; set; }
    public virtual DbSet<Size>? Sizes { get; set; }
    public virtual DbSet<Category>? Categories { get; set; }
    public virtual DbSet<Product>? Products { get; set; }
    public virtual DbSet<Stock>? Stocks { get; set; }
    public virtual DbSet<Order>? Orders { get; set; }
    public virtual DbSet<OrderDetail>? OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
            x => x.Namespace != null && x.Namespace.StartsWith("E_Commerce.MappingConfigurations"));

        modelBuilder.Entity<Color>().ToTable("Colors");
        modelBuilder.Entity<Size>().ToTable("Sizes");
        modelBuilder.Entity<Category>().ToTable("Categories");
        modelBuilder.Entity<Product>().ToTable("Products");
        modelBuilder.Entity<Order>().ToTable("Orders");

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (!string.IsNullOrEmpty(tableName) && tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
    {
        OptionsBuilder.UseSqlServer(@"Server=NHAN;Database=ClothingStore;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true");
        base.OnConfiguring(OptionsBuilder);
    }
}