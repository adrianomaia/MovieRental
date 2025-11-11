using MovieRental.Data;

namespace MovieRental.Movie
{
	public class MovieFeatures : IMovieFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		public MovieFeatures(MovieRentalDbContext movieRentalDb)
		{
			_movieRentalDb = movieRentalDb;
		}
		
		public Movie Save(Movie movie)
		{
			_movieRentalDb.Movies.Add(movie);
			_movieRentalDb.SaveChanges();
			return movie;
		}

		// TODO: tell us what is wrong in this method? Forget about the async, what other concerns do you have?
		public List<Movie> GetAll()
		{
			//possible problemas with this 
			//performance issues: table is loaded into memory which can, with time, grow exponentially
			//we should not expose entities directly to API (expose the schema): instead we should use DTOs, for example
			//contemplate some error handling when, for example, DB is out of reach
			
			//example with paging and with internal dto

			/*

			public List<MovieDto> GetAll(int page = 1, int pageSize = 50)
			{
				return _movieRentalDb.Movies
					.Skip((page - 1) * pageSize)
					.Take(pageSize)
					.Select(m => new MovieDto { Id = m.Id, Title = m.Title })
					.ToList();
			}

			*/

			return _movieRentalDb.Movies.ToList();
		}
	}
}
