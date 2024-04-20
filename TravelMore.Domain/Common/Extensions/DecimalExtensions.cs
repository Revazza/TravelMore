using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Common.Extensions;

public static class DecimalExtensions
{

    public static bool IsLessThanOrEqualToZero(this decimal value) => value <= 0;

}
