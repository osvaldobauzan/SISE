USE [SISE_NEW]
GO
/****** Object:  UserDefinedFunction [SISE3].[fnTableroProyectoDocumento]    Script Date: 18/04/2024 02:57:35 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: Fanny P. Lemus García
-- Create date: 03/04/2024
-- Version: 1
-- Description:	Llena los campos de version, extensión, tipo de documento y ruta del documento que se sube, 
--		puede ser correcció o proyecto. el último input debe ser corr o proy depende del caso
-- SELECT * FROM [SISE3].[fnTableroProyectoDocumento](180, 30313895, 'proyecto_sentencia.0723.docx', 'corr', 123)
-- =============================================

 CREATE    FUNCTION [SISE3].[fnTableroProyectoDocumento](	
	@pi_CatOrganismoId INT,
	@pi_AsuntoNeunId BIGINT,
	@pi_sNombreArchivoReal VARCHAR(500),
	@bTipoDoc VARCHAR(50),
	@pi_pkProyectoId BIGINT=NULL
)

	RETURNS @TableroProyectoDocumento TABLE
	(
		iVersion INT,
		extension VARCHAR(64),
		kid_ruta INT,
		sNombreArchivo VARCHAR(200)
	)

	AS BEGIN

			DECLARE @iVersion INT
			DECLARE @extension VARCHAR(64)
			DECLARE @kid_ruta INT
			DECLARE @sNombreArchivo VARCHAR(200)
			DECLARE @DELTA INT=1
			DECLARE @tipoCuaderno INT



			IF @pi_pkProyectoId IS NULL 
			BEGIN
			SET @iVersion = (
				SELECT
					MAX(iVersion)
				FROM
					[SISE3].[Proyecto] WITH(NOLOCK) 
				WHERE
					AsuntoNeunId = @pi_AsuntoNeunId
					AND iStatusReg = 1
			) 
			END

			IF @bTipoDoc = 'corr' 
				BEGIN
				SELECT
					@iVersion = iVersion
					FROM 
					[SISE3].[Proyecto] WITH(NOLOCK) 
					WHERE
					pkProyectoId = @pi_pkProyectoId

					SET @DELTA = 0
				END

		--SET @extension = SUBSTRING(
		--	@pi_sNombreArchivoReal, 
		--	CHARINDEX('.', @pi_sNombreArchivoReal), 
		--	LEN(@pi_sNombreArchivoReal)
		--)

			
			SET
				@tipoCuaderno=1

			SELECT 
				@extension= 
				REVERSE(left(REVERSE(@pi_sNombreArchivoReal), charindex('.',REVERSE(@pi_sNombreArchivoReal))))

			SET @kid_ruta = (
				SELECT
					[kId]
				FROM 
					[dbo].[CAT_RutasChunk] WITH(NOLOCK) 
				WHERE 
					iGrupo = 15  -- Corresponde al modulo ProyectoSentencia
					AND iEscritura = 1
					AND StatusReg = 1
			)

			SET @sNombreArchivo = (
				SELECT 
					[dbo].[fnPonCeros](CAST(@pi_CatOrganismoId AS VARCHAR(7)), 6)
					+ '_'
					+ [dbo].[fnPonCeros](CAST(@pi_AsuntoNeunId AS VARCHAR(13)), 11)
					+ '_'
					+ [dbo].[fnPonCeros](CAST(@tipoCuaderno AS VARCHAR(4)),2)
					+ '_'
					+ [dbo].[fnPonCeros](CAST(ISNULL(@iVersion, 0) + @DELTA AS VARCHAR(4)), 3)
					+ '_'
					+ @bTipoDoc
					+ @extension
			)


		INSERT INTO @TableroProyectoDocumento VALUES(@iVersion, @extension, @kid_ruta, @sNombreArchivo)


RETURN
END
