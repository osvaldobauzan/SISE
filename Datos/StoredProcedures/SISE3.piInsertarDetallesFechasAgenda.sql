USE [SISE_NEW]
GO
/****** Object:  StoredProcedure [SISE3].[piInsertarDetallesFechasAgenda]    Script Date: 14/06/2024 01:33:14 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================= 
-- Autor: Anabel Gonzalez
-- Fecha de Creación: 14 de Junio 2024
-- Descripción: Ejecuta el detalle para agendar una audiencia
-- Resultado: 
-- ============================================= 
CREATE PROCEDURE [SISE3].[piInsertarDetallesFechasAgenda]
(
	@pi_AsuntoNeunId INT,	
	@pi_AudienciaId INT,
	@pi_AsuntoId INT,
	@pi_AsuntoPersonas_type  [SISE3].[AsuntoPersonas_type] READONLY,
	@pi_AsuntosDetalleFechas [SISE3].[AsuntoDetalleFechas_type] READONLY,
	@pi_Empleado INT,
	@pi_NoCaptura INT,
	@pi_EsAudienciaOraltis INT = NULL,
	@pi_IdAudienciaOraltis INT = NULL,
	@pi_ParteSel INT,
	@pi_SecretarioId INT = NULL,
	@pi_NombreSolicitante VARCHAR(100) = NULL,
	@pi_MotivoConsulta VARCHAR(500) = NULL,
	@pi_FechaSolicitudAudiencia DATETIME = NULL,
	@pi_FechaAcuerdoSolicitud DATETIME = NULL
)
AS
BEGIN
    BEGIN TRY
		   DECLARE @CatOrganismoId INT = (SELECT CatOrganismoId FROM Asuntos WITH(NOLOCK) WHERE AsuntoNeunId = @pi_AsuntoNeunId AND StatusReg = 1)   
		   DECLARE @MaxAsuntoDetalleFechaId INT = (SELECT ISNULL(MAX([AsuntoDetalleFechasId]),0) 
												   FROM [AsuntosDetalleFechas] WITH(NOLOCK) WHERE [AsuntoNeunId] = @pi_AsuntoNeunId)
		   DECLARE @CatTipoAsuntoId INT = (SELECT CatTipoAsuntoId FROM Asuntos WHERE AsuntoNeunId = @pi_AsuntoNeunId AND StatusReg = 1)
		

		  CREATE TABLE #ids (id INT)
		
		  INSERT INTO #ids (id)
		  SELECT IdFecha Id FROM AUD_ResultadoCierraAudiencia WITH(NOLOCK)
		  WHERE IdTipoAudiencia = @pi_AudienciaId
		  	AND IdTipoAsunto = @CatTipoAsuntoId
		  UNION(SELECT IdHora Id FROM AUD_ResultadoCierraAudiencia WITH(NOLOCK)
		  		WHERE IdTipoAudiencia = @pi_AudienciaId
		  		AND IdTipoAsunto = @CatTipoAsuntoId)
		  UNION(SELECT Identificadores FROM (SELECT FAud, HAud, FDif, HDif
		  		FROM AUD_TipoAudienciaPorAsunto
		  		WHERE IdTipoAudiencia = @pi_AudienciaId
		  		AND IdTipoAsunto = @CatTipoAsuntoId) ta
		  		UNPIVOT (identificadores FOR Id IN (FAud, HAud, FDif, HDif))AS idsTA)	

		  DECLARE @NoBloquePadre SMALLINT

	      IF @pi_AudienciaId = 3
	      BEGIN
	      	SET @NoBloquePadre = ISNULL((SELECT MAX(adfp.NoBloquePadre) FROM AsuntosDetalleFechas adfp WITH(NOLOCK) 
										 INNER JOIN PersonasAsuntosDetalleFechas padfp  WITH(NOLOCK) ON padfp.AsuntoNeunId = adfp.AsuntoNeunId
	      						                    AND padfp.AsuntoDetalleFechasId = adfp.AsuntoDetalleFechasId 
													AND padfp.PersonaId IN (SELECT PersonaId FROM @pi_AsuntoPersonas_type) 
													AND padfp.StatusReg = 1 
										 WHERE adfp.AsuntoNeunId = @pi_AsuntoNeunId 
										 AND adfp.TipoAsuntoId IN (SELECT id FROM #ids) 
										 AND adfp.StatusReg = 1), 0)
	      END
	      ELSE
	      BEGIN
	      	SET @NoBloquePadre = ISNULL((SELECT MAX(NoBloquePadre) FROM AsuntosDetalleFechas WITH(NOLOCK) 
										 WHERE AsuntoNeunId = @pi_AsuntoNeunId AND TipoAsuntoId IN (SELECT id FROM #ids) AND StatusReg = 1), 1)
	      END


		  IF @pi_AudienciaId IN (1, 2, 88, 89, 90, 91, 92, 93, 94, 97, 98, 101,3)
		  BEGIN 
			IF @NoBloquePadre = 0
				BEGIN
						SET @NoBloquePadre = 1

					UPDATE AsuntosDetalleFechas
						SET NoBloquePadre = @NoBloquePadre
						WHERE AsuntoNeunId = @pi_AsuntoNeunId
						AND TipoAsuntoId IN (SELECT IdFecha Id FROM AUD_ResultadoCierraAudiencia WITH(NOLOCK) 
												WHERE IdTipoAudiencia = @pi_AudienciaId	
												AND IdTipoAsunto = @CatTipoAsuntoId 
												AND StatusReg = 1
											UNION(SELECT IdHora Id FROM AUD_ResultadoCierraAudiencia WITH(NOLOCK) 
												WHERE IdTipoAudiencia = @pi_AudienciaId	
												AND IdTipoAsunto = @CatTipoAsuntoId)
											UNION(SELECT Identificadores FROM(SELECT FAud, HAud, FDif, Hdif, FCel, HCel	FROM AUD_TipoAudienciaPorAsunto
														WHERE IdTipoAudiencia = @pi_AudienciaId	AND IdTipoAsunto = @CatTipoAsuntoId) ta
											UNPIVOT (identificadores FOR Id IN (FAud, HAud, FDif, Hdif, FCel, HCel))AS idsTA))
						AND NoBloquePadre = 0
			END
            
			DECLARE @IdResultado SMALLINT

			IF @pi_AudienciaId = 3
			BEGIN
				SET @IdResultado = (SELECT TOP 1 ResultadoId 
								    FROM AUD_AsuntosDetalleFechas 
									WHERE AsuntoNeunId = @pi_AsuntoNeunId 
									AND AudienciaId = @pi_AudienciaId 
									AND StatusReg = 1 
									AND Parte IN (SELECT PersonaId FROM @pi_AsuntoPersonas_type)
									ORDER BY AgendaId DESC)
			END
			ELSE
			BEGIN
				SET @IdResultado = (SELECT TOP 1 ResultadoId FROM AUD_AsuntosDetalleFechas 
									WHERE AsuntoNeunId = @pi_AsuntoNeunId AND AudienciaId = @pi_AudienciaId AND StatusReg = 1 ORDER BY AgendaId DESC)
			END

			IF @IdResultado NOT IN (2, 16, 21, 22, 24, 98, 116, 120, 124, 128, 135, 141, 147, 164, 203, 180, 185, 186, 189,24)
				SET @NoBloquePadre = @NoBloquePadre + 1
		END

		CREATE TABLE #NuevosValores (AsuntoNeunId BIGINT, AsuntoId INT, AsuntoDetalleFechasId INT, TipoAsuntoId INT, ValorCampoAsunto DATETIME, NoCaptura SMALLINT,
									NoBloque SMALLINT, NoBloquePadre SMALLINT, Consecutivo INT, EmpleadoId BIGINT, CatOrganismoId INT)

		INSERT INTO #NuevosValores
		SELECT  @pi_AsuntoNeunId AsuntoNeunId
				, @pi_AsuntoId AsuntoId
				, (ROW_NUMBER ()OVER (ORDER BY @pi_AsuntoId ASC) ) + @MaxAsuntoDetalleFechaId AsuntoDetalleFechasId
				, adf.TipoAsuntoId
				, adf.ValorCampoAsunto
				, adf.NoCaptura
				, adf.NoBloque
				, CASE WHEN @pi_AudienciaId IN (1, 2, 3, 88, 89, 90, 91, 92, 93, 94, 97, 98, 101,3) THEN @NoBloquePadre ELSE adf.NoBloquePadre END AS NoBloquePadre
				, adf.Consecutivo 
				, @pi_Empleado as EmpleadoId
				, @CatOrganismoId as CatOrganismoId
		FROM @pi_AsuntosDetalleFechas adf 
		WHERE Consecutivo IN (SELECT Consecutivo FROM @pi_AsuntoPersonas_type  WHERE NOT Consecutivo IN (SELECT Consecutivo FROM AsuntosDetalleFechas WITH(NOLOCK) WHERE Eliminar=1))
		ORDER BY adf.Consecutivo

		IF @pi_EsAudienciaOraltis = 1
		BEGIN
            DECLARE @MaxAsuntoDetalleFechasId INT, @TaFechaId INT, @TaBloquePadre INT
                                  
            SELECT TOP 1 @TaFechaId = TipoAsuntoId, @TaBloquePadre = NoBloquePadre
            FROM @pi_AsuntosDetalleFechas                                

            SELECT @MaxAsuntoDetalleFechasId = COUNT(tipoasuntoid)
            FROM asuntosdetallefechas WITH(NOLOCK)
            WHERE asuntoneunid = @pi_AsuntoNeunId   
				AND NoBloquePadre = @TaBloquePadre                           
				AND TipoAsuntoId = @TaFechaId
				AND statusreg = 1				

            IF(@MaxAsuntoDetalleFechasId = 0)
            BEGIN
                UPDATE #NuevosValores
                SET NoBloque = 1
            END
        END
		--INSERT AsuntosDetalleFechas
		 INSERT INTO AsuntosDetalleFechas WITH (ROWLOCK)(AsuntoNeunId ,AsuntoId ,AsuntoDetalleFechasId ,TipoAsuntoId ,ValorCampoAsunto ,NoCaptura,NoBloque,NoBloquePadre,Consecutivo, EmpleadoId)
         SELECT AsuntoNeunId, AsuntoId, AsuntoDetalleFechasId, TipoAsuntoId, ValorCampoAsunto, NoCaptura, NoBloque, NoBloquePadre, Consecutivo, EmpleadoId 
		 FROM #NuevosValores

		 --INSERT AUD_AsuntosDetalleFechas
		 INSERT INTO AUD_AsuntosDetalleFechas WITH (ROWLOCK)
	     (AsuntoNeunId ,AsuntoID , Parte, AudienciaId ,FechaId,ControlFecha,HoraId,ControlHora,OrganoId, EmpleadoId, StatusReg, SecretarioId )
         SELECT TOP 1 @pi_AsuntoNeunId AsuntoNeunId
				, @pi_AsuntoId AsuntoId
				, @pi_ParteSel Persona
				, @pi_AudienciaId AudienciaId
				, a.AsuntoDetalleFechasId
				, a.TipoAsuntoId ControlFecha
				, b.AsuntoDetalleFechasId
				, b.TipoAsuntoId ControlHora
				, dbo.fnDevuelveOrgano(@pi_AsuntoNeunId) OrganoId
				, @pi_Empleado EmpleadoId
				, 1 StatusReg
				, @pi_SecretarioId
        FROM #NuevosValores a 
        JOIN #NuevosValores b 
        ON a.AsuntoNeunId = b.AsuntoNeunId 
			AND a.AsuntoDetalleFechasId != b.AsuntoDetalleFechasId 
		INNER JOIN AUD_TipoAudienciaPorAsunto taaf WITH(NOLOCK)
		ON a.TipoAsuntoId IN (taaf.FAud, taaf.FDif, taaf.FCel, 18204, 18205, 18206, 18207)
            AND taaf.Activo = 1
		INNER JOIN AUD_TipoAudienciaPorAsunto taah WITH(NOLOCK)
		ON b.TipoAsuntoId IN (taah.HAud, taah.HDif, taah.HCel, 18204, 18205, 18206, 18207)
            AND taah.Activo = 1

		DECLARE @IdAudiencia BIGINT = SCOPE_IDENTITY()
		--INSERT AUD_AudienciaSolicitante
		IF @pi_AudienciaId IN (90, 91) AND ((SELECT TOP 1 TipoAsuntoId FROM @pi_AsuntosDetalleFechas) IN (16754, 16755, 17150, 17151))
		BEGIN			
			INSERT INTO AUD_AudienciaSolicitante WITH(ROWLOCK)
			(IdAgenda, NombreSolicitante)
			VALUES 
			(@IdAudiencia, @pi_NombreSolicitante)
		
			---GUARDANDO INFORMACION DE SOLICITANTE
			DECLARE @AsuntoDetalleDescripcionId INT = (SELECT ISNULL(MAX(AsuntoDetalleDescripcionId), 0) 
													   FROM [AsuntosDetalleDescripcion] WITH(NOLOCK) 
													   WHERE [AsuntoNeunId] = @pi_AsuntoNeunId) + 1
			DECLARE @TipoAsuntoDesc INT
			IF @pi_AudienciaId = 90
			BEGIN
				SET @TipoAsuntoDesc = 17766
			END
			IF @pi_AudienciaId = 91
			BEGIN
				SET @TipoAsuntoDesc = 17767
			END
			
			INSERT INTO AsuntosDetalleDescripcion WITH (ROWLOCK)
			(AsuntoNeunId, AsuntoId, AsuntoDetalleDescripcionId, TipoAsuntoId, Contenido, NoCaptura, NoBloque, NoBloquePadre, Consecutivo, FechaAlta, StatusReg, EmpleadoId)
			SELECT TOP 1 AsuntoNeunId, AsuntoId, @AsuntoDetalleDescripcionId, @TipoAsuntoDesc, @pi_NombreSolicitante, NoCaptura, NoBloque, @NoBloquePadre, Consecutivo, GETDATE(), 1, EmpleadoId FROM #NuevosValores

			INSERT INTO PersonasAsuntoDetalleDescripcion WITH (ROWLOCK)(AsuntoNeunId ,AsuntoID ,AsuntoDetalleDescripcionId ,PersonaId ,FechaAlta ,StatusReg)
			SELECT adf.AsuntoNeunId 
				, adf.AsuntoId 
				, @AsuntoDetalleDescripcionId
				, pa.PersonaId
				, GETDATE() FechaAlta
				, 1 StatusReg
			FROM #NuevosValores adf WITH(NOLOCK)
			INNER JOIN @pi_AsuntoPersonas_type pa
			ON adf.Consecutivo = pa.Consecutivo 
		END	


		------GUARDAR DATOS DE AUDIENCIA INFORMATIVA

		IF @pi_AudienciaId IN (3) AND ((SELECT TOP 1 TipoAsuntoId FROM @pi_AsuntosDetalleFechas) IN (17656, 17657))
		BEGIN			
			SET @AsuntoDetalleDescripcionId  = (SELECT ISNULL(MAX(AsuntoDetalleDescripcionId), 0) FROM [AsuntosDetalleDescripcion] WITH(NOLOCK) WHERE [AsuntoNeunId] = @pi_AsuntoNeunId) + 1
			SET @TipoAsuntoDesc=21845
			
			-----GUARDA CAMPO MOTIVO CONSULTA
			INSERT INTO AsuntosDetalleDescripcion WITH (ROWLOCK)
			(AsuntoNeunId, AsuntoId, AsuntoDetalleDescripcionId, TipoAsuntoId, Contenido, NoCaptura, NoBloque, NoBloquePadre, Consecutivo, FechaAlta, StatusReg, EmpleadoId)
			SELECT TOP 1 AsuntoNeunId, AsuntoId, @AsuntoDetalleDescripcionId, @TipoAsuntoDesc, @pi_MotivoConsulta, NoCaptura, NoBloque, NoBloquePadre, Consecutivo, GETDATE(), 1, EmpleadoId FROM #NuevosValores

			INSERT INTO PersonasAsuntoDetalleDescripcion WITH (ROWLOCK)(AsuntoNeunId ,AsuntoID ,AsuntoDetalleDescripcionId ,PersonaId ,FechaAlta ,StatusReg)
			SELECT distinct adf.AsuntoNeunId 
				, adf.AsuntoId 
				, @AsuntoDetalleDescripcionId
				, pa.PersonaId
				, GETDATE() FechaAlta
				, 1 StatusReg
			FROM #NuevosValores adf WITH(NOLOCK)
			INNER JOIN @pi_AsuntoPersonas_type pa
			ON adf.Consecutivo = pa.Consecutivo

			--------GUARDA CAMPO FECHA SOLICITUD AUDIENCIA
	        DECLARE @MaxFechaSolicitud INT = (SELECT ISNULL(MAX([AsuntoDetalleFechasId]),0) FROM [AsuntosDetalleFechas] WITH(NOLOCK) WHERE [AsuntoNeunId] = @pi_AsuntoNeunId) + 1

			INSERT INTO AsuntosDetalleFechas WITH (ROWLOCK)(AsuntoNeunId ,AsuntoId ,AsuntoDetalleFechasId ,TipoAsuntoId ,ValorCampoAsunto ,NoCaptura, NoBloque, NoBloquePadre, Consecutivo, EmpleadoId)
			SELECT TOP 1 AsuntoNeunId, AsuntoId, @MaxFechaSolicitud, 23903 , @pi_FechaSolicitudAudiencia, NoCaptura, NoBloque, NoBloquePadre, Consecutivo, EmpleadoId FROM #NuevosValores

			INSERT INTO PersonasAsuntosDetalleFechas WITH (ROWLOCK)(AsuntoNeunId ,AsuntoID ,AsuntoDetalleFechasId ,PersonaId ,FechaAlta ,StatusReg)
			SELECT TOP 1 adf.AsuntoNeunId 
				, adf.AsuntoId 
				, @MaxFechaSolicitud
				, pa.PersonaId
				, GETDATE() FechaAlta
				, 1 StatusReg
			FROM #NuevosValores adf WITH(NOLOCK)
			INNER JOIN @pi_AsuntoPersonas_type pa
			ON adf.Consecutivo = pa.Consecutivo

			-----GUARDA CAMPO FECHA ACUERDO SOLICITUD
	        DECLARE @MaxFechaAcuerdo INT = (SELECT ISNULL(MAX([AsuntoDetalleFechasId]),0) FROM [AsuntosDetalleFechas] WITH(NOLOCK) WHERE [AsuntoNeunId] = @pi_AsuntoNeunId) + 1

			INSERT INTO AsuntosDetalleFechas WITH (ROWLOCK)(AsuntoNeunId ,AsuntoId ,AsuntoDetalleFechasId ,TipoAsuntoId ,ValorCampoAsunto ,NoCaptura, NoBloque, NoBloquePadre, Consecutivo, EmpleadoId)
			SELECT TOP 1 AsuntoNeunId, AsuntoId, @MaxFechaAcuerdo, 23904, @pi_FechaAcuerdoSolicitud, NoCaptura, NoBloque, NoBloquePadre, Consecutivo, EmpleadoId FROM #NuevosValores

			INSERT INTO PersonasAsuntosDetalleFechas WITH (ROWLOCK)(AsuntoNeunId ,AsuntoID ,AsuntoDetalleFechasId ,PersonaId ,FechaAlta ,StatusReg)
			SELECT TOP 1 adf.AsuntoNeunId 
				, adf.AsuntoId 
				, @MaxFechaAcuerdo
				, pa.PersonaId
				, GETDATE() FechaAlta
				, 1 StatusReg
			FROM #NuevosValores adf WITH(NOLOCK)
			INNER JOIN @pi_AsuntoPersonas_type pa
			ON adf.Consecutivo = pa.Consecutivo
		END	

		----GUARDADO DE PARTES
		INSERT INTO PersonasAsuntosDetalleFechas WITH (ROWLOCK)(AsuntoNeunId ,AsuntoID ,AsuntoDetalleFechasId ,PersonaId ,FechaAlta ,StatusReg)
        SELECT adf.AsuntoNeunId 
            , adf.AsuntoId 
            , adf.AsuntoDetalleFechasId
            , pa.PersonaId
            , GETDATE() FechaAlta
            , 1 StatusReg
        FROM #NuevosValores adf WITH(NOLOCK)
		INNER JOIN @pi_AsuntoPersonas_type pa
        ON adf.Consecutivo = pa.Consecutivo    

		------ Guardado del IdAudiencia en SISE, campo en base al cual se obtiene el NoBloque y NoBloquePadre para su manipulación (Altas, bajas y modificaciones)----------
        ------ Guardado del Status de la audiencia -------------------------------------------------------------------------------------------------------------------------
		
		IF @pi_EsAudienciaOraltis = 1                           
        BEGIN
            DECLARE @noBloque INT
            DECLARE @TipoAsuntoId INT
            DECLARE @TipoOrganismoId INT
            DECLARE @FechaAud INT
                                  
            SELECT TOP 1 @noBloque = NoBloque, @noBloquePadre = noBloquePadre, @FechaAud = TipoAsuntoId FROM #NuevosValores

            SELECT @TipoAsuntoId = asun.cattipoasuntoid, @TipoOrganismoId = corg.cattipoorganismoid 
            FROM catorganismos corg WITH(NOLOCK)
            LEFT JOIN asuntos asun WITH(NOLOCK)
            ON asun.catorganismoid = corg.catorganismoid
            WHERE asun.asuntoneunid = @pi_AsuntoNeunId

            DECLARE @TaIdAudiencia INT
            DECLARE @TaIdStatus INT

            SELECT @TaIdAudiencia = TaIdAudiencia, @TaIdStatus = TaIdStatus 
            FROM AUD_TipoAudienciaPorAsunto with(nolock)
            WHERE (FAud = @FechaAud OR HAud = @FechaAud)
				AND cattipoorganismoid = @TipoOrganismoId
				AND idtipoasunto = @TipoAsuntoId

            DECLARE @MaxAsuntoDetalleNumerosId INT
            SET @MaxAsuntoDetalleNumerosId = ( SELECT ISNULL(MAX([AsuntoDetalleNumerosId]),0) FROM [AsuntosDetalleNumeros] WITH(NOLOCK) WHERE [AsuntosNeunId] = @pi_AsuntoNeunId AND [AsuntoID] = @pi_AsuntoId )

            DECLARE @MaxAsuntoDetalleCatalogosId INT
            SET @MaxAsuntoDetalleCatalogosId = (SELECT ISNULL(MAX([AsuntoDetalleCatalogosId]),0) FROM [AsuntosDetalleCatalogos] WITH(NOLOCK) WHERE [AsuntosNeunId] = @pi_AsuntoNeunId AND [AsuntoID] = @pi_AsuntoId )

            INSERT INTO AsuntosDetalleNumeros WITH (ROWLOCK) (AsuntosNeunId,AsuntoID,AsuntoDetalleNumerosId,TipoAsuntoId,NumeroCampoAsunto,NoCaptura,NoBloque,NoBloquePadre,FechaAlta,StatusReg)
            VALUES (@pi_AsuntoNeunId, 1 ,@MaxAsuntoDetalleNumerosId + 1, @TaIdAudiencia, @pi_IdAudienciaOraltis, @pi_NoCaptura, @noBloque, @noBloquePadre, GETDATE(), 1)

            -------820 es el id del catálogo de status de audiencias de ORALTIS
            INSERT INTO AsuntosDetalleCatalogos WITH (ROWLOCK) (AsuntosNeunId,AsuntoID,AsuntoDetalleCatalogosId,TipoAsuntoId,CatTipoCatalogoAsuntoId,CatCatalogoAsuntoId,NoCaptura,NoBloque,NoBloquePadre,FechaAlta,StatusReg)
            VALUES (@pi_AsuntoNeunId, 1, @MaxAsuntoDetalleCatalogosId + 1, @TaIdStatus, 820, 1, @pi_NoCaptura, @noBloque, @noBloquePadre, GETDATE(), 1)
        END

		DROP TABLE #NuevosValores

	END TRY
    BEGIN CATCH
		IF OBJECT_ID(N'tempdb..#NuevosValores', N'U') IS NOT NULL 
		BEGIN
				PRINT 'Existe'
				DROP TABLE #NuevosValores
		END
        EXECUTE dbo.usp_GetErrorInfo;
    END CATCH;
END;