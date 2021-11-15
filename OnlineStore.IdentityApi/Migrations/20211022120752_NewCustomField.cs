using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.IdentityApi.Migrations
{
    public partial class NewCustomField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PetName",
                schema: "idt",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "idt",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "462b758b-18f8-4244-8741-f0bdaf67a7f2", "4611fe9c-d917-447a-87b4-ca864a30e258" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PetName",
                schema: "idt",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                schema: "idt",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "87b79197-5da3-43ea-a765-e22d83cdf2d1", "7ddff3b4-7bfe-47ed-bfeb-e2761b7b4f84" });
        }
    }
}
