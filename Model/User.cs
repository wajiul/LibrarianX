using System.ComponentModel.DataAnnotations;

namespace LibrarianX.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public ICollection<Transaction> Transactions { get; set; } 
    }
}
