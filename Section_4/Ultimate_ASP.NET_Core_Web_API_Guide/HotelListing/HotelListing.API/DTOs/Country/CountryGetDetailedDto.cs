using HotelListing.API.Data;
using HotelListing.API.DTOs.Hotel;

namespace HotelListing.API.DTOs.Country
{
    public class CountryGetDetailedDto : CountryBaseDto
    {
        public int Id { get; set; }
        public virtual IList<HotelDto>? Hotels { get; set; }
    }
}
