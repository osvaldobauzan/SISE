USE [SISE_NEW]
GO
/****** Object:  View [SISE3].[vwPartesAutoridadesAcuerdo]    Script Date: 29/02/2024 10:05:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [SISE3].[vwPartesAutoridadesAcuerdo]
AS

select nep.PersonaId as ParteID
        ,nep.TipoConstanciaId as TipoConstanciaId
		,nep.ActuarioId
        ,nep.FechaNotificacion
        ,nep.TipoNotificacion
        ,nep.NombreArchivo
        ,nep.NotElecId 
        ,ad.AsuntoNeunId
		,ad.AsuntoId
		,ad.AsuntoDocumentoId
		,ad.SintesisOrden
		,(CASE WHEN pas.CatTipoPersonaJuridicaId = 1
			THEN CONCAT(pas.Nombre,' ',pas.APaterno,' ',pas.AMaterno) 
			ELSE pas.DenominacionDeAutoridad
			END)AS NombreParteoAutoridad
		,pas.CatTipoPersonaId
		,pas.CatCaracterPersonaAsuntoId
--		,*
FROM AsuntosDocumentos ad WITH(NOLOCK) 
INNER JOIN NotificacionElectronica_Personas nep ON ad.AsuntoID=nep.AsuntoId AND ad.AsuntoNeunId=nep.AsuntoNeunId AND  ad.SintesisOrden = nep.SintesisOrden
INNER join PersonasAsunto  pas ON  ad.AsuntoId = pas.AsuntoId and ad.AsuntoNeunId = pas.AsuntoNeunId AND nep.PersonaId = pas.PersonaId 
LEFT JOIN NotificacionElectronica_Archivos nea ON nep.NotElecId = nea.NotElecId
--where ad.AsuntoNeunId = 30315077
--and ad.AsuntoDocumentoId = 8
UNION ALL
select ax.AnexoParteId as ParteID
        ,NULL as TipoConstanciaId
        ,NULL as ActuarioId
        ,NULL as FechaNotificacion
        ,NULL as TipoNotificacion
        ,NULL as NombreArchivo
        ,NULL as NotElecId
		,ad.AsuntoNeunId
		,ad.AsuntoId
		,ad.AsuntoDocumentoId
		,ad.SintesisOrden
		,pas.DenominacionDeAutoridad as NombreParteoAutoridad
--        ,IIF(ax.NombreArchivo is null,0,1) AS TieneArchivo
		,pas.CatTipoPersonaId
		,pas.CatCaracterPersonaAsuntoId
--		,ad.
FROM AsuntosDocumentos ad WITH(NOLOCK) 
			--INNER JOIN NotificacionElectronica_Personas nep ON ad.AsuntoID=nep.AsuntoId AND ad.AsuntoNeunId=nep.AsuntoNeunId AND  ad.SintesisOrden = nep.SintesisOrden
			INNER JOIN Anexos ax on ax.AsuntoNeunId = ad.AsuntoNeunId and ax.AsuntoDocumentoId = ad.AsuntoDocumentoId
			INNER join PersonasAsunto  pas ON  ad.AsuntoId = pas.AsuntoId and ad.AsuntoNeunId = pas.AsuntoNeunId and ax.AnexoParteId = pas.PersonaId
			--LEFT JOIN NotificacionElectronica_Archivos nea ON ax.NotElecId = nea.NotElecId
			 --and ax.AnexoParteId = pas.PersonaId
--where ad.AsuntoNeunId = 30315077
--and ad.AsuntoDocumentoId = 8
GO
