using System.ComponentModel.DataAnnotations;

namespace LibrarianX.Model
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [Required]
        public string AuthorName { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
