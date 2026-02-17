using MovieRental.Enums;

namespace MovieRental.Entities.PriceCodes.Contracts;

public interface IPriceCode
{
    double ComputeRentalAmountFor(int daysRented);

    int ComputeBonusRenterPointsFor(int daysRented);

    int ComputeDaysRentedFor(double rentalAmount, MovieRatingEnum movieRating);
}