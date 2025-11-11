using Microsoft.AspNetCore.Mvc;
using MovieRental.Customers;
using MovieRental.Middleware;

namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerFeature _features;

        public CustomerController(ICustomerFeature features)
        {
            _features = features;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            if (string.IsNullOrEmpty(customer.CustomerName))
            {
                return BadRequest("Customer name must be provided!");
            }

            var saved = await _features.CreateAsync(customer);
            return Ok(saved);
        }

        //create new endpoint to get rental services by customer name (exact)
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _features.GetAllAsync();

            if (customers == null || !customers.Any())
                //call error middleware
                throw new CustomerNotFoundException();

            return Ok(customers);
        }
    }
}