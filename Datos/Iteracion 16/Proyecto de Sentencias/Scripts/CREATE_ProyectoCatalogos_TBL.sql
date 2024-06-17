-- =============================================
-- Author: Daniel A Rangel Gavia
-- Create date: 08/03/2024
-- Version: 1
-- Description:	Script creación de tablas catálogos, e inserción de registros.
-- Objetos:		[SISE3].[CAT_Sentido]
--				[SISE3].CAT_ProyectoEstado]
--				[SISE3].[CAT_TipoSentencia]
-- =============================================

USE [SISE_NEW]
GO

/*************************************************************************************************/
/****** Object:  Table [SISE3].[CAT_Sentido]												******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SISE3].[CAT_Sentido]') AND type in (N'U'))
DROP TABLE [SISE3].[CAT_Sentido]
GO
CREATE TABLE [SISE3].[CAT_Sentido](
	[pkSentidoId] [int] NOT NULL,
	[sSentido] [varchar](100) NOT NULL,
	[CatTipoAsuntoId] [int] NOT NULL,
	[iStatusReg] [int] NULL,
	CONSTRAINT PK_CAT_Sentido PRIMARY KEY([pkSentidoId])
)
GO
INSERT INTO  [SISE3].[CAT_Sentido] VALUES(1, 'Ampara', 1, 1)
INSERT INTO  [SISE3].[CAT_Sentido] VALUES(2, 'No Ampara', 1, 1)
INSERT INTO  [SISE3].[CAT_Sentido] VALUES(3, 'Sobresee', 1, 1)
INSERT INTO  [SISE3].[CAT_Sentido] VALUES(4, 'Otro', 1, 1)
GO
SELECT * FROM [SISE3].[CAT_Sentido]
GO


/*************************************************************************************************/
/****** Object:  Table [SISE3].CAT_ProyectoEstado]											******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SISE3].[CAT_ProyectoEstado]') AND type in (N'U'))
DROP TABLE [SISE3].[CAT_ProyectoEstado]
GO
CREATE TABLE [SISE3].[CAT_ProyectoEstado](
	[pkProyectoEstadoId] [int] NOT NULL,
	[sProyectoEstado] [varchar](120) NOT NULL,
	[iOrdenEstado] [int] NULL,
	[iStatusReg] [int] NULL,
	CONSTRAINT PK_CAT_ProyectoEstado PRIMARY KEY([pkProyectoEstadoId])
)
GO
/*
ALTER TABLE [SISE3].[CAT_ProyectoEstado]
ADD [iOrdenEstado] [int] NULL
GO 
*/

INSERT INTO  [SISE3].[CAT_ProyectoEstado] VALUES(1, 'Sin proyecto', 1, 1)
INSERT INTO  [SISE3].[CAT_ProyectoEstado] VALUES(2, 'Para revisión', 2, 1)
INSERT INTO  [SISE3].[CAT_ProyectoEstado] VALUES(3, 'No aprobado', 3, 1)
INSERT INTO  [SISE3].[CAT_ProyectoEstado] VALUES(4, 'Con ajustes de fondo', 4, 1)
INSERT INTO  [SISE3].[CAT_ProyectoEstado] VALUES(5, 'Con ajustes de forma', 5, 1)
INSERT INTO  [SISE3].[CAT_ProyectoEstado] VALUES(6, 'Aprobado', 6, 1)
--INSERT INTO  [SISE3].[CAT_ProyectoEstado] VALUES(7, 'Sin audiencia', 7, 1)
--INSERT INTO  [SISE3].[CAT_ProyectoEstado] VALUES(6, 'Preautorizado',6 , 1)
--INSERT INTO  [SISE3].[CAT_ProyectoEstado] VALUES(7, 'Autorizado', 7, 1)
GO
SELECT * FROM [SISE3].[CAT_ProyectoEstado]
GO

/****** Object:  Table [SISE3].[CAT_TipoSentencia]     ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SISE3].[CAT_TipoSentencia]') AND type in (N'U'))
DROP TABLE [SISE3].[CAT_TipoSentencia]
GO
CREATE TABLE [SISE3].[CAT_TipoSentencia](
	[pkTipoSentenciaId] [int] NOT NULL,
	[sTipoSentencia] [varchar](120) NOT NULL,
	[CatTipoAsuntoId] [int] NOT NULL,
	[iStatusReg] [int] NULL,
	CONSTRAINT PK_CAT_TipoSentencia PRIMARY KEY([pkTipoSentenciaId])
)
GO

INSERT INTO  [SISE3].[CAT_TipoSentencia] VALUES(1, 'Sentencia definitiva', 1, 1)
INSERT INTO  [SISE3].[CAT_TipoSentencia] VALUES(2, 'Resolución', 1, 1)
INSERT INTO  [SISE3].[CAT_TipoSentencia] VALUES(3, 'Aclaración de sentencia', 1, 1)
INSERT INTO  [SISE3].[CAT_TipoSentencia] VALUES(4, 'Interlocutoria', 1, 1)
GO
SELECT * FROM [SISE3].[CAT_TipoSentencia]
GO

/*
SELECT * FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='Proyecto';
SELECT * FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='ProyectoVersion';
SELECT * FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='ProyectoArchivo';
*/