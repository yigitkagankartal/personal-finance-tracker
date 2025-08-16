using Microsoft.EntityFrameworkCore;
using FinanceTracker.Models;

namespace FinanceTracker.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<User> Users => Set<User>();
		public DbSet<Account> Accounts => Set<Account>();
		public DbSet<Category> Categories => Set<Category>();
		public DbSet<Transaction> Transactions => Set<Transaction>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// User.Email unique
			modelBuilder.Entity<User>()
				.HasIndex(u => u.Email)
				.IsUnique();

			// User (1) - (N) Account
			modelBuilder.Entity<Account>()
				.HasOne(a => a.User)
				.WithMany(u => u.Accounts)
				.HasForeignKey(a => a.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			// User (1) - (N) Category
			modelBuilder.Entity<Category>()
				.HasOne(c => c.User)
				.WithMany(u => u.Categories)
				.HasForeignKey(c => c.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			// User (1) - (N) Transaction
			modelBuilder.Entity<Transaction>()
				.HasOne(t => t.User)
				.WithMany(u => u.Transactions)
				.HasForeignKey(t => t.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			// Account (1) - (N) Transaction
			modelBuilder.Entity<Transaction>()
				.HasOne(t => t.Account)
				.WithMany(a => a.Transactions)
				.HasForeignKey(t => t.AccountId)
				.OnDelete(DeleteBehavior.Cascade);

			// Category (1) - (N) Transaction
			modelBuilder.Entity<Transaction>()
				.HasOne(t => t.Category)
				.WithMany(c => c.Transactions)
				.HasForeignKey(t => t.CategoryId)
				.OnDelete(DeleteBehavior.Restrict); // kategoriyi silince iþlemler kalsýn (isteðe baðlý)

			// Composite unique: Category Name per User
			modelBuilder.Entity<Category>()
				.HasIndex(c => new { c.UserId, c.Name })
				.IsUnique();

			// Decimal precision (alternatif: [Column(TypeName = "...")])
			modelBuilder.Entity<Account>()
				.Property(a => a.Balance)
				.HasPrecision(18, 2);

			modelBuilder.Entity<Transaction>()
				.Property(t => t.Amount)
				.HasPrecision(18, 2);
		}
	}
}
