USE [SISE_NEW]
GO
/****** Object:  UserDefinedFunction [SISE3].[fnEstatusPromocion]    Script Date: 29/02/2024 10:05:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- =============================================
-- Author:		Diana Quiroga Microsoft
-- Create date: 06/09/2023
-- Description:	Retorna el Estatus de la promoción
-- Example: SELECT [SISE3].[fnEstatusPromocion] (NULL, 1, NULL, 5)
-- =============================================
CREATE FUNCTION [SISE3].[fnEstatusPromocion]
(
	@pi_catAutorizacionDocumentosId INT NULL, 
	@pi_EsPromocionE INT, 
	@pi_NombreArchivo NVARCHAR(300),
	@pi_Origen INT = 0,
	@pi_kIdElectronica BIGINT NULL
)
RETURNS INT
AS
BEGIN

	DECLARE @ps_Estatus VARCHAR(300)
	SET @ps_Estatus  = (CASE 
							--WHEN @pi_EsPromocionE = 1 THEN 1 
							WHEN @pi_EsPromocionE = 1 AND @pi_kIdElectronica IS NOT NULL THEN 1 
							WHEN @pi_EsPromocionE = 1 THEN 4
							WHEN  @pi_EsPromocionE = 0 AND @pi_catAutorizacionDocumentosId IS NULL 	AND @pi_NombreArchivo IS NULL THEN 2
							WHEN  @pi_EsPromocionE = 0 AND @pi_NombreArchivo IS NOT NULL THEN 4							
							ELSE 0
						END )
	
	
	RETURN @ps_Estatus

END
GO
