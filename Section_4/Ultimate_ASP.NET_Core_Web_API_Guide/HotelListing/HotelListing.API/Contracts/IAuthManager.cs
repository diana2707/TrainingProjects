using HotelListing.API.DTOs.User;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Contracts
{
    public interface IAuthManager
    {
        public Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);
        public Task<AuthResponseDto> Login(LoginDto userDto);
        public Task<string> CreateRefreshToken();
        public Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
    }
}
