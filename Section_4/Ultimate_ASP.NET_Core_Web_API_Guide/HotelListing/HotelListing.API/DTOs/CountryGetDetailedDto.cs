using HotelListing.API.Data;

namespace HotelListing.API.DTOs
{
    public class CountryGetDetailedDto : CountryBaseDto
    {
        public int Id { get; set; }
        public virtual IList<HotelResponseDto>? Hotels { get; set; }
    }
}
