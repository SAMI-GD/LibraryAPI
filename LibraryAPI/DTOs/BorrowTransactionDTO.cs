namespace LibraryAPI.DTOs
{
    public class BorrowTransactionDTO
    {
            public int TransactionID { get; set; }
            public int UserID { get; set; }
            public string UserName { get; set; }
            public int ItemID { get; set; }
            public string LibraryItemTitle { get; set; }
            public DateTime BorrowDate { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime? ReturnDate { get; set; }
            public decimal LateFee { get; set; }


    }

}
