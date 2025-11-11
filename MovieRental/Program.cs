using MovieRental.Data;
using MovieRental.Movie;
using MovieRental.Rental;
using MovieRental.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkSqlite().AddDbContext<MovieRentalDbContext>();

//since the RentalFeatures implements IRentalFeatures and the IRentalFeatures is registred 
//as a Singleton, the scope cannot change 
//Singletons are created once in a project lifetime
//change the registration to be scoped
//builder.Services.AddSingleton<IRentalFeatures, RentalFeatures>();
builder.Services.AddScoped<IRentalFeatures, RentalFeatures>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<GlobalExceptionMiddleware>();

using (var client = new MovieRentalDbContext())
{
	client.Database.EnsureCreated();
}

app.Run();
