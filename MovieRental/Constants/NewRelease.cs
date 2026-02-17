namespace MovieRental.Constants;

public class NewRelease
{
    public NewRelease()
    {
        BASE_AMOUNT_NEWRELEASE = 0;
        BASE_DAYS_LIMIT_NEWRELEASE = 0;
        RENTAL_COEFFICIENT_NEWRELEASE = 3;
    }
    public double BASE_AMOUNT_NEWRELEASE { get; }
    public double BASE_DAYS_LIMIT_NEWRELEASE { get; }
    public double RENTAL_COEFFICIENT_NEWRELEASE { get; }
}
