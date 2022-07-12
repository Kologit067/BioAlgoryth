




CREATE PROCEDURE [dbo].[addTravellingSalesmanPerfomance]
    @TravellingSalesmanPerfomanceType dbo.[TravellingSalesmanPerfomanceType] READONLY
AS    
BEGIN
	

			
	INSERT [TravellingSalesmanPerfomance] ( [Algorithm], [InputSize], [Limit], [Step],	[InputPresentation], [OutputPresentation], [NumberOfIteration],
			[NumberOfComparison], [Duration], [DurationMilliSeconds], [DateComplete], [AdditionalInfo])
	SELECT 	[Algorithm], [InputSize], [Limit], [Step],	[InputPresentation], [OutputPresentation], [NumberOfIteration],
			[NumberOfComparison], [Duration], [DurationMilliSeconds], [DateComplete], [AdditionalInfo]
	FROM @TravellingSalesmanPerfomanceType;


END