using LibrarianX.Data;
using LibrarianX.DTO;
using LibrarianX.Model;
using Microsoft.EntityFrameworkCore;

namespace LibrarianX.Repository
{
    public class AuthorRepository
    {
        private readonly LibrarianXDbContext _context;

        public AuthorRepository(LibrarianXDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AuthorExist(string authorName)
        {
            return await _context.Authors.AnyAsync(a => a.AuthorName == authorName);
        }
        public async Task<AuthorDto> AddAuthor(AuthorDto authorDto)
        {
            var author = new Author()
            {
                AuthorName = authorDto.AuthorName
            };
            await _context.AddAsync(author);
            await _context.SaveChangesAsync();

            authorDto.AuthorId = author.AuthorId;

            return authorDto;
        }

        public async Task<List<AuthorDto>> GetAuthors()
        {
            return await _context.Authors.Select(a => new AuthorDto()
            {
                AuthorId = a.AuthorId, 
                AuthorName = a.AuthorName

            }).ToListAsync();
        }

        public async Task<AuthorDto> GetAuthor(int authorId)
        {
           var author =  await _context.Authors.FindAsync(authorId);

           if(author != null)
            {
                return new AuthorDto()
                {
                    AuthorId = author.AuthorId,
                    AuthorName = author.AuthorName
                };
            }
            return null;
        }

    }
}
