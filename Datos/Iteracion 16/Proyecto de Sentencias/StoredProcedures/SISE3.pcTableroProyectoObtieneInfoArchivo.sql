USE [SISE_NEW]
GO
/****** Object:  StoredProcedure [SISE3].[pcTableroProyectoObtieneInfoArchivo]    Script Date: 18/04/2024 02:54:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: Mario A Hernández Román
-- Create date: 09/04/2024
-- Description:	Obtiene informaci'on del archivo y el NEun Asociado
-- Version: 1.3
-- EXEC [SISE3].[pcTableroProyectoObtieneInfoArchivo] 214
-- =============================================

CREATE   PROCEDURE [SISE3].[pcTableroProyectoObtieneInfoArchivo](
	@pi_pkProyectoArchivoId BIGINT
) AS BEGIN

		SET NOCOUNT ON

		BEGIN TRY
			SELECT 
				AsuntoNeunId,
				pkProyectoArchivoId,
				sNombreArchivo,
				sNombreArchivoReal,
				iRutaArchivoNAS,
				fFechaAlta,
				iRegistroEmpleadoId,
				sIPUsuario,
				iStatusReg,
				sAnioRuta,
				CatOrganismoId
			FROM 
				[SISE3].[fnTableroProyectoObtieneInfoArchivo](
					@pi_pkProyectoArchivoId
				)

		END TRY

		BEGIN CATCH
			-- Ejecuta la rutina de recuperacion de errores.
			EXECUTE dbo.usp_GetErrorInfo;
		END CATCH;
		SET NOCOUNT OFF
END;

