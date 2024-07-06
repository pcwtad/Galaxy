using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    HeadImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPasswod = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    Userfwi = table.Column<int>(type: "int", nullable: false),
                    Userfans = table.Column<int>(type: "int", nullable: false),
                    Userlike = table.Column<int>(type: "int", nullable: false),
                    Usertime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Userintroduce = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Usermail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Users");
        }
    }
}
