using LibraryAPI.DataAccess;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repositories
{
    public class LibraryItemRepository : ILibraryItemRepository
    {
        private readonly LibraryDbContext _context;

        public LibraryItemRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LibraryItem>> GetAllAsync()
        {
            return await _context.LibraryItems.ToListAsync();
        }

        public async Task<LibraryItem> GetByIdAsync(int id)
        {
            return await _context.LibraryItems.FindAsync(id);
        }

        public async Task AddAsync(LibraryItem libraryItem)
        {
            await _context.LibraryItems.AddAsync(libraryItem);
        }

        public void Update(LibraryItem libraryItem)
        {
            _context.Entry(libraryItem).State = EntityState.Modified;
        }

        public void Delete(LibraryItem libraryItem)
        {
            _context.LibraryItems.Remove(libraryItem);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }



        public async Task<IEnumerable<LibraryItem>> SearchByTitleAsync(string title)
        {
            if (title.Length < 3)
            {
                return new List<LibraryItem>();
            }

            return await _context.LibraryItems
                .Where(li => EF.Functions.Like(li.Title, $"%{title}%"))
                .ToListAsync();
        }


        public async Task<IEnumerable<LibraryItem>> SearchByAuthorAndAvailabilityAsync(string author, AvailabilityStatus availabilityStatus)
        {
            return await _context.LibraryItems
                .Where(li => li.Author.Contains(author) && li.AvailabilityStatus == availabilityStatus)
                .ToListAsync();
        }



    }
}
