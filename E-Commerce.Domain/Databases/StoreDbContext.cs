using E_Commerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Databases;

public class StoreDbContext : IdentityDbContext<User>
{
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
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Color>().ToTable("Colors");
        modelBuilder.Entity<Size>().ToTable("Sizes");
        modelBuilder.Entity<Category>().ToTable("Categories");
        modelBuilder.Entity<Product>().ToTable("Products");
        modelBuilder.Entity<Stock>(entity =>
        {
            entity.ToTable("Stocks");
            entity.HasOne(s => s.Product)
                .WithMany(p => p.Stocks)
                .HasForeignKey(s => s.ProductId)
                .HasConstraintName("FK__Stock__ProductId");
            entity.HasOne(s => s.Size)
                .WithMany(s => s.Stocks)
                .HasForeignKey(s => s.SizeId)
                .HasConstraintName("FK__Stock__SizeId");
            entity.HasOne(s => s.Color)
                .WithMany(c => c.Stocks)
                .HasForeignKey(s => s.ColorId)
                .HasConstraintName("FK__Stock__ColorId");
        });
        modelBuilder.Entity<Order>().ToTable("Orders");
        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("OrderDetails");
            entity.HasKey(od => new { od.OrderId, od.StockId });
            entity.HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .HasConstraintName("FK__OrderDetail__OrderId");
            entity.HasOne(od => od.Stock)
                .WithMany(s => s.OrderDetails)
                .HasForeignKey(od => od.StockId)
                .HasConstraintName("FK__OrderDetail__StockId");
        });

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (!string.IsNullOrEmpty(tableName) && tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }
    }
   
}