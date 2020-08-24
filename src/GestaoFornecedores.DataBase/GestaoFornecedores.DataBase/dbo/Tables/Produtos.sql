CREATE TABLE [dbo].[Produtos] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [FornecedorId] UNIQUEIDENTIFIER NOT NULL,
    [Nome]         VARCHAR (200)    NOT NULL,
    [Descricao]    VARCHAR (200)    NOT NULL,
    [Imagem]       VARCHAR (200)    NOT NULL,
    [Valor]        DECIMAL (18, 2)  NOT NULL,
    [DataCadastro] DATETIME2 (7)    NOT NULL,
    [Ativo]        BIT              NOT NULL,
    CONSTRAINT [PK_Produtos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Produtos_Fornecedores_FornecedorId] FOREIGN KEY ([FornecedorId]) REFERENCES [dbo].[Fornecedores] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Produtos_FornecedorId]
    ON [dbo].[Produtos]([FornecedorId] ASC);

