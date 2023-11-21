using System.ComponentModel.DataAnnotations;

namespace LibrarianX.DTO
{
    public class BookDto
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public DateTime PublicationDate { get; set; }
        [Required]
        public string Genre { get; set; }
    }
}
