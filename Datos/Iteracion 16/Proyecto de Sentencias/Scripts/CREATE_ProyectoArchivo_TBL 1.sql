USE [SISE_NEW]
GO

-- =============================================
-- Author: Daniel A Rangel Gavia
-- Create date: 08/03/2024
-- Version: 1
-- Description:	Script creaci�n de tabla para el manejo de los archivos que se integran a las vers�ones de Proyectos.
-- Objetos:		[SISE3].[ProyectoArchivo]
-- =============================================

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SISE3].[ProyectoArchivo]') AND type in (N'U'))
DROP TABLE [SISE3].[ProyectoArchivo]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [SISE3].[ProyectoArchivo](
	[pkProyectoArchivoId] [bigint] PRIMARY KEY IDENTITY(1,1),
	[sNombreArchivo] [varchar](300) NULL,
	[sNombreArchivoReal] [nvarchar](300) NULL,
	[iRutaArchivoNAS] [int] NULL,
	[fFechaAlta] [datetime] NULL,
	[iRegistroEmpleadoId] [bigint] NULL,
	[sIPUsuario] [nvarchar](50) NULL,
	[sAnioRuta] [varchar](50) NULL,
	[CatOrganismoId] [int] NOT NULL,
	[iStatusReg] [int] NULL
) 
GO

--USE [SISE_NEW]
--GO
--GRANT REFERENCES ON [SISE3].[ProyectoArchivo] TO darangel;
--GO