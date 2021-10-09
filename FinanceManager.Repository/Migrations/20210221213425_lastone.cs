using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceManager.Repository.Migrations
{
    public partial class lastone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Senders_Shippers",
                schema: "Batches",
                table: "Senders");

            migrationBuilder.AddForeignKey(
                name: "FK_Senders_Shippers_ShippersID",
                schema: "Batches",
                table: "Senders",
                column: "ShippersID",
                principalSchema: "Batches",
                principalTable: "Shippers",
                principalColumn: "ShippersID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Senders_Shippers_ShippersID",
                schema: "Batches",
                table: "Senders");

            migrationBuilder.AddForeignKey(
                name: "FK_Senders_Shippers",
                schema: "Batches",
                table: "Senders",
                column: "ShippersID",
                principalSchema: "Batches",
                principalTable: "Shippers",
                principalColumn: "ShippersID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
