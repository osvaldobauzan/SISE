USE [SISE_NEW]
GO
/****** Object:  StoredProcedure [SISE3].[pcPersonaCaracterXAsunto]    Script Date: 14/06/2024 01:04:09 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================= 
-- Autor: Anabel Gonzalez
-- Fecha de Creación: 14 de Junio 2024
-- Descripción: Consulta para obtener informacion del caracter (quejoso) por id asunto y id neun
-- Ejemplo : EXEC [SISE3].[pcPersonaCaracterXAsunto] 2,30312461
-- ============================================= 
CREATE PROCEDURE [SISE3].[pcPersonaCaracterXAsunto]
@pi_IdAsunto INT,
@pi_IdNeun INT
AS
  BEGIN
	SET NOCOUNT ON
	  BEGIN
		DECLARE  @pi_CatTipoAsuntoId INT
		SET @pi_CatTipoAsuntoId=(SELECT TOP 1 CatTipoAsuntoId FROM Asuntos WITH(NOLOCK) WHERE StatusReg=1 and AsuntoNeunId = @pi_IdNeun)
        SET @pi_IdAsunto = (SELECT TOP 1 AsuntoId FROM Asuntos WITH(NOLOCK) WHERE AsuntoNeunId = @pi_IdNeun and StatusReg=1)

        IF (@pi_CatTipoAsuntoId = '11' or @pi_CatTipoAsuntoId = '24' or @pi_CatTipoAsuntoId = '55')--11:Amparo en revisión; 24:Repetición del Acto Reclamado; 5:Amparo Directo, Amparo en Revisión, Revisión Fiscal, Recurso de Queja
			BEGIN
                 SELECT ISNULL(ctp.Descripcion ,'') as DescripcionTipoPersona,
					ISNULL(p.DenominacionDeAutoridad,'') DenominacionDeAutoridad,
					ISNULL(cpa.Descripcion,'') as DescripcionCaracterPersona,
					ISNULL(ccag.Descripcion,'') as DescripcionClasificaAutoridadGenerica,
					p.PersonaId,
					p.Nombre,
					p.APaterno, 
					p.AMaterno,       
					p.CatCaracterPersonaAsuntoId,
					p.Foraneo,
					p.CatAutoridadId,										 
					ccag.ClasificaAutoridadGenericaId,										 
					TieneCaptura = dbo.fn_TieneCamposCapturaPorParte(p.AsuntoNeunId,p.PersonaId),     
					p.CatTipoPersonaId,
					p.Recurrente
                  FROM PersonasAsunto p WITH(NOLOCK)
					LEFT JOIN  CatTiposPersona ctp WITH(NOLOCK) ON (ctp.CatTipoPersonaId = p.CatTipoPersonaId ) 
					LEFT JOIN  CatCaracterPersonaAsunto cpa WITH(NOLOCK) ON (cpa.CatCaracterPersonaAsuntoId = p.CatCaracterPersonaAsuntoId) 
					LEFT JOIN  CatClasificaAutoridadGenerica ccag WITH(NOLOCK) ON (ccag.ClasificaAutoridadGenericaId = p.ClasificaAutoridadGenericaId)                      
                    WHERE p.AsuntoNeunId = @pi_IdNeun AND p.StatusReg = 1
                    ORDER BY ISNULL(ctp.Descripcion ,''), p.Recurrente DESC
              END
			  IF (@pi_CatTipoAsuntoId = '137' or @pi_CatTipoAsuntoId = '138' or @pi_CatTipoAsuntoId = '139' or @pi_CatTipoAsuntoId = '140')--137:Investigación de Responsabilidades Administrativas --SBGE --090424 Recursos de UGIRA
				 BEGIN
					 SELECT      
						DescripcionTipoPersona= CASE p.CatTipoPersonaId WHEN 1 THEN 'Persona Física' WHEN 2  THEN 'Persona Jurídica' WHEN 3 THEN 'Autoridad' WHEN 8 THEN 'Servidor Público del CJF' END,
						ISNULL(p.DenominacionDeAutoridad,'') DenominacionDeAutoridad,
						DescripcionCaracterPersona= CASE ISNULL(cpa.Descripcion,'') WHEN 'SIN VALOR' THEN '' ELSE isnull(cpa.Descripcion,'') END,
						ISNULL(ccag.Descripcion,'') DescripcionClasificaAutoridadGenerica,
						p.PersonaId,
						Nombre= CASE p.CatTipoPersonaId WHEN 8 THEN pau.ServidorPublico ELSE p.Nombre END,
						p.APaterno, 
						p.AMaterno,       
						p.CatCaracterPersonaAsuntoId,
						p.Foraneo,
						p.CatAutoridadId,										 
						ccag.ClasificaAutoridadGenericaId,										 
						TieneCaptura = dbo.fn_TieneCamposCapturaPorParte(p.AsuntoNeunId,p.PersonaId),     
						p.CatTipoPersonaId,
						p.Recurrente
						FROM PersonasAsunto p WITH(NOLOCK)
					LEFT JOIN CatTiposPersona ctp WITH(NOLOCK) ON (ctp.CatTipoPersonaId = p.CatTipoPersonaId ) 
					LEFT JOIN CatCaracterPersonaAsunto cpa WITH(NOLOCK) ON (cpa.CatCaracterPersonaAsuntoId = p.CatCaracterPersonaAsuntoId)
					LEFT JOIN CatClasificaAutoridadGenerica ccag WITH(NOLOCK) ON (ccag.ClasificaAutoridadGenericaId = p.ClasificaAutoridadGenericaId) 
					LEFT JOIN personasasuntougira pau WITH(NOLOCK) ON (p.PersonaId = pau.PersonaId)
					WHERE p.AsuntoNeunId = @pi_IdNeun AND p.StatusReg = 1
					ORDER BY ISNULL(ctp.Descripcion ,''), p.Recurrente DESC
			     END
				 ELSE
					BEGIN
					   SELECT 
					     ISNULL(ctp.Descripcion ,'') DescripcionTipoPersona,
						 ISNULL(p.DenominacionDeAutoridad,'') DenominacionDeAutoridad,
						 ISNULL(cpa.Descripcion,'') DescripcionCaracterPersona,
						 ISNULL(ccag.Descripcion,'') DescripcionClasificaAutoridadGenerica,
						 p.PersonaId,
						 p.Nombre,  
						 p.APaterno,
						 p.AMaterno, 
						 p.CatCaracterPersonaAsuntoId,
						 p.Foraneo,  
						 p.Alias,
						 p.CatAutoridadId,										 
						 ccag.ClasificaAutoridadGenericaId,
						 p.EsParteGrupoVulnerable,
						 p.GrupoVulnerable,
						 TieneCaptura = dbo.fn_TieneCamposCapturaPorParte(p.AsuntoNeunId,p.PersonaId),
						 p.CatTipoPersonaId,
						 p.Recurrente
						 FROM    dbo.PersonasAsuntoOrden pao WITH(NOLOCK) 
						 INNER JOIN	 dbo.CatCaracterPersonaAsunto cpa WITH(NOLOCK)ON pao.CatCaracterPersonaAsuntoId = cpa.CatCaracterPersonaAsuntoId
						 RIGHT OUTER JOIN dbo.PersonasAsunto p WITH(NOLOCK)
						 LEFT OUTER JOIN dbo.CatTiposPersona ctp WITH(NOLOCK) ON ctp.CatTipoPersonaId = p.CatTipoPersonaId ON  cpa.CatCaracterPersonaAsuntoId = p.CatCaracterPersonaAsuntoId 
						 LEFT OUTER JOIN dbo.CatClasificaAutoridadGenerica ccag WITH(NOLOCK) ON ccag.ClasificaAutoridadGenericaId = p.ClasificaAutoridadGenericaId
						 WHERE p.AsuntoNeunId = @pi_IdNeun AND p.StatusReg = 1
						 ORDER BY pao.orden, p.PersonaId
			       END 
         END
	SET NOCOUNT OFF
END
   