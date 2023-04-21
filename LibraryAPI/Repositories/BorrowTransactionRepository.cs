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
            List<BorrowTransaction> borrowTransactions = await _context.BorrowTransactions
                .Include(bt => bt.User)
                .Include(bt => bt.LibraryItem)
                .ToListAsync();

            return borrowTransactions;
        }

        public async Task<BorrowTransaction> GetByIdAsync(int id)
        {
            BorrowTransaction borrowTransaction = await _context.BorrowTransactions
                .Include(bt => bt.User)
                .Include(bt => bt.LibraryItem)
                .FirstOrDefaultAsync(bt => bt.TransactionID == id);

            return borrowTransaction;
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

        public async Task <IEnumerable<BorrowTransaction>> GetBorrowingHistoryByUserIdAsync(int userId)
        {
            return await _context.BorrowTransactions
                .Include(bt => bt.User)
                .Include(bt => bt.LibraryItem)
                .Where(bt =>bt.UserID == userId).ToListAsync();
        }

        public async Task<bool> UserHasTransactionsAsync(int userId)
        {
            return await _context.BorrowTransactions.AnyAsync(t => t.UserID == userId);
        }
        public async Task<bool> LibraryItemHasTransactionsAsync(int itemId)
        {
            return await _context.BorrowTransactions.AnyAsync(t => t.ItemID == itemId);
        }
    }
}
