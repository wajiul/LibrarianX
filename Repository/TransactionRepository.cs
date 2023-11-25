using LibrarianX.Data;
using LibrarianX.DTO;
using LibrarianX.Model;
using Microsoft.EntityFrameworkCore;

namespace LibrarianX.Repository
{
    public class TransactionRepository
    {
        private readonly LibrarianXDbContext _context;

        public TransactionRepository(LibrarianXDbContext context)
        {
            _context = context;
        }

        public async Task<TransactionDto> AddTransactionAsync(TransactionDto transactionDto)
        {
            var transaction = new Transaction()
            {
                BookId = transactionDto.BookId,
                UserId = transactionDto.UserId,
                CheckoutDate = transactionDto.CheckoutDate,
                DueDate = transactionDto.DueDate,
            };
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            transactionDto.Id = transaction.Id;
            return transactionDto;
        }

        public async Task<bool> UpdateTransactionAsync(TransactionDto transactionDto)
        {
            var transacton = new Transaction()
            {
                Id = transactionDto.Id,
                BookId = transactionDto.BookId,
                UserId = transactionDto.UserId,
                CheckoutDate = transactionDto.CheckoutDate,
                DueDate = transactionDto.DueDate,
                ReturnDate = transactionDto.ReturnDate
            };

            try
            {
                _context.Transactions.Attach(transacton);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(InvalidOperationException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return false;
            }
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TransactionDto>> GetTransactionsAsync()
        {
            var transactions = await _context.Transactions.Select(t => new TransactionDto()
            {
                Id = t.Id,
                BookId= t.BookId,
                UserId = t.UserId,  
                CheckoutDate = t.CheckoutDate,  
                DueDate = t.DueDate,
                ReturnDate = t.ReturnDate
            }).ToListAsync();

            return transactions;
        }

        public async Task<TransactionDto> GetTransactionByIdAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if(transaction == null)
            {
                return null;
            }

            return new TransactionDto()
            {
                Id = transaction.Id,
                BookId = transaction.BookId,    
                UserId = transaction.UserId,
                CheckoutDate = transaction.CheckoutDate,
                DueDate = transaction.DueDate,
                ReturnDate = transaction.ReturnDate
            };
        }

       
        public async Task<List<TransactionDto>> GetTransactionsOfDueDateOverAsync()
        {
            var transactions = await _context.Transactions.Where(d => d.DueDate < DateTime.Now).Select(t => new TransactionDto()
            {
                Id= t.Id,
                BookId= t.BookId,
                UserId= t.UserId,
                CheckoutDate = t.CheckoutDate,
                DueDate = t.DueDate,
                ReturnDate = t.ReturnDate
            }).ToListAsync();

            return transactions;
        }


        public async Task<List<TransactionDto>> GetTransactionsOfUserAsync(int userId)
        {
            var transactions = await _context.Transactions.Where(u => u.UserId == userId).Select(t => new TransactionDto()
            {
                Id = t.Id,
                BookId = t.BookId,
                UserId = t.UserId,
                CheckoutDate = t.CheckoutDate,
                DueDate = t.DueDate,
                ReturnDate = t.ReturnDate
            }).ToListAsync();

            return transactions;
        }

    }
}
