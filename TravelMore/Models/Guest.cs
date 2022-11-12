using System.ComponentModel.DataAnnotations;

namespace TravelMore.Models
{
    public class Guest
    {
        [Required]
        public Guid GuestId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime HostFrom { get; set; } 
        public DateTime HostTo { get; set; }
        public Guid HotelId { get; set; }



        public Guest()
        {

        }

        public void Validate()
        {
            if(string.IsNullOrEmpty(FirstName))
            {
                throw new ArgumentException("First name field is empty");
            }
            if(string.IsNullOrEmpty(LastName))
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
