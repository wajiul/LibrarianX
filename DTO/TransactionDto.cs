using System.ComponentModel.DataAnnotations;

namespace LibrarianX.DTO
{
    public class TransactionDto
    {
        [Required]
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
    }
}
