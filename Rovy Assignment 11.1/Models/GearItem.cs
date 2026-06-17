using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MilitaryGearInventory.Models
{
    public class GearItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string GearName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Brand { get; set; }

        public int Quantity { get; set; }

        [MaxLength(50)]
        public string? Condition { get; set; }

        [MaxLength(100)]
        public string? Location { get; set; }

        // Stored as one combined date/time value in 24-hour format in the UI.
        public DateTime? LastInspectionDate { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}
    