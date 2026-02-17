using MovieRental.Constants;
using MovieRental.Entities.PriceCodes.Contracts;
using MovieRental.Enums;
using System;

namespace MovieRental.Entities.PriceCodes;

public class PriceCode
{
    public static IPriceCode Regular() => new RegularPriceCode();

    public static IPriceCode NewRelease() => new NewReleasePriceCode();

    public static IPriceCode Children() => new ChildrenPriceCode();

    public static IPriceCode GetPriceCode(MovieRatingEnum movieRating)
        => movieRating switch
        {
            MovieRatingEnum.REGULAR => Regular(),
            MovieRatingEnum.NEW_RELEASE => NewRelease(),
            MovieRatingEnum.CHILDREN => Children(),
            _ => throw new ArgumentOutOfRangeException(nameof(movieRating), "Invalid movie rating")
        };
}

public class RegularPriceCode : IPriceCode
{
    public RegularRelease regularRelease = new RegularRelease();

    public double ComputeRentalAmountFor(int daysRented)
        => daysRented.ComputeRentalAmountFor(regularRelease.BASE_AMOUNT_REGULAR, regularRelease.BASE_DAYS_LIMIT_REGULAR, regularRelease.RENTAL_COEFFICIENT_REGULAR);

    public int ComputeBonusRenterPointsFor(int daysRented)
        => RentalAgreementValues.RENTER_POINT;

    public int ComputeDaysRentedFor(double rentalAmount, MovieRatingEnum movieRating)
        => rentalAmount.ComputeDaysRentedFor(regularRelease.BASE_AMOUNT_REGULAR, regularRelease.BASE_DAYS_LIMIT_REGULAR, regularRelease.RENTAL_COEFFICIENT_REGULAR);
}

public class NewReleasePriceCode : IPriceCode
{
    public NewRelease newRelease = new NewRelease();

    public double ComputeRentalAmountFor(int daysRented)
        => daysRented.ComputeRentalAmountFor(newRelease.BASE_AMOUNT_NEWRELEASE, newRelease.BASE_DAYS_LIMIT_NEWRELEASE, newRelease.RENTAL_COEFFICIENT_NEWRELEASE);

    public int ComputeBonusRenterPointsFor(int daysRented)
        => daysRented > 1 ? RentalAgreementValues.RENTER_POINT + 1 : RentalAgreementValues.RENTER_POINT;

    public int ComputeDaysRentedFor(double rentalAmount, MovieRatingEnum movieRating)
        => rentalAmount.ComputeDaysRentedFor(newRelease.BASE_AMOUNT_NEWRELEASE, newRelease.BASE_DAYS_LIMIT_NEWRELEASE, newRelease.RENTAL_COEFFICIENT_NEWRELEASE);
}

public class ChildrenPriceCode : IPriceCode
{
    public ChildrenRelease childrenRelease = new ChildrenRelease();

    public double ComputeRentalAmountFor(int daysRented)
        => daysRented.ComputeRentalAmountFor(childrenRelease.BASE_AMOUNT_CHILDREN, childrenRelease.BASE_DAYS_LIMIT_CHILDREN, childrenRelease.RENTAL_COEFFICIENT_CHILDREN);

    public int ComputeBonusRenterPointsFor(int daysRented)
        => RentalAgreementValues.RENTER_POINT;

    public int ComputeDaysRentedFor(double rentalAmount, MovieRatingEnum movieRating)
        => rentalAmount.ComputeDaysRentedFor(childrenRelease.BASE_AMOUNT_CHILDREN, childrenRelease.BASE_DAYS_LIMIT_CHILDREN, childrenRelease.RENTAL_COEFFICIENT_CHILDREN);
}

public static class ComputeRule
{
    public static double ComputeRentalAmountFor(this int daysRented, double baseRentalAmount, double baseDaysLimit, double rentalCoefficient)
    {
        double rentalAmount = baseRentalAmount;
        if (daysRented > baseDaysLimit)
        {
            rentalAmount += (daysRented - baseDaysLimit) * rentalCoefficient;
        }
        return rentalAmount;
    }

    public static int ComputeDaysRentedFor(this double rentalAmount, double baseRentalAmount, double baseDaysLimit, double rentalCoefficient)
    {
        int daysRented = 0;
        if (rentalAmount > baseRentalAmount)
        {
            double additionalAmount = rentalAmount - baseRentalAmount;
            daysRented = (int)Math.Ceiling(additionalAmount / rentalCoefficient);
        }
        return daysRented + (int)baseDaysLimit;
    }

    public static int ComputeBaseDaysLimitFor(this double rentalAmount, double baseRentalAmount, double rentalCoefficient)
    {
        int baseDaysLimit = 0;
        if (rentalAmount > baseRentalAmount)
        {
            double additionalAmount = rentalAmount - baseRentalAmount;
            baseDaysLimit = (int)Math.Ceiling(additionalAmount / rentalCoefficient);
        }
        return baseDaysLimit;
    }

    public static double ComputeRentalCoefficientFor(this int daysRented, double rentalAmount, double baseRentalAmount, double baseDaysLimit)
    {
        double rentalCoefficient = 0;
        if (rentalAmount > baseRentalAmount)
        {
            double additionalAmount = rentalAmount - baseRentalAmount;
            rentalCoefficient = additionalAmount / (daysRented - baseDaysLimit);
        }
        return rentalCoefficient;
    }
}