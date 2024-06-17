USE [SISE_NEW]
GO

/****** Object:  StoredProcedure [SISE3].[pcRutasChunkXModulo]    Script Date: 22/04/2024 09:23:15 a. m. ******/
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
--execute [SISE3].[pcRutasChunkXModulo] 'Actuaria'
-- ============================================= 
CREATE PROCEDURE [SISE3].[pcRutasChunkXModulo] 
(                                            
@pi_Modulo  VARCHAR(MAX)
)						
AS
DECLARE 
@ErrorMessage  NVARCHAR(4000),
@ErrorSeverity INT,
@ErrorState    INT,
@Grupo        INT=NULL

			--IF @pi_Modulo = 'Oficialia'
            BEGIN TRY 
					/*Variable temporal por problema en NAS
					*/
					declare @iEscritura bit
						set @iEscritura = 1

					IF @pi_Modulo = 'Oficialia'
					BEGIN
						
						select KId,iGrupo,sDescripcion,iTipoArchivo,sTipoArchivoDesc,sRuta,iEscritura from CAT_RutasChunk rc with(nolock) where rc.iGrupo = 2 and rc.iEscritura = 1 and StatusReg = 1 order by fFechaAlta desc
						
						--select Top 1 KId,iGrupo,sDescripcion,iTipoArchivo,sTipoArchivoDesc,sRuta,@iEscritura as iEscritura/*iEscritura*/ from CAT_RutasChunk rc with(nolock) where rc.iGrupo = 2 and rc.iEscritura = 0 and StatusReg = 1 order by fFechaAlta desc
					End

					ELSE IF @pi_Modulo = 'Tramite'
					BEGIN
						
						select KId,iGrupo,sDescripcion,iTipoArchivo,sTipoArchivoDesc,sRuta,iEscritura from CAT_RutasChunk rc with(nolock) where rc.iGrupo = 1 and rc.iEscritura = 1 and StatusReg = 1 order by fFechaAlta desc
						
						--select top 1 KId,iGrupo,sDescripcion,iTipoArchivo,sTipoArchivoDesc,sRuta,@iEscritura as iEscritura/*iEscritura*/ from CAT_RutasChunk rc with(nolock) where rc.iGrupo = 1 and rc.iEscritura = 0 and StatusReg = 1 order by fFechaAlta desc
					End

                    ELSE IF @pi_Modulo = 'Actuaria'
					BEGIN
						
						select KId,iGrupo,sDescripcion,iTipoArchivo,sTipoArchivoDesc,sRuta,iEscritura from CAT_RutasChunk rc with(nolock) where rc.iGrupo = 3 and rc.iEscritura = 1 and StatusReg = 1 order by fFechaAlta desc
						
						--select top 1 KId,iGrupo,sDescripcion,iTipoArchivo,sTipoArchivoDesc,sRuta,@iEscritura as iEscritura/*iEscritura*/ from CAT_RutasChunk rc with(nolock) where rc.iGrupo = 1 and rc.iEscritura = 0 and StatusReg = 1 order by fFechaAlta desc
					End
					ELSE IF @pi_Modulo = 'Proyectos'
					BEGIN
						
						select KId,iGrupo,sDescripcion,iTipoArchivo,sTipoArchivoDesc,sRuta,iEscritura from CAT_RutasChunk rc with(nolock) where rc.iGrupo = 15 and rc.iEscritura = 1 and StatusReg = 1 order by fFechaAlta desc
						
						--select top 1 KId,iGrupo,sDescripcion,iTipoArchivo,sTipoArchivoDesc,sRuta,@iEscritura as iEscritura/*iEscritura*/ from CAT_RutasChunk rc with(nolock) where rc.iGrupo = 1 and rc.iEscritura = 0 and StatusReg = 1 order by fFechaAlta desc
					End
					ELSE
					BEGIN
						select KId,iGrupo,sDescripcion,iTipoArchivo,sTipoArchivoDesc,sRuta,iEscritura from CAT_RutasChunk rc with(nolock) where rc.iGrupo = 0 and rc.iEscritura = 1 and StatusReg = 1 order by fFechaAlta desc
						
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


