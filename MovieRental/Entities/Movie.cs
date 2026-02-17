using MovieRental.Entities.PriceCodes.Contracts;
using MovieRental.Enums;
using System;

namespace MovieRental.Entities;

public class Movie
{
    public string Title { get; }
    public IPriceCode PriceCode { get; }

    public Movie(string title, IPriceCode priceCode)
    {
        if (string.IsNullOrEmpty(title))
            throw new NullReferenceException("Movie title cannot be null or empty");

        Title = title.Trim();
        PriceCode = priceCode;
    }

    public double AmountFor(int daysRented)
        => PriceCode.ComputeRentalAmountFor(daysRented);

    public int BonusRenterPointsFor(int daysRented)
        => PriceCode.ComputeBonusRenterPointsFor(daysRented);

    public int RentalRegularReleaseInformations(double rentalAmount) 
        => PriceCode.ComputeDaysRentedFor(rentalAmount, MovieRatingEnum.REGULAR);

    public int RentalNewReleaseInformations(double rentalAmount) 
        => PriceCode.ComputeDaysRentedFor(rentalAmount, MovieRatingEnum.NEW_RELEASE);

    public int RentalChildrenReleaseInformations(double rentalAmount) 
        => PriceCode.ComputeDaysRentedFor(rentalAmount, MovieRatingEnum.CHILDREN);
}