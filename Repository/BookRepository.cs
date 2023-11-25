using LibrarianX.Data;
using LibrarianX.DTO;
using LibrarianX.Model;
using Microsoft.EntityFrameworkCore;

namespace LibrarianX.Repository
{
    public class BookRepository
    {
        private readonly LibrarianXDbContext _context;

        public BookRepository(LibrarianXDbContext context)
        {
            _context = context;
        }

        public async Task<bool> BookExistAsync(string bookTitle)
        {
            return await _context.Books.AnyAsync(b =>  b.Title == bookTitle);
        }

        public async Task<BookDto> AddBookAsync(BookDto bookDto)
        {
            var book = new Book()
            {
                Title = bookDto.Title,
                AuthorId = bookDto.AuthorId,
                PublicationDate = bookDto.PublicationDate,
                Genre = bookDto.Genre
            };

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            bookDto.BookId = book.BookId;
            return bookDto;
        }

        public async Task<List<BookDto>> GetBooksAsync()
        {
            return await _context.Books.Select(b => new BookDto()
            {
                BookId = b.BookId,
                Title = b.Title,
                AuthorId = b.AuthorId,
                PublicationDate = b.PublicationDate,
                Genre = b.Genre

            }).ToListAsync();
        }

        public async Task<BookDto> GetBookAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if(book == null)
            {
                return null;
            }

            return new BookDto()
            {
                BookId = book.BookId,
                Title = book.Title,
                AuthorId = book.AuthorId,
                PublicationDate = book.PublicationDate,
                Genre = book.Genre
            };
        }
        
        public async Task UpdateBookAsync(BookDto bookDto)
        {
            var book = new Book()
            {
                BookId = bookDto.BookId,
                Title = bookDto.Title,
                AuthorId = bookDto.AuthorId,
                PublicationDate= bookDto.PublicationDate,
                Genre = bookDto.Genre
            };
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

    }
}
