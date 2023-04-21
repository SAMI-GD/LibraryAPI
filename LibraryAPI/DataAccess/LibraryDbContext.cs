using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DataAccess
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }

        public DbSet<LibraryItem> LibraryItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BorrowTransaction> BorrowTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BorrowTransaction>()
                .HasKey(bt => bt.TransactionID);

            modelBuilder.Entity<BorrowTransaction>()
                .Property(bt => bt.LateFee)
                .HasPrecision(18, 2);

            modelBuilder.Entity<LibraryItem>()
                .HasKey(li => li.ItemID);

            modelBuilder.Entity<LibraryItem>()
                .HasMany(li => li.BorrowTransactions)
                .WithOne(bt => bt.LibraryItem)
                .HasForeignKey(bt => bt.ItemID);

            modelBuilder.Entity<User>()
                .HasMany(u => u.BorrowTransactions)
                .WithOne(bt => bt.User)
                .HasForeignKey(bt => bt.UserID);

            // Seed data
            modelBuilder.Entity<LibraryItem>().HasData(
                new LibraryItem { ItemID = 1, Title = "Book1", Author = "Author1", PublicationDate = new DateTime(2000, 1, 1), ItemType = ItemType.Book, AvailabilityStatus = AvailabilityStatus.Available },
                new LibraryItem { ItemID = 2, Title = "Magazine1", Author = "Author2", PublicationDate = new DateTime(2005, 6, 1), ItemType = ItemType.Magazine, AvailabilityStatus = AvailabilityStatus.Available },
                new LibraryItem { ItemID = 3, Title = "Newspaper1", Author = "Author3", PublicationDate = new DateTime(2005, 6, 1), ItemType = ItemType.Newspaper, AvailabilityStatus = AvailabilityStatus.Available }
            );

            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, FirstName = "Samila", LastName = "Gunarathna", Email = "samilagunarathna@gmail.com", PhoneNumber = "076-938-6116" },
                new User { UserID = 2, FirstName = "Bob", LastName = "Williams", Email = "bob.williams@gmail.com", PhoneNumber = "123-456-7891" }
            );

            modelBuilder.Entity<BorrowTransaction>().HasData(
                new BorrowTransaction { TransactionID = 1, UserID = 1, ItemID = 1, BorrowDate = new DateTime(2023, 1, 1), DueDate = new DateTime(2023, 1, 15), LateFee = 0 },
                new BorrowTransaction { TransactionID = 2, UserID = 2, ItemID = 3, BorrowDate = new DateTime(2023, 1, 1), DueDate = new DateTime(2023, 1, 15), LateFee = 0 }
            );


        }

    }
}
