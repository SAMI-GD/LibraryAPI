using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models
{
    public class LibraryItem
    {
        public int ItemID { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        public string Author { get; set; } 
        public DateTime PublicationDate { get; set; }
        public ItemType ItemType { get; set; }
        public AvailabilityStatus AvailabilityStatus { get; set; }

        // Navigation property
        public ICollection<BorrowTransaction> BorrowTransactions { get; set; }

    }
}
