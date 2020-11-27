using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Digital_nomads.Data.Migrations
{
    public partial class inicijalnaDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginRola",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpisRole = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginRola", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projekt",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivProjekta = table.Column<string>(nullable: true),
                    DatumPocetka = table.Column<DateTime>(nullable: false),
                    DatumZavrsetka = table.Column<DateTime>(nullable: true),
                    Rok = table.Column<DateTime>(nullable: false),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleNaProjektu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rola = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleNaProjektu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trofej",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivTrofeja = table.Column<string>(nullable: true),
                    PhotoRuta = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trofej", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vjestina",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vjestina", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Korsinik",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    DatumZaposlenja = table.Column<DateTime>(nullable: false),
                    LoginRolaId = table.Column<int>(nullable: false),
                    LoginID = table.Column<int>(nullable: false),
                    PhotoRuta = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korsinik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Korsinik_Login_LoginID",
                        column: x => x.LoginID,
                        principalTable: "Login",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Korsinik_LoginRola_LoginRolaId",
                        column: x => x.LoginRolaId,
                        principalTable: "LoginRola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ChatPoruka",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(nullable: false),
                    ProjektId = table.Column<int>(nullable: false),
                    Pin = table.Column<bool>(nullable: false),
                    Sadrzaj = table.Column<string>(nullable: true),
                    VrijemePoruke = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatPoruka", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatPoruka_Korsinik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korsinik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ChatPoruka_Projekt_ProjektId",
                        column: x => x.ProjektId,
                        principalTable: "Projekt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ProjektniTim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjektId = table.Column<int>(nullable: false),
                    KorisnikID = table.Column<int>(nullable: false),
                    RolaNaProjektuId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjektniTim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjektniTim_Korsinik_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korsinik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProjektniTim_Projekt_ProjektId",
                        column: x => x.ProjektId,
                        principalTable: "Projekt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProjektniTim_RoleNaProjektu_RolaNaProjektuId",
                        column: x => x.RolaNaProjektuId,
                        principalTable: "RoleNaProjektu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Opis = table.Column<string>(nullable: true),
                    Bodovi = table.Column<int>(nullable: false),
                    Zauzet = table.Column<bool>(nullable: false),
                    Pocetak = table.Column<DateTime>(nullable: false),
                    Kraj = table.Column<DateTime>(nullable: false),
                    Rok = table.Column<DateTime>(nullable: false),
                    ProjektId = table.Column<int>(nullable: false),
                    KorisnikId = table.Column<int>(nullable: true),
                    VjestinaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Korsinik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korsinik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Task_Projekt_ProjektId",
                        column: x => x.ProjektId,
                        principalTable: "Projekt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Task_Vjestina_VjestinaID",
                        column: x => x.VjestinaID,
                        principalTable: "Vjestina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TrofejKorisnik",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(nullable: false),
                    TrofejId = table.Column<int>(nullable: false),
                    DatumDodjele = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrofejKorisnik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrofejKorisnik_Korsinik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korsinik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TrofejKorisnik_Trofej_TrofejId",
                        column: x => x.TrofejId,
                        principalTable: "Trofej",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "VjestinaKorisnik",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikID = table.Column<int>(nullable: false),
                    VjestinaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VjestinaKorisnik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VjestinaKorisnik_Korsinik_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korsinik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_VjestinaKorisnik_Vjestina_VjestinaID",
                        column: x => x.VjestinaID,
                        principalTable: "Vjestina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatPoruka_KorisnikId",
                table: "ChatPoruka",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatPoruka_ProjektId",
                table: "ChatPoruka",
                column: "ProjektId");

            migrationBuilder.CreateIndex(
                name: "IX_Korsinik_LoginID",
                table: "Korsinik",
                column: "LoginID");

            migrationBuilder.CreateIndex(
                name: "IX_Korsinik_LoginRolaId",
                table: "Korsinik",
                column: "LoginRolaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjektniTim_KorisnikID",
                table: "ProjektniTim",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjektniTim_ProjektId",
                table: "ProjektniTim",
                column: "ProjektId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjektniTim_RolaNaProjektuId",
                table: "ProjektniTim",
                column: "RolaNaProjektuId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_KorisnikId",
                table: "Task",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_ProjektId",
                table: "Task",
                column: "ProjektId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_VjestinaID",
                table: "Task",
                column: "VjestinaID");

            migrationBuilder.CreateIndex(
                name: "IX_TrofejKorisnik_KorisnikId",
                table: "TrofejKorisnik",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_TrofejKorisnik_TrofejId",
                table: "TrofejKorisnik",
                column: "TrofejId");

            migrationBuilder.CreateIndex(
                name: "IX_VjestinaKorisnik_KorisnikID",
                table: "VjestinaKorisnik",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_VjestinaKorisnik_VjestinaID",
                table: "VjestinaKorisnik",
                column: "VjestinaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatPoruka");

            migrationBuilder.DropTable(
                name: "ProjektniTim");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "TrofejKorisnik");

            migrationBuilder.DropTable(
                name: "VjestinaKorisnik");

            migrationBuilder.DropTable(
                name: "RoleNaProjektu");

            migrationBuilder.DropTable(
                name: "Projekt");

            migrationBuilder.DropTable(
                name: "Trofej");

            migrationBuilder.DropTable(
                name: "Korsinik");

            migrationBuilder.DropTable(
                name: "Vjestina");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "LoginRola");
        }
    }
}
