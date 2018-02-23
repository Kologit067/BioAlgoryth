SELECT [Algorithm], COUNT(*)
FROM [BioAlgorythm].[dbo].[FindPatternPerfomance]
GROUP BY [Algorithm]

SELECT [Algorithm],[TextSize],[PatternSize],[AlphabetSize], COUNT(*)
FROM [BioAlgorythm].[dbo].[FindPatternPerfomance]
GROUP BY [Algorithm],[TextSize],[PatternSize],[AlphabetSize]

SELECT COUNT(*)
FROM [BioAlgorythm].[dbo].[FindPatternPerfomance]

/*
TRUNCATE TABLE [dbo].[FindPatternPerfomance]
DELETE FROM [BioAlgorythm].[dbo].[FindPatternPerfomance]
WHERE [Algorithm] = 'BFSC' AND [TextSize] = 10 AND PatternSize = 5 AND [AlphabetSize] = 3
WHERE [Algorithm] = 'BMC'
*/

