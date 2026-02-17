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
            var movie1 = new Movie("Jaws", PriceCode.GetPriceCode(MovieRatingEnum.REGULAR));
            var movie3 = new Movie("TMNT", PriceCode.GetPriceCode(MovieRatingEnum.CHILDREN));
            var movie2 = new Movie("The Hangover", PriceCode.GetPriceCode(MovieRatingEnum.NEW_RELEASE));

            var rental1 = new Rental(movie1, 2);
            var rental2 = new Rental(movie2, 6);
            var rental3 = new Rental(movie3, 1);

            var customer = new Customer("John Doe");
            var textCustomerHistory = new CustomerRentalHistory(customer, new List<Rental>() { rental1, rental2, rental3 }, OutputFormatter.CreateOutputFormatter(false));
            var htmlCustomerHistory = new CustomerRentalHistory(customer, new List<Rental>() { rental1, rental2, rental3 }, OutputFormatter.CreateOutputFormatter(true));

            Console.WriteLine("--------------------------------------------------------------");
            //Statement 1 => text output
            var textStatement = new Services.GenerateStatementService().CreateStatement(textCustomerHistory);
            Console.WriteLine("Text statement: \n");
            Console.WriteLine(textStatement);
            Console.WriteLine("--------------------------------------------------------------");

            //Statement 2 => html output
            var htmlStatement = new Services.GenerateStatementService().CreateStatement(htmlCustomerHistory);
            Console.WriteLine("HTML statement: \n");
            Console.WriteLine(htmlStatement);
            Console.WriteLine("--------------------------------------------------------------");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}