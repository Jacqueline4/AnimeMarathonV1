CREATE FUNCTION dbo.GenerateCompositeId (@AnimeId INT, @GeneroId INT)
RETURNS INT
AS
BEGIN
    DECLARE @IdString NVARCHAR(20)
    SET @IdString = '1' + RIGHT('000' + CAST(@AnimeId AS NVARCHAR(4)), 4) + RIGHT('000' + CAST(@GeneroId AS NVARCHAR(4)), 4)

    RETURN CAST(@IdString AS INT)
END;
