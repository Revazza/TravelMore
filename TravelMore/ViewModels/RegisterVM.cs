using System.ComponentModel.DataAnnotations;

namespace TravelMore.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? RePassword{ get; set; }


    }
}
