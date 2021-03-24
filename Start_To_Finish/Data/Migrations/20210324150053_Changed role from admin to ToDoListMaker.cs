using Microsoft.EntityFrameworkCore.Migrations;

namespace Start_To_Finish.Data.Migrations
{
    public partial class ChangedrolefromadmintoToDoListMaker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca4c6c5c-09dc-4d97-bfd3-d6815bf631d8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8a0db473-b59a-449a-8f85-cf2a3d83f39f", "505b46e8-9650-4697-9ee9-7d7935dbf589", "ToDoListMaker", "ToDoListMaker" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a0db473-b59a-449a-8f85-cf2a3d83f39f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ca4c6c5c-09dc-4d97-bfd3-d6815bf631d8", "71e7e78d-6b51-4d83-9fb4-f4e94b3032e4", "Admin", "ADMIN" });
        }
    }
}
