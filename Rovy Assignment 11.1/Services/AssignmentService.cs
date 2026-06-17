using MilitaryGearInventory.Models;

namespace MilitaryGearInventory.Services;

public class AssignmentService
{
    public List<GearItem> GetGearPresets()
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
             },
             new GearItem
             {
                 GearName = "Night Vision Goggles",
                 Category = "Optics",
                 Brand = "VisionMax",
                 Quantity = 4,
                 Condition = "Excellent",
                 Location = "Optics Cabinet",
                 LastInspectionDate = DateTime.Today.AddDays(-3).AddHours(21),
                 Notes = "For low-light operations."
             },
             new GearItem
             {
                 GearName = "Combat Boots",
                 Category = "Uniform Gear",
                 Brand = "FieldStep",
                 Quantity = 18,
                 Condition = "Good",
                 Location = "Uniform Locker",
                 LastInspectionDate = DateTime.Today.AddDays(-10).AddHours(7),
                 Notes = "Mixed sizes available."
             },
             new GearItem
             {
                 GearName = "Camouflage Uniform",
                 Category = "Uniform Gear",
                 Brand = "CamoCore",
                 Quantity = 15,
                 Condition = "New",
                 Location = "Uniform Storage",
                 LastInspectionDate = DateTime.Today.AddDays(-1).AddHours(16),
                 Notes = "Multiple sizes ready."
             },
             new GearItem
             {
                 GearName = "Medical Kit",
                 Category = "Support Gear",
                 Brand = "MedTact",
                 Quantity = 8,
                 Condition = "Good",
                 Location = "Field Med Bay",
                 LastInspectionDate = DateTime.Today.AddDays(-5).AddHours(10),
                 Notes = "Includes first aid supplies."
             },
             new GearItem
             {
                 GearName = "Ammo Pouch",
                 Category = "Ammunition Gear",
                 Brand = "LoadOut",
                 Quantity = 25,
                 Condition = "Good",
                 Location = "Armory C",
                 LastInspectionDate = DateTime.Today.AddDays(-12).AddHours(13),
                 Notes = "Used with tactical belt."
             }
         };
    }

    public List<GearItem> GetSampleGearItems()
    {
         return GetGearPresets()
             .Take(3)
             .Select(item => new GearItem
             {
                 GearName = item.GearName,
                 Category = item.Category,
                 Brand = item.Brand,
                 Quantity = item.Quantity,
                 Condition = item.Condition,
                 Location = item.Location,
                 LastInspectionDate = item.LastInspectionDate,
                 Notes = item.Notes
             })
             .ToList();
    }
}