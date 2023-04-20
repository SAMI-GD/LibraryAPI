using LibraryAPI.DataAccess;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repositories
{
    public class BorrowTransactionRepository : IBorrowTransactionRepository
    {
        private readonly LibraryDbContext _context;

        public BorrowTransactionRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BorrowTransaction>> GetAllAsync()
        {
            return await _context.BorrowTransactions
                .Include(bt => bt.User)
                .Include(bt => bt.LibraryItem)
                .ToListAsync();
        }

        public async Task<BorrowTransaction> GetByIdAsync(int id)
        {
            return await _context.BorrowTransactions
                .Include(bt => bt.User)
                .Include(bt => bt.LibraryItem)
                .FirstOrDefaultAsync(bt => bt.TransactionID == id);
        }

        public async Task AddAsync(BorrowTransaction borrowTransaction)
        {
            await _context.BorrowTransactions.AddAsync(borrowTransaction);
        }

        public void Update(BorrowTransaction borrowTransaction)
        {
            _context.Entry(borrowTransaction).State = EntityState.Modified;
        }

        public void Delete(BorrowTransaction borrowTransaction)
        {
            _context.BorrowTransactions.Remove(borrowTransaction);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
