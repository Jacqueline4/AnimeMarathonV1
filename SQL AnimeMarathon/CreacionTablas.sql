/*CREATE TYPE EstadoEmisionEnum AS ENUM ('current', 'finished');
CREATE TYPE SubtipoEnum AS ENUM ('TV', 'movie', 'OVA','special');
CREATE TYPE EstadoAnimeUsuarioEnum AS ENUM ('Viendo', 'Completado', 'Abandonado', 'Para ver');*/

--Creacion de la bbdd
Use ANIMEMARATHON_DB;
CREATE TABLE Generos (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(50) NOT NULL
);

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(50) NOT NULL,
    Apellidos NVARCHAR(50),
    Email NVARCHAR(50) NOT NULL,
    Contraseña NVARCHAR(25),
    CONSTRAINT UQ_Usuarios_Nombre_Unico UNIQUE (Nombre),
    CONSTRAINT UQ_Usuarios_Email_Unico UNIQUE (Email)
);
CREATE TABLE Animes (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL,
    Estado_emision NVARCHAR(25),
    Subtipo NVARCHAR(25),
    AgeRating NVARCHAR(25),
    Fecha_publicacion DATETIME,
    Fecha_final DATETIME,
    Descripcion NVARCHAR(MAX),
    Total_episodios INT,
    Valoracion_media DECIMAL,
    PosterUrl NVARCHAR(MAX)
);
CREATE TABLE Comentarios (
    Id INT PRIMARY KEY IDENTITY,
    Fecha_hora DATETIME,
    Comentario NVARCHAR(MAX),
    UsuarioId INT,
    AnimeId INT,
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (AnimeId) REFERENCES Animes(Id) ON DELETE CASCADE ON UPDATE CASCADE  
);
CREATE TABLE Valoraciones (
    Id INT PRIMARY KEY IDENTITY,
    Valoracion_media_propia DECIMAL,
    UsuarioId INT,
    AnimeId INT,
    FOREIGN KEY (AnimeId) REFERENCES Animes(Id) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE TABLE UsuariosAnimes (
    Id INT PRIMARY KEY,
    UsuarioId INT,
    AnimeId INT,
    Estado  NVARCHAR(25),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (AnimeId) REFERENCES Animes(Id) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE TABLE AnimesGeneros (
    Id INT PRIMARY KEY,
    AnimeId INT,
    GeneroId INT,
    FOREIGN KEY (AnimeId) REFERENCES Animes(Id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (GeneroId) REFERENCES Generos(Id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Categorias (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(50)
);
CREATE TABLE AnimesCategorias (
    Id INT PRIMARY KEY,
    AnimeId INT,
    CategoriaId INT,
    FOREIGN KEY (AnimeId) REFERENCES Animes(Id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (CategoriaId) REFERENCES Categorias(Id) ON DELETE CASCADE ON UPDATE CASCADE
);