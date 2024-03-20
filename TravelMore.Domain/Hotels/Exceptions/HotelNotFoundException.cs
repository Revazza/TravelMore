namespace TravelMore.Domain.Hotels.Exceptions;

public class HotelNotFoundException : Exception
{
    public HotelNotFoundException() : base("Hotel not found")
    {
    }
}
