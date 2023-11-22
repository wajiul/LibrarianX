using System.ComponentModel.DataAnnotations;

namespace LibrarianX.DTO
{
    public class AuthorDto
    {
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public string AuthorName { get; set; }
    }
}
