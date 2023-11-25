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

        public async Task<bool> AuthorExistAsync(string authorName)
        {
            return await _context.Authors.AnyAsync(a => a.AuthorName == authorName);
        }
        public async Task<AuthorDto> AddAuthorAsync(AuthorDto authorDto)
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

        public async Task<List<AuthorDto>> GetAuthorsAsync()
        {
            return await _context.Authors.Select(a => new AuthorDto()
            {
                AuthorId = a.AuthorId, 
                AuthorName = a.AuthorName

            }).ToListAsync();
        }

        public async Task<AuthorDto> GetAuthorAsync(int authorId)
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

        public async Task<bool> UpdateAuthorAsync(AuthorDto authorDto)
        {
            try
            {
                var author = new Author()
                {
                    AuthorId = authorDto.AuthorId,
                    AuthorName = authorDto.AuthorName
                };

                _context.Update(author);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (InvalidOperationException) 
            {
                return false;
            }
        }

        public async Task<bool> DeleteAuthorAsync(int authorId)
        {
            var author = await _context.Authors.FindAsync(authorId);
            if(author == null)
            {
                return false;
            }
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
