using LibrarianX.Data;
using LibrarianX.DTO;
using LibrarianX.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LibrarianX.Repository
{
    public class UserRepository
    {
        private readonly LibrarianXDbContext _context;

        public UserRepository(LibrarianXDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExistAsync(string Email)
        {
            return await _context.Users.AnyAsync(u => u.Email == Email);
        }
        public async Task<bool> UserExistAsync(int userId)
        {
            return await _context.Users.AnyAsync(u => u.UserId == userId);
        }

        public async Task<UserDto> AddUserAsync(UserDto userDto)
        {
            var user = new User()
            {
                Email = userDto.Email,
                Name = userDto.Name,
                Phone = userDto.Phone
            };
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            userDto.UserId = user.UserId;
            return userDto;
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
           return await _context.Users.Select(u => new UserDto
                {
                    UserId = u.UserId,
                    Email = u.Email,
                    Name = u.Name,
                    Phone = u.Phone
                }).ToListAsync();

        }

        public async Task<UserDto> GetUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if(user != null)
            {
                return new UserDto
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    Name = user.Name,
                    Phone = user.Phone
                };
            }
            return null;
        }

        public async Task<bool> UpdateUserAsync(UserDto userDto)
        {
            try
            {
                var user = new User()
                {
                    Name = userDto.Name,
                    Phone = userDto.Phone
                };
                await _context.SaveChangesAsync();
                return true;

            }
            catch(InvalidOperationException) 
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {

            var user = await _context.Users.FindAsync(userId);
            if(user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
