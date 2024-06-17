USE [SISE_NEW]
GO
/****** Object:  UserDefinedTableType [SISE3].[AutoridadAsunto_type2]    Script Date: 29/02/2024 10:05:38 p. m. ******/
CREATE TYPE [SISE3].[AutoridadAsunto_type2] AS TABLE(
	[TipoAnexoId] [int] NULL,
	[AnexoParteId] [int] NULL,
	[AnexoParteDescripcion] [varchar](max) NULL,
	[TextoOficioLibre] [nvarchar](max) NULL
)
GO
