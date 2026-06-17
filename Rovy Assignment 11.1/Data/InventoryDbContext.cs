using Microsoft.EntityFrameworkCore;
using MilitaryGearInventory.Models;

namespace MilitaryGearInventory.Data;

public class InventoryDbContext : DbContext
{
    public DbSet<GearItem> GearItems => Set<GearItem>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=militarygear.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GearItem>(entity =>
        {
            entity.HasKey(g => g.Id);

            entity.Property(g => g.GearName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(g => g.Category)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(g => g.Brand)
                .HasMaxLength(100);

            entity.Property(g => g.Condition)
                .HasMaxLength(50);

            entity.Property(g => g.Location)
                .HasMaxLength(100);

            entity.Property(g => g.Notes)
                .HasMaxLength(500);

            entity.Property(g => g.LastInspectionDate)
                .HasColumnType("TEXT");
        });
    }
}