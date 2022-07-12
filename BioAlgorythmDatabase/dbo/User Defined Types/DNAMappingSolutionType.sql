CREATE TYPE [dbo].[DNAMappingSolutionType] AS TABLE (
    [Algorithm]          VARCHAR (200)  NULL,
    [Size]               INT            NOT NULL,
    [Limit]              INT            NOT NULL,
    [InputData]          NVARCHAR (MAX) NOT NULL,
    [OutputPresentation] NVARCHAR (MAX) NOT NULL);





