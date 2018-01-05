CREATE TABLE [dbo].[RegulatoryMotifSolution] (
    [RegulatoryMotifSolutionId]   INT           IDENTITY (1, 1) NOT NULL,
    [RegulatoryMotifPerfomanceId] INT           NOT NULL,
    [StartPosition]               VARCHAR (500) NULL,
    [Motif]                       VARCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([RegulatoryMotifSolutionId] ASC)
);

