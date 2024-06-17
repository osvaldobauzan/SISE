USE [SISE_NEW]
GO
/****** Object:  StoredProcedure [SISE3].[paActualizarResultadoAudiencia]    Script Date: 14/06/2024 01:27:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================= 
-- Autor: Anabel Gonzalez
-- Fecha de Creación: 14 de Junio 2024
-- Descripción: Ejecuta la actualización de resultado por audiencia
-- Ejemplo : EXEC [SISE3].[paActualizarResultadoAudiencia] 3047, 65
-- Resultado: IdResultado,Descripcion y IdAgenda
-- ============================================= 
CREATE PROCEDURE [SISE3].[paActualizarResultadoAudiencia]
@pi_IdAgenda INT,
@pi_IdResultado INT
AS
	BEGIN
		SET NOCOUNT ON
		BEGIN TRY

		UPDATE [AUD_AsuntosDetalleFechas] SET  [ResultadoId] = @pi_IdResultado
		WHERE AgendaId=@pi_IdAgenda

		SELECT 
		ADF.ResultadoId IdResultado
		,CatResult.Descripcion
	    , CONVERT(INT,ADF.AgendaId) IdAgenda		
		FROM [AUD_AsuntosDetalleFechas] ADF WITH(NOLOCK)
		INNER JOIN AUD_CatResultado CatResult WITH(NOLOCK) ON CatResult.IdResultado = ADF.ResultadoId 
		WHERE ADF.AgendaId = @pi_IdAgenda AND ADF.ResultadoId = @pi_IdResultado
		 
		END TRY
		BEGIN CATCH
			EXECUTE dbo.usp_GetErrorInfo;
		END CATCH;
		SET NOCOUNT OFF
	END
