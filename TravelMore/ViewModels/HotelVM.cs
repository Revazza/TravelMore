using System.ComponentModel.DataAnnotations;

namespace TravelMore.ViewModels
{
    public class HotelVM
    {
        public Guid Id { get; set; }
        [Required]
        public string? City { get; set; }
        public string? Address { get; set; }
        public int NumberOfBeds { get; set; }
        public string? Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int DistanceToCenter { get; set; }
        public Guid OwnerId { get; set; }
        public ImageVM? ImageVM { get; set; }

    }
}
