CREATE TABLE [dbo].[Adres] (
    [Id]                INT         IDENTITY (1, 1) NOT NULL,
    [straatnaamID]      INT         NOT NULL,
    [huisnummer]        NCHAR (10)  NOT NULL,
    [appartementnummer] NCHAR (25)  NULL,
    [busnummer]         NCHAR (25)  NULL,
    [huisnummerlabel]   NCHAR (100) NULL,
    [adreslocatieID]    INT         NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC, [straatnaamID] ASC, [adreslocatieID] ASC),  
	
    CONSTRAINT [FK_Adres_Straatnaam] FOREIGN KEY ([straatnaamID]) REFERENCES [dbo].[Straatnaam]([Id]),
    CONSTRAINT [FK_Adres_ToAdreslocatie] FOREIGN KEY ([adreslocatieID]) REFERENCES [dbo].[Adreslocatie] ([Id])
);

