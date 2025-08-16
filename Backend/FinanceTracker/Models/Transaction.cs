using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinanceTracker.Models;
using FinanceTracker.Models.Enums;
namespace FinanceTracker.Models
{
	public class Transaction
	{
		public int Id { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		[Range(typeof(decimal), "0.00", "79228162514264337593543950335")]
		public decimal Amount { get; set; } // pozitif deðer

		[Required]
		public DateTime Date { get; set; } = DateTime.UtcNow;

		[MaxLength(500)]
		public string? Note { get; set; }

		// Türü kategoriden çýkarabiliriz ama sorgularý kolaylaþtýrmak için tutuyoruz
		[Required]
		public TransactionType Type { get; set; }

		// FKs
		public int AccountId { get; set; }
		public Account Account { get; set; } = null!;

		public int CategoryId { get; set; }
		public Category Category { get; set; } = null!;

		public int UserId { get; set; }
		public User User { get; set; } = null!;
	}
}
