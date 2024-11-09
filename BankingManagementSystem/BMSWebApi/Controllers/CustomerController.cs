using BMSWebApi.DTO;
using BMSWebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly BMSDbContext _context;
        public CustomerController(BMSDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// This action is to get all the accounts list
        /// </summary>
        /// <returns>list of accounts</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }


        /// <summary>
        /// This action will get the customer by id
        /// </summary>
        /// <param name="id">Particular Id of the customer for which you want the customer details.</param>
        /// <returns>Customer</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }


        /// <summary>
        /// This action helps us to edit the customer details
        /// </summary>
        /// <param name="id">CustomerId and Modified Customer Details</param>
        /// <param name="customerDTO"></param>
        /// <returns>Nothing</returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerDTO customerDTO)
        {
            Customer customer = new Customer();
            customer.CustomerId = customerDTO.CustomerId;
            customer.FirstName = customerDTO.FirstName;
            customer.LastName = customerDTO.LastName;
            customer.Email = customerDTO.Email;
            customer.PhoneNumber = customerDTO.PhoneNumber;
            customer.Address = customerDTO.Address;
            customer.PasswordHash = customerDTO.PasswordHash;
            customer.RoleId = customerDTO.RoleId;

            //Write your logic here

            return NoContent();
        }

        /// <summary>
        /// This action is to create a customer and update in database
        /// </summary>
        /// <param name="customerDTO">New Customer details</param>
        /// <returns>The updated new record details</returns>

        [HttpPost]
        public async Task<ActionResult<Account>> PostCustomer(CustomerDTO customerDTO)
        {
            Customer customer = new Customer();
            customer.CustomerId = customerDTO.CustomerId;
            customer.FirstName = customerDTO.FirstName;
            customer.LastName = customerDTO.LastName;
            customer.Email = customerDTO.Email;
            customer.PhoneNumber = customerDTO.PhoneNumber;
            customer.Address = customerDTO.Address;
            customer.PasswordHash = customerDTO.PasswordHash;
            customer.RoleId = customerDTO.RoleId;

            //Write your logic here


            return CreatedAtAction("GetCustomerById", new { id = customer.CustomerId, customer });
        }

        /// <summary>
        /// This action is to delete the customer
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns>Nothing</returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            //Write your logic here

            return NoContent();
        }

        /// <summary>
        /// This method is to know whether the account is present.
        /// </summary>
        /// <param name="id">Account Id</param>
        /// <returns>True or False</returns>

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
