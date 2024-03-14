using TravelMore.Domain.Common.Results;

namespace TravelMore.Domain.Errors;

public static partial class DomainErrors
{
    public static class Guest
    {
        public static readonly Error InsufficientBalance = new("", "Not enough balance in the account");
    }
}
