using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_DetailsImgs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FileSHA256Hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RemoteUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_DetailsImgs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Shows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Showbrin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Showcover = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Showwhere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Showaddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Showindex = table.Column<int>(type: "int", nullable: false),
                    Showinformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Showstate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Shows", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_DetailsImgs");

            migrationBuilder.DropTable(
                name: "T_Shows");
        }
    }
}
