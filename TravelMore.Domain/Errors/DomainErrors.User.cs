using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Errors;

public static partial class DomainErrors
{
    public static class User
    {
        public static readonly Error NotFoundByEmail = new("", "User not found by given email");
        public static readonly Error AlreadyExistByEmail = new("", "User already exists by given email");
        public static readonly Error IncorrectPassword = new("", "Entered Password is not correct");
    }
}
