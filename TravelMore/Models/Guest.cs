using System.ComponentModel.DataAnnotations;
using TravelMore.ViewModels;

namespace TravelMore.Models
{
    public enum Status
    {
        Accepted = 1,
        Declined = 2,
        NotPossible = 3,
        Pending = 4,
    }
    public class Guest
    {
        [Required]
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public Guid? GuestId { get; set; }
        public DateTime? HostFrom { get; set; }
        public DateTime? HostTo { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public Guid? HotelOwnerId { get; set; }
        public Guid? HotelId { get; set; }





        public void Validate()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                throw new ArgumentException("First name field is empty");
            }
            if (string.IsNullOrEmpty(LastName))
            {
                throw new ArgumentException("Last name field is empty");
            }
            /*if(DateTime.Now > HostFrom)
            {
                throw new ArgumentException("Start date should be more or equal than today");
            }*/

        }
    }
}
