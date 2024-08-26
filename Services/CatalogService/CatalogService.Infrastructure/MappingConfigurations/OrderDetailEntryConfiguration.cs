using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.MappingConfigurations;
public class OrderDetailEntryConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable("OrderDetails");
        builder.HasKey(od => new { od.OrderId, od.StockId });
        builder.HasIndex(od => od.OrderId);
        builder.HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId)
            .HasConstraintName("FK__OrderDetail__OrderId");
        builder.HasOne(od => od.Stock)
            .WithMany(s => s.OrderDetails)
            .HasForeignKey(od => od.StockId)
            .HasConstraintName("FK__OrderDetail__StockId");
    }
}
