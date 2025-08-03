using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mobile_Phone_Project.Migrations
{
    /// <inheritdoc />
    public partial class m16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RechargePackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValidityDays = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OperatorId = table.Column<int>(type: "int", nullable: false),
                    MBs = table.Column<int>(type: "int", nullable: true),
                    OnNetMinutes = table.Column<int>(type: "int", nullable: true),
                    OffNetMinutes = table.Column<int>(type: "int", nullable: true),
                    SMS = table.Column<int>(type: "int", nullable: true),
                    InternetType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RechargePackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RechargePackages_Operators_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Operators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RechargePackages_OperatorId",
                table: "RechargePackages",
                column: "OperatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RechargePackages");
        }
    }
}
