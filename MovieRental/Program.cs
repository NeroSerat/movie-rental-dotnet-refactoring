using MovieRental.Entities;
using MovieRental.Entities.Formatter;
using MovieRental.Entities.PriceCodes;
using MovieRental.Enums;
using System;
using System.Collections.Generic;

namespace MovieRental;

public class Program
{
    static void Main(string[] args)
    {
        try
        {
            var movie = new Movie("Jaws", PriceCode.GetPriceCode(MovieRatingEnum.REGULAR));
            var movie2 = new Movie("The Hangover", PriceCode.GetPriceCode(MovieRatingEnum.NEW_RELEASE));
            var movie3 = new Movie("TMNT", PriceCode.GetPriceCode(MovieRatingEnum.CHILDREN));
            var rental = new Rental(movie, 2);
            var rental2 = new Rental(movie2, 6);
            var rental3 = new Rental(movie3, 1);
            var customer = new Customer("John Doe");
            var customerHistory = new CustomerRentalHistory(customer, new List<Rental>() { rental, rental2, rental3 }, OutputFormatter.CreateOutputFormatter(false));
            var customerHistory2 = new CustomerRentalHistory(customer, new List<Rental>() { rental, rental2, rental3 }, OutputFormatter.CreateOutputFormatter(true));

            var statement = new Services.GenerateStatementService().CreateStatement(customerHistory);
            var statement2 = new Services.GenerateStatementService().CreateStatement(customerHistory2);

            Console.WriteLine(statement);
            Console.WriteLine("\n");
            Console.WriteLine(statement2);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
