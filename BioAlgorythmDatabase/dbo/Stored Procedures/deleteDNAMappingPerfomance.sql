

CREATE PROCEDURE [dbo].[deleteDNAMappingPerfomance]
    @Algorithm VARCHAR(200),
    @Size INT = NULL
AS    
BEGIN
	
	IF (@Size IS NULL)		
		DELETE FROM [DNAMappingPerfomance] 
		WHERE [Algorithm] = @Algorithm;
	ELSE
		DELETE FROM [DNAMappingPerfomance] 
		WHERE [Size] = @Size AND [Algorithm] = @Algorithm;


END