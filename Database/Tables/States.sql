CREATE TABLE [dbo].[States] (
    [StateId]        INT          NOT NULL,
    [StateName] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED ([StateId] ASC)
);