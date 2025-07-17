using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTraining1101Demo.Migrations
{
    public partial class RemoveCustomerUserCompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerUsers",
                table: "CustomerUsers");

            migrationBuilder.DropIndex(
                name: "IX_CustomerUsers_UserId",
                table: "CustomerUsers");

            // Add the new Id column with IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CustomerUsers",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerUsers",
                table: "CustomerUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerUsers_CustomerId",
                table: "CustomerUsers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerUsers_UserId",
                table: "CustomerUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerUsers",
                table: "CustomerUsers");

            migrationBuilder.DropIndex(
                name: "IX_CustomerUsers_CustomerId",
                table: "CustomerUsers");

            migrationBuilder.DropIndex(
                name: "IX_CustomerUsers_UserId",
                table: "CustomerUsers");

            // Drop the identity Id column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "CustomerUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerUsers",
                table: "CustomerUsers",
                columns: new[] { "CustomerId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerUsers_UserId",
                table: "CustomerUsers",
                column: "UserId",
                unique: true);
        }
    }
}
