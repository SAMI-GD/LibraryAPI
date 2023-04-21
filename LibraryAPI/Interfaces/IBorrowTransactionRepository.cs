using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    public interface IBorrowTransactionRepository
    {
        Task<IEnumerable<BorrowTransaction>> GetAllAsync();
        Task<BorrowTransaction> GetByIdAsync(int id);
        Task AddAsync(BorrowTransaction borrowTransaction);
        void Update(BorrowTransaction borrowTransaction);
        void Delete(BorrowTransaction borrowTransaction);
        Task SaveChangesAsync();

        Task<IEnumerable<BorrowTransaction>> GetBorrowingHistoryByUserIdAsync(int userId);
        Task<bool> UserHasTransactionsAsync(int userId);
        Task<bool> LibraryItemHasTransactionsAsync(int itemId);

    }
}
