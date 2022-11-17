using System.ComponentModel.DataAnnotations;

namespace TravelMore.Models
{
    public class Hotel
    {
        // TODO:
        // Add Price property
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


        

        public void Validate()
        {
            if (string.IsNullOrEmpty(Description))
            {
                throw new ArgumentException("Provide information about hotel");
            }
            if (string.IsNullOrEmpty(Address))
            {
                throw new ArgumentException("Provide information about hotel address");
            }
            if (string.IsNullOrEmpty(City))
            {
                throw new ArgumentException("Provide information about hotel location");
            }
            if (NumberOfBeds <= 0)
            {
                throw new ArgumentException("Hotel must have at least 1 bed");
            }
            if (Latitude == 0 || Longitude == 0)
            {
                throw new ArgumentException("Invalid coordinates");
            }

        }
    }
}
