CREATE TABLE [dbo].[DNAMappingSolution] (
    [DNAMappingSolutioniD]   INT            IDENTITY (1, 1) NOT NULL,
    [DNAMappingPerfomanceId] INT            NOT NULL,
    [OutputPresentation]     NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([DNAMappingSolutioniD] ASC)
);

