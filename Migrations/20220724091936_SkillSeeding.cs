using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetRPG.Migrations
{
    public partial class SkillSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Damage", "Name" },
                values: new object[] { 1, 30, "Frenzy" });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "Id", "Damage", "Name" },
                values: new object[] { 2, 20, "Fire" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skill",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
