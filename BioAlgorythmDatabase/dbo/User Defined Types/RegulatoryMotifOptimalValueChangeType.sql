CREATE TYPE [dbo].[RegulatoryMotifOptimalValueChangeType] AS TABLE (
    [Algorithm]            NVARCHAR (MAX) NOT NULL,
    [SequenceLengthes]     VARCHAR (500)  NOT NULL,
    [MotifLength]          INT            NOT NULL,
    [InputData]            NVARCHAR (MAX) NOT NULL,
    [NumberOfIteration]    BIGINT         NULL,
    [Duration]             BIGINT         NULL,
    [DurationMilliSeconds] BIGINT         NULL,
    [OptimalValue]         INT            NULL,
    [StartPosition]        VARCHAR (500)  NULL,
    [Motif]                VARCHAR (500)  NULL);



