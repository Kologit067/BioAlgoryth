CREATE TABLE [dbo].[AlgorithmPerfomance] (
    [AlgorithmPerfomance]  INT            IDENTITY (1, 1) NOT NULL,
    [DBName]               NVARCHAR (500) NOT NULL,
    [Tables]               NVARCHAR (MAX) NOT NULL,
    [Algorithm]            NVARCHAR (MAX) NOT NULL,
    [NumberOfIteration]    BIGINT         NULL,
    [Duration]             BIGINT         NULL,
    [DurationMilliSeconds] BIGINT         NULL,
    [DateComplete]         DATETIME       NULL,
    [IsComplete]           BIT            NULL,
    [ElementCount]         INT            NULL,
    [TableSetAsNumber]     INT            NULL,
    [LastRoute]            VARCHAR (500)  NULL,
    [CountTerminal]        INT            NULL,
    [UpdateOptcount]       INT            NULL,
    [OptimalValue]         INT            NULL,
    [ElemenationCount]     INT            NULL,
    [OptimalRoute]         VARCHAR (500)  NULL,
    PRIMARY KEY CLUSTERED ([AlgorithmPerfomance] ASC)
);

