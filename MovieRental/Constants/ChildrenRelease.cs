namespace MovieRental.Constants;

public class ChildrenRelease
{
    public ChildrenRelease()
    {
        BASE_AMOUNT_CHILDREN = 1.5;
        BASE_DAYS_LIMIT_CHILDREN = 3;
        RENTAL_COEFFICIENT_CHILDREN = 1.5;
    }
    public double BASE_AMOUNT_CHILDREN { get; }
    public double BASE_DAYS_LIMIT_CHILDREN { get; }
    public double RENTAL_COEFFICIENT_CHILDREN { get; }
}
