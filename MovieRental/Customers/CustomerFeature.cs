using Microsoft.EntityFrameworkCore;
using MovieRental.Customers;
using MovieRental.Data;

namespace MovieRental.Rental
{
    public class CustomerFeature : ICustomerFeature
    {
        private readonly MovieRentalDbContext _movieRentalDb;
        public CustomerFeature(MovieRentalDbContext movieRentalDb)
        {
            _movieRentalDb = movieRentalDb;
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            await _movieRentalDb.Customers.AddAsync(customer);
            await _movieRentalDb.SaveChangesAsync();
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var customers = await _movieRentalDb.Customers.ToListAsync();
            return customers;
        }
    }
}