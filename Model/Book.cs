using System.ComponentModel.DataAnnotations;

namespace LibrarianX.Model
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Genre { get; set; }
        public Author Author { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

    }
}
