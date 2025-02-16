using System.ComponentModel.DataAnnotations;

namespace BackendChallenge.Model
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string Isbn13 { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
