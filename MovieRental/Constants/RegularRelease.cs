namespace MovieRental.Constants;

public class RegularRelease
{
    public RegularRelease()
    {
        BASE_AMOUNT_REGULAR = 2;
        BASE_DAYS_LIMIT_REGULAR = 2;
        RENTAL_COEFFICIENT_REGULAR = 1.5;
    }
    public double BASE_AMOUNT_REGULAR { get; }
    public double BASE_DAYS_LIMIT_REGULAR { get; }
    public double RENTAL_COEFFICIENT_REGULAR { get; }
}
