
using Microsoft.EntityFrameworkCore;
using MilitaryGearInventory.Data;
using MilitaryGearInventory.Models;

namespace MilitaryGearInventory.Services;

public class GearService
{
    public void InitializeDatabase()
    {
        using var db = new InventoryDbContext();
        db.Database.EnsureCreated();
    }

    public List<GearItem> GetAllGearItems()
    {
        using var db = new InventoryDbContext();
        return db.GearItems.AsNoTracking().OrderBy(g => g.GearName).ToList();
    }

    public void AddGearItem(GearItem gearItem)
    {
        using var db = new InventoryDbContext();
        db.GearItems.Add(gearItem);
        db.SaveChanges();
    }

    public void UpdateGearItem(GearItem gearItem)
    {
        using var db = new InventoryDbContext();

        var existingItem = db.GearItems.Find(gearItem.Id);
        if (existingItem is null)
        {
            return;
        }

        existingItem.GearName = gearItem.GearName;
        existingItem.Category = gearItem.Category;
        existingItem.Brand = gearItem.Brand;
        existingItem.Quantity = gearItem.Quantity;
        existingItem.Condition = gearItem.Condition;
        existingItem.Location = gearItem.Location;
        existingItem.LastInspectionDate = gearItem.LastInspectionDate;
        existingItem.Notes = gearItem.Notes;

        db.SaveChanges();
    }

    

    public void DeleteGearItem(int id)
    {
        using var db = new InventoryDbContext();

        var existingItem = db.GearItems.Find(id);
        if (existingItem is null)
        {
            return;
        }

        db.GearItems.Remove(existingItem);
        db.SaveChanges();
    }
}