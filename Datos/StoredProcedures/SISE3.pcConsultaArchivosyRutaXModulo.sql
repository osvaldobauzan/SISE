SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




--SISE3.pcConsultaPromocionArchivosyRuta 30312375,1,2023,1494
-- =============================================
-- Author:		Christian Araujo - MS
-- Alter date: 02/11/09
-- Objetivo: Carga el detalle de una promoción electrónica seleccionada en el detalle de promoción
-- EXEC SISE3.pcConsultaPromocionArchivosyRuta 30301133, 1,2023,4
      --  SISE3.[pcConsultaArchivosyRutaXModulo] 2016580,0,0,1494,29,1,null;
	  --  SISE3.[pcConsultaArchivosyRutaXModulo] 23035824,2024,329,1494,22,1,null; 
      -- SISE3.[pcConsultaArchivosyRutaXModulo] 30313417,null,null,1494,null,3,null,1;
-- =============================================
CREATE OR ALTER PROCEDURE [SISE3].[pcConsultaArchivosyRutaXModulo]
(
@pi_AsuntoNeunId BIGINT ,
@pi_YearPromocion INT NULL, 
@pi_NumeroOrden INT NULL,  --
@pi_catIdOrganismo INT,
@pi_Origen INT NULL, 
@pi_TipoModulo INT, --1 Promocion 2 Acuerdo 3 Acuse
@pi_AsuntoDocumentoId INT NULL,
@pi_SintesisOrden INT = NULL
)

 

