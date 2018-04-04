



CREATE PROCEDURE [dbo].[deleteStringExactPerfomance]
    @Algorithm VARCHAR(200),
	@TextSize INT = NULL,
	@AlphabetSize INT = NULL
AS    
BEGIN
	
	IF (@TextSize IS NULL AND @AlphabetSize IS NULL)	
	BEGIN	
		DELETE FROM [StringExactPerfomance] 
		WHERE [Algorithm] = @Algorithm;
	END
	IF (@TextSize IS NOT NULL AND @AlphabetSize IS NOT NULL)	
	BEGIN
		DELETE FROM [StringExactPerfomance] 
		WHERE  [TextSize] = @TextSize AND [Algorithm] = @Algorithm;
    END

END

/*
EXEC [dbo].[deleteFindPatternPerfomance] @Algorithm = 'NNN'
*/