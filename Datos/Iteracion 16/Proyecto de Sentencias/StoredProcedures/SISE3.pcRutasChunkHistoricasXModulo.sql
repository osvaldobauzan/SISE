USE [SISE_NEW]
GO

/****** Object:  StoredProcedure [SISE3].[pcRutasChunkHistoricasXModulo]    Script Date: 22/04/2024 09:23:04 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- ============================================= 
-- Author: Christian Araujo - MS
-- Alter date: 09/10/2023 
-- Description: Se utiliza para devolver las rutas de los archivos que se van a generar en el sistema
-- Según el tipo de archivo que se requiera generar
-- Basado en: pcWSRutasChunk
--execute [SISE3].[pcRutasChunkHistoricasXModulo] 'Oficialia'
-- ============================================= 
CREATE PROCEDURE [SISE3].[pcRutasChunkHistoricasXModulo] 
(                                            
@pi_Modulo  VARCHAR(50)
)						
AS
DECLARE 
@ErrorMessage  NVARCHAR(4000),
@ErrorSeverity INT,
@ErrorState    INT,
@Grupo        INT=NULL

			--IF @pi_Modulo = 'Oficialia'
            BEGIN TRY 
					IF @pi_Modulo = 'Oficialia'
					BEGIN
						select KId,iGrupo,sDescripcion,iTipoArchivo,sTipoArchivoDesc,sRuta,iEscritura from CAT_RutasChunk rc with(nolock) where rc.iGrupo IN (2, 9, 16) and rc.iEscritura IN(1, 0)  order by fFechaAlta desc
					End

					ELSE IF @pi_Modulo = 'Tramite'
					BEGIN
						select KId,iGrupo,sDescripcion,iTipoArchivo,sTipoArchivoDesc,sRuta,iEscritura from CAT_RutasChunk rc with(nolock) where rc.iGrupo = 1 and rc.iEscritura IN (1, 0) order by fFechaAlta desc
					End

					ELSE IF @pi_Modulo = 'Proyectos'
					BEGIN
						select KId,iGrupo,sDescripcion,iTipoArchivo,sTipoArchivoDesc,sRuta,iEscritura from CAT_RutasChunk rc with(nolock) where rc.iGrupo = 15 and rc.iEscritura IN (1, 0) order by fFechaAlta desc
					END

					ELSE
					BEGIN
						THROW 51000,'Error módulo no configurado',1;
					END

			END TRY
            ------Manejo de Errores.
			BEGIN CATCH
			  
			   SELECT 
					  @ErrorMessage = ERROR_MESSAGE(),
                      @ErrorSeverity = ERROR_SEVERITY(),                 
                      @ErrorState =ERROR_STATE();

			 --RAISERROR (@ErrorMessage,@ErrorSeverity,@ErrorState);
			 THROW 51000,@ErrorMessage,1;

			END CATCH

GO


