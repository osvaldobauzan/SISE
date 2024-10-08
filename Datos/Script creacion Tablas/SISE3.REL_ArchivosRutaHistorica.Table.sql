USE [SISE_NEW]
GO
/****** Object:  Table [SISE3].[REL_ArchivosRutaHistorica]    Script Date: 29/02/2024 10:05:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SISE3].[REL_ArchivosRutaHistorica](
	[AsuntoNeunId] [bigint] NOT NULL,
	[YearPromocion] [int] NOT NULL,
	[CatOrganismoId] [int] NOT NULL,
	[idRuta] [int] NOT NULL,
	[fechaAlta] [datetime] NOT NULL,
	[fechaModificacion] [datetime] NOT NULL,
	[NumeroOrden] [int] NULL
) ON [PRIMARY]
GO
