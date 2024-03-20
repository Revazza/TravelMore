namespace TravelMore.Domain.Hotels.Exceptions;

public class HotelInvalidGuestNumberException : Exception
{
    public HotelInvalidGuestNumberException() : base("Guest number is less than or equal to zero, please choose positive number")
    {
    }
}
