USE [SISE_NEW]
GO
/****** Object:  StoredProcedure [SISE3].[pcObtieneElementosCatalogosProyecto]    Script Date: 22/04/2024 05:12:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- =============================================
-- Author:	Daniel A. Rangel Gavia - DGETD
-- Alter date: 19/03/2024
-- Objetivo: Obtiene el listado de las versiones de proyectos cargadas y revisadas de un NeunId dado.
-- [SISE3].[pcObtieneElementosCatalogosProyecto] 2, 2
-- =============================================
ALTER   PROCEDURE [SISE3].[pcObtieneElementosCatalogosProyecto]
    (
    @pi_CatTipoAsuntoId INT,
		@ps_TipoCatalogo INT 
)

AS
BEGIN
	DECLARE 	@CatalogoId INT
	DECLARE   @CatTipoOrganismoId INT=0
	DECLARE   @TipoAsuntoId INT



		IF (@ps_TipoCatalogo = 1) --sentencias 
		BEGIN 
		--SET @CatalogoId =500
		DECLARE @tmpNewValue TABLE (Id INT, Descripcion VARCHAR(50), elementos INT)
		INSERT INTO @tmpNewValue
		EXEC usp_CatalogosSel 500,0,0
		SELECT Id, Descripcion FROM @tmpNewValue
 		END

	IF (@ps_TipoCatalogo = 2)  --sentidos
		BEGIN
	DECLARE @HoldTable TABLE(
			Catalogo INT,
			CatTipoAsuntoId INT,
			CatTipoOrganismoId INT)

	INSERT INTO @HoldTable 
		SELECT 
			a.Catalogo, 
			a.CatTipoAsuntoId, 
			a.CatTipoOrganismoId
		FROM SISE_NEW.SISE3.CatSentidoSentenciaXCatTipoAsunto  b
			LEFT JOIN TiposAsunto a
			ON b.TipoAsuntoId=a.TipoAsuntoId
		WHERE b.CatTipoAsuntoId=@pi_CatTipoAsuntoId

		DECLARE @pt TABLE(
			Id INT,
			Descripcion VARCHAR(500),
			Elementos INT)

		DECLARE @RowCount INT = (SELECT COUNT(*) FROM @HoldTable);  
			WHILE @RowCount > 0 BEGIN  
		
		SELECT
			@CatalogoId=Catalogo, @CatTipoOrganismoId=CatTipoOrganismoId,@pi_CatTipoAsuntoId=CatTipoAsuntoId
		FROM
			@HoldTable
		ORDER BY 
			Catalogo DESC OFFSET @RowCount - 1 ROWS FETCH NEXT 1 ROWS ONLY

		INSERT INTO @pt EXEC usp_CatalogosSel @CatalogoId, @CatTipoOrganismoId,@pi_CatTipoAsuntoId
		SET @RowCount -= 1
	END
		SELECT Id, Descripcion FROM @pt
		
 		END --termina tipocatalogo=2

END
