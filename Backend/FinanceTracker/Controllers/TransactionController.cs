using Microsoft.AspNetCore.Mvc;
using FinanceTracker.Data;
using FinanceTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/transaction
        [HttpGet]
        public IActionResult GetTransactions()
        {
            var transactions = _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Category)
                .Include(t => t.User)
                .ToList();
            return Ok(transactions);
        }

        // GET: api/transaction/5
        [HttpGet("{id}")]
        public IActionResult GetTransaction(int id)
        {
            var transaction = _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Category)
                .Include(t => t.User)
                .FirstOrDefault(t => t.Id == id);

            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        // POST: api/transaction
        [HttpPost]
        public IActionResult CreateTransaction([FromBody] Transaction transaction)
        {
            if (transaction == null)
                return BadRequest();

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
        }

        // PUT: api/transaction/5
        [HttpPut("{id}")]
        public IActionResult UpdateTransaction(int id, [FromBody] Transaction transaction)
        {
            var existing = _context.Transactions.Find(id);
            if (existing == null)
                return NotFound();

            existing.Amount = transaction.Amount;
            existing.Date = transaction.Date;
            existing.Note = transaction.Note;
            existing.Type = transaction.Type;
            existing.AccountId = transaction.AccountId;
            existing.CategoryId = transaction.CategoryId;
            existing.UserId = transaction.UserId;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/transaction/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            var transaction = _context.Transactions.Find(id);
            if (transaction == null)
                return NotFound();

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
