using MovieRental.Entities;
using MovieRental.Entities.PriceCodes;
using MovieRental.Enums;
using Xunit;

namespace MovieRental.Tests;

public class RentalTests
{
    [Theory]
    [InlineData(2.0, 1, 1, MovieRatingEnum.REGULAR)]
    [InlineData(3.5, 1, 3, MovieRatingEnum.REGULAR)]
    [InlineData(6.5, 1, 5, MovieRatingEnum.REGULAR)]
    [InlineData(3.0, 1, 1, MovieRatingEnum.NEW_RELEASE)]
    [InlineData(9.0, 2, 3, MovieRatingEnum.NEW_RELEASE)]
    [InlineData(15.0, 2, 5, MovieRatingEnum.NEW_RELEASE)]
    [InlineData(1.5, 1, 1, MovieRatingEnum.CHILDREN)]
    [InlineData(1.5, 1, 3, MovieRatingEnum.CHILDREN)]
    [InlineData(4.5, 1, 5, MovieRatingEnum.CHILDREN)]
    public void Rental_Constructor_ShouldSetProperties(double expectedAmount, int expectedBonusPoints, int daysRented, MovieRatingEnum rating)
    {
        // Arrange
        Movie movie = new Movie("Star Trek", PriceCode.GetPriceCode(rating));
        Rental rental = new Rental(movie, daysRented);

        // Act & Assert
        Assert.Equal(movie, rental.Movie);
        Assert.Equal(daysRented, rental.DaysRented);
        Assert.Equal(expectedAmount, rental.ComputeRentalAmount());
        Assert.Equal(expectedBonusPoints, rental.ComputeBonusRenterPoints());
    }
}
