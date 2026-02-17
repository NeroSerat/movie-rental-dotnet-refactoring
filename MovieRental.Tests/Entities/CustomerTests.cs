using MovieRental.Entities;
using System;
using Xunit;

namespace MovieRental.Tests;

public class CustomerTests
{
    [Fact]
    public void GetTrimedCustomerName_ShouldReturnTrimmedName()
    {
        // Arrange
        Customer customer = new Customer(" John Doe ");

        // Act & Assert
        Assert.Equal(" John Doe ".Trim(), customer.Name);
    }

    [Fact]
    public void GetTrimedCustomerName_ShouldThrowException_WhenNameIsNull()
    {
        // Arrange
        string name = null;

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => new Customer(name));
    }
}
