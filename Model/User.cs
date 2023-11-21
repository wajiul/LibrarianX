using System.ComponentModel.DataAnnotations;

namespace LibrarianX.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<Transaction> Transactions { get; set; } 
    }
}
