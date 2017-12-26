



CREATE PROCEDURE [dbo].[SaveDataPerfomanceToArchive]
AS    
BEGIN
	
	DECLARE @VersionNumber INT = (SELECT ISNULL(MAX(VersionNumber),0) + 1 FROM AlgorithmPerfomanceArchive)

	INSERT AlgorithmPerfomanceArchive ( VersionNumber, DBName, [Tables],[Algorithm],[NumberOfIteration],[Duration], [DurationMilliSeconds], 
		[DateComplete], [IsComplete], [ElementCount], TableSetAsNumber, LastRoute,
		[CountTerminal],[UpdateOptcount],[OptimalValue],[ElemenationCount],[OptimalRoute])
	SELECT @VersionNumber, DBName, [Tables],[Algorithm],[NumberOfIteration],[Duration],[DurationMilliSeconds], 
		[DateComplete], [IsComplete], [ElementCount], TableSetAsNumber, LastRoute,
		[CountTerminal],[UpdateOptcount],[OptimalValue],[ElemenationCount],[OptimalRoute]
	FROM [AlgorithmPerfomance]
	

END






