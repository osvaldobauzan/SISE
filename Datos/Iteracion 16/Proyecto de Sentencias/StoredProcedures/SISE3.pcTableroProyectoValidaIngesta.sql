USE [SISE_NEW]
GO
/****** Object:  StoredProcedure [SISE3].[pcTableroProyectoValidaIngesta]    Script Date: 18/04/2024 02:55:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: Fanny Paulet Lemus García
-- Create date: 22/03/2024
-- Description:	Revisa si un asunto puede ser incluido en el TableroProyecto
-- Version: 1.0
-- [SISE3].[pcTableroProyectoValidaIngesta] 180, 2, '8/2024', 27, NULL  /* Puede Ingestar */
-- [SISE3].[pcTableroProyectoValidaIngesta] 180, 2, '669121321/2024', 1, NULL  /* No Puede Ingestar, no existe neun */
-- [SISE3].[pcTableroProyectoValidaIngesta] 180, 2, NULL, NULL, 5232  /* No Puede Ingestar por Neun */
-- [SISE3].[pcTableroProyectoValidaIngesta] 180, 2, NULL, NULL, 16506533  /* No puede ingestar. Ya está en tablero*/

-- =============================================

CREATE PROCEDURE [SISE3].[pcTableroProyectoValidaIngesta](
	@pi_CatOrganismoId INT,
	@pi_CatCuadernoId INT,
	@pi_AsuntoAlias VARCHAR(100)=NULL,
	@pi_CatTipoAsuntoId INT=NULL,
	@pi_AsuntoNeunId BIGINT=NULL


) AS BEGIN

		SET NOCOUNT ON

		DECLARE @PuedeIngestar BIT 
		DECLARE @MotivoNoIngesta VARCHAR(1000)
			

			BEGIN
			SELECT 
				@MotivoNoIngesta = MotivoNoIngesta, 
				@PuedeIngestar = PuedeIngestar
			FROM [SISE3].[fnTableroProyectoValidaIngesta](
				@pi_CatOrganismoId,
				@pi_CatCuadernoId,
				@pi_AsuntoAlias,
				@pi_CatTipoAsuntoId,
				@pi_AsuntoNeunId)
			
			END

			SELECT 
				@PuedeIngestar AS PuedeIngestar,
				@MotivoNoIngesta AS  MotivoNoIngesta
		SET NOCOUNT OFF

END;
