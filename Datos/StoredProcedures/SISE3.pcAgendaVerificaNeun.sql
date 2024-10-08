USE [SISE_NEW]
GO
/****** Object:  StoredProcedure [SISE3].[pcAgendaVerificaNeun]    Script Date: 14/06/2024 01:30:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================= 
-- Autor: Anabel Gonzalez
-- Fecha de Creación: 14 de Junio 2024
-- Descripción: Validación de si el idNeun existe
-- Ejemplo : EXEC [SISE3].[pcAgendaVerificaNeun] 30315414,180
-- ============================================= 
CREATE PROCEDURE [SISE3].[pcAgendaVerificaNeun]
@pi_Neun INT,
@pi_CatOrganismoId INT
AS
	BEGIN
		SET NOCOUNT ON
		BEGIN TRY
		DECLARE @hayexpediente varchar
		
		SET @hayexpediente = dbo.fnDevuelveAliasExpediente(@pi_Neun)
		IF @hayexpediente != '' 
		BEGIN
		SELECT '1' As Resultado
		END
		ELSE
		BEGIN
		SELECT '0' As Resultado
		END
		END TRY
		BEGIN CATCH
			EXECUTE dbo.usp_GetErrorInfo;
		END CATCH;
		SET NOCOUNT OFF
	END