-- Declarar una variable para almacenar el JSON
DECLARE @json NVARCHAR(MAX);

-- Leer el contenido del archivo JSON en la variable @json
SELECT @json = BulkColumn
FROM OPENROWSET(BULK 'C:\Users\jacqueline\OneDrive\Escritorio\AnimeGenero.json', SINGLE_CLOB) AS j;

-- Insertar datos en la tabla AnimesGeneros
INSERT INTO AnimesGeneros (Id, AnimeId, GeneroId)
SELECT
    dbo.GenerateCompositeId(AnimeId, GeneroId) as Id,
    AnimeId,
    GeneroId
FROM OPENJSON(@json)
WITH (
    AnimeId int '$.anime_id',
    GeneroId int '$.genero_id'
);

--------------------aqui AnimeCategory------------
SELECT @json = BulkColumn
FROM OPENROWSET(BULK 'C:\Users\jacqueline\OneDrive\Escritorio\AnimeCategory.json', SINGLE_CLOB) AS j;


INSERT INTO AnimesCategorias (Id, AnimeId, CategoriaId)
SELECT
    dbo.GenerateCompositeId(AnimeId, CategoriaId) as Id,
    AnimeId,
    CategoriaId
FROM OPENJSON(@json)
WITH (
    AnimeId int '$.anime_id',
    CategoriaId int '$.category_id'
);