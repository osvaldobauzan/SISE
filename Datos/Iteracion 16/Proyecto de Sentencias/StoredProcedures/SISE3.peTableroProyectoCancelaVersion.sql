USE [SISE_NEW]
GO
/****** Object:  StoredProcedure [SISE3].[peTableroProyectoCancelaVersion]    Script Date: 18/04/2024 02:56:06 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Fanny Paulet Lemus García
-- Alter date: 26/03/2024
-- Basado en: [SISE3].[peEliminaPromocion]
-- Objetivo: Elimina  el registro a partir del pi_pkProyectoId cambiando el estatus  de las tablas Proyecto y ProyectoArchivo
-- EXEC [SISE3].peTableroProyectoCancelaVersion 17
-- =============================================
ALTER   PROCEDURE [SISE3].[peTableroProyectoCancelaVersion]
@pi_pkProyectoId BIGINT
AS  BEGIN 
  	
		DECLARE @pkProyectoArchivoId BIGINT

		SET @pkProyectoArchivoId = (
			SELECT 
				fkProyectoVersionArchivoId
			FROM 
				[SISE_NEW].[SISE3].[Proyecto] WITH(NOLOCK)
			WHERE 
				pkProyectoId=@pi_pkProyectoId
		)

		--Revisar si la variable fkProyectoVersionArchivoId existe en ProyectoArchivo
  	IF NOT EXISTS (
			SELECT 
				pkProyectoArchivoId
			FROM 
				[SISE_NEW].[SISE3].[ProyectoArchivo] WITH(NOLOCK)
			WHERE 
				[SISE_NEW].[SISE3].[ProyectoArchivo].pkProyectoArchivoId = @pkProyectoArchivoId
		)
		THROW 51000,'Error, versión no existe en ProyectoArchivo',1;
 
  
    BEGIN TRY

		BEGIN TRAN	

			UPDATE [SISE_NEW].[SISE3].[ProyectoArchivo]
				SET 
					[SISE_NEW].[SISE3].[ProyectoArchivo].iStatusReg = 0 
				WHERE 
					[SISE_NEW].[SISE3].[ProyectoArchivo].pkProyectoArchivoId = @pkProyectoArchivoId

			Print 'Versión eliminada exitosamente de ProyectoArchivo'
			
			UPDATE [SISE_NEW].[SISE3].[Proyecto]
				SET
					[SISE_NEW].[SISE3].[Proyecto].iStatusReg = 0
				WHERE 
					[SISE_NEW].[SISE3].[Proyecto].pkProyectoId = @pi_pkProyectoId

			Print 'Versión eliminada exitosamente de Proyecto'
	
		END TRY

	BEGIN CATCH
		 -- Ejecuto ROLLBACK solo en caso de error
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		-- Ejecuta la rutina de recuperacion de errores.
		EXECUTE dbo.usp_GetErrorInfo;
	END CATCH;

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
	SET NOCOUNT OFF

END