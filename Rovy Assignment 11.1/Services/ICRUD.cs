using MilitaryGearInventory.Models;

namespace MilitaryGearInventory.Services;

// This interface matches the class example pattern and keeps the CRUD contract explicit.
public interface ICRUD
{
    // Make sure the SQLite database exists before the UI tries to read or write.
    void InitializeDatabase();

    // Read every gear item from the database so the grid can show the full inventory.
    List<GearItem> GetAllGearItems();

    // Add a new gear item to the database.
    void AddGearItem(GearItem gearItem);

    // Update an existing gear item by primary key.
    void UpdateGearItem(GearItem gearItem);

    // Delete a gear item by primary key.
    void DeleteGearItem(int id);
}
