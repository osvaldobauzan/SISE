USE [SISE_NEW]
GO
/****** Object:  StoredProcedure [dbo].[uspx_op_getTipoPromocion]    Script Date: 11/8/2023 1:27:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		GGHH
-- Create date: 09/05/2013
-- Description:	Obtiene el listado del tipo de promociones por tipo de organismo y tipo de asunto
-- Procedimientos SISE: usp_CatalogosSel
-- EXEC uspx_op_getTipoPromocion 2,1
-- =============================================
ALTER PROCEDURE [dbo].[uspx_op_getTipoPromocion]
	-- REPRESENTA EL IDENTIFICADOR DEL TIPO DE ORGANISMO
	@pi_CatTipoOrganismoId int,
	-- REPRESENTA EL IDENTIFICADOR DEL TIPO DE ASUNTO
	@pi_CatTipoAsuntoId int
AS
BEGIN
	--EXEC usp_CatalogosSel 488,@pi_CatTipoOrganismoId,@pi_CatTipoAsuntoId
	
	SELECT ID, DESCRIPCION
	FROM 
	(
		SELECT CatalogoPromocionId as ID,CatalogoPromocionDescripcion as DESCRIPCION, ROW_NUMBER() OVER(PARTITION BY CatalogoPromocionDescripcion ORDER BY CatalogoPromocionId ASC) ROW
		FROM CatPromocion with(nolock)
		WHERE CatTipoOrganismoId = @pi_CatTipoOrganismoId 
		AND CatTipoAsuntoId = @pi_CatTipoAsuntoId
		AND StatusReg = 1
	)tbl
	WHERE tbl.ROW = 1
    ORDER BY DESCRIPCION
END



