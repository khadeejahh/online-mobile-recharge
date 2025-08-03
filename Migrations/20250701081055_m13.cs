using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mobile_Phone_Project.Migrations
{
    /// <inheritdoc />
    public partial class m13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InternetType",
                table: "RechargePackages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MBs",
                table: "RechargePackages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OffNetMinutes",
                table: "RechargePackages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OnNetMinutes",
                table: "RechargePackages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SMS",
                table: "RechargePackages",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InternetType",
                table: "RechargePackages");

            migrationBuilder.DropColumn(
                name: "MBs",
                table: "RechargePackages");

            migrationBuilder.DropColumn(
                name: "OffNetMinutes",
                table: "RechargePackages");

            migrationBuilder.DropColumn(
                name: "OnNetMinutes",
                table: "RechargePackages");

            migrationBuilder.DropColumn(
                name: "SMS",
                table: "RechargePackages");
        }
    }
}
