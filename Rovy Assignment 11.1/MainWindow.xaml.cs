using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using MilitaryGearInventory.Models;
using MilitaryGearInventory.Services;

namespace MilitaryGearInventory;

public partial class MainWindow : Window
{
    // CRUD service for SQLite operations.
    // This uses the same interface/class pattern shown in the class example.
    private readonly ICRUD _crud = new CRUD();

    // Optional seed data provider for first-time runs.
    private readonly AssignmentService _assignmentService = new();

    // Preset gear items used to auto-fill the form from the dropdown.
    private List<GearItem> _gearPresets = [];

    // Keeps track of the row selected in the DataGrid.
    private GearItem? _selectedGearItem;

    public MainWindow()
    {
        InitializeComponent();

        // Prepare the 24-hour time dropdown.
        PopulateInspectionTimeDropdown();

        // Load the preset gear catalog for quick form population.
        LoadGearPresets();

        // Set a sensible default inspection date/time.
        SetDefaultInspectionDateTime();

        // Create the SQLite database if needed.
        _crud.InitializeDatabase();

        // Seed sample data only if the table is empty.
        SeedSampleDataIfNeeded();

        // Load data into the grid.
        LoadGearItems();
    }

    // Adds 24-hour military time values in 30-minute steps.
    private void PopulateInspectionTimeDropdown()
    {
        cbInspectionTime.Items.Clear();

        for (var time = TimeSpan.Zero; time < TimeSpan.FromDays(1); time += TimeSpan.FromMinutes(30))
        {
            cbInspectionTime.Items.Add(time.ToString(@"hh\:mm", CultureInfo.InvariantCulture));
        }
    }

    // Loads preset gear items so the user can pick a ready-made example from the dropdown.
    private void LoadGearPresets()
    {
        _gearPresets = _assignmentService.GetGearPresets();
        cbGearPreset.ItemsSource = _gearPresets;
        cbGearPreset.DisplayMemberPath = nameof(GearItem.GearName);
        cbGearPreset.SelectedIndex = -1;
    }

    // Gives the form a ready-to-use default date/time.
    private void SetDefaultInspectionDateTime()
    {
        dpInspectionDate.SelectedDate = DateTime.Today;

        var roundedNow = RoundToNearestThirtyMinutes(DateTime.Now).TimeOfDay.ToString(@"hh\:mm", CultureInfo.InvariantCulture);
        cbInspectionTime.SelectedItem = roundedNow;
    }

    // Rounds the current time to the nearest 30-minute slot so it matches the dropdown.
    private static DateTime RoundToNearestThirtyMinutes(DateTime value)
    {
        var minutes = ((int)Math.Round(value.Minute / 30.0)) * 30;

        var rounded = new DateTime(value.Year, value.Month, value.Day, value.Hour, 0, 0);

        if (minutes == 60)
        {
            rounded = rounded.AddHours(1);
        }
        else
        {
            rounded = rounded.AddMinutes(minutes);
        }

        return rounded;
    }

    // Inserts sample data only when the database starts empty.
    private void SeedSampleDataIfNeeded()
    {
        if (_crud.GetAllGearItems().Count > 0)
        {
            return;
        }

        foreach (var item in _assignmentService.GetSampleGearItems())
        {
            _crud.AddGearItem(item);
        }
    }

    // Refreshes the DataGrid from SQLite.
    private void LoadGearItems()
    {
        GearGrid.ItemsSource = _crud.GetAllGearItems();
    }

    // Clears the form and restores default date/time values.
    private void ClearForm()
    {
        txtGearName.Clear();
        txtCategory.Clear();
        txtBrand.Clear();
        txtQuantity.Clear();
        txtCondition.Clear();
        txtLocation.Clear();
        txtNotes.Clear();

        cbGearPreset.SelectedIndex = -1;
        _selectedGearItem = null;
        GearGrid.SelectedItem = null;

        SetDefaultInspectionDateTime();
    }

