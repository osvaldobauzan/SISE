USE [SISE_NEW]
GO

/****** Object:  UserDefinedTableType [SISE3].[TableroProyecto_type]    Script Date: 15/03/2024 11:42:48 a. m. ******/
CREATE TYPE [SISE3].[TableroProyecto_type] AS TABLE(
	[AsuntoNeunId] [bigint] NULL,
	[AsuntoAlias] [varchar](50) NULL,
	[NumeroAlias] [bigint] NULL,
	[CatTipoOrganismoId] [int] NULL,
	[CatOrganismoId] [int] NULL,
	[CatTipoAsuntoId] [int] NULL,
	[CatTipoAsunto] [varchar](150) NULL,
	[TipoProcedimiento] [int] NULL,
	[NombreCorto] [varchar](10) NULL,
	[TieneAudiencia] [bit] NULL,
	[FechaAudiencia] [date] NULL,
	[TieneArchivoAudiencia] [bit] NULL,
	[ArchivoAudiencia] [varchar](150) NULL,
	[ResultadoAudiencia] [varchar](250) NULL,
	[TipoAudiencia] [varchar](250) NULL,
	[SecretarioId] [int] NULL,
	[Secretario] [varchar](250) NULL,
	[Mesa] [varchar](150) NULL,
	[TipoCuaderno] [int] NULL,
	[sTipoCuaderno] [varchar](50) NULL,
	[TieneArchivoProyecto] [bit] NULL,
	[ArchivoProyecto] [varchar](250) NULL,
	[FechaCargaProyecto] [date] NULL,
	[NumeroVersionProyecto] [int] NULL,
	[EstadoProyecto] [int] NULL,
	[sEstadoProyecto] [varchar](50) NULL,
	[FechaEstadoProyecto] [date] NULL,
	[SentidoProyecto] [int] NULL,
	[sSentido] [varchar](50) NULL,
	[TipoSentencia] [int] NULL,
	[sTipoSentencia] [varchar](50) NULL
)
GO


