using System.ComponentModel.DataAnnotations;

namespace MovieRental.Customers
{
	public class Customer
	{
		[Key]
		public int Id { get; set; }
		public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }

        //navigation property for a 1 to many relationship
        //a customer, in my opinion, can have multiple rentals
        public ICollection<Rental.Rental>? Rentals { get; set; }
	}
}
