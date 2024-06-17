USE [SISE_NEW]
GO
/****** Object:  StoredProcedure [SISE3].[paTableroProyectoVersion]    Script Date: 18/04/2024 02:50:33 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: Fanny P. Lemus García
-- Create date: 03/04/2024
-- Description: Actualiza la tabla proyecto con la información del documento corregido y comentarios
-- Basado en: [SISE3].[piTableroProyectoInsertar]
/*

EXEC [SISE3].[paTableroProyectoVersion]
	952, 
	'este es un comentario',
		'comentarios.pdf',
	3,
	'localhost'
*/
-- =============================================

ALTER   PROCEDURE [SISE3].[paTableroProyectoVersion]
	@pi_pkProyectoId BIGINT, 
	@pi_ComentarioTitular VARCHAR(500)=NULL,
	@pi_ArchivoComentarios VARCHAR(200)=NULL,
	@pi_iEstado INT,
	@pi_sIPUsuario VARCHAR(100)


	AS BEGIN

	SET NOCOUNT ON

	  BEGIN TRY

			DECLARE	@pi_CatOrganismoId INT
			DECLARE @pi_AsuntoNeunId BIGINT
			DECLARE @pi_fFechaActualiza DATETIME2=sysdatetime() 
			DECLARE @iVersion INT
			DECLARE @extension VARCHAR(64)
			DECLARE @kid_ruta INT
			DECLARE @sNombreArchivo VARCHAR(200)
			DECLARE @pi_iRegistroEmpleadoId INT
			DECLARE @sAnioRuta VARCHAR(50)
			DECLARE @fkCorreccionArchivoId INT = NULL

			-- Se declaran variables necesarias para ingestar un nuevo registro de corrección
		  DECLARE @pi_iTitular INT
		  DECLARE @pi_iSecretario INT
		  DECLARE @pi_iTipoSentenciaId INT
		  DECLARE @pi_iSentidoId INT
		  DECLARE @pi_sSintesis VARCHAR (500)
		  DECLARE @pi_sNombreArchivoReal VARCHAR(500)
			DECLARE @pi_fFechaProyecto DATETIME2
			DECLARE @pi_fFechaAlta DATETIME2
			DECLARE @pkProyectoArchivoId INT
			DECLARE @pi_pkProyectoIdNew BIGINT
		
			 	 			
			-- Revisamos que exista el pkProyectoId en la tabla Proyecto
			IF @pi_pkProyectoId NOT IN 
					( SELECT pkProyectoId
					FROM [SISE3].[Proyecto] WITH(NOLOCK)
					)
				 THROW 51000, 'NO EXISTE ID DEL PROYECTO', 1

			ELSE
			BEGIN
					 				
			SELECT 
				@pi_CatOrganismoId = CatOrganismoId,
				@pi_AsuntoNeunId = AsuntoNeunId,
				@pi_iTitular = iTitular,
				@pi_iSecretario = iSecretario,
				@pi_iTipoSentenciaId = iTipoSentenciaId,
				@pi_iSentidoId = iSentidoId,
				@pi_sSintesis = sSintesis,
				@pi_fFechaProyecto = fFechaProyecto,
				@pi_fFechaAlta = fFechaAlta,
				@pi_iRegistroEmpleadoId = iRegistroEmpleadoId,
			  @sAnioRuta = CAST(YEAR(fFechaProyecto) AS VARCHAR(4)),
			  @pkProyectoArchivoId  = fkProyectoVersionArchivoId,
				@iVersion = iVersion
			FROM 
				[SISE3].[Proyecto] WITH(NOLOCK)
			WHERE 
				pkProyectoId = @pi_pkProyectoId

				-- Solo se pueden hacer comentarios sobre la version actual, no se permiten comenrarios sobre una version anterior,
		--	IF @iVersion <>1 AND @iVersion <
		--		( SELECT
		--			MAX(iVersion)
		--		FROM 
		--			[SISE3].[Proyecto] WITH(NOLOCK)
		--		WHERE 
		--			AsuntoNeunId=@pi_AsuntoNeunId
		--		)
		--	THROW 51000, 'NO PUEDE HACER CORRECCIONES SOBRE UNA VERSION ANTERIOR', 1
		
			IF @pi_pkProyectoId NOT IN 
			( SELECT
				MAX(pkProyectoId)
			FROM 
				[SISE3].[Proyecto] WITH(NOLOCK)
			WHERE 
				AsuntoNeunId=@pi_AsuntoNeunId
				AND iVersion=	@iVersion
			)
		THROW 51000, 'NO PUEDE HACER CORRECCIONES SOBRE UNA VERSION ANTERIOR', 1




			SELECT 
			  @extension = extension,
			  @kid_ruta = kid_ruta,
			  @sNombreArchivo = sNombreArchivo 
			FROM 
				[SISE3].[fnTableroProyectoDocumento](@pi_CatOrganismoId, @pi_AsuntoNeunId, @pi_ArchivoComentarios, 'corr', @pi_pkProyectoId)


		IF @pi_ArchivoComentarios IS NOT NULL

			BEGIN
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
				@pi_ArchivoComentarios,
				@kid_ruta,
				@pi_fFechaActualiza,
				@pi_iRegistroEmpleadoId,
				@pi_sIPUsuario,
				1,
				@sAnioRuta,
				@pi_CatOrganismoId
			)

			SET @fkCorreccionArchivoId = @@IDENTITY
		
			END
		
		--Actualizamos el statusReg a 0 del proyecto al que se le insertará corrección y se inserta un nuevo registro con status 1. 
		--Además, borra los comentarios anteriores asociados a la versión (solo se tiene un´registro activo por versión)

				UPDATE [SISE3].[Proyecto]
				SET  [iStatusReg]=0
				WHERE 
					pkProyectoId=@pi_pkProyectoId
					OR
					(fkProyectoVersionArchivoId=@pkProyectoArchivoId
					AND iVersion=@iVersion)
		

		--SET IDENTITY_INSERT [SISE3].[Proyecto] ON
		INSERT INTO [SISE3].[Proyecto]
					([CatOrganismoId]
           ,[AsuntoNeunId]
           ,[fFechaProyecto]
           ,[iTitular]
           ,[iSecretario]
           ,[iTipoSentenciaId]
           ,[iSentidoId]
           ,[fkProyectoVersionArchivoId]
           ,[sSintesis]
           ,[iVersion]
           ,[fFechaAlta]
           ,[iRegistroEmpleadoId]
           ,[iEstado]
           ,[fkCorreccionArchivoId]
           ,[sCorreccionComentario]
           ,[fFechaActualiza]
           ,[iStatusReg])
     VALUES
           (@pi_CatOrganismoId,
			      @pi_AsuntoNeunId,
						@pi_fFechaProyecto,
						@pi_iTitular,
						@pi_iSecretario,
						@pi_iTipoSentenciaId,
						@pi_iSentidoId,
						@pkProyectoArchivoId,
						@pi_sSintesis,
						@iVersion,
						@pi_fFechaAlta,
						@pi_iRegistroEmpleadoId,
						@pi_iEstado,
			      @fkCorreccionArchivoId,
			      @pi_ComentarioTitular,
            @pi_fFechaActualiza,
						1		 
					 )

					SET  @pi_pkProyectoId = @@IDENTITY
			--SET IDENTITY_INSERT [SISE3].[Proyecto] OFF 
			END

		PRINT('LA CORRECCIÓN SE HA INGESTADO CORRECTAMENTE')


		
			SELECT
			  p.CatOrganismoId,
				co.NombreOficial as CatOrganismo,
        p.AsuntoNeunId,
				exped.AsuntoAlias,
				exped.CatTipoAsuntoId,
				exped.CatTipoAsunto,
        p.pkProyectoId,
        p.fFechaProyecto,
        p.iTitular,
        p.iSecretario,
        p.iTipoSentenciaId,
        p.iSentidoId,
        p.fkProyectoVersionArchivoId,
        p.sSintesis,
        p.iVersion,
        p.fFechaAlta,
        p.iRegistroEmpleadoId,
        p.iEstado,
        p.fkCorreccionArchivoId,
        p.sCorreccionComentario,
        p.fFechaActualiza,
        p.iStatusReg,
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
				TipoCuaderno=1,
				sTipoCuaderno='Principal',
				CAST(YEAR(p.fFechaProyecto) AS VARCHAR(4)) AS sAnioRuta
			FROM
				SISE3.Proyecto p WITH(NOLOCK) CROSS APPLY SISE3.fnExpediente (@pi_AsuntoNeunId) exped 
				LEFT JOIN [SISE_NEW].[dbo].[CatOrganismos] co WITH(NOLOCK) ON
				p.CatOrganismoId = co.CatOrganismoId
				LEFT JOIN SISE3.ProyectoArchivo pa WITH(NOLOCK) ON
					p.fkCorreccionArchivoId = pa.pkProyectoArchivoId
				LEFT JOIN [dbo].[CAT_RutasChunk] crc WITH(NOLOCK) ON
					crc.kId = pa.iRutaArchivoNAS
			WHERE
				p.pkProyectoId = @pi_pkProyectoId


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