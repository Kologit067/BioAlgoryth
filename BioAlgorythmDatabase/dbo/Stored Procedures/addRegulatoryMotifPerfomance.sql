

CREATE PROCEDURE [dbo].[addRegulatoryMotifPerfomance]
    @RegulatoryMotifPerfomances dbo.[RegulatoryMotifPerfomanceType] READONLY,
    @RegulatoryMotifOptimalValueChanges dbo.[RegulatoryMotifOptimalValueChangeType] READONLY,
    @RegulatoryMotifSolutions dbo.[RegulatoryMotifSolutionType] READONLY
AS    
BEGIN
	
	--IF OBJECT_ID('tempdb..#AlgorithmPerfomance') IS NULL
	--	CREATE TABLE #AlgorithmPerfomance(
	--        [NumberInArray] [int] NULL,
	--		[AlgorithmPerfomanceId] INT
	--		);
			
	INSERT [RegulatoryMotifPerfomance] ( [Size], [NumberOfSequence],	[SequenceLengthes], [MotifLength],
	[InputData], [OutputPresentation], [Algorithm], [NumberOfIteration], [Duration], [DurationMilliSeconds],
	[DateComplete], [IsComplete], [LastRoute], [OptimalRoute], [OptimalValue], [StartPosition],
	[Motif], [CountTerminal], [UpdateOptcount], [ElemenationCount], [IsOptimizitaion], [IsSumAsCriteria], [IsAllResult])
--	OUTPUT INSERTED.[RegulatoryMotifPerfomanceId] INTO #AlgorithmPerfomance([AlgorithmPerfomanceId])
	SELECT [Size], [NumberOfSequence],	[SequenceLengthes], [MotifLength],
	[InputData], [OutputPresentation], [Algorithm], [NumberOfIteration], [Duration], [DurationMilliSeconds],
	[DateComplete], [IsComplete], [LastRoute], [OptimalRoute], [OptimalValue], [StartPosition],
	[Motif], [CountTerminal], [UpdateOptcount], [ElemenationCount], [IsOptimizitaion], [IsSumAsCriteria], [IsAllResult]
	FROM @RegulatoryMotifPerfomances d; 

	--WITH CTE AS
	--(
	--SELECT ROW_NUMBER() OVER(ORDER BY [AlgorithmPerfomanceId]) rownumber, [AlgorithmPerfomanceId]
	--FROM #AlgorithmPerfomance
	--)
	--UPDATE ap
	--SET ap.[NumberInArray] = cte.[NumberInArray]
	--FROM #AlgorithmPerfomance ap
	--INNER JOIN  cte
	--ON ap.[AlgorithmPerfomanceId] = cte.[AlgorithmPerfomanceId]


	INSERT INTO RegulatoryMotifSolution ([RegulatoryMotifPerfomanceId], [StartPosition], [Motif])	
	SELECT ap.[RegulatoryMotifPerfomanceId], avc.[StartPosition], avc.[Motif]
	FROM @RegulatoryMotifSolutions avc
	INNER JOIN [RegulatoryMotifPerfomance] ap
	ON avc.[Algorithm] = ap.[Algorithm] AND
	   avc.[SequenceLengthes] = ap.[SequenceLengthes] AND
	   avc.[MotifLength] = ap.[MotifLength] AND
	   avc.[InputData] = ap.[InputData]


	INSERT INTO RegulatoryMotifOptimalValueChange ([RegulatoryMotifPerfomanceId], [NumberOfIteration], [Duration], [DurationMilliSeconds], [OptimalValue], [StartPosition], [Motif])	
	SELECT ap.[RegulatoryMotifPerfomanceId], avc.[NumberOfIteration], avc.[Duration], avc.[DurationMilliSeconds], avc.[OptimalValue], avc.[StartPosition], avc.[Motif]
	FROM @RegulatoryMotifOptimalValueChanges avc
	INNER JOIN [RegulatoryMotifPerfomance] ap	ON avc.[Algorithm] = ap.[Algorithm] AND
	   avc.[SequenceLengthes] = ap.[SequenceLengthes] AND
	   avc.[MotifLength] = ap.[MotifLength] AND
	   avc.[InputData] = ap.[InputData]

END