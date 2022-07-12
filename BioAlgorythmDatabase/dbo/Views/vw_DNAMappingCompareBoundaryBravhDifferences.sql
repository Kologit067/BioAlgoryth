
CREATE VIEW [dbo].[vw_DNAMappingCompareBoundaryBravhDifferences]
AS
WITH dmd AS
(
SELECT *
FROM [dbo].[DNAMappingPerfomance]
WHERE [Algorithm] = 'EnumerateDNAMappingByDifferences'
),
dmb AS
(
SELECT *
FROM [dbo].[DNAMappingPerfomance]
WHERE [Algorithm] = 'EnumerateDNAMappingBranchBoundary'
)
SELECT 
     dmd.[Size],
     dmd.[Limit],
     dmd.[InputData],
     dmd.[OutputPresentation] as difOutput,
     dmb.[OutputPresentation] as bnchOutput,
     dmd.[NumberOfIteration] as difNumberOfIteration,
     dmb.[NumberOfIteration] as bnchNumberOfIteration,
     dmd.[Duration] as difDuration,
     dmb.[Duration] as bnchDuration,
     dmd.[DurationMilliSeconds] as difDurationMilliSeconds,
     dmb.[DurationMilliSeconds] as bnchDurationMilliSeconds,
     dmd.[DateComplete] as difDateComplete,
     dmb.[DateComplete] as bnchDateComplete,
     dmd.[IsComplete] as difIsComplete,
     dmb.[IsComplete] as bnchIsComplete,
     dmd.[LastRoute] as difLastRoute,
     dmb.[LastRoute] as bnchLastRoute,
     dmd.[OptimalRoute] as difOptimalRoute,
     dmb.[OptimalRoute] as bnchOptimalRoute,
     dmd.[CountTerminal] as difCountTerminal,
     dmb.[CountTerminal] as bnchCountTerminal,
     dmd.[UpdateOptcount] as difUpdateOptcount,
     dmb.[UpdateOptcount] as bnchUpdateOptcount,
     dmd.[ElemenationCount] as difElemenationCount,
     dmb.[ElemenationCount] as bnchElemenationCount,
     dmd.[IsAllResult] as difIsAllResult,
     dmb.[IsAllResult] as bnchIsAllResult
FROM dmd
FULL OUTER JOIN dmb
ON dmd.Size = dmb.size AND dmd.[Limit] = dmb.[Limit] AND dmd.[InputData] = dmb.[InputData] 
--WHERE dmd.[OutputPresentation] <>   dmb.[OutputPresentation] 
--WHERE dmd.Algorithm IS NULL OR dmt.Algorithm IS NULL  