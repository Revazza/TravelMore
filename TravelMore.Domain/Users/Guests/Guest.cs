using System.Security;
using TravelMore.Domain.Bookings;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Common.Requests;
using TravelMore.Domain.Common.Results;
using TravelMore.Domain.Errors;

namespace TravelMore.Domain.Users.Guests;

public class Guest : User
{
    private readonly List<Booking> _bookings = [];
    public IReadOnlyCollection<Booking> Bookings => _bookings;
    public string UserName { get; } = string.Empty;
    public Money Balance { get; private set; } = Money.Create(0).Value;

    public Guest(int id) : base(id)
    {

    }

    public Guest(int id, string userName, Money balance) : base(id)
    {
        UserName = userName;
        Balance = balance;
    }

    public Guest(int id, string userName) : base(id)
    {
        UserName = userName;
    }

    public Result CanBook(BookingRequest request)
    {
        if (!IsBalanceEnough(request.Payment))
        {
            return Result.Failure(DomainErrors.Guest.InsufficientBalance);
        }

        var isHotelAvailableResult = request.Hotel.IsBookable(request.Schedule);
        if (isHotelAvailableResult.IsFailure)
        {
            return Result.Failure(isHotelAvailableResult.Error);
        }

        return Result.Success();
    }

    public Result SetBalance(decimal amount)
    {
        var result = Money.Create(amount);
        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        Balance = result.Value;
        return Result.Success();
    }

    private bool IsBalanceEnough(Money totalPayment) => Balance >= totalPayment;

}
