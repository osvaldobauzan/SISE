USE [SISE_NEW]
GO

/****** Object:  StoredProcedure [SISE3].[piTableroProyectoInsertar]    Script Date: 22/04/2024 04:12:33 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: Mario A Hernández Román
-- Create date: 20/03/2024
-- Description: Registra un nuevo proyecto de Sentencia
-- Basado en: [SISE3].[piInsertarNotificacionesOficio]
/*
EXEC [SISE3].[piTableroProyectoInsertar]
	180,
	5232,
	34556,
	45712,
	1,
	2,
	'Prueba de insert',
	45712,
	'proyecto_sentencia.docx',
	'localhost'
*/
-- =============================================

CREATE PROCEDURE [SISE3].[piTableroProyectoInsertar]
	@pi_CatOrganismoId INT,
	@pi_AsuntoNeunId BIGINT,
	@pi_iTitular INT,
	@pi_iSecretario INT,
	@pi_iTipoSentenciaId INT,
	@pi_iSentidoId INT,
	@pi_sSintesis TEXT,
	@pi_iRegistroEmpleadoId INT,
	@pi_sNombreArchivoReal VARCHAR(500),
	@pi_sIPUsuario VARCHAR(100)

AS BEGIN

	SET NOCOUNT ON

	BEGIN  --Se revisa si se puede ingestar, en caso de que no, arroja error y motivo de no ingesta
		DECLARE @PuedeIngestar BIT 
		DECLARE @MotivoNoIngesta VARCHAR(1000)
		DECLARE @iEstado INT
		DECLARE @iVersionVI INT

		SELECT 
				@MotivoNoIngesta = MotivoNoIngesta, 
				@PuedeIngestar = PuedeIngestar,
				@iEstado = iEstado,
				@iVersionVI = iVersion
		FROM
			[SISE3].[fnTableroProyectoValidaIngesta](
				@pi_CatOrganismoId,
				2,  -- Cuaderno
				NULL,
				NULL,
				@pi_AsuntoNeunId
			) 
	END
		 
	IF (@PuedeIngestar = 0 
		AND @iEstado <> 1
		AND @iVersionVI < 1
		) THROW 51000, @MotivoNoIngesta, 1

	ELSE 
	  BEGIN TRY

			DECLARE @pi_fFechaProyecto DATETIME2=sysdatetime() 
			DECLARE @pi_fFechaAlta DATETIME2=sysdatetime() 
			DECLARE @iVersion INT
			DECLARE @pkProyectoArchivoId BIGINT
			DECLARE @sNombreArchivo VARCHAR(200)
			DECLARE @pkProyectoId BIGINT
			DECLARE @extension VARCHAR(64)
			DECLARE @kid_ruta INT
			DECLARE @sAnioRuta VARCHAR(50)

			
			SELECT 
			  @iVersion = iVersion,
			  @extension = extension,
			  @kid_ruta = kid_ruta,
			  @sNombreArchivo = sNombreArchivo 
			FROM 
				[SISE3].[fnTableroProyectoDocumento](
					@pi_CatOrganismoId, 
					@pi_AsuntoNeunId,
					@pi_sNombreArchivoReal, 
					'proy', 
					NULL
				)

			INSERT INTO [SISE3].[ProyectoArchivo](
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

			SET @pkProyectoArchivoId = @@IDENTITY
		
			INSERT INTO [SISE3].[Proyecto] WITH(ROWLOCK)(
				[CatOrganismoId],
				[AsuntoNeunId],
				[fFechaProyecto],
				[iTitular],
				[iSecretario],
				[iTipoSentenciaId],
				[iSentidoId],
				[fkProyectoVersionArchivoId],
				[sSintesis],
				[iVersion],
				[fFechaAlta],
				[iRegistroEmpleadoId],
				[iEstado],
				[fkCorreccionArchivoId],
				[sCorreccionComentario],
				[fFechaActualiza],
				[iStatusReg]
			)
				VALUES (
					@pi_CatOrganismoId
					,@pi_AsuntoNeunId
					,@pi_fFechaProyecto   -- Si es verion 0, es la fecha GETDATE ELSE fechaProyecto version anterior
					,@pi_iTitular
					,@pi_iSecretario
					,@pi_iTipoSentenciaId
					,@pi_iSentidoId
					,@pkProyectoArchivoId
					,@pi_sSintesis
					,ISNULL(@iVersion, 0) + 1
					,@pi_fFechaAlta
					,@pi_iRegistroEmpleadoId
					,2  -- un nuevo proyecto o nueva version, se va para iEstado = 2 -> Para revisión
					,NULL
					,NULL
					,NULL
					,1
				)

			SET @pkProyectoId = @@IDENTITY

			SET @sAnioRuta = (
				SELECT 
					CAST(YEAR(fFechaProyecto) AS VARCHAR(4))
				FROM 
					SISE3.Proyecto  WITH(NOLOCK)
				WHERE 
					pkProyectoId = @pkProyectoId
			)

			UPDATE [SISE3].[ProyectoArchivo]
				 SET [sAnioRuta] = @sAnioRuta
			 WHERE 
				pkProyectoArchivoId = @pkProyectoArchivoId 

			SELECT
				p.*,
				pa.[pkProyectoArchivoId],
        pa.[sNombreArchivo],
        pa.[sNombreArchivoReal],
        pa.[iRutaArchivoNAS],
        pa.[sIPUsuario],
				crc.sRuta,
				crc.iEscritura,
				crc.iGrupo,
				crc.sDescripcion,
				crc.iTipoArchivo,
				crc.sTipoArchivoDesc,
				CAST(YEAR(p.fFechaProyecto) AS VARCHAR(4)) AS sAnioRuta,
				aud.TipoCuaderno,
				aud.sTipoCuaderno
			FROM
				SISE3.Proyecto p  WITH(NOLOCK)
				LEFT JOIN SISE3.ProyectoArchivo pa  WITH(NOLOCK) ON
					p.fkProyectoVersionArchivoId = pa.pkProyectoArchivoId
				LEFT JOIN [dbo].[CAT_RutasChunk] crc  WITH(NOLOCK) ON
					crc.kId = pa.iRutaArchivoNAS
				LEFT JOIN [SISE3].[fnTableroProyectoTienenAudiencia](@pi_CatOrganismoId, 'Celebrada') aud  ON
				p.AsuntoNeunId=aud.AsuntoNeunId

			WHERE
				p.pkProyectoId = @pkProyectoId

		END TRY

	BEGIN CATCH
		-- Ejecuto ROLLBACK solo en caso de error
		IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;

		-- Ejecuta la rutina de recuperacion de errores.
		EXECUTE dbo.usp_GetErrorInfo;
	END CATCH;

  -- Completo mi transaccion
  IF @@TRANCOUNT > 0 COMMIT TRANSACTION;

  SET NOCOUNT OFF

END
GO


