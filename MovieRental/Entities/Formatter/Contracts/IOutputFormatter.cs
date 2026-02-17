using System.Collections.Generic;

namespace MovieRental.Entities.Formatter.Contracts
{
    public interface IOutputFormatter
    {
        string FormatHeader(string customerName);

        string FormatRentalsTable(IEnumerable<Rental> rentals);

        string FormatRentals(Movie movie, string rentalAmount);

        string FormatTotalAmountOwned(IEnumerable<Rental> rentals);

        string FormatTotalRenterPoints(IEnumerable<Rental> rentals);

        string FormatDaysRented(Rental rental);
    }
}