AS
BEGIN
	SET NOCOUNT ON
	IF @pi_TipoModulo = 1 
	BEGIN 

		IF @pi_Origen IN (0,4,7)
		BEGIN
			SELECT  NombreClase = CASE ClasePromocion WHEN  '1' THEN 'Escrito' ELSE 'Oficio' END				  
				   ,ac.DESCRIPCION AS DescripcionAnexo
				   ,EsPromocion = CASE pa.ClaseAnexo WHEN NULL THEN 1 WHEN 0 THEN 1 ELSE 0 END
				   ,ISNULL(rcb.sRuta,rc.sRuta) sRuta
				   ,Pa.NombreArchivo
				   ,CONCAT(ISNULL(rcb.sRuta,rc.sRuta),'\',[Promociones].CatorganismoId,'\', Pa.NombreArchivo) AS RutaCompleta---Ajuste 
			FROM [dbo].[Promociones] WITH(NOLOCK) 
			LEFT JOIN PromocionArchivos Pa WITH(NOLOCK) 
				ON Pa.AsuntoId=Promociones.AsuntoId 
				AND Pa.AsuntoNeunId=Promociones.AsuntoNeunId 
				AND Pa.NumeroOrden=Promociones.NumeroOrden 
				AND Pa.NumeroRegistro=Promociones.NumeroRegistro
				AND Pa.YearPromocion=Promociones.YearPromocion 
				AND Pa.StatusArchivo=1
				-- AND Pa.DescripcionAnexo=5031
			JOIN CAT_RutasChunk rc 
				ON rc.iGrupo = 2 AND rc.iEscritura = 1 
			LEFT JOIN viCatalogos ac WITH(NOLOCK) 
				ON ac.ID = Pa.DescripcionAnexo 
				AND ac.Catalogo = 17 
			LEFT JOIN CatalogosElementosTiposAsunto bc  WITH(NOLOCK) 
				ON ac.Catalogo = bc.CatalogoId 
				AND ac.ID = bc.CatalogoElementoIdNew 
				AND ac.Catalogo = 17 
				AND bc.CatTipoAsuntoId = 1 
				AND bc.StatusRegistro = 1 
				AND ac.CatalogoPadre > 0  
            LEFT JOIN SISE3.REL_ArchivosRutaHistorica hist WITH(NOLOCK)
                ON hist.AsuntoNeunId = Promociones.AsuntoNeunId
                AND hist.YearPromocion = Promociones.YearPromocion
                AND hist.NumeroOrden = Promociones.NumeroOrden
                AND hist.CatOrganismoId = Promociones.CatOrganismoId 
            LEFT JOIN CAT_RutasChunk rcb 
				ON rcb.kId = hist.idRuta
			WHERE [Promociones].AsuntoNeunId=@pi_AsuntoNeunId 
				  AND [Promociones].YearPromocion=@pi_YearPromocion 
				  AND [Promociones].NumeroOrden=@pi_NumeroOrden 
				  AND Pa.NombreArchivo IS NOT NULL		  
				  AND [Promociones].CatOrganismoId=@pi_catIdOrganismo 
				  AND [Promociones].StatusReg in (1,2)
		END
		ELSE IF @pi_Origen = 6
		BEGIN
			SELECT	NombreClase = 'Pendiente'  --ARCHIVOS EN TABLAS ELECTRÓNICAS
					,null AS DescripcionAnexo
					,EsPromocion = 1
					,rc.sRuta
					,moa.sNombreArchivo+moa.sExtension NombreArchivo
					, CONCAT(rc.sRuta,'\',[pr].fkIdOrgano,'\', moa.sNombreArchivo,moa.sExtension) AS RutaCompleta---Ajuste 
			FROM JL_MOV_Promocion pr 
			LEFT JOIN JL_REL_PromocionArchivo pa ON pr.kIdPromocion = pa.fkIdPromocion
			LEFT JOIN JL_MOV_Archivo  moa ON moa.kIdArchivo = pa.fkIdArchivo
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 9 AND rc.iEscritura = 1
            WHERE pr.kIdPromocion = @pi_AsuntoNeunId
				  AND pr.fkIdEstatus = 1
				  AND pr.fkIdOrgano = @pi_catIdOrganismo
			UNION -- UNION A TABLA RELACIONADA PARA NO TRAER MÁS DE OTROS ORGANISMOS
			SELECT	NombreClase = 'Pendiente'
					,null AS DescripcionAnexo
					,EsPromocion = 1
					,ISNULL(rcb.sRuta,rc.sRuta) sRuta
					,moa.sNombreArchivo+moa.sExtension NombreArchivo
					, CONCAT(ISNULL(rcb.sRuta,rc.sRuta),'\',[pr].fkIdOrgano,'\', moa.sNombreArchivo,moa.sExtension) AS RutaCompleta---Ajuste 
			FROM JL_MOV_Promocion pr 
			LEFT JOIN JL_REL_PromocionArchivo pa ON pr.kIdPromocion = pa.fkIdPromocion
			LEFT JOIN JL_MOV_Archivo  moa ON moa.kIdArchivo = pa.fkIdArchivo
			LEFT JOIN JL_REL_PromocionSISE ps with(nolock) ON pr.kIdPromocion = ps.fkIdPromocion and pr.fkIdAsuntoNeun = ps.AsuntoNeunId and pr.fkIdOrgano = ps.CatOrganismoId
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 9 AND rc.iEscritura = 1
            LEFT JOIN SISE3.REL_ArchivosRutaHistorica hist WITH(NOLOCK)
                ON hist.AsuntoNeunId = ps.AsuntoNeunId
                AND hist.YearPromocion = ps.YearPromocion
                AND hist.NumeroOrden = ps.NumeroOrden
                AND hist.CatOrganismoId = ps.CatOrganismoId
            LEFT JOIN CAT_RutasChunk rcb 
				ON rcb.kId = hist.idRuta
			WHERE ps.AsuntoNeunId=@pi_AsuntoNeunId
				  AND ps.NumeroOrden=@pi_NumeroOrden
				  AND pr.fkIdOrgano = @pi_catIdOrganismo
				  AND pr.fkIdEstatus = 1
			UNION -- ANEXOS
			SELECT
				NombreClase = 'Pendiente'
				,ac.DESCRIPCION AS DescripcionAnexo
				,EsPromocion = CASE pa.ClaseAnexo WHEN NULL THEN 1 WHEN 0 THEN 1 ELSE 0 END
				,ISNULL(rcb.sRuta,rc.sRuta) sRuta
				,Pa.NombreArchivo
				,CONCAT(ISNULL(rcb.sRuta,rc.sRuta),'\',pa.CatorganismoId,'\', Pa.NombreArchivo) AS RutaCompleta---Ajuste 
			FROM PromocionArchivos pa 
			LEFT JOIN viCatalogos ac WITH(NOLOCK) 
				ON ac.ID = pa.DescripcionAnexo 
				AND ac.Catalogo = 17
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 2 AND rc.iEscritura = 1
            LEFT JOIN SISE3.REL_ArchivosRutaHistorica hist WITH(NOLOCK)
                ON hist.AsuntoNeunId = pa.AsuntoNeunId
                AND hist.YearPromocion = pa.YearPromocion
                AND hist.NumeroOrden = pa.NumeroOrden
                AND hist.CatOrganismoId = pa.CatOrganismoId
            LEFT JOIN CAT_RutasChunk rcb 
				ON rcb.kId = hist.idRuta
			WHERE pa.AsuntoNeunId = @pi_AsuntoNeunId
			AND pa.CatOrganismoId = @pi_catIdOrganismo
			AND pa.NumeroOrden = @pi_NumeroOrden
			--AND pa.YearPromocion = @pi_YearPromocion 
			AND pa.StatusArchivo = 1
		END
		ELSE IF @pi_Origen = 14
		BEGIN
			SELECT	NombreClase = 'Pendiente'
					,null AS DescripcionAnexo
					,EsPromocion = 1
					,rc.sRuta
					,moa.sNombreArchivo+moa.sExtension NombreArchivo
					, CONCAT(rc.sRuta,'\',[pr].fkIdOrgano,'\', moa.sNombreArchivo,moa.sExtension) AS RutaCompleta---Ajuste 
			FROM ICOIJ_MOV_Promocion pr 
			LEFT JOIN ICOIJ_MOV_Archivo moa ON moa.kiIdFolio = pr.kiIdFolio
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 20 AND rc.iEscritura = 1
			WHERE (pr.kiIdFolio = @pi_AsuntoNeunId)
				  AND pr.fkIdEstatus = 1
				  AND pr.fkIdOrgano = @pi_catIdOrganismo
			UNION
			SELECT	NombreClase = 'Pendiente'
					,null AS DescripcionAnexo
					,EsPromocion = 1
					,ISNULL(rcb.sRuta,rc.sRuta) sRuta
					,moa.sNombreArchivo+moa.sExtension NombreArchivo
					, CONCAT(ISNULL(rcb.sRuta,rc.sRuta),'\',[pr].fkIdOrgano,'\', moa.sNombreArchivo,moa.sExtension) AS RutaCompleta---Ajuste 
			FROM ICOIJ_MOV_Promocion pr 
			LEFT JOIN ICOIJ_MOV_Archivo moa ON moa.kiIdFolio = pr.kiIdFolio
			LEFT JOIN ICOIJ_REL_PromocionSISE ps with(nolock) ON pr.kIdPromocion = ps.fkIdPromocion AND ps.AsuntoNeunId = pr.fkIdAsuntoNeun AND ps.CatOrganismoId = pr.fkIdOrgano
            LEFT JOIN SISE3.REL_ArchivosRutaHistorica hist WITH(NOLOCK)
                ON hist.AsuntoNeunId = ps.AsuntoNeunId
                AND hist.YearPromocion = ps.YearPromocion
                AND hist.NumeroOrden = ps.NumeroOrden
                AND hist.CatOrganismoId = ps.CatOrganismoId
            LEFT JOIN CAT_RutasChunk rcb 
				ON rcb.kId = hist.idRuta
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 20 AND rc.iEscritura = 1
			WHERE (ps.AsuntoNeunId=@pi_AsuntoNeunId)
				  AND pr.fkIdOrgano = @pi_catIdOrganismo
				  AND ps.NumeroOrden=@pi_NumeroOrden
				  AND pr.fkIdEstatus = 1
			UNION
			SELECT
				NombreClase = 'Pendiente'
				,ac.DESCRIPCION AS DescripcionAnexo
				,EsPromocion = CASE pa.ClaseAnexo WHEN NULL THEN 1 WHEN 0 THEN 1 ELSE 0 END
				,ISNULL(rcb.sRuta,rc.sRuta) sRuta
				,Pa.NombreArchivo
				,CONCAT(ISNULL(rcb.sRuta,rc.sRuta),'\',pa.CatorganismoId,'\', Pa.NombreArchivo) AS RutaCompleta---Ajuste 
			FROM PromocionArchivos pa 
			LEFT JOIN viCatalogos ac WITH(NOLOCK) 
				ON ac.ID = pa.DescripcionAnexo 
				AND ac.Catalogo = 17
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 2 AND rc.iEscritura = 1
            LEFT JOIN SISE3.REL_ArchivosRutaHistorica hist WITH(NOLOCK)
                ON hist.AsuntoNeunId = pa.AsuntoNeunId
                AND hist.YearPromocion = pa.YearPromocion
                AND hist.NumeroOrden = pa.NumeroOrden
                AND hist.CatOrganismoId = pa.CatOrganismoId
            LEFT JOIN CAT_RutasChunk rcb 
				ON rcb.kId = hist.idRuta
			WHERE pa.AsuntoNeunId = @pi_AsuntoNeunId
			AND pa.CatOrganismoId = @pi_catIdOrganismo
			AND pa.NumeroOrden = @pi_NumeroOrden
			--AND pa.YearPromocion = @pi_YearPromocion 
			AND pa.StatusArchivo = 1
		END
		ELSE IF @pi_Origen = 22
		BEGIN
			SELECT	NombreClase = 'Pendiente'
					,NULL AS DescripcionAnexo
					,EsPromocion = 1
					,rc.sRuta
					,moa.sNombreArchivo+moa.sExtension NombreArchivo
					, CONCAT(rc.sRuta,'\',[pr].fkIdOrgano,'\', moa.sNombreArchivo,moa.sExtension) AS RutaCompleta---Ajuste 
			FROM IOJ_MOV_PromocionOJ pr 
			LEFT JOIN IOJ_REL_PromocionArchivoOJ ar ON  ar.fkIdPromocion = pr.kiIdFolio
            LEFT JOIN JL_MOV_Archivo moa ON moa.kIdArchivo = ar.fkIdArchivo
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 9 AND rc.iEscritura = 1
			WHERE (pr.kiIdFolio = @pi_AsuntoNeunId)
				  AND pr.fkIdEstatus = 1
				  AND pr.fkIdOrgano = @pi_catIdOrganismo                  
			UNION
			SELECT	NombreClase = 'Pendiente'
					,NULL AS DescripcionAnexo
					,EsPromocion = 1
					,ISNULL(rcb.sRuta,rc.sRuta) sRuta
					,moa.sNombreArchivo+moa.sExtension NombreArchivo
					, CONCAT(ISNULL(rcb.sRuta,rc.sRuta),'\',[pr].fkIdOrgano,'\', moa.sNombreArchivo,moa.sExtension) AS RutaCompleta---Ajuste 
			FROM IOJ_MOV_PromocionOJ pr 
			LEFT JOIN IOJ_REL_PromocionSISE ps with(nolock) ON pr.kiIdFolio = ps.fkIdPromocion 
            LEFT JOIN IOJ_REL_PromocionArchivoOJ ar ON  ar.fkIdPromocion = pr.kiIdFolio
            LEFT JOIN JL_MOV_Archivo moa ON moa.kIdArchivo = ar.fkIdArchivo
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 9 AND rc.iEscritura = 1
            LEFT JOIN SISE3.REL_ArchivosRutaHistorica hist WITH(NOLOCK)
                ON hist.AsuntoNeunId = ps.AsuntoNeunId
                AND hist.YearPromocion = ps.YearPromocion
                AND hist.NumeroOrden = ps.NumeroOrden
                AND hist.CatOrganismoId = ps.CatOrganismoId
            LEFT JOIN CAT_RutasChunk rcb 
				ON rcb.kId = hist.idRuta
			WHERE (ps.AsuntoNeunId=@pi_AsuntoNeunId)
				  AND pr.fkIdOrgano = @pi_catIdOrganismo
				  AND ps.NumeroOrden=@pi_NumeroOrden
				  AND pr.fkIdEstatus = 1
			UNION
            --ARCHIVOS IOJ CON EXPEDIENTE
            SELECT	NombreClase = 'Pendiente'
					,NULL AS DescripcionAnexo
					,EsPromocion = 1
					,rc.sRuta
					,moa.sNombreArchivo+moa.sExtension NombreArchivo
					, CONCAT(rc.sRuta,'\',[pr].fkIdOrgano,'\', moa.sNombreArchivo,moa.sExtension) AS RutaCompleta---Ajuste 
			FROM JL_MOV_Promocion pr 
			LEFT JOIN JL_REL_PromocionArchivo da with(nolock) on pr.kIdPromocion=da.fkIdPromocion AND da.fkIdEstatus = 1
            LEFT JOIN JL_MOV_Archivo moa ON moa.kIdArchivo = da.fkIdArchivo
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 9 AND rc.iEscritura = 1
           	WHERE (pr.kIdPromocion = @pi_AsuntoNeunId)
				  AND pr.fkIdEstatus = 1
				  AND pr.fkIdOrgano = @pi_catIdOrganismo                  
			UNION
			SELECT	NombreClase = 'Pendiente'
					,NULL AS DescripcionAnexo
					,EsPromocion = 1
					,ISNULL(rcb.sRuta,rc.sRuta) sRuta
					,moa.sNombreArchivo+moa.sExtension NombreArchivo
					, CONCAT(ISNULL(rcb.sRuta,rc.sRuta),'\',[pr].fkIdOrgano,'\', moa.sNombreArchivo,moa.sExtension) AS RutaCompleta---Ajuste 
			FROM JL_MOV_Promocion pr 
			LEFT JOIN JL_REL_PromocionSISE ps with(nolock) ON pr.kIdPromocion = ps.fkIdPromocion 
            LEFT JOIN JL_REL_PromocionArchivo da with(nolock) on pr.kIdPromocion=da.fkIdPromocion AND da.fkIdEstatus = 1
            LEFT JOIN JL_MOV_Archivo moa ON moa.kIdArchivo = da.fkIdArchivo
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 9 AND rc.iEscritura = 1
            LEFT JOIN SISE3.REL_ArchivosRutaHistorica hist WITH(NOLOCK)
                ON hist.AsuntoNeunId = ps.AsuntoNeunId
                AND hist.YearPromocion = ps.YearPromocion
                AND hist.NumeroOrden = ps.NumeroOrden
                AND hist.CatOrganismoId = ps.CatOrganismoId
            LEFT JOIN CAT_RutasChunk rcb 
				ON rcb.kId = hist.idRuta
			WHERE (ps.AsuntoNeunId=@pi_AsuntoNeunId)
				  AND pr.fkIdOrgano = @pi_catIdOrganismo
				  AND ps.NumeroOrden=@pi_NumeroOrden
				  AND pr.fkIdEstatus = 1

            --FIN IOJ CON EXPEDIENTE
			UNION
			SELECT
				NombreClase = 'Pendiente'
				,ac.DESCRIPCION AS DescripcionAnexo
				,EsPromocion = CASE pa.ClaseAnexo WHEN NULL THEN 1 WHEN 0 THEN 1 ELSE 0 END
				,ISNULL(rcb.sRuta,rc.sRuta) sRuta
				,Pa.NombreArchivo
				,CONCAT(ISNULL(rcb.sRuta,rc.sRuta),'\',pa.CatorganismoId,'\', Pa.NombreArchivo) AS RutaCompleta---Ajuste 
			FROM PromocionArchivos pa 
			LEFT JOIN viCatalogos ac WITH(NOLOCK) 
				ON ac.ID = pa.DescripcionAnexo 
				AND ac.Catalogo = 17
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 2 AND rc.iEscritura = 1 
            LEFT JOIN SISE3.REL_ArchivosRutaHistorica hist WITH(NOLOCK)
                ON hist.AsuntoNeunId = pa.AsuntoNeunId
                AND hist.YearPromocion = pa.YearPromocion
                AND hist.NumeroOrden = pa.NumeroOrden
                AND hist.CatOrganismoId = pa.CatOrganismoId
            LEFT JOIN CAT_RutasChunk rcb 
				ON rcb.kId = hist.idRuta
			WHERE pa.AsuntoNeunId = @pi_AsuntoNeunId
			AND pa.CatOrganismoId = @pi_catIdOrganismo
			AND pa.NumeroOrden = @pi_NumeroOrden
			--AND pa.YearPromocion = @pi_YearPromocion 
			AND pa.StatusArchivo = 1
		END
		ELSE IF @pi_Origen = 5
		BEGIN
			SELECT	NombreClase = 'Pendiente'
					,NULL AS DescripcionAnexo
					,EsPromocion = 1
					,rc.sRuta
					,moa.sNombreArchivo+moa.sExtension NombreArchivo
					, CONCAT(rc.sRuta,'\',IIF(moa.fkIdOrigen != 7, ISNULL([d].fkIdOCC,[d].fkIdOrgano), LEFT(moa.sNombreArchivo, 4)),'\'
					, moa.sNombreArchivo,moa.sExtension) AS RutaCompleta---Ajuste 
					,EsBoletaOCC = IIF(moa.fkIdOrigen != 7, 0, 1)
			FROM JL_MOV_Demanda d 
			LEFT JOIN JL_REL_DemandaArchivo da with(nolock) on d.kIdDemanda=da.fkIdDemanda AND da.fkIdEstatus = 1
			LEFT JOIN  dbo.JL_MOV_Archivo AS moa ON moa.kIdArchivo = da.fkIdArchivo and moa.fkIdEstatus = 1
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 9 AND rc.iEscritura = 1
			WHERE d.kIdDemanda = @pi_AsuntoNeunId
				  AND d.fkIdEstatus = 1
				  --AND d.fkIdOrgano = @pi_catIdOrganismo
			UNION
			SELECT	NombreClase = 'Pendiente'
					,NULL AS DescripcionAnexo
					,EsPromocion = 1
					,ISNULL(rcb.sRuta,rc.sRuta) sRuta
					,moa.sNombreArchivo+moa.sExtension NombreArchivo
					,CONCAT(ISNULL(rcb.sRuta,rc.sRuta),'\',IIF(moa.fkIdOrigen != 7, ISNULL([d].fkIdOCC,[d].fkIdOrgano), LEFT(moa.sNombreArchivo, 4)),'\'
					, moa.sNombreArchivo,moa.sExtension) AS RutaCompleta---Ajuste 
					,EsBoletaOCC = IIF(moa.fkIdOrigen != 7, 0, 1)
			FROM JL_MOV_Demanda d 
			LEFT JOIN JL_REL_DemandaArchivo da with(nolock) on d.kIdDemanda=da.fkIdDemanda AND da.fkIdEstatus = 1
			LEFT JOIN  dbo.JL_MOV_Archivo AS moa ON moa.kIdArchivo = da.fkIdArchivo and moa.fkIdEstatus = 1	
			LEFT JOIN JL_REL_DemandaSISE rdem WITH (nolock) on rdem.fkIdDemanda = d.kIdDemanda
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 9 AND rc.iEscritura = 1
            LEFT JOIN SISE3.REL_ArchivosRutaHistorica hist WITH(NOLOCK)
                ON hist.AsuntoNeunId = rdem.AsuntoNeunId
                AND hist.YearPromocion = rdem.YearPromocion
                AND hist.NumeroOrden = rdem.NumeroOrden
                AND hist.CatOrganismoId = rdem.CatOrganismoId
            LEFT JOIN CAT_RutasChunk rcb 
				ON rcb.kId = hist.idRuta
			WHERE rdem.AsuntoNeunId = @pi_AsuntoNeunId
				  AND rdem.CatOrganismoId=@pi_catIdOrganismo
				  AND rdem.NumeroOrden = @pi_NumeroOrden
				  AND d.fkIdEstatus = 1
			UNION
			SELECT
				NombreClase = 'Pendiente'
				,ac.DESCRIPCION AS DescripcionAnexo
				,EsPromocion = CASE pa.ClaseAnexo WHEN NULL THEN 1 WHEN 0 THEN 1 ELSE 0 END
				,ISNULL(rcb.sRuta,rc.sRuta) sRuta
				,Pa.NombreArchivo
				,CONCAT(ISNULL(rcb.sRuta,rc.sRuta),'\',pa.CatorganismoId,'\', Pa.NombreArchivo) AS RutaCompleta---Ajuste 
				,EsBoletaOCC = 0
			FROM PromocionArchivos pa 
			LEFT JOIN viCatalogos ac WITH(NOLOCK) 
				ON ac.ID = pa.DescripcionAnexo 
				AND ac.Catalogo = 17
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 2 AND rc.iEscritura = 1
            LEFT JOIN SISE3.REL_ArchivosRutaHistorica hist WITH(NOLOCK)
                ON hist.AsuntoNeunId = pa.AsuntoNeunId
                AND hist.YearPromocion = pa.YearPromocion
                AND hist.NumeroOrden = pa.NumeroOrden
                AND hist.CatOrganismoId = pa.CatOrganismoId
            LEFT JOIN CAT_RutasChunk rcb 
				ON rcb.kId = hist.idRuta
			WHERE pa.AsuntoNeunId = @pi_AsuntoNeunId
			AND pa.CatOrganismoId = @pi_catIdOrganismo
			AND pa.NumeroOrden = @pi_NumeroOrden
			--AND pa.YearPromocion = @pi_YearPromocion 
			AND pa.StatusArchivo = 1
		END
		ELSE IF @pi_Origen = 15
		BEGIN
			SELECT	NombreClase = 'Pendiente'
					,NULL AS DescripcionAnexo
					,EsPromocion = 1
					,rc.sRuta
					,moa.sNombreArchivo+moa.sExtension NombreArchivo
					,CONCAT(rc.sRuta,'\',IIF(moa.fkIdOrigen != 7, ISNULL([d].fkIdOCC,[d].fkIdOrgano), LEFT(moa.sNombreArchivo, 4)),'\', moa.sNombreArchivo,moa.sExtension) AS RutaCompleta---Ajuste 
					,EsBoletaOCC = IIF(moa.fkIdOrigen != 7, 0, 1)
			FROM ICOIJ_MOV_Demanda d 
			LEFT JOIN  dbo.ICOIJ_MOV_Archivo AS moa ON d.kiIdFolio = moa.kiIdFolio AND moa.fkIdEstatus = 1
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 9 AND rc.iEscritura = 1
			WHERE (d.kiIdFolio = @pi_AsuntoNeunId)
				  AND d.fkIdEstatus = 1
				  --AND d.fkIdOrgano = @pi_catIdOrganismo
			UNION
			SELECT	NombreClase = 'Pendiente'
					,NULL AS DescripcionAnexo
					,EsPromocion = 1
					,ISNULL(rcb.sRuta,rc.sRuta) sRuta
					,moa.sNombreArchivo+moa.sExtension NombreArchivo
					,CONCAT(ISNULL(rcb.sRuta,rc.sRuta),'\',IIF(moa.fkIdOrigen != 7, ISNULL([d].fkIdOCC,[d].fkIdOrgano), LEFT(moa.sNombreArchivo, 4)),'\', moa.sNombreArchivo,moa.sExtension) AS RutaCompleta---Ajuste 
					,EsBoletaOCC = IIF(moa.fkIdOrigen != 7, 0, 1)
			FROM ICOIJ_MOV_Demanda d 
			LEFT JOIN  dbo.ICOIJ_MOV_Archivo AS moa ON d.kiIdFolio = moa.kiIdFolio AND moa.fkIdEstatus = 1
			LEFT JOIN dbo.ICOIJ_REL_DemandaSISE irdem WITH (NOLOCK) ON irdem.fkIdDemanda = d.kIdDemanda
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 9 AND rc.iEscritura = 1
            LEFT JOIN SISE3.REL_ArchivosRutaHistorica hist WITH(NOLOCK)
                ON hist.AsuntoNeunId = irdem.AsuntoNeunId
                AND hist.YearPromocion = irdem.YearPromocion
                AND hist.NumeroOrden = irdem.NumeroOrden
                AND hist.CatOrganismoId = irdem.CatOrganismoId
            LEFT JOIN CAT_RutasChunk rcb 
				ON rcb.kId = hist.idRuta
			WHERE (irdem.AsuntoNeunId = @pi_AsuntoNeunId)
				 AND irdem.CatOrganismoId=@pi_catIdOrganismo
				  AND irdem.NumeroOrden=@pi_NumeroOrden
				  AND d.fkIdEstatus = 1
				  --AND d.fkIdOrgano = @pi_catIdOrganismo
			UNION
			SELECT
				NombreClase = 'Pendiente'
				,ac.DESCRIPCION AS DescripcionAnexo
				,EsPromocion = CASE pa.ClaseAnexo WHEN NULL THEN 1 WHEN 0 THEN 1 ELSE 0 END
				,ISNULL(rcb.sRuta,rc.sRuta) sRuta
				,Pa.NombreArchivo
				,CONCAT(ISNULL(rcb.sRuta,rc.sRuta),'\',pa.CatorganismoId,'\', Pa.NombreArchivo) AS RutaCompleta---Ajuste 
				,EsBoletaOCC = 0
			FROM PromocionArchivos pa 
			LEFT JOIN viCatalogos ac WITH(NOLOCK) 
				ON ac.ID = pa.DescripcionAnexo 
				AND ac.Catalogo = 17
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 2 AND rc.iEscritura = 1
            LEFT JOIN SISE3.REL_ArchivosRutaHistorica hist WITH(NOLOCK)
                ON hist.AsuntoNeunId = pa.AsuntoNeunId
                AND hist.YearPromocion = pa.YearPromocion
                AND hist.NumeroOrden = pa.NumeroOrden
                AND hist.CatOrganismoId = pa.CatOrganismoId
            LEFT JOIN CAT_RutasChunk rcb 
				ON rcb.kId = hist.idRuta
			WHERE pa.AsuntoNeunId = @pi_AsuntoNeunId
			AND pa.CatOrganismoId = @pi_catIdOrganismo
			AND pa.NumeroOrden = @pi_NumeroOrden
			--AND pa.YearPromocion = @pi_YearPromocion 
			AND pa.StatusArchivo = 1
		END
		ELSE IF @pi_Origen = 29
		BEGIN
			SELECT	NombreClase = 'Pendiente'
					,NULL AS DescripcionAnexo
					,EsPromocion = 1
					,IIF(moa.iTipoArchivo = 27,rc.sRuta,rcb.sRuta) as sRuta
					,moa.sNombreArchivo+moa.sExtension NombreArchivo
					,CONCAT(IIF(moa.iTipoArchivo = 27,rc.sRuta,rcb.sRuta),'\',IIF(moa.iTipoArchivo = 27, coe.OrigenCatOrganismoId, LEFT(moa.sNombreArchivo, 4)),'\'
					,moa.sNombreArchivo,moa.sExtension) AS RutaCompleta---Ajuste
					,EsBoletaOCC = IIF(moa.iTipoArchivo = 27, 0, 1)
			FROM JL_MOV_Demanda d 
			LEFT JOIN JL_REL_DemandaArchivo da with(nolock) on d.kIdDemanda=da.fkIdDemanda AND da.fkIdEstatus = 1
			LEFT JOIN ComunicacionesOficialesEnviadas coe with(nolock) on  d.kIdDemanda = coe.fkIdDemanda
			LEFT JOIN  dbo.JL_MOV_Archivo AS moa ON moa.kIdArchivo = da.fkIdArchivo and moa.fkIdEstatus = 1	
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 16 AND rc.iEscritura = 1
            LEFT JOIN CAT_RutasChunk rcb ON rcb.iGrupo = 9 AND rcb.iEscritura = 1
			WHERE (d.kIdDemanda = @pi_AsuntoNeunId)
				  AND d.fkIdEstatus = 1
				  --AND d.fkIdOrgano = @pi_catIdOrganismo
			UNION
			SELECT	NombreClase = 'Pendiente'
					,NULL AS DescripcionAnexo
					,EsPromocion = 1
					,ISNULL(rc2.sRuta,IIF(moa.iTipoArchivo = 27,rc.sRuta,rcb.sRuta)) as sRuta
					,moa.sNombreArchivo+moa.sExtension NombreArchivo
					,CONCAT(ISNULL(rc2.sRuta,IIF(moa.iTipoArchivo = 27,rc.sRuta,rcb.sRuta)),'\',IIF(moa.iTipoArchivo = 27, coe.OrigenCatOrganismoId, LEFT(moa.sNombreArchivo, 4)),'\'
					, moa.sNombreArchivo,moa.sExtension) AS RutaCompleta---Ajuste 
					,EsBoletaOCC = IIF(moa.iTipoArchivo = 27, 0, 1)
			FROM JL_MOV_Demanda d 
			LEFT JOIN JL_REL_DemandaArchivo da with(nolock) on d.kIdDemanda=da.fkIdDemanda AND da.fkIdEstatus = 1
			LEFT JOIN ComunicacionesOficialesEnviadas coe with(nolock) on  d.kIdDemanda = coe.fkIdDemanda
			LEFT JOIN JL_REL_DemandaSISE ps with(nolock) ON d.kIdDemanda = ps.fkIdDemanda 
			LEFT JOIN  dbo.JL_MOV_Archivo AS moa ON moa.kIdArchivo = da.fkIdArchivo and moa.fkIdEstatus = 1	
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 16 AND rc.iEscritura = 1
            LEFT JOIN CAT_RutasChunk rcb ON rcb.iGrupo = 9 AND rcb.iEscritura = 1
            LEFT JOIN SISE3.REL_ArchivosRutaHistorica hist WITH(NOLOCK)
                ON hist.AsuntoNeunId = ps.AsuntoNeunId
                AND hist.YearPromocion = ps.YearPromocion
                AND hist.NumeroOrden = ps.NumeroOrden
                AND hist.CatOrganismoId = ps.CatOrganismoId
            LEFT JOIN CAT_RutasChunk rc2 
				ON rc2.kId = hist.idRuta
			WHERE (ps.AsuntoNeunId = @pi_AsuntoNeunId)
				 AND ps.CatOrganismoId=@pi_catIdOrganismo
				 AND ps.NumeroOrden=@pi_NumeroOrden
				  AND d.fkIdEstatus = 1
				  --AND d.fkIdOrgano = @pi_catIdOrganismo
			UNION
			SELECT
				NombreClase = 'Pendiente'
				,ac.DESCRIPCION AS DescripcionAnexo
				,EsPromocion = CASE pa.ClaseAnexo WHEN NULL THEN 1 WHEN 0 THEN 1 ELSE 0 END
				,ISNULL(rcb.sRuta,rc.sRuta) sRuta
				,Pa.NombreArchivo
				,CONCAT(ISNULL(rcb.sRuta,rc.sRuta),'\',pa.CatorganismoId,'\', Pa.NombreArchivo) AS RutaCompleta---Ajuste 
				,EsBoletaOCC = 0
			FROM PromocionArchivos pa 
			LEFT JOIN viCatalogos ac WITH(NOLOCK) 
				ON ac.ID = pa.DescripcionAnexo 
				AND ac.Catalogo = 17
			JOIN CAT_RutasChunk rc ON rc.iGrupo = 2 AND rc.iEscritura = 1 
            LEFT JOIN SISE3.REL_ArchivosRutaHistorica hist WITH(NOLOCK)
                ON hist.AsuntoNeunId = pa.AsuntoNeunId
                AND hist.YearPromocion = pa.YearPromocion
                AND hist.NumeroOrden = pa.NumeroOrden
                AND hist.CatOrganismoId = pa.CatOrganismoId
            LEFT JOIN CAT_RutasChunk rcb 
				ON rcb.kId = hist.idRuta
			WHERE pa.AsuntoNeunId = @pi_AsuntoNeunId
			AND pa.CatOrganismoId = @pi_catIdOrganismo
			AND pa.NumeroOrden = @pi_NumeroOrden
			--AND pa.YearPromocion = @pi_YearPromocion 
			AND pa.StatusArchivo = 1
		END
	END 
	IF @pi_TipoModulo = 2
	BEGIN 
		DECLARE @sRuta VARCHAR(255)
		DECLARE	@tRutasChunk AS TABLE (
				KId INT,	
				iGrupo INT, 
				sDescripcion VARCHAR(500),
				iTipoArchivo INT,
				sTipoArchivoDesc VARCHAR(500),
				sRuta VARCHAR(500),	
				iEscritura INT)
		INSERT @tRutasChunk(KId,iGrupo,sDescripcion,iTipoArchivo,sTipoArchivoDesc,sRuta,iEscritura)
		EXEC [SISE3].[pcRutasChunkXModulo] 'Trámite' 

		SET @sRuta = (SELECT TOP 1 sRuta FROM @tRutasChunk)

		SELECT 
			 ad.AsuntoDocumentoId
			,ad.AsuntoNeunId
			,ad.AsuntoID
			,a.CatOrganismoId
			,a.AsuntoAlias AS No_Exp
			,ad.SintesisOrden
			,(ad.NombreArchivo+ad.ExtensionDocumento) as NombreArchivo
			,ad.TipoCuaderno
			,dbo.funRecuperaCatalogoDependienteDescripcion(527,ad.TipoCuaderno) AS NombreTipoCuaderno
			,EmpleadoCancela = dbo.fnx_getUserName(ad.EmpleadoIdCancela)
			,EmpleadoAutoriza = dbo.fnx_getUserName(ad.EmpleadoIdAutoriza)
			,EmpleadoPreAutoriza = dbo.fnx_getUserName(ad.EmpleadoIdPreautoriza)
			,FechaAutoriza = ad.FechaAutoriza
			,FechaPreAutoriza = ad.FechaPreAutoriza
			,FechaCancela = ad.FechaCancela
			,userNameCapDJ = dbo.fnx_getUserName(ad.CreadorId)
			,userNameSecretario = s.UserName --dbo.fnx_getUserName(p.Secretario)
			,CONVERT(VARCHAR(10),p.FechaPresentacion,103) + CASE WHEN ISDATE(p.HoraPresentacion) = 1 THEN ' ' + CONVERT(VARCHAR(5),CONVERT(time,p.HoraPresentacion)) 
					ELSE '' END As FechaRecibido_F
			,ISNULL(CONVERT(VARCHAR(10),ad.FechaAlta,103),'') AS FechaAuto_F
			--,rc.sRuta
			,CASE WHEN rc.sRuta IS NULL OR rc.sRuta = ''  THEN @sRuta ELSE rc.sRuta END AS sRuta
			--, CONCAT(rc.sRuta,'\',a.CatorganismoId,'\', IIF(ad.Firmado=1,dj.NombreArchivo,CONCAT(ad.NombreArchivo, ad.ExtensionDocumento))) AS RutaCompleta---Ajuste 
			,CONCAT(CASE WHEN rc.sRuta IS NULL OR rc.sRuta = ''  THEN @sRuta ELSE rc.sRuta END ,'\',a.CatorganismoId,'\', IIF(ad.Firmado=1,dj.NombreArchivo,CONCAT(ad.NombreArchivo, ad.ExtensionDocumento))) AS RutaCompleta---Ajuste 
			,ad.uGuidDocumento GuidDocumento
			--, CONCAT(rc.sRuta,'\', Pa.NombreArchivo) AS RutaCompleta---Ajuste 
		FROM AsuntosDocumentos ad WITH(NOLOCK) 
		JOIN Asuntos a WITH(NOLOCK) 
			ON a.AsuntoNeunId= ad.AsuntoNeunId
		LEFT JOIN Promociones p WITH(NOLOCK) 
			ON ad.AsuntoNeunId = p.AsuntoNeunId 
			AND ad.AsuntoDocumentoId=p.AsuntoDocumentoId 
			AND p.StatusReg=ad.StatusReg
		JOIN CatOrganismos ct WITH(NOLOCK) 
			ON a.CatOrganismoId =ct.CatOrganismoId
		JOIN CatTiposAsunto cto WITH (NOLOCK) 
			ON a.CatTipoAsuntoId = cto.CatTipoAsuntoId
		LEFT JOIN PromocionArchivos pa WITH(NOLOCK) 
			ON pa.AsuntoNeunId=p.AsuntoNeunId 
			AND pa.NumeroOrden=p.NumeroOrden 
			AND pa.NumeroRegistro=p.NumeroRegistro
			AND pa.YearPromocion=p.YearPromocion 
			AND pa.StatusArchivo=1 
			AND pa.ClaseAnexo = 0
        LEFT JOIN DeterminacionesJudiciales dj
            ON dj.AsuntoNeunId = ad.AsuntoNeunId
            AND dj.CatOrganismoId = a.CatOrganismoId
            AND dj.SintesisOrden = ad.SintesisOrden
		LEFT JOIN CAT_RutasChunk rc ON rc.kId = ad.TipoRuta
		LEFT JOIN CatEmpleados s WITH(NOLOCK) ON s.EmpleadoId = p.Secretario
		WHERE ad.AsuntoNeunId=@pi_AsuntoNeunId 
			AND ad.NombreArchivo IS NOT NULL
			AND ad.AsuntoDocumentoId=@pi_AsuntoDocumentoId 			  
			AND ad.StatusReg IN (1,2)
	END 
    IF @pi_TipoModulo = 3
    BEGIN
        SELECT
             rc.sRuta 
            ,nea.NombreArchivo
            ,CONCAT(rc.sRuta,'\',ne.CatOrganismoId,'\', nea.NombreArchivo) AS RutaCompleta
        FROM NotificacionElectronica_Archivos nea        
        INNER JOIN NotificacionElectronica_Personas nep
            ON nea.NotElecId = nep.NotElecId
        INNER JOIN  NotificacionElectronica ne
            ON ne.AsuntoNeunId = nep.AsuntoNeunId
            AND ne.NumeroOrden = nep.NumeroOrden
            AND ne.SintesisOrden = nep.SintesisOrden
        INNER JOIN CAT_RutasChunk rc
            ON rc.iGrupo = 3 and iEscritura = 1
        WHERE nep.AsuntoNeunId = @pi_AsuntoNeunId
        AND ne.CatOrganismoId = @pi_catIdOrganismo
        AND nep.SintesisOrden = @pi_SintesisOrden
    END

	SET NOCOUNT OFF
END
