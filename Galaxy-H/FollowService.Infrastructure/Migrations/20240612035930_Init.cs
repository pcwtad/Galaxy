using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FollowService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Follows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Followers = table.Column<int>(type: "int", nullable: false),
                    Beingfollowed = table.Column<int>(type: "int", nullable: false),
                    FollowTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFollowing = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Follows", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Follows");
        }
    }
}
