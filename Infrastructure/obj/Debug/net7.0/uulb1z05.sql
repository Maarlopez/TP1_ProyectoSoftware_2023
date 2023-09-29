IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Generos] (
    [GeneroId] int NOT NULL IDENTITY,
    [Nombre] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Generos] PRIMARY KEY ([GeneroId])
);
GO

CREATE TABLE [Salas] (
    [SalaId] int NOT NULL IDENTITY,
    [Nombre] nvarchar(50) NOT NULL,
    [Capacidad] int NOT NULL,
    CONSTRAINT [PK_Salas] PRIMARY KEY ([SalaId])
);
GO

CREATE TABLE [Peliculas] (
    [PeliculaId] int NOT NULL IDENTITY,
    [Titulo] nvarchar(50) NOT NULL,
    [Sinopsis] nvarchar(255) NOT NULL,
    [Poster] nvarchar(100) NOT NULL,
    [Trailer] nvarchar(100) NOT NULL,
    [GeneroId] int NOT NULL,
    CONSTRAINT [PK_Peliculas] PRIMARY KEY ([PeliculaId]),
    CONSTRAINT [FK_Peliculas_Generos_GeneroId] FOREIGN KEY ([GeneroId]) REFERENCES [Generos] ([GeneroId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Funciones] (
    [FuncionId] int NOT NULL IDENTITY,
    [PeliculaId] int NOT NULL,
    [SalaId] int NOT NULL,
    [Fecha] datetime2 NOT NULL,
    [Horario] time NOT NULL,
    CONSTRAINT [PK_Funciones] PRIMARY KEY ([FuncionId]),
    CONSTRAINT [FK_Funciones_Peliculas_PeliculaId] FOREIGN KEY ([PeliculaId]) REFERENCES [Peliculas] ([PeliculaId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Funciones_Salas_SalaId] FOREIGN KEY ([SalaId]) REFERENCES [Salas] ([SalaId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Tickets] (
    [TicketId] uniqueidentifier NOT NULL,
    [FuncionId] int NOT NULL,
    [Usuario] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Tickets] PRIMARY KEY ([TicketId]),
    CONSTRAINT [FK_Tickets_Funciones_FuncionId] FOREIGN KEY ([FuncionId]) REFERENCES [Funciones] ([FuncionId]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GeneroId', N'Nombre') AND [object_id] = OBJECT_ID(N'[Generos]'))
    SET IDENTITY_INSERT [Generos] ON;
INSERT INTO [Generos] ([GeneroId], [Nombre])
VALUES (1, N'Acción'),
(2, N'Aventuras'),
(3, N'Ciencia Ficción'),
(4, N'Comedia'),
(5, N'Documental'),
(6, N'Drama'),
(7, N'Fantasia'),
(8, N'Musical'),
(9, N'Suspense'),
(10, N'Terror');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GeneroId', N'Nombre') AND [object_id] = OBJECT_ID(N'[Generos]'))
    SET IDENTITY_INSERT [Generos] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SalaId', N'Capacidad', N'Nombre') AND [object_id] = OBJECT_ID(N'[Salas]'))
    SET IDENTITY_INSERT [Salas] ON;
INSERT INTO [Salas] ([SalaId], [Capacidad], [Nombre])
VALUES (1, 5, N'2D'),
(2, 15, N'3D'),
(3, 35, N'4D');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SalaId', N'Capacidad', N'Nombre') AND [object_id] = OBJECT_ID(N'[Salas]'))
    SET IDENTITY_INSERT [Salas] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PeliculaId', N'GeneroId', N'Poster', N'Sinopsis', N'Titulo', N'Trailer') AND [object_id] = OBJECT_ID(N'[Peliculas]'))
    SET IDENTITY_INSERT [Peliculas] ON;
INSERT INTO [Peliculas] ([PeliculaId], [GeneroId], [Poster], [Sinopsis], [Titulo], [Trailer])
VALUES (1, 4, N'https://i.imgur.com/Qdd6klb.jpg', N'El cirujano Danny decide contratar a su ayudante Katherine, una madre soltera con hijos, para que finjan ser su familia. Su intención es demostrarle a Palmer que su amor por ella es tan grande que está a punto de divorciarse de su mujer.', N'Una esposa de mentira', N'https://www.youtube.com/watch?v=xuZnmYjAKww'),
(2, 3, N'https://i.imgur.com/xz1PBy0.jpg', N'En un futuro prohibido tener más de un hijo, seis hermanas fingen ser la misma para escapar del Gobierno y buscar a la séptima hermana desaparecida.', N'¿Qué le pasó a Lunes?', N'https://www.youtube.com/watch?v=sRIYLZJqbEY'),
(3, 1, N'https://i.imgur.com/gWGCYdr.jpg', N'Un exagente de la DEA regresa a la acción para salvar a su hija y a su nueva ciudad de un psicópata que vende drogas.', N'Línea de fuego', N'https://www.youtube.com/watch?v=zHrnF621ggQ'),
(4, 5, N'https://i.imgur.com/Q2eeSOC.png', N'Un documental sobre la ciberseguridad y se sumerge en uno de los mayores ciberataques, el objetivo era apropiarse de casi mil millones de dólares. En 2016, el Banco Central de Bangladés sufrió un robo de 81 millones de dólares.', N'El robo de mil millones de dolares', N'https://www.youtube.com/watch?v=4zv56MUXgw8'),
(5, 4, N'https://i.imgur.com/VJGTWKG.jpg', N'Cady Heron ha sido criada en la selva africana. Sus padres zoólogos intentaron educarla en las leyes de la naturaleza, pero al cumplir quince años, debe ir al instituto y adaptarse a su nueva vida en Illinois.', N'Mean Girls', N'https://www.youtube.com/watch?v=oDU84nmSDZY'),
(6, 10, N'https://i.imgur.com/HKpufD5.jpg', N'M3GAN es una maravilla de la inteligencia artificial, una muñeca realista diseñada para ser la mejor compañera de los niños. Puede escuchar, observar y aprender, convirtiéndose en amiga, profesora, compañera de juegos y protectora.', N'M3GAN', N'https://www.youtube.com/watch?v=1vTbhymPSA4'),
(7, 1, N'https://i.imgur.com/tGJwzGc.jpg', N'Los héroes de Marvel deben reunirse para deshacer las acciones de Thanos y restaurar el equilibrio en el universo.', N'Avengers: Endgame', N'https://www.youtube.com/watch?v=PyakRSni-c0'),
(8, 9, N'https://i.imgur.com/rRAMOZR.jpg', N'Un cómico fallido se sumerge en la locura y se convierte en el infame villano Joker en Gotham City.', N'Joker', N'https://www.youtube.com/watch?v=zAGVQLHvwOY'),
(9, 7, N'https://i.imgur.com/U6aTYJU.jpg', N'Los juguetes de Andy, encabezados por Woody el vaquero, cobran vida cuando nadie está mirando. Pero cuando un nuevo juguete, Buzz Lightyear, llega al grupo, Woody se siente amenazado y se embarcan en una aventura para regresar a casa.', N'Toy Story', N'https://www.youtube.com/watch?v=v-PjgYDrg70'),
(10, 6, N'https://i.imgur.com/333XNA8.jpg', N'Un banquero es condenado por un asesinato que no cometió y pasa décadas en la prisión de Shawshank, donde forma una amistad única con otro recluso.', N'The Shawshank Redemption', N'https://www.youtube.com/watch?v=6hB3S9bIaco'),
(11, 8, N'https://i.imgur.com/Yo52ZWJ.jpg', N'Un pianista y una actriz en ciernes se enamoran en Los Ángeles mientras persiguen sus sueños artísticos.', N'La La Land', N'https://www.youtube.com/watch?v=0pdqf4P9MB8'),
(12, 5, N'https://i.imgur.com/ACXFboh.jpg', N'Este documental revela la verdad detrás de la cautividad de las orcas en parques acuáticos y la peligrosidad de mantenerlas en cautiverio.', N'Blackfish', N'https://www.youtube.com/watch?v=fLOeH-Oq_1Y'),
(13, 2, N'https://i.imgur.com/zFvBmAK.jpg', N'El intrépido arqueólogo Indiana Jones se embarca en una búsqueda para encontrar el Santo Grial antes que los nazis.', N'Indiana Jones and the Last Crusade', N'https://www.youtube.com/watch?v=a6JB2suJYHM'),
(14, 2, N'https://i.imgur.com/1oE0Ud8.jpg', N'Un científico descubre una forma de viajar al centro de la Tierra y se embarca en una emocionante aventura subterránea junto a su sobrino y un guía local.', N'Viaje al centro de la tierra', N'https://www.youtube.com/watch?v=ytDcArTr0CI'),
(15, 2, N'https://i.imgur.com/xwJNL4Z.jpg', N'Un grupo de personas queda atrapado en una isla donde se han creado dinosaurios clonados y deben luchar por su supervivencia.', N'Jurassic Park', N'https://www.youtube.com/watch?v=lc0UehYemQA'),
(16, 10, N'https://i.imgur.com/vG8P2jY.jpg', N'Basada en hechos reales, una familia experimenta fenómenos paranormales en su nueva casa y busca ayuda de investigadores paranormales.', N'El Conjuro', N'https://www.youtube.com/watch?v=k10ETZ41q5o'),
(17, 9, N'https://i.imgur.com/lYGE3LJ.jpg', N'Un hombre se convierte en el principal sospechoso cuando su esposa desaparece y las evidencias sugieren que él podría estar involucrado.', N'Gone Girl', N'https://www.youtube.com/watch?v=2-_-1nJf8Vg'),
(18, 8, N'https://i.imgur.com/ZrRH8z5.jpg', N'La historia de personajes cuyas vidas se entrelazan en la Francia del siglo XIX, luchando por la justicia y la redención.', N'Les Misérables', N'https://www.youtube.com/watch?v=IuEFm84s4oI'),
(19, 5, N'https://i.imgur.com/xRZC4Ji.jpg', N'Este documental sigue la epopeya de los pingüinos emperadores mientras luchan por sobrevivir y criar a sus crías en la Antártida.', N'March of the Penguins', N'https://www.youtube.com/watch?v=ohL8rF_jluA'),
(20, 6, N'https://i.imgur.com/LeQf3gP.jpg', N'Matt Turner, un hombre de negocios estadounidense que vive en Berlín y que, en el transcurso de un día, se encuentra en una carrera contrarreloj para salvar a su familia y su propia vida. Cuando lleva a sus hijos a la escuela, Matt recibe una llamada telefónica, una voz misteriosa le advierte que su vehículo está lleno de explosivos. Para proteger a su familia y resolver el misterio, debe seguir las instrucciones del extraño y realizar una serie de tareas a gran velocidad.', N'Contrarreloj', N'https://www.youtube.com/watch?v=T7HlHI4S-MY');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PeliculaId', N'GeneroId', N'Poster', N'Sinopsis', N'Titulo', N'Trailer') AND [object_id] = OBJECT_ID(N'[Peliculas]'))
    SET IDENTITY_INSERT [Peliculas] OFF;
GO

CREATE INDEX [IX_Funciones_PeliculaId] ON [Funciones] ([PeliculaId]);
GO

CREATE INDEX [IX_Funciones_SalaId] ON [Funciones] ([SalaId]);
GO

CREATE INDEX [IX_Peliculas_GeneroId] ON [Peliculas] ([GeneroId]);
GO

CREATE INDEX [IX_Tickets_FuncionId] ON [Tickets] ([FuncionId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230913225646_init', N'7.0.10');
GO

COMMIT;
GO

