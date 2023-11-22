using System.ComponentModel.DataAnnotations;

namespace LibrarianX.DTO
{
    public class UserDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
