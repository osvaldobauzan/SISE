USE [SISE_NEW]
GO
/****** Object:  StoredProcedure [SISE3].[peTableroProyectoCancelaCorreccion]    Script Date: 18/04/2024 02:55:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Fanny Paulet Lemus García
-- Alter date: 09/04/2024
-- Basado en: [SISE3].[peTableroProyectoCancelaVersion]
-- Objetivo: Elimina  el registro a partir del pi_pkProyectoId cambiando el estatus  de las tablas Proyecto y ProyectoArchivo y revive el proyecto anterior al que se le hizo corrección
-- EXEC [SISE3].peTableroProyectoCancelaCorreccion 418
-- =============================================
CREATE    PROCEDURE [SISE3].[peTableroProyectoCancelaCorreccion]
		@pi_pkProyectoId BIGINT
AS  BEGIN 
  	
		DECLARE @pkProyectoArchivoId BIGINT
		DECLARE @fkProyectoVersionArchivoId BIGINT
		
			SELECT 
				@pkProyectoArchivoId = fkCorreccionArchivoId, @fkProyectoVersionArchivoId = fkProyectoVersionArchivoId
			FROM 
				[SISE_NEW].[SISE3].[Proyecto]
			WHERE 
				pkProyectoId=@pi_pkProyectoId
	
	
		--Revisar si la variable fkProyectoVersionArchivoId asociada al pkProyectoId existe en la tabla ProyectoArchivo
  	IF NOT EXISTS (
			SELECT 
				pkProyectoArchivoId
			FROM 
				[SISE_NEW].[SISE3].[ProyectoArchivo]
			WHERE 
				[SISE_NEW].[SISE3].[ProyectoArchivo].pkProyectoArchivoId = @pkProyectoArchivoId
		)
		THROW 51000,'Error, versión no existe en ProyectoArchivo',1;
 
  
    BEGIN TRY

		BEGIN TRAN	
		--Borramos la versión con comentarios de la tabla Archivo
			UPDATE [SISE_NEW].[SISE3].[ProyectoArchivo]
				SET 
					[SISE_NEW].[SISE3].[ProyectoArchivo].iStatusReg = 0 
				WHERE 
					[SISE_NEW].[SISE3].[ProyectoArchivo].pkProyectoArchivoId = @pkProyectoArchivoId

			Print 'Versión eliminada exitosamente de ProyectoArchivo'
	

			--Revivimos el proyecto original sin comentarios
		
			UPDATE [SISE3].[Proyecto] 
				SET
					[SISE3].[Proyecto].iStatusReg = 1
				WHERE 
					fkProyectoVersionArchivoId = @fkProyectoVersionArchivoId
				AND
				iStatusReg = 0
				AND
				fkCorreccionArchivoId IS NULL

			--Borramos la versión con comentarios
	
	   UPDATE [SISE3].[Proyecto]
      SET
    	 [iStatusReg]  = 0
    	WHERE 
			 [pkProyectoId]=@pi_pkProyectoId

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