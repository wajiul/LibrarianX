using LibrarianX.DTO;
using LibrarianX.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibrarianX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookRepository _bookRepository;

        public BooksController(BookRepository bookRepository) 
        {
            _bookRepository = bookRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookDto bookDto)
        {
            try
            {
                var bookExist = await _bookRepository.BookExistAsync(bookDto.Title);
                if(bookExist)
                {
                    return Conflict(new { Message = "Book with specified bookId already exist" });
                }
                var addedBook = await _bookRepository.AddBookAsync(bookDto);
                return CreatedAtAction(nameof(GetBook), new {bookId = bookDto.BookId}, addedBook);

            }
            catch(Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Srror" });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            try
            {
                var book = await _bookRepository.GetBookAsync(id);
                if(book == null)
                {
                    return NotFound();
                }
                return Ok(book);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Srror" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                var books = await _bookRepository.GetBooksAsync();
                return Ok(books);
            }
            catch(Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Srror" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody] BookDto bookDto)
        {
            try
            {
                await _bookRepository.UpdateBookAsync(bookDto);
                return Ok("Updated successfully");
            }
            catch(Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Srror" });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookRepository.DeleteBookAsync(id);
                return Ok("Deleted successfully");
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Srror" });
            }
        }

    }
}
