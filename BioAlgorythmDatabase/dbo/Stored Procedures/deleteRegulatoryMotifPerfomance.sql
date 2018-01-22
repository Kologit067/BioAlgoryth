CREATE PROCEDURE [dbo].[deleteRegulatoryMotifPerfomance]
    @Algorithm VARCHAR(200),
    @SequenceLengthes VARCHAR(500),
    @MotifLength INT
AS    
BEGIN
	
	IF (@SequenceLengthes IS NULL)	
	BEGIN	
		DELETE s
		FROM [dbo].[DNAMappingSolution] s
		INNER JOIN 	[DNAMappingPerfomance] p
		ON s.DNAMappingPerfomanceId = p.DNAMappingPerfomanceId
		WHERE [Algorithm] = @Algorithm;

		DELETE FROM RegulatoryMotifPerfomance 
		WHERE [Algorithm] = @Algorithm;
	END
	ELSE
	BEGIN
		DELETE s
		FROM [dbo].RegulatoryMotifOptimalValueChange s
		INNER JOIN 	RegulatoryMotifPerfomance p
		ON s.RegulatoryMotifPerfomanceId = p.RegulatoryMotifPerfomanceId
		WHERE SequenceLengthes = @SequenceLengthes AND MotifLength = @MotifLength AND [Algorithm] = @Algorithm;

		DELETE s
		FROM [dbo].RegulatoryMotifSolution s
		INNER JOIN 	RegulatoryMotifPerfomance p
		ON s.RegulatoryMotifPerfomanceId = p.RegulatoryMotifPerfomanceId
		WHERE SequenceLengthes = @SequenceLengthes AND MotifLength = @MotifLength AND [Algorithm] = @Algorithm;

		DELETE FROM RegulatoryMotifPerfomance 
		WHERE SequenceLengthes = @SequenceLengthes AND MotifLength = @MotifLength AND [Algorithm] = @Algorithm;
    END

END