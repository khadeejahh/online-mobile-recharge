using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mobile_Phone_Project.Migrations
{
    /// <inheritdoc />
    public partial class m38 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillingMonth",
                table: "TopUpTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CNIC",
                table: "TopUpTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "TopUpTransactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "TopUpTransactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "TopUpTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingMonth",
                table: "TopUpTransactions");

            migrationBuilder.DropColumn(
                name: "CNIC",
                table: "TopUpTransactions");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "TopUpTransactions");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "TopUpTransactions");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "TopUpTransactions");
        }
    }
}
