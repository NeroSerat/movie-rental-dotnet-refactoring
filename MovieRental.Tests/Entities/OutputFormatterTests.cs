using MovieRental.Entities.Formatter;
using MovieRental.Entities.PriceCodes;
using MovieRental.Enums;
using System.Collections.Generic;
using Xunit;

namespace MovieRental.Entities;

public class OutputFormatterTests
{
    [Fact]
    public void HtmlReport_ShouldReturnHtmlOutputFormatter()
    {
        Assert.IsType<HtmlOutputFormatter>(OutputFormatter.HtmlReport());
    }

    [Fact]
    public void TextReport_ShouldReturnTextOutputFormatter()
    {
        Assert.IsType<TextOutputFormatter>(OutputFormatter.TextReport());
    }

    [Fact]
    public void HtmlOutputFormatter_FormatHeader_ShouldReturnCorrectHtmlFormat()
    {
        // Arrange
        var outputFormatter = new HtmlOutputFormatter();

        // Act
        var formattedHeader = outputFormatter.FormatHeader("John Doe");

        // Assert
        Assert.Equal("<h1>Rental Record for <em>John Doe</em></h1>\n", formattedHeader);
    }

    [Fact]
    public void HtmlOutputFormatter_FormatRentalsTable_ShouldReturnFormattedRentalsTable()
    {
        // Arrange
        var outputFormatter = new HtmlOutputFormatter();
        var rentals = new List<Rental>
        {
            new Rental(new Movie("Movie 1", PriceCode.GetPriceCode(MovieRatingEnum.REGULAR)), 2),
            new Rental(new Movie("Movie 2", PriceCode.GetPriceCode(MovieRatingEnum.NEW_RELEASE)), 3),
            new Rental(new Movie("Movie 3", PriceCode.GetPriceCode(MovieRatingEnum.CHILDREN)), 4)
        };
        var expectedOutput = "<table>\n" +
            "\t<tr><td>Movie 1</td><td>2</td></tr>\n" +
            "\t<tr><td>Movie 2</td><td>9</td></tr>\n" +
            "\t<tr><td>Movie 3</td><td>3</td></tr>\n" +
            "</table>\n";

        // Act
        var formattedRentalsTable = outputFormatter.FormatRentalsTable(rentals);

        // Assert
        Assert.Equal(expectedOutput, formattedRentalsTable);
    }

    [Fact]
    public void HtmlOutputFormatter_FormatRentals_ShouldReturnFormattedRental()
    {
        // Arrange
        var outputFormatter = new HtmlOutputFormatter();

        // Act
        var formattedRental = outputFormatter.FormatRentals(new Movie("Movie 1", PriceCode.GetPriceCode(MovieRatingEnum.REGULAR)), "2");

        // Assert
        Assert.Equal("\t<tr><td>Movie 1</td><td>2</td></tr>\n", formattedRental);
    }

    [Fact]
    public void HtmlOutputFormatter_FormatTotalAmountOwned_ShouldReturnFormattedTotalAmountOwned()
    {
        // Arrange
        var outputFormatter = new HtmlOutputFormatter();
        var rentals = new List<Rental>
        {
            new Rental(new Movie("Movie 1", PriceCode.GetPriceCode(MovieRatingEnum.REGULAR)), 2),
            new Rental(new Movie("Movie 2", PriceCode.GetPriceCode(MovieRatingEnum.NEW_RELEASE)), 3),
            new Rental(new Movie("Movie 3", PriceCode.GetPriceCode(MovieRatingEnum.CHILDREN)), 4)
        };
        var expectedOutput = "<p>Amount owed is <em>14</em></p>\n";

        // Act
        var formattedTotalAmountOwned = outputFormatter.FormatTotalAmountOwned(rentals);

        // Assert
        Assert.Equal(expectedOutput, formattedTotalAmountOwned);
    }

