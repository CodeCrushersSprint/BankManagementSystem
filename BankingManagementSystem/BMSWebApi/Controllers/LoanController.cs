using BMSWebApi.DTO;
using BMSWebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly BMSDbContext _context;
        private readonly ILogger<AccountController> _logger;
        public LoanController(BMSDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// This action is to get all the loans list
        /// </summary>
        /// <returns>list of loans</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
        {
            _logger.LogInformation("Fetched all the loans data");
            return await _context.Loans.ToListAsync();
        }


        /// <summary>
        /// This action will get the loan by id
        /// </summary>
        /// <param name="id">Particular Id of the loan for which you want the loan details.</param>
        /// <returns>Loan</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoanById(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            _logger.LogInformation($"Fetched the loan of {id}");
            if (loan == null)
            {
                return NotFound();
            }
            return loan;
        }


        /// <summary>
        /// This action helps us to edit the loan details
        /// </summary>
        /// <param name="id">LoanId and Modified Loan Details</param>
        /// <param name="loanDTO"></param>
        /// <returns>Nothing</returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLoan(int id, LoanDTO loanDTO)
        {
            Loan loan = new Loan();
            loan.LoanId = loanDTO.LoanId;
            loan.CustomerId = loanDTO.CustomerId;
            loan.LoanAmount = loanDTO.LoanAmount;
            loan.InterestRate = loanDTO.InterestRate;
            loan.Status = loanDTO.Status;
            loan.ApplicationDate = loanDTO.ApplicationDate;


            //Write your logic here

            if (id != loan.LoanId)
            {
                return BadRequest();
            }

            _context.Entry(loan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated the loan of {id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanExists(id))
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
        /// This action is to create a loan and update in database
        /// </summary>
        /// <param name="loanDTO">New Loan details</param>
        /// <returns>The updated new record details</returns>

        [HttpPost]
        public async Task<ActionResult<Loan>> PostLoan(LoanDTO loanDTO)
        {
            Loan loan = new Loan();
            loan.LoanId = loanDTO.LoanId;
            loan.CustomerId = loanDTO.CustomerId;
            loan.LoanAmount = loanDTO.LoanAmount;
            loan.InterestRate = loanDTO.InterestRate;
            loan.Status = loanDTO.Status;
            loan.ApplicationDate = loanDTO.ApplicationDate;

            //Write your logic here
            _context.Loans.Add(loan);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Created the loan of {loan.LoanId}");
            }
            catch (DbUpdateException)
            {
                if (LoanExists(loan.LoanId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }

            }

            //By Surabhi

            return CreatedAtAction("GetLoanById", new { id = loan.LoanId }, loan);
        }

        /// <summary>
        /// This action is to delete the loan
        /// </summary>
        /// <param name="id">Loan Id</param>
        /// <returns>Nothing</returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            //Write your logic here
            var loan = await _context.Loans.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Removed the loan of {id}");

            return NoContent();
        }

        /// <summary>
        /// This method is to know whether the loan is present.
        /// </summary>
        /// <param name="id">Loan Id</param>
        /// <returns>True or False</returns>

        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.LoanId == id);
        }
    }
}
