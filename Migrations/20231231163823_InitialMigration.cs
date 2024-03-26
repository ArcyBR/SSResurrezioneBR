using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SSResurrezioneBR.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Almanacco",
                columns: table => new
                {
                    Id_Almanacco = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descrizione_Almanacco = table.Column<string>(type: "TEXT", nullable: false),
                    Titolo_Almanacco = table.Column<string>(type: "TEXT", nullable: false),
                    Data_Evento_Almanacco = table.Column<string>(type: "TEXT", nullable: false),
                    Image_Visible_Almanacco = table.Column<int>(type: "INTEGER", nullable: true),
                    Creatore_Evento_Almanacco = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Almanacco", x => x.Id_Almanacco);
                });

            migrationBuilder.CreateTable(
                name: "CoroPolifonicoMaterMisericordie",
                columns: table => new
                {
                    Id_CoroPolifonicoMaterMisericordie = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descrizione_CoroPolifonicoMaterMisericordie = table.Column<string>(type: "TEXT", nullable: false),
                    Titolo_CoroPolifonicoMaterMisericordie = table.Column<string>(type: "TEXT", nullable: true),
                    Data_Evento_CoroPolifonicoMaterMisericordie = table.Column<string>(type: "TEXT", nullable: false),
                    Image_Visible_CoroPolifonicoMaterMisericordie = table.Column<int>(type: "INTEGER", nullable: true),
                    Creatore_Evento_CoroPolifonicoMaterMisericordie = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoroPolifonicoMaterMisericordie", x => x.Id_CoroPolifonicoMaterMisericordie);
                });

            migrationBuilder.CreateTable(
                name: "Img_For_Carousel",
                columns: table => new
                {
                    Image_Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Image_Path = table.Column<string>(type: "TEXT", nullable: false),
                    Image_Label = table.Column<string>(type: "TEXT", nullable: false),
                    Image_Content_Title = table.Column<string>(type: "TEXT", nullable: false),
                    Image_Visible = table.Column<long>(type: "INTEGER", nullable: false),
                    Image_Content_Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Img_For_Carousel", x => x.Image_Id);
                });

            migrationBuilder.CreateTable(
                name: "Incarichi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titolo = table.Column<string>(type: "TEXT", nullable: false),
                    Cognome = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Incarico = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Telefono_Fisso = table.Column<string>(type: "TEXT", nullable: true),
                    Telefono_Cellulare = table.Column<string>(type: "TEXT", nullable: true),
                    Indirizzo = table.Column<string>(type: "TEXT", nullable: true),
                    Numero_Civico = table.Column<string>(type: "TEXT", nullable: true),
                    CAP = table.Column<string>(type: "TEXT", nullable: true),
                    Città = table.Column<string>(type: "TEXT", nullable: true),
                    Link_Diocesi = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incarichi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Almanacco_Foto",
                columns: table => new
                {
                    Id_Almanacco_Foto = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Path_Foto_Almanacco_Foto = table.Column<string>(type: "TEXT", nullable: false),
                    id_Almanacco = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Almanacco_Foto", x => x.Id_Almanacco_Foto);
                    table.ForeignKey(
                        name: "FK_Almanacco_Foto_Almanacco_id_Almanacco",
                        column: x => x.id_Almanacco,
                        principalTable: "Almanacco",
                        principalColumn: "Id_Almanacco");
                });

            migrationBuilder.CreateTable(
                name: "CoroPolifonicoMaterMisericordie_Foto",
                columns: table => new
                {
                    Id_CoroPolifonicoMaterMisericordie_Foto = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Path_Foto_CoroPolifonicoMaterMisericordie_Foto = table.Column<string>(type: "TEXT", nullable: false),
                    id_CoroPolifonicoMaterMisericordie = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoroPolifonicoMaterMisericordie_Foto", x => x.Id_CoroPolifonicoMaterMisericordie_Foto);
                    table.ForeignKey(
                        name: "FK_CoroPolifonicoMaterMisericordie_Foto_CoroPolifonicoMaterMisericordie_id_CoroPolifonicoMaterMisericordie",
                        column: x => x.id_CoroPolifonicoMaterMisericordie,
                        principalTable: "CoroPolifonicoMaterMisericordie",
                        principalColumn: "Id_CoroPolifonicoMaterMisericordie");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Almanacco_Id_Almanacco",
                table: "Almanacco",
                column: "Id_Almanacco",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Almanacco_Foto_id_Almanacco",
                table: "Almanacco_Foto",
                column: "id_Almanacco");

            migrationBuilder.CreateIndex(
                name: "IX_Almanacco_Foto_Id_Almanacco_Foto",
                table: "Almanacco_Foto",
                column: "Id_Almanacco_Foto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoroPolifonicoMaterMisericordie_Id_CoroPolifonicoMaterMisericordie",
                table: "CoroPolifonicoMaterMisericordie",
                column: "Id_CoroPolifonicoMaterMisericordie",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoroPolifonicoMaterMisericordie_Foto_id_CoroPolifonicoMaterMisericordie",
                table: "CoroPolifonicoMaterMisericordie_Foto",
                column: "id_CoroPolifonicoMaterMisericordie");

            migrationBuilder.CreateIndex(
                name: "IX_CoroPolifonicoMaterMisericordie_Foto_Id_CoroPolifonicoMaterMisericordie_Foto",
                table: "CoroPolifonicoMaterMisericordie_Foto",
                column: "Id_CoroPolifonicoMaterMisericordie_Foto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CognomeIdx",
                table: "Incarichi",
                column: "Cognome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Almanacco_Foto");

            migrationBuilder.DropTable(
                name: "CoroPolifonicoMaterMisericordie_Foto");

            migrationBuilder.DropTable(
                name: "Img_For_Carousel");

            migrationBuilder.DropTable(
                name: "Incarichi");

            migrationBuilder.DropTable(
                name: "Almanacco");

            migrationBuilder.DropTable(
                name: "CoroPolifonicoMaterMisericordie");
        }
    }
}
