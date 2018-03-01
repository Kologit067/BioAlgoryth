
CREATE FUNCTION [dbo].[tvfFindPatternPerfomanceCompareOutput]
(     
	@Algorithm1 VARCHAR(500),
	@Algorithm2 VARCHAR(500),
	@TextSize INT,
	@PatternSize INT,
	@AlphabetSize INT
)
RETURNS TABLE
AS
RETURN
(
WITH Alg1 AS
(
SELECT [FindPatternPerfomanceId]
      ,[Algorithm]
      ,[TextSize]
      ,[PatternSize]
      ,[AlphabetSize]
      ,[Text]
      ,[Pattern]
      ,[OutputPresentation]
      ,[NumberOfIteration]
      ,[NumberOfComparison]
      ,[Duration]
      ,[DurationMilliSeconds]
      ,[DateComplete]
FROM [dbo].[FindPatternPerfomance] with (nolock)
WHERE [Algorithm] = @Algorithm1 AND [TextSize] = @TextSize AND PatternSize = @PatternSize AND [AlphabetSize] = @AlphabetSize
),
Alg2 AS
(
SELECT [FindPatternPerfomanceId]
      ,[Algorithm]
      ,[TextSize]
      ,[PatternSize]
      ,[AlphabetSize]
      ,[Text]
      ,[Pattern]
      ,[OutputPresentation]
      ,[NumberOfIteration]
      ,[NumberOfComparison]
      ,[Duration]
      ,[DurationMilliSeconds]
      ,[DateComplete]
FROM [dbo].[FindPatternPerfomance] with (nolock)
WHERE [Algorithm] = @Algorithm2 AND [TextSize] = @TextSize AND PatternSize = @PatternSize AND [AlphabetSize] = @AlphabetSize
)
SELECT TOP(100) 
	Alg1.[Algorithm] as Alg1,
	Alg2.[Algorithm] as Alg2,
    Alg1.[TextSize],
    Alg1.[PatternSize],
    Alg1.[AlphabetSize],
    Alg1.[Text],
    Alg1.[Pattern],
Alg1.OutputPresentation as OutputPresentationAlg1, Alg2.OutputPresentation as OutputPresentationAlg2
FROM Alg1
--FULL OUTER JOIN Alg2
INNER JOIN Alg2
ON Alg1.[Text] = Alg2.[Text] AND Alg1.[Pattern] = Alg2.[Pattern]
WHERE Alg1.FindPatternPerfomanceId IS NULL OR Alg2.FindPatternPerfomanceId IS NULL OR Alg1.OutputPresentation <> Alg2.OutputPresentation
)