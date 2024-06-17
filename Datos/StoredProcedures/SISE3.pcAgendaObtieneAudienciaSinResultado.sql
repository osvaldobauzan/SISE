USE [SISE_NEW]
GO
/****** Object:  StoredProcedure [SISE3].[pcAgendaObtieneAudienciaSinResultado]    Script Date: 14/06/2024 01:17:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================= 
-- Autor: Anabel Gonzalez
-- Fecha de Creación: 14 de Junio 2024
-- Descripción: Consulta para obtener el resultado de ultima audiencia agendada
-- Ejemplo : EXEC [SISE3].[pcAgendaObtieneAudienciaSinResultado] 21859696,1,0
-- ============================================= 
CREATE PROCEDURE [SISE3].[pcAgendaObtieneAudienciaSinResultado]
@pi_Neun INT,
@pi_IdTipoAudiencia INT,
@pi_UsaPartes INT
AS
BEGIN
	SET NOCOUNT ON
		BEGIN TRY
			DECLARE 
				@CatTipoAsuntoId INT,
				@FAud INT,
				@FDif INT,
				@FCel INT, 
				@HAud INT, 
				@HDif INT, 
				@HCel INT
  
			SELECT @CatTipoAsuntoId = a.CatTipoAsuntoId
			FROM Asuntos a WITH(NOLOCK)
			WHERE AsuntoNeunId = @pi_Neun
  
			SELECT @FAud=a2.FAud
				, @FDif=a2.FDif
				, @FCel=a2.FCel
				, @HAud=a2.HAud
				, @HDif=a2.HDif
				, @HCel=a2.HCel
			FROM AUD_TipoAudienciaPorAsunto a2 WITH(NOLOCK) 
			WHERE  a2.IdTipoAudiencia = @pi_IdTipoAudiencia 
			AND a2.IdTipoAsunto= @CatTipoAsuntoId
  
			SELECT asu.AsuntoAlias Expediente
				  , fecha.ValorCampoAsunto FechaAudiencia
				  , CONVERT(INT,aud.AgendaId) IdAgenda
				  , aud.ResultadoId Resultado
				  , aud.AudienciaId IdAudiencia
			FROM AUD_AsuntosDetalleFechas aud WITH(NOLOCK)
			INNER JOIN AsuntosDetalleFechas fecha WITH(NOLOCK) 
			ON aud.ControlFecha = fecha.TipoAsuntoId 
				AND aud.FechaId=fecha.AsuntoDetalleFechasId 
				AND aud.AsuntoNeunId = fecha.AsuntoNeunId
				AND aud.AsuntoId = fecha.AsuntoId
				AND aud.ControlFecha IN (@FAud,@FDif,@FCel) 
			INNER JOIN AsuntosDetalleFechas hora WITH(NOLOCK) 
			ON aud.ControlHora=hora.TipoAsuntoId 
				AND aud.HoraId = hora.AsuntoDetalleFechasId 
				AND aud.AsuntoNeunId = hora.AsuntoNeunId
				AND aud.AsuntoId = hora.AsuntoId
				AND aud.ControlHora IN (@HAud,@HDif,@HCel)
			INNER JOIN Asuntos asu WITH(NOLOCK)
			ON aud.AsuntoNeunId = asu.AsuntoNeunId
				AND asu.StatusReg = 1
			WHERE aud.StatusReg = 1 
				--AND aud.ResultadoId = 0 
				AND aud.AudienciaId = @pi_IdTipoAudiencia
				AND Parte = @pi_UsaPartes 
				AND aud.AsuntoNeunId = @pi_Neun
			ORDER BY FechaAudiencia DESC
		END TRY
		BEGIN CATCH
				EXECUTE dbo.usp_GetErrorInfo;
		END CATCH;	
	SET NOCOUNT OFF
END
