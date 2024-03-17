namespace TravelMore.Domain.Common.Extensions;

public static class NumberExtensions
{
    public static bool IsNegative(this decimal num) => num < 0;
    public static bool IsLessThanOrEqualToZero(this short num) => num <= 0;

}
