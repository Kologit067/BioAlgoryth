
CREATE PROCEDURE [dbo].[DeletePerformanceStatisticsForAlgorythm]
    @DBName VARCHAR(500),
    @Algorithm VARCHAR(500)
AS    
BEGIN

	DELETE AlgorithmOptimalValueChange
	WHERE AlgorithmPerfomanceId IN
	(		
		SELECT AlgorithmPerfomance FROM [AlgorithmPerfomance] 
		WHERE [Algorithm] = @Algorithm AND DBName = @DBName
	)

	DELETE FROM [AlgorithmPerfomance] 
	WHERE [Algorithm] = @Algorithm AND DBName = @DBName


END

