using System;

namespace MovieRental.Entities;

public class Rental
{
    public Movie Movie { get; }
    public int DaysRented { get; }

    public Rental(Movie movie, int daysRented)
    {
        if (movie == null)
            throw new NullReferenceException("movie cannot be null");

        if (daysRented < 1)
            throw new ArgumentException("daysRented is too small");

        Movie = movie;
        DaysRented = daysRented;
    }

    public string Title()
        => Movie.Title;

    public double ComputeRentalAmount()
        => Movie.AmountFor(DaysRented);

    public int ComputeBonusRenterPoints()
        => Movie.BonusRenterPointsFor(DaysRented);

    public int ComputeDaysRentedForRegular(double rentalAmount) 
        => Movie.RentalRegularReleaseInformations(rentalAmount);

    public int ComputeDaysRentedForNewRelease(double rentalAmount)
        => Movie.RentalNewReleaseInformations(rentalAmount);

    public int ComputeDaysRentedForChildren(double rentalAmount)
        => Movie.RentalChildrenReleaseInformations(rentalAmount);
}