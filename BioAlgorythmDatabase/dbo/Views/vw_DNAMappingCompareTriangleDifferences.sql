
CREATE VIEW [dbo].[vw_DNAMappingCompareTriangleDifferences]
AS
WITH dmd AS
(
SELECT *
FROM [dbo].[DNAMappingPerfomance]
WHERE [Algorithm] = 'EnumerateDNAMappingByDifferences'
),
dmt AS
(
SELECT *
FROM [dbo].[DNAMappingPerfomance]
WHERE [Algorithm] = 'EnumerateDNAMappingByIntegerTrangle'
)
SELECT 
     dmd.[Size],
     dmd.[Limit],
     dmd.[InputData],
     dmd.[OutputPresentation] as difOutput,
     dmt.[OutputPresentation] as tngOutput,
     dmd.[NumberOfIteration] as difNumberOfIteration,
     dmt.[NumberOfIteration] as tngNumberOfIteration,
     dmd.[Duration] as difDuration,
     dmt.[Duration] as tngDuration,
     dmd.[DurationMilliSeconds] as difDurationMilliSeconds,
     dmt.[DurationMilliSeconds] as tngDurationMilliSeconds,
     dmd.[DateComplete] as difDateComplete,
     dmt.[DateComplete] as tngDateComplete,
     dmd.[IsComplete] as difIsComplete,
     dmt.[IsComplete] as tngIsComplete,
     dmd.[LastRoute] as difLastRoute,
     dmt.[LastRoute] as tngLastRoute,
     dmd.[OptimalRoute] as difOptimalRoute,
     dmt.[OptimalRoute] as tngOptimalRoute,
     dmd.[CountTerminal] as difCountTerminal,
     dmt.[CountTerminal] as tngCountTerminal,
     dmd.[UpdateOptcount] as difUpdateOptcount,
     dmt.[UpdateOptcount] as tngUpdateOptcount,
     dmd.[ElemenationCount] as difElemenationCount,
     dmt.[ElemenationCount] as tngElemenationCount,
     dmd.[IsAllResult] as difIsAllResult,
     dmt.[IsAllResult] as tngIsAllResult
FROM dmd
FULL OUTER JOIN dmt
ON dmd.Size = dmt.size AND dmd.[Limit] = dmt.[Limit] AND dmd.[InputData] = dmt.[InputData] 
--WHERE dmd.[OutputPresentation] <>   dmt.[OutputPresentation] 
--WHERE dmd.Algorithm IS NULL OR dmt.Algorithm IS NULL  