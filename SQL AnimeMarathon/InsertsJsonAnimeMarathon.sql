use ANIMEMARATHON_DB;
DECLARE @json NVARCHAR(MAX);

SELECT @json = BulkColumn
FROM OPENROWSET(BULK 'C:\Users\jacqueline\OneDrive\Escritorio\genres.json', SINGLE_CLOB) AS j;

INSERT INTO Generos (Nombre)
SELECT 
    [name]
FROM OPENJSON(@json, '$.data')
WITH (
    [name] VARCHAR(100) '$.attributes.name'
   
);

SELECT @json = BulkColumn
FROM OPENROWSET(BULK 'C:\Users\jacqueline\OneDrive\Escritorio\categories.json', SINGLE_CLOB) AS j;

INSERT INTO Categorias (Nombre)
SELECT 
    [title]
FROM OPENJSON(@json, '$.data')
WITH (
   [title] VARCHAR(100) '$.attributes.title'
	
);

SELECT @json = BulkColumn
FROM OPENROWSET(BULK 'C:\Users\jacqueline\OneDrive\Escritorio\animes1-300.json', SINGLE_CLOB) AS j;

INSERT INTO Animes(Nombre,Estado_emision,Subtipo,AgeRating,Valoracion_media,Fecha_publicacion,Fecha_final,Descripcion,Total_episodios, PosterUrl)
SELECT 
    ISNULL([canonicalTitle], [slug]) AS Nombre,
	[status],
	[subtype],
	[ageRating],
	CAST([averageRating] AS DECIMAL(10, 2)), 
    CONVERT(DATE, [startDate]),
    CONVERT(DATE, [endDate]), 
	[description],
	[episodeCount],
	[posterImage]
FROM OPENJSON(@json)
WITH (
    [canonicalTitle] VARCHAR(max) '$.attributes.canonicalTitle',
	[slug] VARCHAR(MAX) '$.attributes.slug', 
	[status] VARCHAR(max) '$.attributes.status',
	[subtype] VARCHAR(MAX) '$.attributes.subtype',
	[ageRating] VARCHAR(max) '$.attributes.ageRating',
	[averageRating] decimal '$.attributes.averageRating',
	[startDate] DATE '$.attributes.startDate',
	[endDate] DATE '$.attributes.endDate',
	[description] VARCHAR(max) '$.attributes.description',
	[episodeCount] int  '$.attributes.episodeCount',
	[posterImage]  VARCHAR(max) '$.attributes.posterImage.small'
   
);
UPDATE [dbo].[Animes]
   SET 
      [Fecha_final] = '9999-12-31'
     
      ,[Total_episodios] = 99999
      
 WHERE [Total_episodios] is null
GO