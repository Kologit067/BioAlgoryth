


CREATE PROCEDURE [dbo].[addFindPatternPerfomance]
    @FindPatternPerfomanceType dbo.[FindPatternPerfomanceType] READONLY
AS    
BEGIN
	

			
	INSERT [FindPatternPerfomance] ( [Algorithm], [TextSize], [PatternSize], AlphabetSize, [Text], [Pattern], 	[OutputPresentation], [NumberOfIteration],
			[NumberOfComparison], [Duration], [DurationMilliSeconds], [DateComplete])
	SELECT 	[Algorithm], [TextSize], [PatternSize], AlphabetSize, [Text], [Pattern], 	[OutputPresentation], [NumberOfIteration],
			[NumberOfComparison], [Duration], [DurationMilliSeconds], [DateComplete]
	FROM @FindPatternPerfomanceType;


END