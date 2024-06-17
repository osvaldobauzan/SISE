USE [SISE_NEW]
GO

-- =============================================
-- Author: Daniel A Rangel Gavia
-- Create date: 08/03/2024
-- Version: 1.1
-- Description:	Script creaci�n de tabla para el control de versiones y estados de documentos de Proyectos.
-- Objetos:		[SISE3].[Proyecto]
-- =============================================


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SISE3].[Proyecto]') AND type in (N'U'))
ALTER TABLE [SISE3].[Proyecto] DROP CONSTRAINT [FK_Proyecto_Version_Archivo]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SISE3].[Proyecto]') AND type in (N'U'))
ALTER TABLE [SISE3].[Proyecto] DROP CONSTRAINT [FK_Version_Correccion_Archivo]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SISE3].[Proyecto]') AND type in (N'U'))
DROP TABLE [SISE3].[Proyecto]
GO

/****** Object:  Table [SISE3].[Proyecto] ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [SISE3].[Proyecto](
	[CatOrganismoId] [int] NOT NULL,
	[AsuntoNeunId] [bigint] NOT NULL,
	[pkProyectoId] [bigint] IDENTITY(1,1),
	[fFechaProyecto] [datetime] NULL,
	-- Bloque de alta de Versi�n
	[iTitular] [bigint] NULL,
	[iSecretario] [bigint] NULL,
	[iTipoSentenciaId] [int] NULL,
	[iSentidoId] [int] NULL,
	[fkProyectoVersionArchivoId] [bigint] NULL,
	[sSintesis] [varchar](max) NULL,
	[iVersion] [int] NOT NULL, --Auto incrementar en el SP de INSERT
	[fFechaAlta] [datetime] NULL,
	[iRegistroEmpleadoId] [bigint] NULL,

	[iEstado] [int] NULL, -- Default "Para revisi�n"
	---

	-- Bloque es de cambio de estado y comentarios
	-- Cambia ESTADO seg�n selecci�n
	[fkCorreccionArchivoId] [bigint] NULL,
	[sCorreccionComentario] [varchar](max) NULL,
	[fFechaActualiza] [datetime] NULL,

	[iStatusReg] [int] NULL,
 CONSTRAINT [PK_Proyecto] PRIMARY KEY CLUSTERED 
(
	[CatOrganismoId] ASC,
	[AsuntoNeunId] ASC,
	[pkProyectoId] ASC,
	[iVersion] ASC
)WITH (PAD_INDEX = ON, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
)
GO

ALTER TABLE [SISE3].[Proyecto] WITH NOCHECK ADD CONSTRAINT [FK_Proyecto_Version_Archivo] FOREIGN KEY ([fkProyectoVersionArchivoId])
REFERENCES [SISE3].[ProyectoArchivo] ([pkProyectoArchivoId])
GO
ALTER TABLE [SISE3].[Proyecto] NOCHECK CONSTRAINT [FK_Proyecto_Version_Archivo]
GO

ALTER TABLE [SISE3].[Proyecto] WITH NOCHECK ADD  CONSTRAINT [FK_Version_Correccion_Archivo] FOREIGN KEY([fkCorreccionArchivoId])
REFERENCES [SISE3].[ProyectoArchivo] ([pkProyectoArchivoId])
GO
ALTER TABLE [SISE3].[Proyecto] NOCHECK CONSTRAINT [FK_Version_Correccion_Archivo]
GO
