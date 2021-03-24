using Microsoft.EntityFrameworkCore.Migrations;

namespace Start_To_Finish.Data.Migrations
{
    public partial class AddedToDoListMaker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "525bef57-97d2-4448-80f5-a7a82a05de13");

            migrationBuilder.CreateTable(
                name: "ToDoListMakers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IdentityUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoListMakers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToDoListMakers_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ca4c6c5c-09dc-4d97-bfd3-d6815bf631d8", "71e7e78d-6b51-4d83-9fb4-f4e94b3032e4", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoListMakers_IdentityUserId",
                table: "ToDoListMakers",
                column: "IdentityUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDoListMakers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca4c6c5c-09dc-4d97-bfd3-d6815bf631d8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "525bef57-97d2-4448-80f5-a7a82a05de13", "f4c75192-805d-4bad-a08e-a63d3596be20", "Admin", "ADMIN" });
        }
    }
}
