namespace TravelMore.ViewModels
{
    public class HotelVM
    {
        public Guid Id { get; set; } = Guid.Empty;
        public double MinPrice { get; set; } = double.MinValue;
        public double MaxPrice { get; set; } = double.MaxValue;
        public int Amount { get; set; }

    }
}
