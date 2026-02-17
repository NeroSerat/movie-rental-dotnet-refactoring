using MovieRental.Entities.PriceCodes;
using MovieRental.Entities.PriceCodes.Contracts;
using MovieRental.Enums;
using System;
using Xunit;

namespace MovieRental.Tests.Entities;

public class PriceCodeTests
{
    [Fact]
    public void GetPriceCode_RegularRating_ReturnsRegularPriceCode()
    {
        Assert.IsType<RegularPriceCode>(new RegularPriceCode());
        Assert.Equal(new RegularPriceCode().GetType(), PriceCode.GetPriceCode(MovieRatingEnum.REGULAR).GetType());
    }

    [Fact]
    public void GetPriceCode_NewReleaseRating_ReturnsNewReleasePriceCode()
    {
        Assert.IsType<NewReleasePriceCode>(new NewReleasePriceCode());
        Assert.Equal(new NewReleasePriceCode().GetType(), PriceCode.GetPriceCode(MovieRatingEnum.NEW_RELEASE).GetType());
    }

    [Fact]
    public void GetPriceCode_ChildrenRating_ReturnsChildrenPriceCode()
    {
        Assert.IsType<ChildrenPriceCode>(new ChildrenPriceCode());
        Assert.Equal(new ChildrenPriceCode().GetType(), PriceCode.GetPriceCode(MovieRatingEnum.CHILDREN).GetType());
    }

    [Fact]
    public void GetPriceCode_InvalidRating_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => PriceCode.GetPriceCode((MovieRatingEnum)99));
    }

    [Theory]
    [InlineData(1, 2.0, 1)]
    [InlineData(3, 3.5, 1)]
    [InlineData(5, 6.5, 1)]
    [InlineData(Int32.MaxValue, 3221225469.5, 1)]
    public void RegularPriceCode_ComputeRentalAmountFor_ReturnsCorrectAmountAndPoints(int daysRented, double expectedAmount, int expectedPoints)
    {
        // Arrange
        IPriceCode priceCode = new RegularPriceCode();

        // Act & Assert
        Assert.Equal(expectedAmount, priceCode.ComputeRentalAmountFor(daysRented));
        Assert.Equal(expectedPoints, priceCode.ComputeBonusRenterPointsFor(daysRented));
    }

    [Theory]
    [InlineData(1, 3.0, 1)]
    [InlineData(3, 9.0, 2)]
    [InlineData(5, 15.0, 2)]
    [InlineData(Int32.MaxValue, 6442450941, 2)]
    public void NewReleasePriceCode_ComputeRentalAmountFor_ReturnsCorrectAmountAndPoints(int daysRented, double expectedAmount, int expectedPoints)
    {
        // Arrange
        IPriceCode priceCode = new NewReleasePriceCode();

        // Act & Assert
        Assert.Equal(expectedAmount, priceCode.ComputeRentalAmountFor(daysRented));
        Assert.Equal(expectedPoints, priceCode.ComputeBonusRenterPointsFor(daysRented));
    }

    [Theory]
    [InlineData(1, 1.5, 1)]
    [InlineData(3, 1.5, 1)]
    [InlineData(5, 4.5, 1)]
    [InlineData(Int32.MaxValue, 3221225467.5, 1)]
    public void ChildrenPriceCode_ComputeRentalAmountFor_ReturnsCorrectAmountAndPoints(int daysRented, double expectedAmount, int expectedPoints)
    {
        // Arrange
        IPriceCode priceCode = new ChildrenPriceCode();

        // Act & Assert
        Assert.Equal(expectedAmount, priceCode.ComputeRentalAmountFor(daysRented));
        Assert.Equal(expectedPoints, priceCode.ComputeBonusRenterPointsFor(daysRented));
    }

    [Theory]
    [InlineData(5, 10, 2, 1.5, 2)]
    [InlineData(3, 5, 1, 0.5, 1)]
    [InlineData(7, 15, 3, 2.5, 3)]
    public void ComputeDaysRentedFor_ValidParameters_ReturnsCorrectResult(double rentalAmount, double baseRentalAmount, double baseDaysLimit, double rentalCoefficient, int expectedDaysRented)
    {
        // Arrange
        int daysRented = ComputeRule.ComputeDaysRentedFor(rentalAmount, baseRentalAmount, baseDaysLimit, rentalCoefficient);

        // Act & Assert
        Assert.Equal(expectedDaysRented, daysRented);
    }

    [Theory]
    [InlineData(10, 5, 1.5, 4)]
    [InlineData(5, 3, 0.5, 4)]
    [InlineData(15, 7, 2.5, 4)]
    public void ComputeBaseDaysLimitFor_ValidParameters_ReturnsCorrectResult(double rentalAmount, double baseRentalAmount, double rentalCoefficient, int expectedBaseDaysLimit)
    {
        // Arrange
        int baseDaysLimit = ComputeRule.ComputeBaseDaysLimitFor(rentalAmount, baseRentalAmount, rentalCoefficient);

        // Act & Assert
        Assert.Equal(expectedBaseDaysLimit, baseDaysLimit);
    }

    [Theory]
    [InlineData(5, 10, 12, 2, 0)]
    [InlineData(3, 5, 4, 1, 0.5)]
    [InlineData(7, 15, 18, 3, 0)]
    public void ComputeRentalCoefficientFor_ValidParameters_ReturnsCorrectResult(int daysRented, double rentalAmount, double baseRentalAmount, double baseDaysLimit, double expectedRentalCoefficient)
    {
        // Arrange
        double rentalCoefficient = ComputeRule.ComputeRentalCoefficientFor(daysRented, rentalAmount, baseRentalAmount, baseDaysLimit);

        // Act & Assert
        Assert.Equal(expectedRentalCoefficient, rentalCoefficient);
    }
}