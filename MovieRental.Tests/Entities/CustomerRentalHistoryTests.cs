using Moq;
using MovieRental.Entities;
using MovieRental.Entities.Formatter.Contracts;
using MovieRental.Entities.PriceCodes;
using MovieRental.Enums;
using System;
using System.Collections.Generic;
using Xunit;

namespace MovieRental.Tests;

public class CustomerRentalHistoryTests
{
    [Fact]
    public void GenerateCustomerRentalHistory_NullRentals_ThrowsException()
    {
        // Arrange
        var outputFormatterMock = new Mock<IOutputFormatter>();

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => new CustomerRentalHistory(new Customer("John Doe"), null, outputFormatterMock.Object));
    }

    [Fact]
    public void GenerateCustomerRentalHistory_EmptyRentals_ThrowsException()
    {
        // Arrange
        var outputFormatterMock = new Mock<IOutputFormatter>();

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => new CustomerRentalHistory(new Customer("John Doe"), new List<Rental>(), outputFormatterMock.Object));
    }

    [Fact]
    public void GenerateCustomerRentalHistory_ValidInput_ReturnsExpectedOutput()
    {
        // Arrange
        var customer = new Customer("John Doe");
        var rentals = new List<Rental>
        {
            new Rental(new Movie("Movie 1", PriceCode.GetPriceCode(MovieRatingEnum.REGULAR)), 3),
            new Rental(new Movie("Movie 2", PriceCode.GetPriceCode(MovieRatingEnum.NEW_RELEASE)), 5),
            new Rental(new Movie("Movie 3", PriceCode.GetPriceCode(MovieRatingEnum.CHILDREN)), 2)
        };

        var outputFormatterMock = new Mock<IOutputFormatter>();
        outputFormatterMock.Setup(f => f.FormatHeader(It.IsAny<string>())).Returns("Header");
        outputFormatterMock.Setup(f => f.FormatRentalsTable(It.IsAny<IEnumerable<Rental>>())).Returns("Rentals");
        outputFormatterMock.Setup(f => f.FormatTotalAmountOwned(It.IsAny<IEnumerable<Rental>>())).Returns("Total Amount");
        outputFormatterMock.Setup(f => f.FormatTotalRenterPoints(It.IsAny<IEnumerable<Rental>>())).Returns("Total Points");

        var customerRentalHistory = new CustomerRentalHistory(customer, rentals, outputFormatterMock.Object);

        // Act
        string result = customerRentalHistory.GenerateCustomerRentalHistory();

        // Assert
        Assert.Equal("HeaderRentalsTotal AmountTotal Points", result);
        outputFormatterMock.Verify(f => f.FormatHeader(customer.Name), Times.Once);
        outputFormatterMock.Verify(f => f.FormatRentalsTable(rentals), Times.Once);
        outputFormatterMock.Verify(f => f.FormatTotalAmountOwned(rentals), Times.Once);
        outputFormatterMock.Verify(f => f.FormatTotalRenterPoints(rentals), Times.Once);
    }

    [Fact]
    public void GenerateCustomerRentalHistory_NullCustomer_ThrowsException()
    {
        // Arrange
        Customer customer = null;
        var rentals = new List<Rental>
        {
            new Rental(new Movie("Movie 1", PriceCode.GetPriceCode(MovieRatingEnum.REGULAR)), 3),
            new Rental(new Movie("Movie 2", PriceCode.GetPriceCode(MovieRatingEnum.NEW_RELEASE)), 5),
            new Rental(new Movie("Movie 3", PriceCode.GetPriceCode(MovieRatingEnum.CHILDREN)), 2)
        };

        var outputFormatterMock = new Mock<IOutputFormatter>();

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => new CustomerRentalHistory(customer, rentals, outputFormatterMock.Object));
    }
}
