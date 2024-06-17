USE [SISE_NEW]
GO
/****** Object:  UserDefinedFunction [SISE3].[fnTableroProyectoValidaIngesta]    Script Date: 18/04/2024 02:58:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: Fanny P. Lemus García
-- Create date: 02/04/2024
-- Version: 1
-- Description:	Valida Ingesta y arroja el motivo de no ingesta
-- SELECT * FROM [SISE3].[fnTableroProyectoValidaIngesta](180, 2, '8/2024', 27, NULL)
-- SELECT * FROM [SISE3].[fnTableroProyectoValidaIngesta](180, 2, NULL, NULL, 5232)
-- =============================================

CREATE   FUNCTION [SISE3].[fnTableroProyectoValidaIngesta](	
		@pi_CatOrganismoId INT,
		@pi_CatCuadernoId INT,
		@pi_AsuntoAlias VARCHAR(100)=NULL,
		@pi_CatTipoAsuntoId INT=NULL,
		@pi_AsuntoNeunId BIGINT=NULL
)

	RETURNS @TableroProyectoValida TABLE (
	PuedeIngestar BIT,
	MotivoNoIngesta VARCHAR (1000),
	iEstado INT,
	iVersion INT
	)
AS BEGIN
	
		DECLARE @PuedeIngestar BIT
		DECLARE @MotivoNoIngesta VARCHAR(1000)
		DECLARE @iEstado INT = NULL
		DECLARE @iVersion INT = NULL

		-- No existe el Neun
		IF @pi_AsuntoNeunId IS NULL BEGIN
			SET @pi_AsuntoNeunId = (
				SELECT 
					AsuntoNeunId 
				FROM 
					Asuntos WITH(NOLOCK) 
				WHERE
					StatusReg = 1
					AND CatOrganismoId = @pi_CatOrganismoId
					AND AsuntoAlias = @pi_AsuntoAlias
					AND CatTipoAsuntoId = @pi_CatTipoAsuntoId
			)
		END
		ELSE BEGIN  -- No existe el Neun o no pertenece al órgano
			SET @pi_AsuntoNeunId = (
				SELECT 
					AsuntoNeunId 
				FROM 
					Asuntos WITH(NOLOCK) 
				WHERE
					StatusReg = 1
					AND CatOrganismoId = @pi_CatOrganismoId
					AND AsuntoNeunId = @pi_AsuntoNeunId
			)
		END

		IF @pi_AsuntoNeunId IS NULL BEGIN
			SET @MotivoNoIngesta = 'NO EXISTE NEUN'
		END 

		-- Regla Ya esta en el Tablero
		IF @pi_AsuntoNeunId IN (
			SELECT 
				AsuntoNeunId
			FROM
				[SISE3].[fnTableroProyectoV3](	
					@pi_CatOrganismoId,
					NULL,
					NULL,
					NULL,
					NULL
				)
		) BEGIN

			SET  @MotivoNoIngesta = CONCAT_WS( ',', @MotivoNoIngesta, 'YA ESTÁ EN TABLERO')
			SELECT 
				@iEstado = EstadoProyecto,
				@iVersion = NumeroVersionProyecto
			FROM
				[SISE3].[fnTableroProyectoV3](	
					@pi_CatOrganismoId,
					NULL,
					NULL,
					NULL,
					NULL
				)
			WHERE
				AsuntoNeunId = @pi_AsuntoNeunId

		END

		SET @PuedeIngestar = CAST(
			(CASE WHEN @MotivoNoIngesta <> '' 
				 THEN 
				0 
			ELSE 
				1 
			END
			) AS BIT
		)

		INSERT INTO @TableroProyectoValida VALUES(
			@PuedeIngestar, 
			@MotivoNoIngesta,
			@iEstado,
			@iVersion
		)

RETURN
END
