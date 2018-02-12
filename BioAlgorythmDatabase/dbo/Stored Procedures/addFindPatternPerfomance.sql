
CREATE PROCEDURE [dbo].[addFindPatternPerfomance]
    @FindPatternPerfomanceType dbo.[FindPatternPerfomanceType] READONLY
AS    
BEGIN
	

			
	INSERT [FindPatternPerfomance] ( [Algorithm], [TextSize], [PatternSize], [Text], [Pattern], 	[OutputPresentation], [NumberOfIteration],
			[Duration], [DurationMilliSeconds], [DateComplete])
	SELECT 	[Algorithm], [TextSize], [PatternSize], [Text], [Pattern], 	[OutputPresentation], [NumberOfIteration],
			[Duration], [DurationMilliSeconds], [DateComplete]
	FROM @FindPatternPerfomanceType;


END