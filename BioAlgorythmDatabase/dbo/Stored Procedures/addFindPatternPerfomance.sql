



CREATE PROCEDURE [dbo].[addFindPatternPerfomance]
    @FindPatternPerfomanceType dbo.[FindPatternPerfomanceType] READONLY
AS    
BEGIN
	

			
	INSERT [FindPatternPerfomance] ( [Algorithm], [TextSize], [PatternSize], AlphabetSize, [Text], [Pattern], 	[OutputPresentation], [NumberOfIteration],
			[NumberOfComparison], [Duration], [DurationMilliSeconds], [DateComplete], [AdditionalInfo])
	SELECT 	[Algorithm], [TextSize], [PatternSize], AlphabetSize, [Text], [Pattern], 	[OutputPresentation], [NumberOfIteration],
			[NumberOfComparison], [Duration], [DurationMilliSeconds], [DateComplete], [AdditionalInfo]
	FROM @FindPatternPerfomanceType;


END