CREATE PROCEDURE ActualizarValoracionMediaAnime
AS
BEGIN
    UPDATE Animes
    SET Valoracion_media = (
        SELECT AVG(Valoracion)
        FROM Valoraciones
        WHERE AnimeId = Animes.Id
    )
END;


CREATE TRIGGER Trigger_ActualizarValoracionMediaAnime
ON Valoraciones
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    EXEC ActualizarValoracionMediaAnime;
END;