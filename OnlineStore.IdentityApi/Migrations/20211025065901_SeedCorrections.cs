using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.IdentityApi.Migrations
{
    public partial class SeedCorrections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "idt",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "idt",
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PetName", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b74ddd14-6340-4840-95c2-db12554843e5", 0, "462b758b-18f8-4244-8741-f0bdaf67a7f2", "admin@gmail.com", false, false, null, null, null, null, null, null, false, "4611fe9c-d917-447a-87b4-ca864a30e258", false, "Admin" });
        }
    }
}
