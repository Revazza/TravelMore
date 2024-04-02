using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Errors;

public static partial class DomainErrors
{
    public static class Guest
    {
        public static readonly Error NotFoundByUsername = new("", "Guest not found by given username");
        public static readonly Error AlreadyExistByUsername = new("", "Guest already exists by given username");
    }
}

