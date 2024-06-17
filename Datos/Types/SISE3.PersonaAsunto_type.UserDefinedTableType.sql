USE [SISE_NEW]
GO
/****** Object:  UserDefinedTableType [SISE3].[PersonaAsunto_type]    Script Date: 29/02/2024 10:05:38 p. m. ******/
CREATE TYPE [SISE3].[PersonaAsunto_type] AS TABLE(
	[Nombre] [varchar](500) NOT NULL,
	[APaterno] [varchar](50) NULL,
	[AMaterno] [varchar](50) NULL,
	[CatTipoPersonaId] [smallint] NULL,
	[CatCaracterPersonaAsuntoId] [smallint] NOT NULL,
	[CatTipoPersonaJuridicaId] [smallint] NULL,
	[CaracterPromueveNombre] [int] NULL
)
GO
