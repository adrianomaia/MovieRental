namespace MovieRental.Rental;

public interface IRentalFeatures
{
	//add Task<T> to make this extension method async
	Task<Rental> SaveAsync(Rental rental);
	Task<IEnumerable<Rental>> GetRentalsByCustomerNameAsync(string customerName);
}