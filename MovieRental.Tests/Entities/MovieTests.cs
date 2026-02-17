using Moq;
using MovieRental.Entities;
using MovieRental.Entities.PriceCodes;
using MovieRental.Entities.PriceCodes.Contracts;
using MovieRental.Enums;
using Xunit;

namespace MovieRental.Tests;

public class MovieTests
{
    public void Movie_Constructor_ValidTitleAndPriceCode_ReturnsMovieWithCorrectProperties()
    {
        // Arrange
        var movie = new Movie(" The Shawshank Redemption ", PriceCode.GetPriceCode(MovieRatingEnum.REGULAR));

        // Act & Assert
        Assert.Equal(" The Shawshank Redemption ".Trim(), movie.Title);
        Assert.Equal(PriceCode.GetPriceCode(MovieRatingEnum.REGULAR), movie.PriceCode);
    }

    [Theory]
    [InlineData(1, 2.0)]
    [InlineData(3, 3.5)]
    [InlineData(5, 6.5)]
    public void AmountFor_ShouldReturn_Correct_RentalAmount(int daysRented, double expectedAmount)
    {
        // Arrange
        new Mock<IPriceCode>().Setup(pc => pc.ComputeRentalAmountFor(daysRented)).Returns(expectedAmount);
        var movie = new Movie("The Dark Knight Rises", PriceCode.GetPriceCode(MovieRatingEnum.REGULAR));

        // Act & Assert
        Assert.Equal(expectedAmount, movie.AmountFor(daysRented));
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(3, 2)]
    [InlineData(5, 2)]
    public void BonusRenterPointsFor_ShouldReturn_Correct_BonusPoints(int daysRented, int expectedPoints)
    {
        // Arrange
        new Mock<IPriceCode>().Setup(pc => pc.ComputeBonusRenterPointsFor(daysRented)).Returns(expectedPoints);
        var movie = new Movie("The Godfather", PriceCode.GetPriceCode(MovieRatingEnum.NEW_RELEASE));

        // Act & Assert
        Assert.Equal(expectedPoints, movie.BonusRenterPointsFor(daysRented));
    }
}