using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.DTOs.Country
{
    public class CountryBaseDto
    {
        [Required]
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