    [Fact]
    public void HtmlOutputFormatter_FormatTotalRenterPoints_ShouldReturnFormattedTotalRenterPoints()
    {
        // Arrange
        var outputFormatter = new HtmlOutputFormatter();
        var rentals = new List<Rental>
        {
            new Rental(new Movie("Movie 1", PriceCode.GetPriceCode(MovieRatingEnum.REGULAR)), 2),
            new Rental(new Movie("Movie 2", PriceCode.GetPriceCode(MovieRatingEnum.NEW_RELEASE)), 3),
            new Rental(new Movie("Movie 3", PriceCode.GetPriceCode(MovieRatingEnum.CHILDREN)), 4)
        };

        // Act
        var formattedTotalRenterPoints = outputFormatter.FormatTotalRenterPoints(rentals);

        // Assert
        Assert.Equal("<p>You earned <em>4</em> frequent renter points</p>\n\n", formattedTotalRenterPoints);
    }

    [Fact]
    public void TextOutputFormatter_FormatHeader_ShouldReturnCorrectText()
    {
        // Arrange
        var formatter = new TextOutputFormatter();

        // Act
        var result = formatter.FormatHeader("John Doe");

        // Assert
        Assert.Equal("Rental Record for John Doe\n", result);
    }

    [Fact]
    public void TextOutputFormatter_FormatRentalsTable_ShouldReturnCorrectText()
    {
        // Arrange
        var formatter = new TextOutputFormatter();
        var rentals = new List<Rental>
        {
            new Rental(new Movie("Movie 1", PriceCode.GetPriceCode(MovieRatingEnum.REGULAR)), 2),
            new Rental(new Movie("Movie 2", PriceCode.GetPriceCode(MovieRatingEnum.NEW_RELEASE)), 3),
            new Rental(new Movie("Movie 3", PriceCode.GetPriceCode(MovieRatingEnum.CHILDREN)), 4)
        };

        // Act
        var result = formatter.FormatRentalsTable(rentals);

        // Assert
        Assert.Equal("\tMovie 1 2\n\tMovie 2 9\n\tMovie 3 3\n", result);
    }

    [Fact]
    public void TextOutputFormatter_FormatRentals_ShouldReturnCorrectText()
    {
        // Arrange
        var formatter = new TextOutputFormatter();

        // Act
        var result = formatter.FormatRentals(new Movie("Movie 1", PriceCode.GetPriceCode(MovieRatingEnum.REGULAR)), "2");

        // Assert
        Assert.Equal("\tMovie 1 2\n", result);
    }

    [Fact]
    public void TextOutputFormatter_FormatTotalAmountOwned_ShouldReturnCorrectText()
    {
        // Arrange
        var formatter = new TextOutputFormatter();
        var rentals = new List<Rental>
        {
            new Rental(new Movie("Movie 1", PriceCode.GetPriceCode(MovieRatingEnum.REGULAR)), 2),
            new Rental(new Movie("Movie 2", PriceCode.GetPriceCode(MovieRatingEnum.NEW_RELEASE)), 3),
            new Rental(new Movie("Movie 3", PriceCode.GetPriceCode(MovieRatingEnum.CHILDREN)), 4)
        };

        // Act
        var result = formatter.FormatTotalAmountOwned(rentals);

        // Assert
        Assert.Equal("Amount owed is 14\n", result);
    }

    [Fact]
    public void TextOutputFormatter_FormatTotalRenterPoints_ShouldReturnCorrectText()
    {
        // Arrange
        var formatter = new TextOutputFormatter();
        var rentals = new List<Rental>
        {
            new Rental(new Movie("Movie 1", PriceCode.GetPriceCode(MovieRatingEnum.REGULAR)), 2),
            new Rental(new Movie("Movie 2", PriceCode.GetPriceCode(MovieRatingEnum.NEW_RELEASE)), 3),
            new Rental(new Movie("Movie 3", PriceCode.GetPriceCode(MovieRatingEnum.CHILDREN)), 4)
        };

        // Act
        var result = formatter.FormatTotalRenterPoints(rentals);

        // Assert
        Assert.Equal("You earned 4 frequent renter points\n\n", result);
    }
}
