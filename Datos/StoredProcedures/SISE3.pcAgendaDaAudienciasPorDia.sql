USE [SISE_NEW]
GO
/****** Object:  StoredProcedure [SISE3].[pcAgendaDaAudienciasPorDia]    Script Date: 14/06/2024 01:08:06 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================= 
-- Autor: Anabel Gonzalez
-- Fecha de Creación: 14 de Junio 2024
-- Descripción: Consulta para obtener el de detalle de la agenda de audiencias por fecha, expediente o nombre de la persona
-- Ejemplo : EXEC [SISE3].[pcAgendaDaAudienciasPorDia] 180, '2012/04/27', '2018/04/27',NULL,NULL
-- ============================================= 
CREATE PROCEDURE [SISE3].[pcAgendaDaAudienciasPorDia]
	@pi_CatOrganismoId INT,
	@pi_FAudIni DATETIME = NULL,
	@pi_FAudFin DATETIME = NULL,
	@pi_Expediente VARCHAR(20) = NULL,
	@pi_Persona VARCHAR(50) = NULL
AS
BEGIN	
		IF(@pi_FAudIni IS NOT NULL AND @pi_FAudFin IS NOT NULL )
	BEGIN
		 SELECT
		     CONVERT(INT,aud.AsuntoNeunId) IdNeun			
			, cta.Descripcion  TipoAsunto
			, asu.AsuntoAlias  Expediente
			, CONVERT(INT,caud.IdTipoAudiencia) IdTipoAudiencia 
			, caud.Descripcion  Audiencia
			, CONVERT(INT,aud.AgendaId) IdAgenda
			, CASE WHEN aud.Parte = 0 THEN 'Todas las Partes' ELSE ISNULL(pa.Nombre,'') + ' ' + ISNULL(pa.APaterno,'') + ' ' + ISNULL(pa.AMaterno,'') END AS Parte
			, CONVERT(VARCHAR(10),CONVERT(DATE, fecha.ValorCampoAsunto)) FechaAudiencia
			, CONVERT(VARCHAR(10),CONVERT(TIME, hora.ValorCampoAsunto)) HoraAudiencia
			, ISNULL(cr.Descripcion, '') Resultado
			, CONVERT(INT,cr.IdResultado) IdResultado
			, ISNULL(LTRIM(UPPER(e.Nombre)) + ' ' + UPPER(e.ApellidoPaterno) + ' ' + UPPER(e.ApellidoMaterno), 'Sin Valor') Empleado
			, ISNULL(LTRIM(UPPER(s.Nombre)) + ' ' + UPPER(s.ApellidoPaterno) + ' ' + UPPER(s.ApellidoMaterno), 'Sin Valor') Secretario			
		FROM [AUD_AsuntosDetalleFechas] aud WITH(NOLOCK)
		INNER JOIN AUD_CatTipoAudiencia caud WITH(NOLOCK) ON aud.AudienciaId = caud.IdTipoAudiencia
		INNER JOIN AsuntosDetalleFechas fecha WITH(NOLOCK) ON aud.ControlFecha=fecha.TipoAsuntoId 
			AND aud.FechaId=fecha.AsuntoDetalleFechasId 
			AND aud.AsuntoNeunId = fecha.AsuntoNeunId
			AND aud.AsuntoId = fecha.AsuntoId
		INNER JOIN AsuntosDetalleFechas hora WITH(NOLOCK) ON aud.ControlHora=hora.TipoAsuntoId 
			AND aud.HoraId=hora.AsuntoDetalleFechasId 
			AND aud.AsuntoNeunId = hora.AsuntoNeunId
			AND aud.AsuntoId = hora.AsuntoId
		INNER JOIN Asuntos asu WITH(NOLOCK)	ON asu.AsuntoNeunId = aud.AsuntoNeunId
		INNER JOIN CatTiposAsunto cta WITH(NOLOCK) ON asu.CatTipoAsuntoId = cta.CatTipoAsuntoId
			AND cta.StatusReg = 1
		LEFT JOIN AUD_CatResultado cr WITH(NOLOCK)	ON aud.ResultadoId = cr.IdResultado
		LEFT JOIN PersonasAsunto pa WITH(NOLOCK) ON aud.Parte = pa.PersonaId
			AND pa.AsuntoNeunId = aud.AsuntoNeunId
			AND pa.StatusReg = 1 
		LEFT JOIN CatEmpleados e WITH(NOLOCK)	ON aud.EmpleadoId = e.EmpleadoId
			AND e.StatusRegistro = 1
		LEFT JOIN CatEmpleados s WITH(NOLOCK)
		ON aud.SecretarioId = s.EmpleadoId
			AND s.StatusRegistro = 1
		WHERE 
		    aud.OrganoId=@pi_CatOrganismoId 			
			AND aud.StatusReg=1	
			AND (fecha.ValorCampoAsunto BETWEEN CONVERT(date,@pi_FAudIni) AND CONVERT(date,@pi_FAudFin))
			AND ((@pi_Expediente IS NULL) OR (@pi_Expediente = asu.AsuntoAlias )) 
			AND ((@pi_Persona IS NULL) OR (@pi_Persona = pa.Nombre ) OR (@pi_Persona = e.Nombre ) OR (@pi_Persona = s.Nombre ))
		ORDER BY FechaAudiencia , HoraAudiencia ASC		
	END
	ELSE
	BEGIN
	    SELECT
		      CONVERT(INT,aud.AsuntoNeunId) IdNeun			
			, cta.Descripcion  TipoAsunto
			, asu.AsuntoAlias  Expediente
			, CONVERT(INT,caud.IdTipoAudiencia) IdTipoAudiencia 
			, caud.Descripcion  Audiencia
			, CONVERT(INT,aud.AgendaId) IdAgenda
			, CASE WHEN aud.Parte = 0 THEN 'Todas las Partes' ELSE ISNULL(pa.Nombre,'') + ' ' + ISNULL(pa.APaterno,'') + ' ' + ISNULL(pa.AMaterno,'') END AS Parte
			, CONVERT(VARCHAR(10),CONVERT(DATE, fecha.ValorCampoAsunto)) FechaAudiencia
			, CONVERT(VARCHAR(10),CONVERT(TIME, hora.ValorCampoAsunto)) HoraAudiencia
			, ISNULL(cr.Descripcion, '') Resultado
			, CONVERT(INT,cr.IdResultado) IdResultado
			, ISNULL(LTRIM(UPPER(e.Nombre)) + ' ' + UPPER(e.ApellidoPaterno) + ' ' + UPPER(e.ApellidoMaterno), 'Sin Valor') Empleado
			, ISNULL(LTRIM(UPPER(s.Nombre)) + ' ' + UPPER(s.ApellidoPaterno) + ' ' + UPPER(s.ApellidoMaterno), 'Sin Valor') Secretario			
		FROM [AUD_AsuntosDetalleFechas] aud WITH(NOLOCK)
		INNER JOIN AUD_CatTipoAudiencia caud WITH(NOLOCK) ON aud.AudienciaId = caud.IdTipoAudiencia
		INNER JOIN AsuntosDetalleFechas fecha WITH(NOLOCK) ON aud.ControlFecha=fecha.TipoAsuntoId 
			AND aud.FechaId=fecha.AsuntoDetalleFechasId 
			AND aud.AsuntoNeunId = fecha.AsuntoNeunId
			AND aud.AsuntoId = fecha.AsuntoId
		INNER JOIN AsuntosDetalleFechas hora WITH(NOLOCK) ON aud.ControlHora=hora.TipoAsuntoId 
			AND aud.HoraId=hora.AsuntoDetalleFechasId 
			AND aud.AsuntoNeunId = hora.AsuntoNeunId
			AND aud.AsuntoId = hora.AsuntoId
		INNER JOIN Asuntos asu WITH(NOLOCK)	ON asu.AsuntoNeunId = aud.AsuntoNeunId
		INNER JOIN CatTiposAsunto cta WITH(NOLOCK) ON asu.CatTipoAsuntoId = cta.CatTipoAsuntoId
			AND cta.StatusReg = 1
		LEFT JOIN AUD_CatResultado cr WITH(NOLOCK)	ON aud.ResultadoId = cr.IdResultado
		LEFT JOIN PersonasAsunto pa WITH(NOLOCK) ON aud.Parte = pa.PersonaId
			AND pa.AsuntoNeunId = aud.AsuntoNeunId
			AND pa.StatusReg = 1 
		LEFT JOIN CatEmpleados e WITH(NOLOCK)	ON aud.EmpleadoId = e.EmpleadoId
			AND e.StatusRegistro = 1
		LEFT JOIN CatEmpleados s WITH(NOLOCK)
		ON aud.SecretarioId = s.EmpleadoId
			AND s.StatusRegistro = 1
		WHERE 
		    aud.OrganoId=@pi_CatOrganismoId 			
			AND aud.StatusReg=1			
			AND ((@pi_Expediente IS NULL) OR (@pi_Expediente = asu.AsuntoAlias )) 
			AND ((@pi_Persona IS NULL) OR (@pi_Persona = pa.Nombre ) OR (@pi_Persona = e.Nombre ) OR (@pi_Persona = s.Nombre ))
			ORDER BY FechaAudiencia , HoraAudiencia ASC	
	END;
END