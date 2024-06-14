using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BITS.Migrations.BITS
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "Source",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ExternalLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Source", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "User",
            //    columns: table => new
            //    {
            //        UserId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Roles = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Suburb = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        State = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CardOwner = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Expiry = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CVV = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_User", x => x.UserId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Product",
            //    columns: table => new
            //    {
            //        ProductId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Developer = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Genres = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Features = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastUpdatedByUserId = table.Column<int>(type: "int", nullable: true),
            //        LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Product", x => x.ProductId);
            //        table.ForeignKey(
            //            name: "FK_Product_User_LastUpdatedByUserId",
            //            column: x => x.LastUpdatedByUserId,
            //            principalTable: "User",
            //            principalColumn: "UserId");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Product_LastUpdatedByUserId",
            //    table: "Product",
            //    column: "LastUpdatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Source");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
