using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Migrations
{
    /// <inheritdoc />
    public partial class AlterCheckConstraintOfMemberEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "EmailCheckConstraint",
                table: "Members");

            migrationBuilder.AddCheckConstraint(
                name: "EmailCheckConstraint",
                table: "Members",
                sql: "Email like '_%@_%._%'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "EmailCheckConstraint",
                table: "Members");

            migrationBuilder.AddCheckConstraint(
                name: "EmailCheckConstraint",
                table: "Members",
                sql: "Email like '--%@-%.-%'");
        }
    }
}
