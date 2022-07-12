

CREATE PROCEDURE [dbo].[deleteDNAMappingPerfomance]
    @Algorithm VARCHAR(200),
    @Limit INT = NULL,
    @Size INT = NULL
AS    
BEGIN
	
	IF (@Size IS NULL)	
	BEGIN	
		DELETE s
		FROM [dbo].[DNAMappingSolution] s
		INNER JOIN 	[DNAMappingPerfomance] p
		ON s.DNAMappingPerfomanceId = p.DNAMappingPerfomanceId
		WHERE [Algorithm] = @Algorithm;

		DELETE FROM [DNAMappingPerfomance] 
		WHERE [Algorithm] = @Algorithm;
	END
	ELSE
	BEGIN
		DELETE s
		FROM [dbo].[DNAMappingSolution] s
		INNER JOIN 	[DNAMappingPerfomance] p
		ON s.DNAMappingPerfomanceId = p.DNAMappingPerfomanceId
		WHERE [Size] = @Size AND Limit = @Limit AND [Algorithm] = @Algorithm;

		DELETE FROM [DNAMappingPerfomance] 
		WHERE [Size] = @Size AND Limit = @Limit AND [Algorithm] = @Algorithm;
    END

END

/*
EXEC [dbo].[deleteDNAMappingPerfomance] @Algorithm = 'EnumerateDNAMappingByIntegerTrangle'
*/