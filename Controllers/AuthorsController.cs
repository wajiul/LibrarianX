using LibrarianX.DTO;
using LibrarianX.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrarianX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorRepository _authorRepository;

        public AuthorsController(AuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorDto authorDto)
        {
            try
            {
                var authorExist = await _authorRepository.AuthorExist(authorDto.AuthorName);
                if (authorExist)
                {
                    return Conflict(new { Message = "Author with the specified name already exist" });
                }
                var addedAuthor = await _authorRepository.AddAuthor(authorDto);
                return CreatedAtAction(nameof(GetAuthors), new { authorId = addedAuthor.AuthorId }, addedAuthor);

            }
            catch(Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            try
            {
                var authors =  await _authorRepository.GetAuthors();
                return Ok(authors);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Srror" });
            }
        }

        [HttpGet("{authorId}")]
        public async Task<IActionResult> GetAuthor(int authorId)
        {
            try
            {
                var author = await _authorRepository.GetAuthor(authorId);
                return Ok(author);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Srror" });
            }
        }

    }
}
