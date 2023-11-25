using LibrarianX.DTO;
using LibrarianX.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibrarianX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionRepository _transactionRepository;

        public TransactionsController(TransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] TransactionDto transactionDto)
        {
            try
            {
                var addedTransaction = await _transactionRepository.AddTransactionAsync(transactionDto);
                return CreatedAtAction(nameof(GetTransaction), new { id =  addedTransaction.Id }, addedTransaction);
                
            }
            catch (Exception) 
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTransaction(int id)
        {
            try
            {
                var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
                if(transaction == null)
                {
                    return NotFound();
                }

                return Ok(transaction);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            try
            {
                var transactions = await _transactionRepository.GetTransactionsAsync();
                if(transactions == null)
                {
                    return NotFound();
                }
                return Ok(transactions);
            }
            catch(Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransaction([FromBody] TransactionDto transactionDto)
        {
            try
            {
                await _transactionRepository.UpdateTransactionAsync(transactionDto);
                return Ok("Updated successfully");
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            try
            {
                await _transactionRepository.DeleteAsync(id);
                return Ok("Deleted successfully");
            }
            catch(Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpGet]
        [Route("user/{userId}")]
        public async Task<IActionResult> TransactionOfUser(int userId)
        {
            try
            {
                var transactions = await _transactionRepository.GetTransactionsOfUserAsync(userId);
                if(transactions == null)
                {
                    return NotFound();
                }
                return Ok(transactions);
            }
            catch(Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }


        [HttpGet]
        [Route("due")]
        public async Task<IActionResult> TransactionsWithDueDateOver()
        {
            try
            {
                var transactions = await _transactionRepository.GetTransactionsOfDueDateOverAsync();
                if (transactions == null)
                {
                    return NotFound();
                }
                return Ok(transactions);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }
    }
}
