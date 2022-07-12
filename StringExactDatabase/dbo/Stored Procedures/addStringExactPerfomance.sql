




CREATE PROCEDURE [dbo].[addStringExactPerfomance]
    @StringExactPerfomanceType dbo.[StringExactPerfomanceType] READONLY
AS    
BEGIN
	

			
	INSERT [SuffixTreePerfomance] ( [Algorithm], [TextSize], AlphabetSize, [Text], [OutputPresentation], [NumberOfIteration],
			[NumberOfComparison], [Duration], [DurationMilliSeconds], [DateComplete], [AdditionalInfo])
	SELECT 	[Algorithm], [TextSize], AlphabetSize, [Text], [OutputPresentation], [NumberOfIteration],
			[NumberOfComparison], [Duration], [DurationMilliSeconds], [DateComplete], [AdditionalInfo]
	FROM @StringExactPerfomanceType;


END