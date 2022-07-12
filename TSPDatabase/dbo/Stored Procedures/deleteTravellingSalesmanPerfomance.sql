



CREATE PROCEDURE [dbo].[deleteTravellingSalesmanPerfomance]
    @Algorithm VARCHAR(200),
	@InputSize INT,
	@Limit INT,
	@Step INT
AS    
BEGIN
	
	IF (@InputSize IS NULL AND @Limit IS NULL AND @Step IS NULL)	
	BEGIN	
		DELETE FROM [TravellingSalesmanPerfomance] 
		WHERE [Algorithm] = @Algorithm;
	END
	IF (@InputSize IS NOT NULL AND @Limit IS NOT NULL  AND @Step IS NOT NULL)	
	BEGIN
		DELETE FROM [TravellingSalesmanPerfomance] 
		WHERE  InputSize = @InputSize AND Limit = @Limit AND Step = @Step;
    END

END

/*
EXEC [dbo].[deleteTravellingSalesmanPerfomance] @Algorithm = 'NNN'
*/