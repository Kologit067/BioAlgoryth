CREATE VIEW [dbo].[vwAlgorithmPerfomance]
AS
SELECT [AlgorithmPerfomance]
      ,[DBName]
      ,[TableSetAsNumber]
      ,[OptimalValue]
      ,[OptimalRoute]
      ,[Tables]
      ,[Algorithm]
      ,[NumberOfIteration]
      ,[Duration]
      ,[DurationMilliSeconds]
      ,[DateComplete]
      ,[IsComplete]
      ,[ElementCount]
      ,[LastRoute]
      ,[CountTerminal]
      ,[UpdateOptcount]
      ,[ElemenationCount]
FROM [dbo].[AlgorithmPerfomance]
WHERE [DBName] = 'North'
--ORDER BY [AlgorithmPerfomance] DESC
  
