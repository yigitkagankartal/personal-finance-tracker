using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using FinanceTracker.Models;
using FinanceTracker.Models.Enums;

namespace FinanceTracker.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!; // �rn: Kira, Maa�, Market

        [Required]
        public TransactionType Type { get; set; } // Income / Expense

        // FK (kullan�c�ya �zel kategoriler)
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // Navigation
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
