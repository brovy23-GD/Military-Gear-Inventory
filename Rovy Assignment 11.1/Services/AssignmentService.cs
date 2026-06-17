using MilitaryGearInventory.Models;

namespace MilitaryGearInventory.Services;

public class AssignmentService
{
    public List<GearItem> GetSampleGearItems()
    {
        return new List<GearItem>
         {
             new GearItem
             {
                 GearName = "Tactical Helmet",
                 Category = "Protective Gear",
                 Brand = "Armored Pro",
                 Quantity = 10,
                 Condition = "New",
                 Location = "Armory A",
                 LastInspectionDate = DateTime.Today.AddHours(8),
                 Notes = "Standard infantry helmet."
             },
             new GearItem
             {
                 GearName = "Battle Vest",
                 Category = "Protective Gear",
                 Brand = "ShieldWorks",
                 Quantity = 12,
                 Condition = "Good",
                 Location = "Armory B",
                 LastInspectionDate = DateTime.Today.AddDays(-7).AddHours(14),
                 Notes = "Used by infantry units."
             },
             new GearItem
             {
                 GearName = "Field Radio",
                 Category = "Communication",
                 Brand = "SignalCore",
                 Quantity = 6,
                 Condition = "Good",
                 Location = "Communication Locker",
                 LastInspectionDate = DateTime.Today.AddDays(-14).AddHours(9),
                 Notes = "Battery checked and ready."
             }
         };
    }
}