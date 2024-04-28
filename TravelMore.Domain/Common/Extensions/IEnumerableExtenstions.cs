using TravelMore.Domain.Common.Intefaces;

namespace TravelMore.Domain.Common.Extensions;

public static class IEnumerableExtenstions
{
    public static List<T> Clone<T>(this IEnumerable<T> listToClone) where T : ICloneable<T>
    {
        return listToClone.Select(item => item.Clone()).ToList();
    }
}
