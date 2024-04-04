using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Errors;

public static partial class DomainErrors
{
    public static class Guest
    {
        public static readonly Error NotFoundByEmail = new("", "Guest not found by given email");
        public static readonly Error AlreadyExistByEmail = new("", "Guest already exists by given email");
    }
}

