using TravelMore.Domain.Common.Results;

namespace TravelMore.Domain.Errors;

public static partial class DomainErrors
{
    public static class Money
    {
        public static readonly Error InvalidAmount = new("", "Amount must be non-negative");
    }
}
