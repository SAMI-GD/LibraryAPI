using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models
{
    public class User
    {
        public int UserID { get; set; }
        [MaxLength(150)]
        public string FirstName { get; set; }

        [MaxLength(150)]
        public string LastName { get; set; }

        [EmailAddress]
        [MaxLength(250)]
        public string Email { get; set; }

        [Phone]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        // Navigation property
        public ICollection<BorrowTransaction> BorrowTransactions { get; set; }


    }
}
