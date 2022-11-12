using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelMore.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public List<Hotel>? BookedHotels { get; set; } = new List<Hotel>();
        [Timestamp]
        public List<Guest> Guests { get; set; } = new List<Guest>();
        public Guid? OwnedHotelID { get; set; } = null;


    }
}
