using System.ComponentModel.DataAnnotations;

namespace TravelMore.ViewModels
{
    public class AuthVM
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
