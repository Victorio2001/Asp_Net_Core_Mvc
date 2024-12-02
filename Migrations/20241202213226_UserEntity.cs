using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NomDuProjet.Migrations
{
    /// <inheritdoc />
    public partial class UserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id_address = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    street_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state_address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id_address);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id_utilisateur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    prenom_utilisateur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nom_utilisateur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mail_utilisateur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    allergie_utilisateur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    handicap_utilisateur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    valide_compte_utilisateur = table.Column<bool>(type: "bit", nullable: false),
                    created_at_utilisateur = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at_utilisateur = table.Column<DateTime>(type: "datetime2", nullable: false),
                    addressId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id_utilisateur);
                    table.ForeignKey(
                        name: "FK_User_Address_addressId",
                        column: x => x.addressId,
                        principalTable: "Address",
                        principalColumn: "Id_address",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_addressId",
                table: "User",
                column: "addressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
