using System.ComponentModel.DataAnnotations;

namespace LibrarianX.Model
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime CheckoutDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; } 
        
        public Book Book { get; set; }
        public User User { get; set; }

    }
}
