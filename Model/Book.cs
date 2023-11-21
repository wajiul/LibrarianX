using System.ComponentModel.DataAnnotations;

namespace LibrarianX.Model
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public DateTime PublicationDate { get; set; }
        [Required]
        public string Genre { get; set; }
        public Author Author { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

    }
}
