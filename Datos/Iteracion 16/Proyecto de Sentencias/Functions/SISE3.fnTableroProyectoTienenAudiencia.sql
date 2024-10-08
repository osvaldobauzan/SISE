USE [SISE_NEW]
GO
/****** Object:  UserDefinedFunction [SISE3].[fnTableroProyectoTienenAudiencia]    Script Date: 18/04/2024 02:58:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: Mario A Hernández Román
-- Create date: 11/03/2024
-- Version: 1
-- Description:	Obtiene Registros con Audiencia Constitucional Celebrada
--		Tablero SISE3.Proyecto
-- SELECT * FROM [SISE3].[fnTableroProyectoTienenAudiencia](180, 'Celebrada')
-- =============================================

CREATE FUNCTION [SISE3].[fnTableroProyectoTienenAudiencia](	
	@pi_CatOrganismoId INT,
	@pi_AudienciaDesripcion CHAR(50)='Celebrada'
	
)

RETURNS TABLE 
AS
RETURN 
(

	SELECT
		a.AsuntoNeunId,
		ast.AsuntoAlias,
		ast.NumeroAlias,
		ast.CatTipoOrganismoId,
		ast.CatOrganismoId,
		ast.CatTipoAsuntoId,
		ast.CatTipoAsunto,
		TipoProcedimiento = ast.CatTipoProcedimiento,
		ast.NombreCorto,
		TieneAudidencia = CAST(1 AS BIT),
		FechaAudiencia = CONVERT(
			DATETIME, 
			CONVERT(char(10), f.ValorCampoAsunto, 112) 
			+ ' ' 
			+ CONVERT(CHAR(8), h.ValorCampoAsunto, 108)
		),
		TieneArchivoAudiencia = CAST(1 AS BIT),
		ArchivoAudiencia = 'archivoAudiencia_PENDIENTE.docx',
		ResultadoAudiencia = ISNULL(r.Descripcion, ''),
		TipoAudiencia = ISNULL(t.Descripcion, ''),
		SecretarioId = ISNULL(a.SecretarioId, ''),
		Secretario = SISE3.ConcatenarNombres(
			s.Nombre, 
			s.ApellidoPaterno, 
			s.ApellidoMaterno
		),
		Mesa = ISNULL(ars.Nombre, 'Sin asignar'),
		TipoCuaderno = 1,                                   -- En su momento estas variables vendrá de un left join de la tabla Determinaciones judiciales
		sTipoCuaderno = ''
	FROM 
		AUD_AsuntosDetalleFechas a 
		CROSS APPLY SISE3.fnExpediente(a.AsuntoNeunId) ast
		LEFT JOIN AsuntosDetalleFechas f WITH(NOLOCK) ON 
			a.AsuntoNeunId = f.AsuntoNeunId 
			AND a.FechaId = f.AsuntoDetalleFechasId 
			AND f.StatusReg = 1
		LEFT JOIN AsuntosDetalleFechas h WITH(NOLOCK) ON 
			a.AsuntoNeunId = h.AsuntoNeunId 
			AND a.HoraId = h.AsuntoDetalleFechasId 
			AND f.StatusReg = 1
		LEFT JOIN AUD_CatResultado r WITH(NOLOCK) ON 
			a.ResultadoId = r.IdResultado
		LEFT JOIN AUD_CatTipoAudiencia t WITH(NOLOCK) ON 
			a.AudienciaId = t.IdTipoAudiencia
		INNER JOIN Asuntos asun ON 
			asun.AsuntoNeunId = a.AsuntoNeunId  
		LEFT JOIN CatEmpleados s WITH(NOLOCK) ON 
			s.EmpleadoId = a.SecretarioId
		LEFT JOIN Areas ars WITH(NOLOCK) ON 
			ars.EmpleadoId = a.SecretarioId
	WHERE 
		a.OrganoId = @pi_CatOrganismoId
		AND a.AudienciaId in (1,12,34,14,90,92,95,97,99,100,101)
		AND r.Descripcion = @pi_AudienciaDesripcion
		AND t.IdTipoAudiencia in (1,12,34,14,90,92,95,97,99,100,101)
		AND a.StatusReg = 1
		AND asun.StatusReg = 1
)
