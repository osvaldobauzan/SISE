USE [SISE_NEW]
GO
/****** Object:  UserDefinedFunction [SISE3].[fnTableroProyectoArchivoInserta]    Script Date: 18/04/2024 02:56:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: Fanny P. Lemus García
-- Create date: 05/04/2024
-- Version: 1
-- Description:	Inserta registro en Tabla ProyectoArchivo
-- SELECT * FROM [SISE3].[fnTableroProyectoArchivoInserta](180, 2, '8/2024', 27, NULL)
-- SELECT * FROM [SISE3].[fnTableroProyectoArchivoInserta](180, 5232,2, 'proyecto_sentencia.docx', 'localhost', 'proy')
-- =============================================

ALTER   FUNCTION [SISE3].[fnTableroProyectoArchivoInserta](	
	@pi_CatOrganismoId INT,
	@pi_AsuntoNeunId BIGINT,
	@pi_iRegistroEmpleadoId INT,
	@pi_sNombreArchivoReal VARCHAR(500),
	@pi_sIPUsuario VARCHAR(100),
	@pi_sTipoDocumento VARCHAR (50)
)

	RETURNS @TableroArchivoProyecto TABLE (
	sNombreArchivo VARCHAR(300),
	sNombreArchivoReal NVARCHAR(300),
	iRutaArchivoNAS INT,
	fFechaAlta DATETIME,
	iRegistroEmpleadoId INT,
	sIPUsuario NVARCHAR(50),
	iStatusReg INT,
	sAnioRuta VARCHAR(50),
	CatOrganismoId INT
	)


AS BEGIN
	
			DECLARE @pi_fFechaProyecto DATETIME2=sysdatetime() 
			DECLARE @pi_fFechaAlta DATETIME2=sysdatetime() 
			DECLARE @sNombreArchivo VARCHAR(200)
			DECLARE @kid_ruta INT
			DECLARE @sAnioRuta VARCHAR(50)

					
			SELECT 
			  @kid_ruta = kid_ruta,
			  @sNombreArchivo = sNombreArchivo 
			FROM 
				[SISE3].[fnTableroProyectoDocumento](@pi_CatOrganismoId, @pi_AsuntoNeunId ,@pi_sNombreArchivoReal, @pi_sTipoDocumento)


			INSERT INTO @TableroArchivoProyecto(
			 [sNombreArchivo],
			 [sNombreArchivoReal],
			 [iRutaArchivoNAS],
			 [fFechaAlta],
			 [iRegistroEmpleadoId],
			 [sIPUsuario],
			 [iStatusReg],
			 [sAnioRuta],
			 [CatOrganismoId]
			)
			 VALUES (
				@sNombreArchivo,
				@pi_sNombreArchivoReal,
				@kid_ruta,
				@pi_fFechaAlta,
				@pi_iRegistroEmpleadoId,
				@pi_sIPUsuario,
				1,
				NULL,
				@pi_CatOrganismoId
			)


RETURN
END
