using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.DTOs
{
    public class CountryBaseDto
    {
        [Required]
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
