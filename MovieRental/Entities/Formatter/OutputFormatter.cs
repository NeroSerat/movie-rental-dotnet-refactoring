using MovieRental.Constants.HtmlOutput;
using MovieRental.Constants.TextOutput;
using MovieRental.Entities.Formatter.Contracts;
using MovieRental.Entities.PriceCodes;
using System.Collections.Generic;
using System.Linq;

namespace MovieRental.Entities.Formatter;

public class OutputFormatter
{
    public static IOutputFormatter HtmlReport() => new HtmlOutputFormatter();

    public static IOutputFormatter TextReport() => new TextOutputFormatter();

    public static IOutputFormatter CreateOutputFormatter(bool isHtml) => isHtml ? HtmlReport() : TextReport();
}

public class HtmlOutputFormatter : IOutputFormatter
{
    public string FormatHeader(string customerName)
        => $"{HtmlTags.OPEN_TAG_TITLE_H1}" +
           $"{TextOutputValues.HEADER_CUSTOMER_RENTAL_RECORD}" +
           $"{CustomerNameHtmlOutput(customerName)}" +
           $"{HtmlTags.CLOSE_TAG_TITLE_H1}" +
           $"{TextOutputValues.NEW_LINE}";
    private string CustomerNameHtmlOutput(string customerName)
        => $"{HtmlTags.OPEN_TAG_EMPHASIS}" +
           $"{customerName}" +
           $"{HtmlTags.CLOSE_TAG_EMPHASIS}";

    public string FormatRentalsTable(IEnumerable<Rental> rentals)
        => $"{HtmlTags.OPEN_TAG_TABLE}" +
           $"{TextOutputValues.NEW_LINE}" +
           $"{string.Join("", rentals.Select(rental => FormatRentals(rental.Movie, rental.ComputeRentalAmount().ToString())))}" +
           $"{HtmlTags.CLOSE_TAG_TABLE}" +
           $"{TextOutputValues.NEW_LINE}";

    public string FormatRentals(Movie movie, string rentalAmount)
        => $"{TextOutputValues.TAB}" +
           $"{HtmlTags.OPEN_TAG_TABLE_ROW}" +
           $"{MovieTitleHtmlOutput(movie.Title)}" +
           $"{RentalAmountHtmlOutput(rentalAmount)}" +
           $"{HtmlTags.CLOSE_TAG_TABLE_ROW}" +
           $"{TextOutputValues.NEW_LINE}";
    private string MovieTitleHtmlOutput(string movieTitle)
        => $"{HtmlTags.OPEN_TAG_TABLE_CELL}" +
           $"{movieTitle}" +
           $"{HtmlTags.CLOSE_TAG_TABLE_CELL}";
    private string RentalAmountHtmlOutput(string rentalAmount)
        => $"{HtmlTags.OPEN_TAG_TABLE_CELL}" +
           $"{rentalAmount}" +
           $"{HtmlTags.CLOSE_TAG_TABLE_CELL}";

    public string FormatTotalAmountOwned(IEnumerable<Rental> rentals)
        => $"{HtmlTags.OPEN_TAG_PARAGRAPH}" +
           $"{TextOutputValues.FOOTER_TOTAL_AMOUNT_OWNED}" +
           $"{TotalAmountOwnedHtmlOutput(rentals.Sum(rental => rental.ComputeRentalAmount()).ToString())}" +
           $"{HtmlTags.CLOSE_TAG_PARAGRAPH}" +
           $"{TextOutputValues.NEW_LINE}";
    private string TotalAmountOwnedHtmlOutput(string amountOwned)
        => $"{HtmlTags.OPEN_TAG_EMPHASIS}" +
           $"{amountOwned}" +
           $"{HtmlTags.CLOSE_TAG_EMPHASIS}";

