USE [SISE_NEW]
GO
/****** Object:  UserDefinedFunction [SISE3].[fnTableroProyectoV3]    Script Date: 18/04/2024 02:58:26 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: Mario A Hernández Román
-- Create date: 11/03/2024
-- Version: 2
-- Description:	Arma e integra las fuentes de información para el
--		Tablero SISE3.Proyecto con funcionalidades de filtrado
-- SELECT * FROM [SISE3].[fnTableroProyectoV3](180, '1,2,3,4,5,6', NULL, NULL, NULL)
-- SELECT * FROM [SISE3].[fnTableroProyectoV3](180, '1,2,3,4,5,6', NULL, '2024-03-26', '2024-05-26')
-- =============================================

CREATE FUNCTION [SISE3].[fnTableroProyectoV3](	
	@pi_CatOrganismoId INT,
	@pi_Estados VARCHAR(30)=NULL,  -- Define el Estado de un Proyecto
	@pi_texto VARCHAR(250)=NULL,
	@pi_FechaPresentacionIni DATE=NULL,
	@pi_FechaPresentacionFin DATE=NULL
)

RETURNS @TableroProyectoMod TABLE (
	lastVersion INT,
	pkProyectoId BIGINT,
	AsuntoNeunId BIGINT, 
	AsuntoAlias VARCHAR(50),
	NumeroAlias BIGINT,
	CatTipoOrganismoId INT,
	CatOrganismoId INT,
	CatTipoAsuntoId INT,
	CatTipoAsunto VARCHAR(150),
	TipoProcedimiento INT,
	NombreCorto VARCHAR(10),
	TieneAudiencia BIT,
	FechaAudiencia DATE,
	TieneArchivoAudiencia BIT,
	ArchivoAudiencia VARCHAR(150),
	ResultadoAudiencia VARCHAR(250),
	TipoAudiencia VARCHAR(250),
	SecretarioId INT,
	Secretario VARCHAR(250),
	Mesa VARCHAR(150),
	TipoCuaderno INT,
	sTipoCuaderno VARCHAR(150),
	TieneArchivoProyecto BIT,
	ArchivoProyecto VARCHAR(250),
	FechaCargaProyecto DATE,
	NumeroVersionProyecto INT,
	EstadoProyecto INT,
	sEstadoProyecto VARCHAR(50),
	FechaEstadoProyecto DATE,
	SentidoProyecto INT,
	sSentido VARCHAR(50),
	TipoSentencia INT,
	sTipoSentencia VARCHAR(50)
)
AS BEGIN

	INSERT INTO @TableroProyectoMod
		SELECT
			pv.lastVersion,
			pv.pkProyectoId,
			aud.*,
			TieneArchivoProyecto = CAST(1 AS BIT),
			ArchivoProyecto = 'archivoAudiencia.docx',
			FechaCargaProyecto = pv.fFechaProyecto,
			NumeroVersionProyecto = ISNULL(pv.iVersion, 0),
			EstadoProyecto = ISNULL(pv.iEstado, 1),
			sEstadoProyecto = ISNULL(cpe.sProyectoEstado, 'Sin proyecto'),
			FechaEstadoProyecto = CAST('2014-01-02 00:00:00.000' AS DATETIME),
			SentidoProyecto = pv.iSentidoId,
			sSentido = ISNULL(cs.sSentido, ''),
			TipoSentencia = pv.iTipoSentenciaId,
			sTipoSentencia = ISNULL(cts.sTipoSentencia, '')
		FROM
			[SISE3].[fnTableroProyectoTienenAudiencia](	
				@pi_CatOrganismoId,
				'Celebrada'
			)	aud
			LEFT JOIN (
				SELECT 
					RANK() OVER (
						PARTITION BY AsuntoNeunId 
						ORDER BY iVersion DESC
					) lastVersion, *
				FROM
					[SISE3].[Proyecto] py WITH(NOLOCK) 
				WHERE
					py.iStatusReg = 1
			) pv ON
				aud.AsuntoNeunId = pv.AsuntoNeunId
			LEFT JOIN [SISE3].[CAT_ProyectoEstado] cpe WITH(NOLOCK)  ON
				cpe.pkProyectoEstadoId = pv.iEstado
			LEFT JOIN [SISE3].[CAT_Sentido] cs WITH(NOLOCK)  ON
				cs.pkSentidoId = pv.iSentidoId
			LEFT JOIN [SISE3].[CAT_TipoSentencia] cts WITH(NOLOCK)  ON
				cts.pkTipoSentenciaId = pv.iTipoSentenciaId
		WHERE
			pv.lastVersion = 1  -- 1 = True
			OR pv.lastVersion IS NULL  -- No tiene versión -> No tiene proyecto
			AND aud.CatOrganismoId = @pi_CatOrganismoId;
		
		--IMPORTANTE considerar que en el tablero Proyecto los asuntos NeunId deben ser únicos
		--Este caso es relevante si se tiene que considerar el tipo de cuaderno al momento de agregar un uevo proyecto sentencia

		WITH RegistrosSubidosSinAudiencia AS ( SELECT
			RANK() OVER (
						PARTITION BY pv.AsuntoNeunId 
						ORDER BY iVersion DESC
					) lastVersion, 
			pv.pkProyectoId,
			ast.AsuntoNeunId,
			ast.AsuntoAlias,
		  ast.NumeroAlias,
		  ast.CatTipoOrganismoId,
		  ast.CatOrganismoId,
		  ast.CatTipoAsuntoId,
		  ast.CatTipoAsunto,
		  TipoProcedimiento = ast.CatTipoProcedimiento,
		  ast.NombreCorto,
			TieneAudidencia = CAST(0 AS BIT),
		  FechaAudiencia = NULL,
		  TieneArchivoAudiencia = CAST(0 AS BIT),
		  ArchivoAudiencia = NULL,
		  ResultadoAudiencia = ISNULL(NULL, ''),
		  TipoAudiencia = ISNULL(NULL, ''),
		  SecretarioId = ISNULL(NULL, ''),
		  Secretario = '',
		  Mesa = ISNULL(NULL, 'Sin asignar'),
		  TipoCuaderno = 1,
		  sTipoCuaderno = '',
			TieneArchivoProyecto = CAST(1 AS BIT),
			ArchivoProyecto = 'archivoAudiencia.docx',
			FechaCargaProyecto = pv.fFechaProyecto,
			NumeroVersionProyecto = ISNULL(pv.iVersion, 0),
			EstadoProyecto = ISNULL(pv.iEstado, 1),
			sEstadoProyecto = ISNULL(cpe.sProyectoEstado, 'Sin proyecto'),
			FechaEstadoProyecto = CAST('2014-01-02 00:00:00.000' AS DATETIME),
			SentidoProyecto = pv.iSentidoId,
			sSentido = ISNULL(cs.sSentido, ''),
			TipoSentencia = pv.iTipoSentenciaId,
			sTipoSentencia = ISNULL(cts.sTipoSentencia, '')

        FROM
            [SISE3].[Proyecto] pv WITH(NOLOCK) 
			CROSS APPLY SISE3.fnExpediente(pv.AsuntoNeunId) ast
            LEFT JOIN [SISE3].[CAT_ProyectoEstado] cpe WITH(NOLOCK)  ON
                cpe.pkProyectoEstadoId = pv.iEstado
            LEFT JOIN [SISE3].[CAT_Sentido] cs WITH(NOLOCK) ON
                cs.pkSentidoId = pv.iSentidoId
            LEFT JOIN [SISE3].[CAT_TipoSentencia] cts WITH(NOLOCK) ON
                cts.pkTipoSentenciaId = pv.iTipoSentenciaId
        WHERE
            pv.AsuntoNeunId NOT IN (
                SELECT 
                    AsuntoNeunId
                FROM
                    [SISE3].[fnTableroProyectoTienenAudiencia]( 
                        @pi_CatOrganismoId,
                        'Celebrada'
                )
            )
        AND pv.iStatusReg = 1
		AND pv.CatOrganismoId = @pi_CatOrganismoId
		
		)

	INSERT INTO @TableroProyectoMod
		SELECT 
			lastVersion, 
			pkProyectoId,
			AsuntoNeunId,
			AsuntoAlias,
		  NumeroAlias,
		  CatTipoOrganismoId,
		  CatOrganismoId,
		  CatTipoAsuntoId,
		  CatTipoAsunto,
		  TipoProcedimiento,
		  NombreCorto,
			TieneAudidencia,
		  FechaAudiencia,
		  TieneArchivoAudiencia,
		  ArchivoAudiencia,
		  ResultadoAudiencia,
		  TipoAudiencia,
		  SecretarioId,
		  Secretario,
		  Mesa,
		  TipoCuaderno,
		  sTipoCuaderno,
			TieneArchivoProyecto,
			ArchivoProyecto,
			FechaCargaProyecto,
			NumeroVersionProyecto,
			EstadoProyecto,
			sEstadoProyecto,
			FechaEstadoProyecto,
			SentidoProyecto,
			sSentido,
			TipoSentencia,
			sTipoSentencia
		FROM 
			RegistrosSubidosSinAudiencia
		WHERE 
			lastVersion=1
	
	-- Filtrado por Estado de proyecto: En Revisión, Con ajustes, Aprobados, etc
	IF (@pi_Estados IS NOT NULL) BEGIN
		DELETE FROM @TableroProyectoMod
			WHERE NOT (
				EstadoProyecto IN (
					SELECT 
						CAST(value AS INT)
					FROM 
						STRING_SPLIT(@pi_Estados, ',')
				)
			)
	END

	-- Filtrado para los que no cumplen el buscado en Texto

	IF(LOWER(@pi_texto) LIKE '%version%') BEGIN
		DELETE FROM @TableroProyectoMod
		WHERE NOT NumeroVersionProyecto IN (
			SELECT CAST(SUBSTRING(@pi_texto, 9, 1000) AS INT)
		)
	END

	ELSE if (@pi_texto IS NOT NULL
		OR @pi_texto <> '' AND LOWER(@pi_texto) NOT LIKE '%version%') BEGIN
		DELETE FROM @TableroProyectoMod
			WHERE NOT (
				Secretario LIKE CONCAT('%', @pi_Texto, '%')
				OR TipoAudiencia LIKE CONCAT('%', @pi_Texto, '%')
				OR Mesa LIKE CONCAT('%', @pi_Texto, '%')
				OR sEstadoProyecto LIKE CONCAT('%', @pi_Texto, '%')
				OR AsuntoAlias LIKE CONCAT('%', @pi_Texto, '%')
				OR sSentido LIKE CONCAT('%', @pi_Texto, '%')
				OR sTipoSentencia LIKE CONCAT('%', @pi_Texto, '%')
				OR AsuntoNeunId LIKE CONCAT('%', @pi_Texto, '%')
			)
	END

	-- Filtrado por fecha de Proyecto
	IF (@pi_FechaPresentacionIni IS NOT NULL 
		OR @pi_FechaPresentacionFin IS NOT NULL) BEGIN
		DELETE FROM @TableroProyectoMod
			WHERE  (
				FechaCargaProyecto NOT BETWEEN 
					@pi_FechaPresentacionIni
					AND @pi_FechaPresentacionFin
			)

		--si la fechaCargaProyecto es null, se filtra por fechaAudiencia
		DELETE FROM @TableroProyectoMod
			WHERE 
				FechaCargaProyecto IS NULL 
					AND NOT (
						FechaAudiencia BETWEEN 
							@pi_FechaPresentacionIni
							AND @pi_FechaPresentacionFin
					)
	END

	RETURN 
END
