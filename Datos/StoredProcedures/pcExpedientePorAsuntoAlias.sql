USE [SISE_NEW]
GO

/****** Object:  StoredProcedure [SISE3].[pcExpedientePorAsuntoAlias]    Script Date: 12/1/2023 6:15:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- =============================================
-- Author: Diana Quiroga 
-- Create date: 13/10/2013
-- Description:	Busca la información principal de un asunto dado un Asunto Alias que tiene promociones asignadas
-- Basado en: uspx_getExpedientePorAsuntoAlias
-- EXEC [SISE3].[pcExpedientePorAsuntoAlias] '334343/2023',1494,NULL, 1, 11896
--  EXEC [SISE3].[pcExpedientePorAsuntoAlias] '999655/2023',180,NULL, 2, null
-- =============================================
CREATE PROCEDURE [SISE3].[pcExpedientePorAsuntoAlias]
	-- REPRESENTA EL ASUNTO ALIAS POR EL CUAL SE DESEA OBTENER
	@pi_AsuntoAlias VARCHAR(50),
	-- REPRESENTA EL IDENTIFICADOR DEL ORGANISMO
	@pi_CatOrganismoId INT,
	-- REPRESENTA EL IDENTIFICADOR DEL TIPO DE ORGANISMO, PARAMETRO OPCIONAL VALOR NULO POR DEFAULT
	@pio_CatTipoAsuntoId INT = NULL,
	@pi_Modulo INT ,-- 1 Promocion 2 Tramite 
	@pi_CatTipoProcedimiento INT  = NULL
AS
BEGIN

	IF @pi_Modulo = 1 
	BEGIN
		SELECT	
			 a.AsuntoNeunId
			,@pi_CatOrganismoId CatOrganismoId 
			,a.AsuntoAlias
			,a.CatTipoAsuntoId
			,a.CatMateriaId
			,a.NumeroOCC
			,ta.Descripcion AS TipoAsunto/*
			CASE WHEN adc.CatCatalogoAsuntoId IS NULL THEN 0 ELSE adc.CatCatalogoAsuntoId END SecretarioId,
			dbo.FNOBTIENEEMPLEADO(adc.CatCatalogoAsuntoId)Secretario,*/
			--IIF(ISNULL(@pi_CatTipoProcedimiento, 0) = 0 , null, ctp.CatTipoProcedimiento ) as CatTipoProcedimiento,
			--IIF(ISNULL(@pi_CatTipoProcedimiento, 0) = 0 , null, ctp.TipoProcedimiento ) as TipoProcedimiento
			,a2.TipoProcedimientoId AS CatTipoProcedimiento
			,a2.TipoProcedimiento AS TipoProcedimiento
		
		FROM Asuntos a 
		CROSS APPLY SISE3.fnExpediente(a.AsuntoNeunId) a2
		INNER JOIN CatTiposAsunto ta 
			ON a.CatTipoAsuntoId = ta.CatTipoAsuntoId
		/*LEFT JOIN TiposAsunto tas WITH(NOLOCK) ON a.CatTipoAsuntoId=tas.CatTipoAsuntoId AND tas.Descripcion = 'Secretario' AND tas.StatusReg=1  
		LEFT JOIN AsuntosDetalleCatalogos adc WITH(NOLOCK) ON a.AsuntoNeunId=adc.AsuntosNeunId AND adc.TipoAsuntoId=tas.TipoAsuntoId AND adc.StatusReg = 1*/
		LEFT JOIN (SELECT  
						 row = ROW_NUMBER() OVER(PARTITION BY cd.CatalogoDependienteElementoIDNew
						,ceta.CatTipoAsuntoId  ORDER BY cd.CatalogoDependienteElementoIDNew)
						,TipoProcedimiento = ced.CatalogoElementoDescripcion
						,CatTipoProcedimiento = cd.CatalogoDependienteElementoIDNew
						,ceta.CatTipoAsuntoId
					FROM dbo.CatalogosDependientes AS cd WITH(NOLOCK)  
					INNER JOIN dbo.CatalogosElementosDescripcion AS ced WITH(NOLOCK)
						ON cd.CatalogoDependienteElementoIDNew = ced.CatalogoElementoDescripcionID
					INNER JOIN CatalogosElementosTiposAsunto ceta WITH(NOLOCK) 
						ON cd.CatalogoDependienteId=ceta.CatalogoId 
						AND cd.CatalogoDependienteElementoIDNew = ceta.CatalogoElementoIdNew
					WHERE cd.CatalogoDependienteId IN (464,124,208,1207,734,1933,1892)
		) ctp 
			ON a.CatTipoProcedimiento = ctp.CatTipoProcedimiento 
			AND a.CatTipoAsuntoId = ctp.CatTipoAsuntoId 
			AND ctp.row = 1
		WHERE  a.StatusReg = 1
		AND a.CatOrganismoId = @pi_CatOrganismoId
		AND a.AsuntoAlias = @pi_AsuntoAlias
		AND a.CatTipoAsuntoId = ISNULL(@pio_CatTipoAsuntoId,a.CatTipoAsuntoId)
		AND IIF(ISNULL(@pi_CatTipoProcedimiento,0)=0,0,ctp.CatTipoProcedimiento) = ISNULL(@pi_CatTipoProcedimiento,0)
	END 
	ELSE 
	BEGIN
		/*Cargar Asuntos Documentos que no tienen promoción*/
		CREATE TABLE #MaxSec
		(AsuntoNeunId BIGINT, 
			Expediente VARCHAR(50) collate SQL_Latin1_General_CP850_CI_AI, 
			Mesa varchar(15),
			Id int 
		)


		INSERT INTO #MaxSec
		SELECT p.AsuntoNeunId, 
		aa.AsuntoAlias ,
		p.Mesa,
		ROW_NUMBER() OVER (PARTITION BY p.AsuntoNeunId, aa.AsuntoAlias ORDER BY CAST(CONCAT(CONVERT(VARCHAR,p.FechaPresentacion,112),' ',p.HoraPresentacion) AS DATETIME) DESC) AS id
		FROM Promociones p WITH(NOLOCK) 
		CROSS APPLY SISE3.fnExpediente(p.AsuntoNeunId) aa
		INNER JOIN CatTiposAsunto ta ON aa.CatTipoAsuntoId = ta.CatTipoAsuntoId
		INNER  JOIN PromocionArchivos pa WITH(NOLOCK) ON pa.AsuntoNeunId = p.AsuntoNeunId
													AND pa.CatOrganismoId = p.CatOrganismoId 
													AND pa.NumeroOrden = p.NumeroOrden
													AND pa.Origen = p.OrigenPromocion 
													AND pa.YearPromocion = p.YearPromocion
													AND pa.StatusArchivo = 1
													AND pa.ClaseAnexo = 0
		WHERE p.StatusReg = 1 
		AND aa.AsuntoAlias = @pi_AsuntoAlias
		AND aa.CatOrganismoId = @pi_CatOrganismoId
		AND aa.CatTipoAsuntoId = ta.CatTipoAsuntoId


		SELECT	
			 a.AsuntoNeunId
			,@pi_CatOrganismoId CatOrganismoId
			,a.AsuntoAlias
			,a.CatTipoAsuntoId
			,a.CatMateriaId
			,a.NumeroOCC
			,ta.Descripcion as TipoAsunto/*,
			CASE WHEN adc.CatCatalogoAsuntoId IS NULL THEN 0 ELSE adc.CatCatalogoAsuntoId END SecretarioId,
			dbo.FNOBTIENEEMPLEADO(adc.CatCatalogoAsuntoId)Secretario,
			ctp.CatTipoProcedimiento,
			ctp.TipoProcedimiento*/
			,a2.TipoProcedimientoId as CatTipoProcedimiento
			,a2.TipoProcedimiento as TipoProcedimiento
			,m.Mesa
		FROM Asuntos a 
		CROSS APPLY SISE3.fnExpediente(a.AsuntoNeunId) a2
		INNER JOIN CatTiposAsunto ta 
			ON a.CatTipoAsuntoId = ta.CatTipoAsuntoId
		LEFT JOIN #MaxSec m ON  a.AsuntoNeunId = m.AsuntoNeunId AND a.AsuntoAlias = m.Expediente AND m.id = 1
		/*LEFT JOIN TiposAsunto tas WITH(NOLOCK) ON a.CatTipoAsuntoId=tas.CatTipoAsuntoId AND tas.Descripcion = 'Secretario' AND tas.StatusReg=1  
		LEFT JOIN AsuntosDetalleCatalogos adc WITH(NOLOCK) ON a.AsuntoNeunId=adc.AsuntosNeunId AND adc.TipoAsuntoId=tas.TipoAsuntoId AND adc.StatusReg = 1
		LEFT JOIN (
			SELECT row = ROW_NUMBER() OVER(PARTITION BY cd.CatalogoDependienteElementoIDNew,ceta.CatTipoAsuntoId  ORDER BY cd.CatalogoDependienteElementoIDNew) 
				,TipoProcedimiento = ced.CatalogoElementoDescripcion, 
				CatTipoProcedimiento = cd.CatalogoDependienteElementoIDNew, 
				ceta.CatTipoAsuntoId
			FROM dbo.CatalogosDependientes AS cd WITH(NOLOCK)  
			INNER JOIN dbo.CatalogosElementosDescripcion AS ced WITH(NOLOCK)  ON cd.CatalogoDependienteElementoIDNew = ced.CatalogoElementoDescripcionID
			INNER JOIN CatalogosElementosTiposAsunto ceta with(nolock) on cd.CatalogoDependienteId=ceta.CatalogoId and cd.CatalogoDependienteElementoIDNew = ceta.CatalogoElementoIdNew
			WHERE cd.CatalogoDependienteId  IN (464,124,208,1207,734,1933,1892)
		) ctp ON a.CatTipoProcedimiento = ctp.CatTipoProcedimiento AND a.CatTipoAsuntoId = ctp.CatTipoAsuntoId AND ctp.row = 1*/
		WHERE a.CatOrganismoId = @pi_CatOrganismoId
		AND a.AsuntoAlias = @pi_AsuntoAlias
		AND a.CatTipoAsuntoId = ISNULL(@pio_CatTipoAsuntoId,a.CatTipoAsuntoId)
		AND a.StatusReg = 1
		AND EXISTS (SELECT TOP 1 p.NumeroOrden 
					FROM Promociones p WITH(NOLOCK) 
						CROSS APPLY SISE3.fnExpediente(p.AsuntoNeunId) aa
						LEFT JOIN AsuntosDocumentos ad on ad.AsuntoNeunId = p.AsuntoNeunId and p.AsuntoDocumentoId = ad.AsuntoDocumentoId
						INNER JOIN CatTiposAsunto ta ON aa.CatTipoAsuntoId = ta.CatTipoAsuntoId
						INNER  JOIN PromocionArchivos pa WITH(NOLOCK) ON pa.AsuntoNeunId = p.AsuntoNeunId
													AND pa.CatOrganismoId = p.CatOrganismoId 
													AND pa.NumeroOrden = p.NumeroOrden
													AND pa.Origen = p.OrigenPromocion 
													AND pa.YearPromocion = p.YearPromocion
													AND pa.StatusArchivo = 1
													AND pa.ClaseAnexo = 0
					WHERE aa.CatTipoProcedimiento is not null
				   AND aa.AsuntoAlias = @pi_AsuntoAlias
				   AND aa.CatOrganismoId = @pi_CatOrganismoId
				   AND aa.CatTipoAsuntoId = ta.CatTipoAsuntoId
				   AND aa.AsuntoNeunId = a.AsuntoNeunId
					)
	END

	

END
GO

