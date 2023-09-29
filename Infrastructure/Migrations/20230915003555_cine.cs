﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    GeneroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.GeneroId);
                });

            migrationBuilder.CreateTable(
                name: "Salas",
                columns: table => new
                {
                    SalaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Capacidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salas", x => x.SalaId);
                });

            migrationBuilder.CreateTable(
                name: "Peliculas",
                columns: table => new
                {
                    PeliculaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sinopsis = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Poster = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Trailer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GeneroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peliculas", x => x.PeliculaId);
                    table.ForeignKey(
                        name: "FK_Peliculas_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "GeneroId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Funciones",
                columns: table => new
                {
                    FuncionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeliculaId = table.Column<int>(type: "int", nullable: false),
                    SalaId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Horario = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funciones", x => x.FuncionId);
                    table.ForeignKey(
                        name: "FK_Funciones_Peliculas_PeliculaId",
                        column: x => x.PeliculaId,
                        principalTable: "Peliculas",
                        principalColumn: "PeliculaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funciones_Salas_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salas",
                        principalColumn: "SalaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FuncionId = table.Column<int>(type: "int", nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Funciones_FuncionId",
                        column: x => x.FuncionId,
                        principalTable: "Funciones",
                        principalColumn: "FuncionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "GeneroId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Acción" },
                    { 2, "Aventuras" },
                    { 3, "Ciencia Ficción" },
                    { 4, "Comedia" },
                    { 5, "Documental" },
                    { 6, "Drama" },
                    { 7, "Fantasia" },
                    { 8, "Musical" },
                    { 9, "Suspense" },
                    { 10, "Terror" }
                });

            migrationBuilder.InsertData(
                table: "Salas",
                columns: new[] { "SalaId", "Capacidad", "Nombre" },
                values: new object[,]
                {
                    { 1, 5, "2D" },
                    { 2, 15, "3D" },
                    { 3, 35, "4D" }
                });

            migrationBuilder.InsertData(
                table: "Peliculas",
                columns: new[] { "PeliculaId", "GeneroId", "Poster", "Sinopsis", "Titulo", "Trailer" },
                values: new object[,]
                {
                    { 1, 4, "https://i.imgur.com/Qdd6klb.jpg", "El cirujano Danny decide contratar a su ayudante Katherine, una madre soltera con hijos, para que finjan ser su familia. Su intención es demostrarle a Palmer que su amor por ella es tan grande que está a punto de divorciarse de su mujer.", "Una esposa de mentira", "https://www.youtube.com/watch?v=xuZnmYjAKww" },
                    { 2, 3, "https://i.imgur.com/xz1PBy0.jpg", "En un futuro prohibido tener más de un hijo, seis hermanas fingen ser la misma para escapar del Gobierno y buscar a la séptima hermana desaparecida.", "¿Qué le pasó a Lunes?", "https://www.youtube.com/watch?v=sRIYLZJqbEY" },
                    { 3, 1, "https://i.imgur.com/gWGCYdr.jpg", "Un exagente de la DEA regresa a la acción para salvar a su hija y a su nueva ciudad de un psicópata que vende drogas.", "Línea de fuego", "https://www.youtube.com/watch?v=zHrnF621ggQ" },
                    { 4, 5, "https://i.imgur.com/Q2eeSOC.png", "Un documental sobre la ciberseguridad y se sumerge en uno de los mayores ciberataques, el objetivo era apropiarse de casi mil millones de dólares. En 2016, el Banco Central de Bangladés sufrió un robo de 81 millones de dólares.", "El robo de mil millones de dolares", "https://www.youtube.com/watch?v=4zv56MUXgw8" },
                    { 5, 4, "https://i.imgur.com/VJGTWKG.jpg", "Cady Heron ha sido criada en la selva africana. Sus padres zoólogos intentaron educarla en las leyes de la naturaleza, pero al cumplir quince años, debe ir al instituto y adaptarse a su nueva vida en Illinois.", "Mean Girls", "https://www.youtube.com/watch?v=oDU84nmSDZY" },
                    { 6, 10, "https://i.imgur.com/HKpufD5.jpg", "M3GAN es una maravilla de la inteligencia artificial, una muñeca realista diseñada para ser la mejor compañera de los niños. Puede escuchar, observar y aprender, convirtiéndose en amiga, profesora, compañera de juegos y protectora.", "M3GAN", "https://www.youtube.com/watch?v=1vTbhymPSA4" },
                    { 7, 1, "https://i.imgur.com/tGJwzGc.jpg", "Los héroes de Marvel deben reunirse para deshacer las acciones de Thanos y restaurar el equilibrio en el universo.", "Avengers: Endgame", "https://www.youtube.com/watch?v=PyakRSni-c0" },
                    { 8, 9, "https://i.imgur.com/rRAMOZR.jpg", "Un cómico fallido se sumerge en la locura y se convierte en el infame villano Joker en Gotham City.", "Joker", "https://www.youtube.com/watch?v=zAGVQLHvwOY" },
                    { 9, 7, "https://i.imgur.com/U6aTYJU.jpg", "Los juguetes de Andy, encabezados por Woody el vaquero, cobran vida cuando nadie está mirando. Pero cuando un nuevo juguete, Buzz Lightyear, llega al grupo, Woody se siente amenazado y se embarcan en una aventura para regresar a casa.", "Toy Story", "https://www.youtube.com/watch?v=v-PjgYDrg70" },
                    { 10, 6, "https://i.imgur.com/333XNA8.jpg", "Un banquero es condenado por un asesinato que no cometió y pasa décadas en la prisión de Shawshank, donde forma una amistad única con otro recluso.", "The Shawshank Redemption", "https://www.youtube.com/watch?v=6hB3S9bIaco" },
                    { 11, 8, "https://i.imgur.com/Yo52ZWJ.jpg", "Un pianista y una actriz en ciernes se enamoran en Los Ángeles mientras persiguen sus sueños artísticos.", "La La Land", "https://www.youtube.com/watch?v=0pdqf4P9MB8" },
                    { 12, 5, "https://i.imgur.com/ACXFboh.jpg", "Este documental revela la verdad detrás de la cautividad de las orcas en parques acuáticos y la peligrosidad de mantenerlas en cautiverio.", "Blackfish", "https://www.youtube.com/watch?v=fLOeH-Oq_1Y" },
                    { 13, 2, "https://i.imgur.com/zFvBmAK.jpg", "El intrépido arqueólogo Indiana Jones se embarca en una búsqueda para encontrar el Santo Grial antes que los nazis.", "Indiana Jones and the Last Crusade", "https://www.youtube.com/watch?v=a6JB2suJYHM" },
                    { 14, 2, "https://i.imgur.com/1oE0Ud8.jpg", "Un científico descubre una forma de viajar al centro de la Tierra y se embarca en una emocionante aventura subterránea junto a su sobrino y un guía local.", "Viaje al centro de la tierra", "https://www.youtube.com/watch?v=ytDcArTr0CI" },
                    { 15, 2, "https://i.imgur.com/xwJNL4Z.jpg", "Un grupo de personas queda atrapado en una isla donde se han creado dinosaurios clonados y deben luchar por su supervivencia.", "Jurassic Park", "https://www.youtube.com/watch?v=lc0UehYemQA" },
                    { 16, 10, "https://i.imgur.com/vG8P2jY.jpg", "Basada en hechos reales, una familia experimenta fenómenos paranormales en su nueva casa y busca ayuda de investigadores paranormales.", "El Conjuro", "https://www.youtube.com/watch?v=k10ETZ41q5o" },
                    { 17, 9, "https://i.imgur.com/lYGE3LJ.jpg", "Un hombre se convierte en el principal sospechoso cuando su esposa desaparece y las evidencias sugieren que él podría estar involucrado.", "Gone Girl", "https://www.youtube.com/watch?v=2-_-1nJf8Vg" },
                    { 18, 8, "https://i.imgur.com/ZrRH8z5.jpg", "La historia de personajes cuyas vidas se entrelazan en la Francia del siglo XIX, luchando por la justicia y la redención.", "Les Misérables", "https://www.youtube.com/watch?v=IuEFm84s4oI" },
                    { 19, 5, "https://i.imgur.com/xRZC4Ji.jpg", "Este documental sigue la epopeya de los pingüinos emperadores mientras luchan por sobrevivir y criar a sus crías en la Antártida.", "March of the Penguins", "https://www.youtube.com/watch?v=ohL8rF_jluA" },
                    { 20, 6, "https://i.imgur.com/LeQf3gP.jpg", "Matt Turner, un hombre de negocios estadounidense que vive en Berlín y que se encuentra en una carrera contrarreloj para salvar a su familia y su propia vida.", "Contrarreloj", "https://www.youtube.com/watch?v=T7HlHI4S-MY" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funciones_PeliculaId",
                table: "Funciones",
                column: "PeliculaId");

            migrationBuilder.CreateIndex(
                name: "IX_Funciones_SalaId",
                table: "Funciones",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_GeneroId",
                table: "Peliculas",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FuncionId",
                table: "Tickets",
                column: "FuncionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Funciones");

            migrationBuilder.DropTable(
                name: "Peliculas");

            migrationBuilder.DropTable(
                name: "Salas");

            migrationBuilder.DropTable(
                name: "Generos");
        }
    }
}
