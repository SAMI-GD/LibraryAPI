namespace LibraryAPI.DTOs
{
    public class BorrowTransactionDTO
    {
            public int TransactionID { get; set; }
            public int UserID { get; set; }
            public string UserName { get; set; } = string.Empty;
            public int ItemID { get; set; }
            public string LibraryItemTitle { get; set; } = string.Empty;
            public DateTime BorrowDate { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime? ReturnDate { get; set; }
            public decimal LateFee { get; set; }


    }

}
