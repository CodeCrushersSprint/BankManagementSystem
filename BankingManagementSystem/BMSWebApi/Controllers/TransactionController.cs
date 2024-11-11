using BMSWebApi.DTO;
using BMSWebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly BMSDbContext _context;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(BMSDbContext context, ILogger<TransactionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// This action is to get all the transactions list
        /// </summary>
        /// <returns>list of transactions</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            _logger.LogInformation("Fetched all the transactions data");
            return await _context.Transactions.ToListAsync();
        }


        /// <summary>
        /// This action will get the transaction by id
        /// </summary>
        /// <param name="id">Particular Id of the transaction for which you want the transaction details.</param>
        /// <returns>Transaction</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransactionById(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            _logger.LogInformation($"Fetched the transaction of {id}");
            if (transaction == null)
            {
                return NotFound();
            }
            return transaction;
        }


        /// <summary>
        /// This action helps us to edit the transaction details
        /// </summary>
        /// <param name="id">TransactionId and Modified Transaction Details</param>
        /// <param name="transactionDTO"></param>
        /// <returns>Nothing</returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, TransactionDTO transactionDTO)
        {
            Transaction transaction = new Transaction();
            transaction.TransactionId = transactionDTO.TransactionId;
            transaction.AccountId = transactionDTO.AccountId;
            transaction.Amount = transactionDTO.Amount;
            transaction.TransactionType = transactionDTO.TransactionType;
            transaction.TransactionDate = transactionDTO.TransactionDate;


            //Write your logic here

            if (id != transaction.TransactionId)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated the transaction of {id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //By Deepthi

            return NoContent();
        }

        /// <summary>
        /// This action is to create a transaction and update in database
        /// </summary>
        /// <param name="transactionDTO">New Transaction details</param>
        /// <returns>The updated new record details</returns>

        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(TransactionDTO transactionDTO)
        {
            Transaction transaction = new Transaction();
            transaction.TransactionId = transactionDTO.TransactionId;
            transaction.AccountId = transactionDTO.AccountId;
            transaction.Amount = transactionDTO.Amount;
            transaction.TransactionType = transactionDTO.TransactionType;
            transaction.TransactionDate = transactionDTO.TransactionDate;

            //Write your logic here
            _context.Transactions.Add(transaction);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Created the transaction of {transaction.TransactionId}");
            }
            catch (DbUpdateException)
            {
                if (TransactionExists(transaction.TransactionId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }

            }

            //By Surabhi

            return CreatedAtAction("GetTransactionById", new { id = transaction.TransactionId }, transaction);
        }

        /// <summary>
        /// This action is to delete the transaction
        /// </summary>
        /// <param name="id">Transaction Id</param>
        /// <returns>Nothing</returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            //Write your logic here

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Deleted the transaction of {transaction.TransactionId}");

            //By Archana

            return NoContent();
        }

        /// <summary>
        /// This method is to know whether the transaction is present.
        /// </summary>
        /// <param name="id">Transaction Id</param>
        /// <returns>True or False</returns>

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }

    }
}
