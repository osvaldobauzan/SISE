USE [SISE_NEW]
GO
/****** Object:  StoredProcedure [SISE3].[pcCatResultadoAudiencia]    Script Date: 14/06/2024 01:22:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================= 
-- Autor: Anabel Gonzalez
-- Fecha de Creación: 14 de Junio 2024
-- Descripción: Consulta para obtener los 3 resultados que forman parte de la audiencia
-- Ejemplo : EXEC [SISE3].[pcCatResultadoAudiencia] 12
-- ============================================= 
CREATE PROCEDURE [SISE3].[pcCatResultadoAudiencia]
@pi_IdTipoAudiencia INT
AS
	BEGIN
		SET NOCOUNT ON
		BEGIN TRY
		SELECT 
		     CONVERT(INT,IdResultado) Id
			,Descripcion  Descripcion
		FROM AUD_CatResultado
		WHERE IdTipoAudiencia = @pi_IdTipoAudiencia
			AND CierraAudiencia = 0
			AND StatusReg = 1
		END TRY
		BEGIN CATCH
			EXECUTE dbo.usp_GetErrorInfo;
		END CATCH;
		SET NOCOUNT OFF
	END
