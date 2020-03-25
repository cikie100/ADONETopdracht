CREATE TABLE [dbo].[Straatnaam] (
    [Id]         INT         IDENTITY (1, 1) NOT NULL,
    [straatnaam] NCHAR (250) NULL,
    [NIScode]    INT         NOT NULL,
    PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [FK_Adres_Gemeente] FOREIGN KEY ([NIScode]) REFERENCES [dbo].[Gemeente] ([NIScode])
);