
CREATE PROCEDURE [dbo].[addDNAMappingPerfomance]
    @DNAMappingPerfomances dbo.[DNAMappingPerfomanceType] READONLY,
    @DNAMappingSolutions dbo.[DNAMappingSolutionType] READONLY
AS    
BEGIN
	
	IF OBJECT_ID('tempdb..#AlgorithmPerfomance') IS NULL
		CREATE TABLE #AlgorithmPerfomance(
	        [NumberInArray] [int] NULL,
			[AlgorithmPerfomanceId] INT
			);
			
	INSERT [DNAMappingPerfomance] ( [Size], [InputData],	[OutputPresentation], [Algorithm],
	[NumberOfIteration], [Duration], [DurationMilliSeconds], [DateComplete], [IsComplete], [LastRoute],
	[OptimalRoute], [CountTerminal], [UpdateOptcount], [ElemenationCount], [IsAllResult])
	OUTPUT INSERTED.[DNAMappingPerfomanceId] INTO #AlgorithmPerfomance([AlgorithmPerfomanceId])
	SELECT [Size], [InputData],	[OutputPresentation], [Algorithm],
	[NumberOfIteration], [Duration], [DurationMilliSeconds], [DateComplete], [IsComplete], [LastRoute],
	[OptimalRoute], [CountTerminal], [UpdateOptcount], [ElemenationCount], [IsAllResult]
	FROM @DNAMappingPerfomances d; 

	WITH CTE AS
	(
	SELECT ROW_NUMBER() OVER(ORDER BY [AlgorithmPerfomanceId]) rownumber, [AlgorithmPerfomanceId]
	FROM #AlgorithmPerfomance
	)
	UPDATE ap
	SET ap.[NumberInArray] = cte.[NumberInArray]
	FROM #AlgorithmPerfomance ap
	INNER JOIN  cte
	ON ap.[AlgorithmPerfomanceId] = cte.[AlgorithmPerfomanceId]


	INSERT INTO [DNAMappingSolution] ([DNAMappingPerfomanceId], [OutputPresentation])	
	SELECT ap.[AlgorithmPerfomanceId], avc.[OutputPresentation]
	FROM @DNAMappingSolutions avc
	INNER JOIN #AlgorithmPerfomance ap
	ON avc.[NumberInArray] = ap.[NumberInArray]

END