using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "b44efd65-5d3a-4da2-bd6e-66077092f1fd",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    ConcurrencyStamp = "b0210230-3fe9-4e8e-9a29-6c02fb1f8e90"
                },
                new IdentityRole
                {
                    Id = "c21f1178-3570-47a6-a6ec-0f7dc1261dcd",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "ae32181f-a478-4ff2-bf60-48cb34a3afcb"
                }
            );
        }
    }
}
