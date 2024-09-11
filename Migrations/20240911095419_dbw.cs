using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMSWEBAPI.Migrations
{
    /// <inheritdoc />
    public partial class dbw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "StudentAttendance");

            migrationBuilder.AlterColumn<string>(
                name: "ClassId",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Classname",
                table: "StudentAttendance",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Classname",
                table: "StudentAttendance");

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "StudentAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
