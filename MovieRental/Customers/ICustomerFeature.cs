namespace MovieRental.Customers;

public interface ICustomerFeature
{
	Task<Customer> CreateAsync(Customer customer);
    Task<IEnumerable<Customer>> GetAllAsync();
    
    //we could add more, for example, edit, delete, to obtain full CRUD
    //for this example, it's enough
}