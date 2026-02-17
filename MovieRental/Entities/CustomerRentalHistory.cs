using MovieRental.Entities.Formatter.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieRental.Entities;

public class CustomerRentalHistory
{
    public Customer Customer { get; set; }
    public IEnumerable<Rental> Rentals { get; set; }

    public IOutputFormatter OutputFormatter { get; set; }

    public CustomerRentalHistory(Customer customer, IEnumerable<Rental> rentals, IOutputFormatter outputFormatter)
    {
        if (customer == null || rentals == null || !rentals.Any())
            throw new NullReferenceException("Customer and customer's rentals cannot be null or empty");

        Customer = customer;
        Rentals = rentals;
        OutputFormatter = outputFormatter;
    }

    public string GenerateCustomerRentalHistory()
    {
        string rentalHistoryOutput = string.Empty;
        rentalHistoryOutput += OutputFormatter.FormatHeader(Customer.Name);
        rentalHistoryOutput += OutputFormatter.FormatRentalsTable(Rentals);
        rentalHistoryOutput += OutputFormatter.FormatTotalAmountOwned(Rentals);
        rentalHistoryOutput += OutputFormatter.FormatTotalRenterPoints(Rentals);
        foreach (var Rental in Rentals)
        {
            rentalHistoryOutput += OutputFormatter.FormatDaysRented(Rental);
        }
        return rentalHistoryOutput;
    }
}