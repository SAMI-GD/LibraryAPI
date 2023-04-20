namespace LibraryAPI.Models
{
    public class BorrowTransaction
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public int ItemID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal LateFee { get; set; }

        // Navigation properties
        public User User { get; set; }
        public LibraryItem LibraryItem { get; set; }
    }
}
