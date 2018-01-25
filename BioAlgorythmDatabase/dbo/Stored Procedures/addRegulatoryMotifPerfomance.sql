


CREATE PROCEDURE [dbo].[addRegulatoryMotifPerfomance]
    @RegulatoryMotifPerfomances dbo.[RegulatoryMotifPerfomanceType] READONLY,
    @RegulatoryMotifOptimalValueChanges dbo.[RegulatoryMotifOptimalValueChangeType] READONLY,
    @RegulatoryMotifSolutions dbo.[RegulatoryMotifSolutionType] READONLY
AS    
BEGIN
	
			
	INSERT [RegulatoryMotifPerfomance] ( [Size], [NumberOfSequence],	[SequenceLengthes], [MotifLength],
	[InputData], [OutputPresentation], [Algorithm], [NumberOfIteration], [Duration], [DurationMilliSeconds],
	[DateComplete], [IsComplete], [LastRoute], [OptimalRoute], [OptimalValue], [StartPosition],
	[Motif], [CountTerminal], [UpdateOptcount], [ElemenationCount], [IsOptimizitaion], [IsSumAsCriteria], [IsAllResult], [AcceptibleDistance])
	SELECT [Size], [NumberOfSequence],	[SequenceLengthes], [MotifLength],
	[InputData], [OutputPresentation], [Algorithm], [NumberOfIteration], [Duration], [DurationMilliSeconds],
	[DateComplete], [IsComplete], [LastRoute], [OptimalRoute], [OptimalValue], [StartPosition],
	[Motif], [CountTerminal], [UpdateOptcount], [ElemenationCount], [IsOptimizitaion], [IsSumAsCriteria], [IsAllResult], [AcceptibleDistance]
	FROM @RegulatoryMotifPerfomances d; 


	INSERT INTO RegulatoryMotifSolution ([RegulatoryMotifPerfomanceId], [StartPosition], [Motif])	
	SELECT ap.[RegulatoryMotifPerfomanceId], avc.[StartPosition], avc.[Motif]
	FROM @RegulatoryMotifSolutions avc
	INNER JOIN [RegulatoryMotifPerfomance] ap
	ON avc.[Algorithm] = ap.[Algorithm] AND
	   avc.[SequenceLengthes] = ap.[SequenceLengthes] AND
	   avc.[MotifLength] = ap.[MotifLength] AND
	   avc.[IsOptimizitaion] = ap.[IsOptimizitaion] AND
	   avc.[IsSumAsCriteria] = ap.[IsSumAsCriteria] AND
	   avc.[IsAllResult] = ap.[IsAllResult] AND
	   avc.[AcceptibleDistance] = ap.[AcceptibleDistance] AND
	   avc.[InputData] = ap.[InputData]


	INSERT INTO RegulatoryMotifOptimalValueChange ([RegulatoryMotifPerfomanceId], [NumberOfIteration], [Duration], [DurationMilliSeconds], [OptimalValue], [StartPosition], [Motif])	
	SELECT ap.[RegulatoryMotifPerfomanceId], avc.[NumberOfIteration], avc.[Duration], avc.[DurationMilliSeconds], avc.[OptimalValue], avc.[StartPosition], avc.[Motif]
	FROM @RegulatoryMotifOptimalValueChanges avc
	INNER JOIN [RegulatoryMotifPerfomance] ap	ON avc.[Algorithm] = ap.[Algorithm] AND
	   avc.[SequenceLengthes] = ap.[SequenceLengthes] AND
	   avc.[MotifLength] = ap.[MotifLength] AND
	   avc.[IsOptimizitaion] = ap.[IsOptimizitaion] AND
	   avc.[IsSumAsCriteria] = ap.[IsSumAsCriteria] AND
	   avc.[IsAllResult] = ap.[IsAllResult] AND
	   avc.[AcceptibleDistance] = ap.[AcceptibleDistance] AND
	   avc.[InputData] = ap.[InputData]

END