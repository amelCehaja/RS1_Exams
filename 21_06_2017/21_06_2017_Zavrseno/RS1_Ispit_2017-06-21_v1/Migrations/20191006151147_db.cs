using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_Ispit_20170621_v1.Migrations
{
    public partial class db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nastavnik",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImePrezime = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nastavnik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ucenik",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImePrezime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucenik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Odjeljenje",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NastavnikId = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odjeljenje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Odjeljenje_Nastavnik_NastavnikId",
                        column: x => x.NastavnikId,
                        principalTable: "Nastavnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaturskiIspit",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Datum = table.Column<DateTime>(nullable: false),
                    IspitivacID = table.Column<int>(nullable: false),
                    OdjeljenjeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaturskiIspit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaturskiIspit_Nastavnik_IspitivacID",
                        column: x => x.IspitivacID,
                        principalTable: "Nastavnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaturskiIspit_Odjeljenje_OdjeljenjeID",
                        column: x => x.OdjeljenjeID,
                        principalTable: "Odjeljenje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UpisUOdjeljenje",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojUDnevniku = table.Column<int>(nullable: false),
                    OdjeljenjeId = table.Column<int>(nullable: false),
                    OpciUspjeh = table.Column<int>(nullable: false),
                    UcenikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpisUOdjeljenje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpisUOdjeljenje_Odjeljenje_OdjeljenjeId",
                        column: x => x.OdjeljenjeId,
                        principalTable: "Odjeljenje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UpisUOdjeljenje_Ucenik_UcenikId",
                        column: x => x.UcenikId,
                        principalTable: "Ucenik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaturskiIspitStavka",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bodovi = table.Column<float>(nullable: true),
                    MaturskiIspitID = table.Column<int>(nullable: false),
                    Osloboden = table.Column<bool>(nullable: false),
                    UpisUOdjeljenjeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaturskiIspitStavka", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaturskiIspitStavka_MaturskiIspit_MaturskiIspitID",
                        column: x => x.MaturskiIspitID,
                        principalTable: "MaturskiIspit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaturskiIspitStavka_UpisUOdjeljenje_UpisUOdjeljenjeID",
                        column: x => x.UpisUOdjeljenjeID,
                        principalTable: "UpisUOdjeljenje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspit_IspitivacID",
                table: "MaturskiIspit",
                column: "IspitivacID");

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspit_OdjeljenjeID",
                table: "MaturskiIspit",
                column: "OdjeljenjeID");

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspitStavka_MaturskiIspitID",
                table: "MaturskiIspitStavka",
                column: "MaturskiIspitID");

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspitStavka_UpisUOdjeljenjeID",
                table: "MaturskiIspitStavka",
                column: "UpisUOdjeljenjeID");

            migrationBuilder.CreateIndex(
                name: "IX_Odjeljenje_NastavnikId",
                table: "Odjeljenje",
                column: "NastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX_UpisUOdjeljenje_OdjeljenjeId",
                table: "UpisUOdjeljenje",
                column: "OdjeljenjeId");

            migrationBuilder.CreateIndex(
                name: "IX_UpisUOdjeljenje_UcenikId",
                table: "UpisUOdjeljenje",
                column: "UcenikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaturskiIspitStavka");

            migrationBuilder.DropTable(
                name: "MaturskiIspit");

            migrationBuilder.DropTable(
                name: "UpisUOdjeljenje");

            migrationBuilder.DropTable(
                name: "Odjeljenje");

            migrationBuilder.DropTable(
                name: "Ucenik");

            migrationBuilder.DropTable(
                name: "Nastavnik");
        }
    }
}
