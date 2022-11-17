namespace TravelMore.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid GuestId { get; set; }
        public Hotel? BookedHotel { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public DateTime HostFrom { get; set; }
        public DateTime HostTo { get; set; }

    }
}
