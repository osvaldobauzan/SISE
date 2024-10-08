USE [SISE_NEW]
GO
/****** Object:  StoredProcedure [SISE3].[pcAgendaObtineEmpalmes]    Script Date: 14/06/2024 01:11:31 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================= 
-- Autor: Anabel Gonzalez
-- Fecha de Creación: 14 de Junio 2024
-- Descripción: Consulta para validar si existe la agenda de una audiencia en la misma fecha y hora
-- Ejemplo : EXEC [SISE3].[pcAgendaObtineEmpalmes] 180,'2018/02/01','10:00:00'
-- ============================================= 
CREATE PROCEDURE [SISE3].[pcAgendaObtineEmpalmes]
(
 @pi_CatOrganismoId INT					
,@pi_FechaAud DATETIME				
,@pi_HoraAud TIME				
)
AS
	BEGIN
		SET NOCOUNT ON
			BEGIN TRY
				SELECT TOP 1 
					CONVERT(VARCHAR(10),dbo.fnDevuelveAliasExpediente(aud.AsuntoNeunId)) Expediente
					,CONVERT(VARCHAR(10),CONVERT(DATE,fecha.ValorCampoAsunto,103)) FechaAudiencia
					,CONVERT(VARCHAR(10),CONVERT(TIME,hora.ValorCampoAsunto,103)) HoraAudiencia
					,CONVERT(VARCHAR(50),dbo.FNOBTIENEEMPLEADO(aud.EmpleadoId)) Empleado
					,tpaud.Descripcion Descripcion 
				FROM [AUD_AsuntosDetalleFechas] aud WITH(NOLOCK)
				  JOIN AUD_CatTipoAudiencia tpaud WITH(NOLOCK) ON tpaud.IdTipoAudiencia=aud.AudienciaId
				  JOIN AsuntosDetalleFechas fecha WITH(NOLOCK) ON aud.ControlFecha=fecha.TipoAsuntoId 
						AND aud.FechaId=fecha.AsuntoDetalleFechasId 
						AND aud.AsuntoNeunId = fecha.AsuntoNeunId
						AND aud.AsuntoId = fecha.AsuntoId
				  JOIN AsuntosDetalleFechas hora WITH(NOLOCK) ON aud.ControlHora=hora.TipoAsuntoId 
						AND aud.HoraId=hora.AsuntoDetalleFechasId 
						AND aud.AsuntoNeunId = hora.AsuntoNeunId
						AND aud.AsuntoId = hora.AsuntoId
				WHERE aud.StatusReg=1 
				AND aud.OrganoId=@pi_CatOrganismoId 
				AND CONVERT(DATE,fecha.ValorCampoAsunto,103) = @pi_FechaAud 
				AND CONVERT(TIME,hora.ValorCampoAsunto,103) = @pi_HoraAud
				AND tpaud.IdTipoAgenda = 1 
				AND Aud.ResultadoId <> 5
				AND aud.StatusReg = 1
				ORDER BY FechaAudiencia, HoraAudiencia ASC
			END TRY 
			BEGIN CATCH
				EXECUTE dbo.usp_GetErrorInfo;
			END CATCH;
		SET NOCOUNT OFF
	END

