


CREATE PROCEDURE [dbo].[deleteFindPatternPerfomance]
    @Algorithm VARCHAR(200),
	@TextSize INT = NULL,
	@PatternSize INT = NULL,
	@AlphabetSize INT = NULL
AS    
BEGIN
	
	IF (@TextSize IS NULL AND @PatternSize IS NULL AND @AlphabetSize IS NULL)	
	BEGIN	
		DELETE FROM [FindPatternPerfomance] 
		WHERE [Algorithm] = @Algorithm;
	END
	IF (@TextSize IS NOT NULL AND @PatternSize IS NOT NULL  AND @AlphabetSize IS NOT NULL)	
	BEGIN
		DELETE FROM [FindPatternPerfomance] 
		WHERE  [TextSize] = @TextSize AND PatternSize = @PatternSize AND [Algorithm] = @Algorithm;
    END

END

/*
EXEC [dbo].[deleteFindPatternPerfomance] @Algorithm = 'NNN'
*/