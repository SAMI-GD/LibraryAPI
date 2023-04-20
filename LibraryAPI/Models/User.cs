namespace LibraryAPI.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Navigation property
        public ICollection<BorrowTransaction> BorrowTransactions { get; set; }

    }
}
