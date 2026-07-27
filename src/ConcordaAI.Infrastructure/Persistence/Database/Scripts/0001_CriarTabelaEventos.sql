CREATE TABLE Evento
(
    Id              INT IDENTITY(1,1)      NOT NULL,
    Nome            NVARCHAR(200)          NOT NULL,
    Cidade          NVARCHAR(100)          NOT NULL,
    Estado          CHAR(2)                NOT NULL,
    DataInicio      DATE                   NOT NULL,
    DataFim         DATE                   NOT NULL,
    Status          TINYINT                NOT NULL DEFAULT (1),
    CriadoEm        DATETIME2(0)           NOT NULL DEFAULT (SYSUTCDATETIME()),
    AtualizadoEm    DATETIME2(0)           NULL,

    CONSTRAINT PK_Evento PRIMARY KEY CLUSTERED (Id),

    CONSTRAINT CK_Evento_Status
        CHECK (Status IN (1, 2, 3, 4)), -- 1=Planejado, 2=EmAndamento, 3=Concluido, 4=Cancelado

    CONSTRAINT CK_Evento_DataFim
        CHECK (DataFim >= DataInicio),

    CONSTRAINT CK_Evento_Estado
        CHECK (LEN(Estado) = 2)
);
GO

CREATE NONCLUSTERED INDEX IX_Evento_Status
    ON Evento (Status)
    INCLUDE (Nome, Cidade, Estado, DataInicio, DataFim);
GO

CREATE NONCLUSTERED INDEX IX_Evento_Cidade_Estado
    ON Evento (Estado, Cidade);
GO