using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelListing.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentityRolesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b44efd65-5d3a-4da2-bd6e-66077092f1fd", "b0210230-3fe9-4e8e-9a29-6c02fb1f8e90", "Administrator", "ADMINISTRATOR" },
                    { "c21f1178-3570-47a6-a6ec-0f7dc1261dcd", "ae32181f-a478-4ff2-bf60-48cb34a3afcb", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b44efd65-5d3a-4da2-bd6e-66077092f1fd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c21f1178-3570-47a6-a6ec-0f7dc1261dcd");
        }
    }
}
