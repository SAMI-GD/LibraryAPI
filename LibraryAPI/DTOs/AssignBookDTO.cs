namespace LibraryAPI.DTOs
{
    public class AssignBookDTO
    {
        public int UserID { get; set; }
        public int ItemID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
