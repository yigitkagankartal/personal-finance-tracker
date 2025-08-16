using Microsoft.AspNetCore.Mvc;
using FinanceTracker.Data;
using FinanceTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/account
        [HttpGet]
        public IActionResult GetAccounts()
        {
            var accounts = _context.Accounts
                .Include(a => a.User)
                .ToList();
            return Ok(accounts);
        }

        // GET: api/account/5
        [HttpGet("{id}")]
        public IActionResult GetAccount(int id)
        {
            var account = _context.Accounts
                .Include(a => a.User)
                .FirstOrDefault(a => a.Id == id);

            if (account == null)
                return NotFound();

            return Ok(account);
        }

        // POST: api/account
        [HttpPost]
        public IActionResult CreateAccount([FromBody] Account account)
        {
            if (account == null)
                return BadRequest();

            _context.Accounts.Add(account);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
        }

        // PUT: api/account/5
        [HttpPut("{id}")]
        public IActionResult UpdateAccount(int id, [FromBody] Account account)
        {
            var existing = _context.Accounts.Find(id);
            if (existing == null)
                return NotFound();

            existing.Name = account.Name;
            existing.Balance = account.Balance;
            existing.UserId = account.UserId;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/account/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            var account = _context.Accounts.Find(id);
            if (account == null)
                return NotFound();

            _context.Accounts.Remove(account);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
