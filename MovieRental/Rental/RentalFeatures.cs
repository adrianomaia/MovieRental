using Microsoft.EntityFrameworkCore;
using MovieRental.Data;

namespace MovieRental.Rental
{
	public class RentalFeatures : IRentalFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		public RentalFeatures(MovieRentalDbContext movieRentalDb)
		{
			//since the RentalFeatures implements IRentalFeatures and the IRentalFeatures is registred 
			//as a Singleton, the scope cannot change 
			//Singletons are created once in a project lifetime
			//change the registration to be scoped
			_movieRentalDb = movieRentalDb;
		}

		//TODO: make me async :(
		//in synchronous calls, the thread is blocked while the operation is processed
		//when we change the async keyword we are telling c# that this method can run asynchronously
		//in the return type we need to add Task<T> and use the async methods to add and save changes
		public async Task<Rental> SaveAsync(Rental rental)
		{
			await _movieRentalDb.Rentals.AddAsync(rental);
			await _movieRentalDb.SaveChangesAsync();
			return rental;
		}

		//TODO: finish this method and create an endpoint for it
		public async Task<IEnumerable<Rental>> GetRentalsByCustomerNameAsync(string customerName)
		{
			return await _movieRentalDb.Rentals
			.Include(d => d.Customer)
			.Where(d => d.Customer.CustomerName.Trim() == customerName.Trim())
			.ToListAsync();

			//we can search by exact name like the above 
			//or check if contains
			//return await _movieRentalDb.Rentals
			//.Where(d => d.CustomerName.Contains(customerName.Trim()))
			//.ToListAsync();

		}

	}
}
