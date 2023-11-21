using System.ComponentModel.DataAnnotations;

namespace LibrarianX.Model
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; } 
        
        public Book Book { get; set; }
        public User User { get; set; }

    }
}
