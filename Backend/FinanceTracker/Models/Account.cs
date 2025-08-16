using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTracker.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!; // Örn: "Ziraat Vadesiz", "Nakit"

        [MaxLength(300)]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; } = 0m;

        [Required, MaxLength(3)]
        public string Currency { get; set; } = "TRY"; // TRY, USD, EUR

        // FK
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // Navigation
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
