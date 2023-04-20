using LibraryAPI.Models;

namespace LibraryAPI.DTOs
{
    public class LibraryItemDTO
    {
        public int ItemID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public ItemType ItemType { get; set; }
        public DateTime PublicationDate { get; set; }
        public AvailabilityStatus AvailabilityStatus { get; set; }
    }

    public class LibraryItemBasicDTO
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public ItemType ItemType { get; set; }
        public DateTime PublicationDate { get; set; }
        public AvailabilityStatus AvailabilityStatus { get; set; }
    }

}
