using LibraryAPI.DataAccess;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

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

        //Search Books by Title repo
        public async Task<IEnumerable<LibraryItem>> SearchByTitleAndTypeAsync(string title, ItemType itemType)
        {
            if (title.Length < 3)
            { return new List<LibraryItem>(); }

            return await _context.LibraryItems
                .Where(li => li.Title.Contains(title) && li.ItemType == itemType)
                .ToListAsync();
        }


        //Search Items by Author and Availability Status
        public async Task<IEnumerable<LibraryItem>> SearchByAuthorAndAvailabilityAsync(string author, AvailabilityStatus availabilityStatus)
        {
            return await _context.LibraryItems
                .Where(li => li.Author.Contains(author) && li.AvailabilityStatus == availabilityStatus)
                .ToListAsync();
        }

        //Search Books by Author and Availability Status
        public async Task<IEnumerable<LibraryItem>> SearchByAuthorAndAvailabilityAndTypeAsync(string author, AvailabilityStatus availabilityStatus, ItemType itemType)
        {
            return await _context.LibraryItems
                .Where(li => li.Author.Contains(author) && li.AvailabilityStatus == availabilityStatus && li.ItemType == itemType)
                .ToListAsync();
        }



    }
}
