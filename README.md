# Military Gear Inventory

<p align="center">
  <img src="https://img.shields.io/badge/WPF-Desktop_App-4F7D4A?style=for-the-badge&logo=windows&logoColor=white" alt="WPF" />
  <img src="https://img.shields.io/badge/SQLite-Local_DB-1F6FEB?style=for-the-badge&logo=sqlite&logoColor=white" alt="SQLite" />
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET" />
</p>

<p align="center">
  <strong>Army-style inventory management app built with WPF, SQLite, and EF Core Code First.</strong>
</p>

---

## Project Goal

This desktop app manages military gear records like helmets, vests, radios, and other equipment.

---

## Whiteboard Logic

```text
User opens app
        ↓
SQLite database is created if missing
        ↓
Seed data is loaded only if the table is empty
        ↓
Gear list appears in the DataGrid
        ↓
User adds / updates / deletes gear
        ↓
Database and UI refresh
```

### Layer breakdown

```text
Models   → GearItem data shape
Data     → InventoryDbContext + SQLite mapping
Services → CRUD and seed data logic
UI       → MainWindow.xaml + button events
```

---

## Workflow

1. Launch the app.
2. Database initializes automatically.
3. Sample items appear if the table is empty.
4. Select a row to edit.
5. Add, update, or delete gear records.
6. Clear the form when finished.

---

## Features

- Add gear items
- Update selected gear items
- Delete gear items
- Seed sample records for demo use
- Choose from a preset gear dropdown to auto-fill the form
- Army / infantry color theme
- Military-style date and time input

---

## Tech Stack

- C#
- .NET 8 WPF
- Entity Framework Core
- SQLite

---

## Folder Structure

```text
Rovy Assignment 11.1/
├─ Data/
│  └─ InventoryDbContext.cs
├─ Models/
│  └─ GearItem.cs
├─ Services/
│  ├─ GearService.cs
│  └─ AssignmentService.cs
├─ App.xaml
├─ MainWindow.xaml
└─ MainWindow.xaml.cs
```

---

## Sample Data

- Tactical Helmet
- Battle Vest
- Field Radio
- Night Vision Goggles
- Combat Boots
- Camouflage Uniform
- Medical Kit
- Ammo Pouch

---

## Run Instructions

1. Open the solution in Visual Studio.
2. Restore NuGet packages.
3. Build the project.
4. Run the app.

---

## Demo Flow

1. Show the Army-style UI.
2. Add a gear item.
3. Update the inspection date/time.
4. Delete a gear item.
5. Show the saved SQLite records.
