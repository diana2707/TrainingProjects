using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.DTOs;

namespace HotelListing.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // Create mapping configurations here
            CreateMap<CountryPostDto, Country>().ReverseMap();
            CreateMap<CountryGetSimpleDto, Country>().ReverseMap();
            CreateMap<CountryGetDetailedDto, Country>().ReverseMap();
            CreateMap<CountryPutDto, Country>().ReverseMap();
            CreateMap<HotelResponseDto, Hotel>().ReverseMap();
        }
    }
}
