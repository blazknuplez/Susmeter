using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Susmeter.Ef.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    HexCode = table.Column<string>(maxLength: 7, nullable: false),
                    ColorName = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.HexCode);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    MatchId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Winner = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.MatchId);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    Nickname = table.Column<string>(maxLength: 25, nullable: false),
                    AvatarHexColor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Player_Color_AvatarHexColor",
                        column: x => x.AvatarHexColor,
                        principalTable: "Color",
                        principalColumn: "HexCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchPlayer",
                columns: table => new
                {
                    MatchPlayerId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MatchId = table.Column<long>(nullable: false),
                    PlayerId = table.Column<long>(nullable: false),
                    HexColor = table.Column<string>(nullable: true),
                    PlayerRole = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPlayer", x => x.MatchPlayerId);
                    table.ForeignKey(
                        name: "FK_MatchPlayer_Color_HexColor",
                        column: x => x.HexColor,
                        principalTable: "Color",
                        principalColumn: "HexCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchPlayer_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchPlayer_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "HexCode", "ColorName" },
                values: new object[] { "#C51111", "Red" });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "HexCode", "ColorName" },
                values: new object[] { "#132ED1", "Blue" });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "HexCode", "ColorName" },
                values: new object[] { "#117F2D", "Green" });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "HexCode", "ColorName" },
                values: new object[] { "#ED54BA", "Pink" });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "HexCode", "ColorName" },
                values: new object[] { "#EF7D0E", "Orange" });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "HexCode", "ColorName" },
                values: new object[] { "#F6F658", "Yellow" });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "HexCode", "ColorName" },
                values: new object[] { "#3F474E", "Black" });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "HexCode", "ColorName" },
                values: new object[] { "#D6E0F0", "White" });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "HexCode", "ColorName" },
                values: new object[] { "#6B31BC", "Purple" });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "HexCode", "ColorName" },
                values: new object[] { "#71491E", "Brown" });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "HexCode", "ColorName" },
                values: new object[] { "#38FEDB", "Cyan" });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "HexCode", "ColorName" },
                values: new object[] { "#50EF39", "Lime" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "PlayerId", "AvatarHexColor", "Name", "Nickname" },
                values: new object[] { 7L, "#132ED1", "Dani", "Bani" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "PlayerId", "AvatarHexColor", "Name", "Nickname" },
                values: new object[] { 9L, "#117F2D", "Jure", "Jure" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "PlayerId", "AvatarHexColor", "Name", "Nickname" },
                values: new object[] { 2L, "#ED54BA", "Lara", "Meronja" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "PlayerId", "AvatarHexColor", "Name", "Nickname" },
                values: new object[] { 1L, "#F6F658", "Blaž", "not knupli" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "PlayerId", "AvatarHexColor", "Name", "Nickname" },
                values: new object[] { 5L, "#3F474E", "Kevin", "Krušni oče" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "PlayerId", "AvatarHexColor", "Name", "Nickname" },
                values: new object[] { 6L, "#D6E0F0", "Eagle", "Eagle" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "PlayerId", "AvatarHexColor", "Name", "Nickname" },
                values: new object[] { 10L, "#6B31BC", "Arijan", "Archie" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "PlayerId", "AvatarHexColor", "Name", "Nickname" },
                values: new object[] { 4L, "#71491E", "Roki", "thefk69" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "PlayerId", "AvatarHexColor", "Name", "Nickname" },
                values: new object[] { 8L, "#38FEDB", "Valerija", "Val" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "PlayerId", "AvatarHexColor", "Name", "Nickname" },
                values: new object[] { 3L, "#50EF39", "Tancer", "c00kie" });

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayer_HexColor",
                table: "MatchPlayer",
                column: "HexColor");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayer_MatchId",
                table: "MatchPlayer",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayer_PlayerId",
                table: "MatchPlayer",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_AvatarHexColor",
                table: "Player",
                column: "AvatarHexColor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchPlayer");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Color");
        }
    }
}
