using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.DTOs.User
{
    public class ApiUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "The password must be at most 15 characters long.")]
        public string Password { get; set; }
    }
}