    public string FormatTotalRenterPoints(IEnumerable<Rental> rentals)
        => $"{HtmlTags.OPEN_TAG_PARAGRAPH}" +
           $"{TextOutputValues.FOOTER_TOTAL_RENTER_POINTS_1}" +
           $"{TotalRenterPointsHtmlOutput(rentals.Sum(rental => rental.ComputeBonusRenterPoints()).ToString())}" +
           $"{TextOutputValues.FOOTER_TOTAL_RENTER_POINTS_2}" +
           $"{HtmlTags.CLOSE_TAG_PARAGRAPH}" +
           $"{TextOutputValues.NEW_LINE}" +
           $"{TextOutputValues.NEW_LINE}";
    private string TotalRenterPointsHtmlOutput(string renterPoints)
        => $"{HtmlTags.OPEN_TAG_EMPHASIS}" +
           $"{renterPoints}" +
           $"{HtmlTags.CLOSE_TAG_EMPHASIS}";

    public string FormatDaysRented(Rental rental)
    {
        string formattedText = "";
        formattedText += $"{HtmlTags.OPEN_TAG_EMPHASIS}";
        formattedText += rental.Movie.PriceCode switch
        {
            RegularPriceCode => $"Number of days rented for regular movie : {rental.ComputeDaysRentedForRegular(rental.ComputeRentalAmount())}",
            NewReleasePriceCode => $"Number of days rented for this new release movie : {rental.ComputeDaysRentedForNewRelease(rental.ComputeRentalAmount())}",
            ChildrenPriceCode => $"Number of days rented for this children movie : {rental.ComputeDaysRentedForChildren(rental.ComputeRentalAmount())}",
            _ => "",
        };
        formattedText += $"{HtmlTags.CLOSE_TAG_EMPHASIS}";
        formattedText += "\n";
        return formattedText;
    }

}

public class TextOutputFormatter : IOutputFormatter
{
    public string FormatHeader(string customerName)
        => $"{TextOutputValues.HEADER_CUSTOMER_RENTAL_RECORD}" +
           $"{customerName}" +
           $"{TextOutputValues.NEW_LINE}";

    public string FormatRentalsTable(IEnumerable<Rental> rentals)
        => string.Join("", rentals.Select(rental => FormatRentals(rental.Movie, rental.ComputeRentalAmount().ToString())));


    public string FormatRentals(Movie movie, string rentalAmount)
        => $"{TextOutputValues.TAB}" +
           $"{movie.Title} " +
           $"{rentalAmount}" +
           $"{TextOutputValues.NEW_LINE}";

    public string FormatTotalAmountOwned(IEnumerable<Rental> rentals)
        => $"{TextOutputValues.FOOTER_TOTAL_AMOUNT_OWNED}" +
           $"{rentals.Sum(rental => rental.ComputeRentalAmount())}" +
           $"{TextOutputValues.NEW_LINE}";

    public string FormatTotalRenterPoints(IEnumerable<Rental> rentals)
        => $"{TextOutputValues.FOOTER_TOTAL_RENTER_POINTS_1}" +
           $"{rentals.Sum(rental => rental.ComputeBonusRenterPoints())}" +
           $"{TextOutputValues.FOOTER_TOTAL_RENTER_POINTS_2}" +
           $"{TextOutputValues.NEW_LINE}" +
           $"{TextOutputValues.NEW_LINE}";

    public string FormatDaysRented(Rental rental) =>
        rental.Movie.PriceCode switch
        {
            RegularPriceCode => $"Number of days rented for regular movie : {rental.ComputeDaysRentedForRegular(rental.ComputeRentalAmount())}\n",
            NewReleasePriceCode => $"Number of days rented for this new release movie : {rental.ComputeDaysRentedForNewRelease(rental.ComputeRentalAmount())}\n",
            ChildrenPriceCode => $"Number of days rented for this children movie : {rental.ComputeDaysRentedForChildren(rental.ComputeRentalAmount())}\n",
            _ => "",
        };
}