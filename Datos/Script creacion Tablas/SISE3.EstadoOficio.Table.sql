USE [SISE_NEW]
GO
/****** Object:  Table [SISE3].[EstadoOficio]    Script Date: 29/02/2024 10:05:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SISE3].[EstadoOficio](
	[IdEstadoOficio] [int] IDENTITY(1,1) NOT NULL,
	[AsuntoNeunId] [int] NOT NULL,
	[AsuntoId] [int] NOT NULL,
	[AsuntoDocumentoId] [int] NOT NULL,
	[CatOrganismoId] [int] NOT NULL,
	[AnexoId] [int] NOT NULL,
	[AnexoTipoID] [int] NOT NULL,
	[ParteId] [int] NOT NULL,
	[Folio] [int] NOT NULL,
	[Año] [int] NOT NULL,
	[uGuid] [uniqueidentifier] NOT NULL,
	[TipoNotificacion] [int] NULL,
	[kIdRuta] [int] NULL,
	[ExtensionDocumento] [varchar](5) NULL,
	[NombreArchivo] [varchar](100) NULL,
	[Firmado] [bit] NULL,
	[Estatus] [bit] NULL,
	[FechaAlta] [datetime] NULL,
	[FechaBaja] [datetime] NULL
) ON [PRIMARY]
GO
