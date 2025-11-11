using Microsoft.AspNetCore.Mvc;
using MovieRental.Rental;

namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {

        private readonly IRentalFeatures _features;

        public RentalController(IRentalFeatures features)
        {
            _features = features;
        }


        //when we change the async keyword we are telling c# that this method can run asynchronously
        //after also changing the return type and add Task<T>
        //we are allowing the framework to free the thread while waiting for the response of the async method
        //the wait keyword pauses the method without blocking the thread
        //when the operation finishes, the method can resume and proceed to the next line
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Rental.Rental rental)
        {
            var saved = await _features.SaveAsync(rental);
            return Ok(saved);
        }

        //create new endpoint to get rental services by customer name (exact)
        [HttpGet("rentals-by-customer/{customerName}")]
        public async Task<IActionResult> GetRentalsByCustomerName(string customerName)
        {
            var rentals = await _features.GetRentalsByCustomerNameAsync(customerName);

            if (rentals == null || !rentals.Any())
                return NotFound($"No rentals found for customer '{customerName}'.");

            return Ok(rentals);
        }

    }
}