    // Builds a GearItem from the form inputs.
    private GearItem? BuildGearItemFromForm(bool includeId)
    {
        if (string.IsNullOrWhiteSpace(txtGearName.Text) ||
            string.IsNullOrWhiteSpace(txtCategory.Text))
        {
            MessageBox.Show("Gear Name and Category are required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return null;
        }

        if (!int.TryParse(txtQuantity.Text, out int quantity))
        {
            MessageBox.Show("Quantity must be a valid number.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return null;
        }

        if (dpInspectionDate.SelectedDate is null)
        {
            MessageBox.Show("Please select an inspection date.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return null;
        }

        if (cbInspectionTime.SelectedItem is not string selectedTime ||
            !TimeSpan.TryParse(selectedTime, CultureInfo.InvariantCulture, out var inspectionTime))
        {
            MessageBox.Show("Please select a valid inspection time.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return null;
        }

        var inspectionDateTime = dpInspectionDate.SelectedDate.Value.Date + inspectionTime;

        return new GearItem
        {
            Id = includeId ? _selectedGearItem?.Id ?? 0 : 0,
            GearName = txtGearName.Text.Trim(),
            Category = txtCategory.Text.Trim(),
            Brand = string.IsNullOrWhiteSpace(txtBrand.Text) ? null : txtBrand.Text.Trim(),
            Quantity = quantity,
            Condition = string.IsNullOrWhiteSpace(txtCondition.Text) ? null : txtCondition.Text.Trim(),
            Location = string.IsNullOrWhiteSpace(txtLocation.Text) ? null : txtLocation.Text.Trim(),
            LastInspectionDate = inspectionDateTime,
            Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim()
        };
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        var gearItem = BuildGearItemFromForm(includeId: false);
        if (gearItem is null)
        {
            return;
        }

        _crud.AddGearItem(gearItem);
        LoadGearItems();
        ClearForm();
    }

    private void Update_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedGearItem is null)
        {
            MessageBox.Show("Please select a gear item to update.", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var gearItem = BuildGearItemFromForm(includeId: true);
        if (gearItem is null)
        {
            return;
        }

        _crud.UpdateGearItem(gearItem);
        LoadGearItems();
        ClearForm();
    }

    private void Delete_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedGearItem is null)
        {
            MessageBox.Show("Please select a gear item to delete.", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var result = MessageBox.Show(
            $"Delete {_selectedGearItem.GearName}?",
            "Confirm Delete",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result != MessageBoxResult.Yes)
        {
            return;
        }

        _crud.DeleteGearItem(_selectedGearItem.Id);
        LoadGearItems();
        ClearForm();
    }

    private void Clear_Click(object sender, RoutedEventArgs e)
    {
        ClearForm();
    }

    private void GearGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (GearGrid.SelectedItem is not GearItem selectedItem)
        {
            return;
        }

        cbGearPreset.SelectedIndex = -1;
        _selectedGearItem = selectedItem;

        txtGearName.Text = selectedItem.GearName;
        txtCategory.Text = selectedItem.Category;
        txtBrand.Text = selectedItem.Brand ?? string.Empty;
        txtQuantity.Text = selectedItem.Quantity.ToString();
        txtCondition.Text = selectedItem.Condition ?? string.Empty;
        txtLocation.Text = selectedItem.Location ?? string.Empty;
        txtNotes.Text = selectedItem.Notes ?? string.Empty;

        if (selectedItem.LastInspectionDate is not null)
        {
            dpInspectionDate.SelectedDate = selectedItem.LastInspectionDate.Value.Date;
            cbInspectionTime.SelectedItem = selectedItem.LastInspectionDate.Value.ToString("HH:mm", CultureInfo.InvariantCulture);
        }
        else
        {
            SetDefaultInspectionDateTime();
        }
    }

    // When a preset is selected, the form auto-fills with the preset values.
    private void GearPreset_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cbGearPreset.SelectedItem is not GearItem preset)
        {
            return;
        }

        _selectedGearItem = null;
        GearGrid.SelectedItem = null;

        txtGearName.Text = preset.GearName;
        txtCategory.Text = preset.Category;
        txtBrand.Text = preset.Brand ?? string.Empty;
        txtQuantity.Text = preset.Quantity.ToString();
        txtCondition.Text = preset.Condition ?? string.Empty;
        txtLocation.Text = preset.Location ?? string.Empty;
        txtNotes.Text = preset.Notes ?? string.Empty;

        if (preset.LastInspectionDate is not null)
        {
            dpInspectionDate.SelectedDate = preset.LastInspectionDate.Value.Date;
            cbInspectionTime.SelectedItem = preset.LastInspectionDate.Value.ToString("HH:mm", CultureInfo.InvariantCulture);
        }
        else
        {
            SetDefaultInspectionDateTime();
        }
    }
}