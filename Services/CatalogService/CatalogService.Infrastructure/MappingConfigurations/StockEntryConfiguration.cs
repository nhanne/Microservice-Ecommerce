using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.MappingConfigurations;
public class StockEntryConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable("Stocks");
        builder.HasOne(s => s.Product)
                .WithMany(p => p.Stocks)
                .HasForeignKey(s => s.ProductId)
                .HasConstraintName("FK__Stock__ProductId");
        builder.HasOne(s => s.Size)
               .WithMany(s => s.Stocks)
               .HasForeignKey(s => s.SizeId)
               .HasConstraintName("FK__Stock__SizeId");
        builder.HasOne(s => s.Color)
            .WithMany(c => c.Stocks)
            .HasForeignKey(s => s.ColorId)
            .HasConstraintName("FK__Stock__ColorId");
    }
}
