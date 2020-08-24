CREATE TABLE [dbo].[Fornecedores] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [Nome]           VARCHAR (200)    NOT NULL,
    [Documento]      VARCHAR (14)     NOT NULL,
    [TipoFornecedor] INT              NOT NULL,
    [Ativo]          BIT              NOT NULL,
    CONSTRAINT [PK_Fornecedores] PRIMARY KEY CLUSTERED ([Id] ASC)
);

