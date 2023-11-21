using LibrarianX.Model;
using Microsoft.EntityFrameworkCore;

namespace LibrarianX.Data
{
    public class LibrarianXDbContext: DbContext
    {
        public LibrarianXDbContext(DbContextOptions<LibrarianXDbContext> options): base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
