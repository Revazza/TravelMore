namespace TravelMore.ViewModels
{
    public class GuestVM
    {
        public Guid GuestId { get; set; }
        public DateTime HostFrom { get; set; }
        public DateTime HostTo { get; set; }
        public Guid HotelId { get; set; }
    }
}
