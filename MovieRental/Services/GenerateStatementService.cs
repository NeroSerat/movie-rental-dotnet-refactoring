using MovieRental.Entities;

namespace MovieRental.Services;

public class GenerateStatementService
{
    public string CreateStatement(CustomerRentalHistory customerRentalHistory)
        => customerRentalHistory.GenerateCustomerRentalHistory();
}