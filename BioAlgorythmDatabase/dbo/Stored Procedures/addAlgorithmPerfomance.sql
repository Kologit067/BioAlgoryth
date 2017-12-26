CREATE PROCEDURE [dbo].[addAlgorithmPerfomance]
    @AlgorithmPerfomances dbo.[AlgorithmPerfomanceType] READONLY,
    @AlgorithmOptimalValueChange dbo.[AlgorithmOptimalValueChangeType] READONLY
AS    
BEGIN
	
	IF OBJECT_ID('tempdb..#AlgorithmPerfomanceShort') IS NULL
		CREATE TABLE #AlgorithmPerfomanceShort(
			[AlgorithmPerfomanceId] INT,
			DBName VARCHAR(500),
			[Algorithm] VARCHAR(500),
			TableSetAsNumber INT);
			
	INSERT [AlgorithmPerfomance] ( DBName, [Tables],[Algorithm],[NumberOfIteration],[Duration], [DurationMilliSeconds], 
		[DateComplete], [IsComplete], [ElementCount], TableSetAsNumber, LastRoute,
		[CountTerminal],[UpdateOptcount],[OptimalValue],[ElemenationCount],[OptimalRoute])
	OUTPUT 	INSERTED.[AlgorithmPerfomance], INSERTED.DBName, INSERTED.[Algorithm], INSERTED.TableSetAsNumber INTO #AlgorithmPerfomanceShort
	SELECT DBName, [Tables],[Algorithm],[NumberOfIteration],[Duration],[DurationMilliSeconds], 
		[DateComplete], [IsComplete], [ElementCount], TableSetAsNumber, LastRoute,
		[CountTerminal],[UpdateOptcount],[OptimalValue],[ElemenationCount],[OptimalRoute]
	FROM @AlgorithmPerfomances 

	INSERT INTO AlgorithmOptimalValueChange (AlgorithmPerfomanceId, [NumberOfIteration], [Duration], [DurationMilliSeconds],
				[OptimalValue], [OptimalRoute], [OptimalNative])	
	SELECT ap.[AlgorithmPerfomanceId], avc.[NumberOfIteration], avc.[Duration], avc.[DurationMilliSeconds],
				avc.[OptimalValue], avc.[OptimalRoute], avc.[OptimalNative]
	FROM @AlgorithmOptimalValueChange avc
	INNER JOIN #AlgorithmPerfomanceShort ap
	ON avc.DBName = ap.DBName AND avc.[Algorithm] = ap.[Algorithm] AND avc.TableSetAsNumber = ap.TableSetAsNumber

END






