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
                var authorExist = await _authorRepository.AuthorExistAsync(authorDto.AuthorName);
                if (authorExist)
                {
                    return Conflict(new { Message = "Author with the specified name already exist" });
                }
                var addedAuthor = await _authorRepository.AddAuthorAsync(authorDto);
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
                var authors =  await _authorRepository.GetAuthorsAsync();
                return Ok(authors);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Srror" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            try
            {
                var author = await _authorRepository.GetAuthorAsync(id);
                return Ok(author);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Srror" });
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorDto authorDto)
        {
            if(id != authorDto.AuthorId)
            {
                return BadRequest();
            }

            try
            {
                var status = await _authorRepository.UpdateAuthorAsync(authorDto);
                if(status == false)
                {
                    return BadRequest();
                }
                return Ok("Updated successfully");
            }
            catch(Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Srror" });
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var status = await _authorRepository.DeleteAuthorAsync(id);
                if(status == false)
                {
                    return BadRequest();
                }

                return Ok("Deleted successfully");
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Srror" });
            }
        }

    }
}
