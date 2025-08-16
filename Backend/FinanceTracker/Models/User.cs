using System.ComponentModel.DataAnnotations;
using FinanceTracker.Models; // gerekirse
using System.Collections.Generic;
namespace FinanceTracker.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MinLength(2), MaxLength(100)]
        public string FullName { get; set; } = null!;

        [Required, MaxLength(200), EmailAddress]
        public string Email { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
