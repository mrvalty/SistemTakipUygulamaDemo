using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TekstilTakipSistemiCore.DATA.Migrations
{
    public partial class newDbCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KirliDepolari",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OlusturmaZamani = table.Column<DateTime>(nullable: false),
                    GuncellemeZamani = table.Column<DateTime>(nullable: false),
                    AktifMi = table.Column<bool>(nullable: false),
                    KirlenmeSayisi = table.Column<int>(nullable: false),
                    HasarDurumu = table.Column<int>(nullable: false),
                    TeslimDurumu = table.Column<int>(nullable: false),
                    CamasirKirliMi = table.Column<bool>(nullable: false),
                    TartimTarihi = table.Column<DateTime>(nullable: false),
                    YipranmaAgirligi = table.Column<long>(nullable: false),
                    KullanimdaMi = table.Column<bool>(nullable: false),
                    HurdaMi = table.Column<bool>(nullable: false),
                    KimliklendirmeId = table.Column<int>(nullable: false),
                    KullaniciId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KirliDepolari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mefrusatlar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OlusturmaZamani = table.Column<DateTime>(nullable: false),
                    GuncellemeZamani = table.Column<DateTime>(nullable: false),
                    AktifMi = table.Column<bool>(nullable: false),
                    MefrusatTuru = table.Column<string>(nullable: true),
                    MefrusatKodu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mefrusatlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaketAlanlari",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OlusturmaZamani = table.Column<DateTime>(nullable: false),
                    GuncellemeZamani = table.Column<DateTime>(nullable: false),
                    AktifMi = table.Column<bool>(nullable: false),
                    YikanmaSayisi = table.Column<int>(nullable: false),
                    OdaNumarasi = table.Column<string>(nullable: true),
                    PaketNo = table.Column<string>(nullable: true),
                    MefrusatId = table.Column<int>(nullable: false),
                    KimliklendirmeId = table.Column<int>(nullable: false),
                    KullaniciId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaketAlanlari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemizDepolari",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OlusturmaZamani = table.Column<DateTime>(nullable: false),
                    GuncellemeZamani = table.Column<DateTime>(nullable: false),
                    AktifMi = table.Column<bool>(nullable: false),
                    YikanmaSayisi = table.Column<int>(nullable: false),
                    YikamaDurumu = table.Column<int>(nullable: false),
                    TeslimDurumu = table.Column<int>(nullable: false),
                    CamasirYikandiMi = table.Column<bool>(nullable: false),
                    TartimTarihi = table.Column<DateTime>(nullable: false),
                    YipranmaAgirligi = table.Column<long>(nullable: false),
                    AmbalajlandiMi = table.Column<bool>(nullable: false),
                    BohcalandiMi = table.Column<bool>(nullable: false),
                    KimliklendirmeId = table.Column<int>(nullable: false),
                    KullaniciId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemizDepolari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UygulamaKullanicilar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OlusturmaZamani = table.Column<DateTime>(nullable: false),
                    GuncellemeZamani = table.Column<DateTime>(nullable: false),
                    AktifMi = table.Column<bool>(nullable: false),
                    Ad = table.Column<string>(nullable: true),
                    Soyad = table.Column<string>(nullable: true),
                    KullaniciAdi = table.Column<string>(nullable: true),
                    Sifre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UygulamaKullanicilar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kimliklendirmeler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OlusturmaZamani = table.Column<DateTime>(nullable: false),
                    GuncellemeZamani = table.Column<DateTime>(nullable: false),
                    AktifMi = table.Column<bool>(nullable: false),
                    EtiketNo = table.Column<string>(nullable: true),
                    HasarAciklama = table.Column<string>(nullable: true),
                    MefrusatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kimliklendirmeler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kimliklendirmeler_Mefrusatlar_MefrusatId",
                        column: x => x.MefrusatId,
                        principalTable: "Mefrusatlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UygulamaKullanicilar",
                columns: new[] { "Id", "Ad", "AktifMi", "GuncellemeZamani", "KullaniciAdi", "OlusturmaZamani", "Sifre", "Soyad" },
                values: new object[] { 1, "Admin", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin.admin", new DateTime(2023, 6, 21, 14, 18, 46, 290, DateTimeKind.Local).AddTicks(2423), "123", "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Kimliklendirmeler_MefrusatId",
                table: "Kimliklendirmeler",
                column: "MefrusatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kimliklendirmeler");

            migrationBuilder.DropTable(
                name: "KirliDepolari");

            migrationBuilder.DropTable(
                name: "PaketAlanlari");

            migrationBuilder.DropTable(
                name: "TemizDepolari");

            migrationBuilder.DropTable(
                name: "UygulamaKullanicilar");

            migrationBuilder.DropTable(
                name: "Mefrusatlar");
        }
    }
}
