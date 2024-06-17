USE [SISE_NEW]
GO
/****** Object:  UserDefinedFunction [SISE3].[ConcatenarNombres]    Script Date: 29/02/2024 10:05:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		GGHH
-- Create date: 24/07/2023
-- Description:	Concatena varios valores para el nombre
-- Example: SELECT SISE3.ConcatenarNombres( 'D.', '', '')
-- =============================================
CREATE FUNCTION [SISE3].[ConcatenarNombres]
(
	@pi_Primero VARCHAR(100), 
	@pi_Segundo VARCHAR(100), 
	@pi_Tercero VARCHAR(100)
)
RETURNS VARCHAR(300)
AS
BEGIN

	DECLARE @ps_Nombre VARCHAR(300)
	
	SET @pi_Primero = IIF(LEN(LTRIM(RTRIM(@pi_Primero)))= 1, REPLACE(@pi_Primero, '.', ' '),@pi_Primero)
	SET @pi_Segundo = IIF(LEN(LTRIM(RTRIM(@pi_Segundo)))= 1, REPLACE(@pi_Segundo, '.', ' '),@pi_Segundo)
	SET @pi_Tercero = IIF(LEN(LTRIM(RTRIM(@pi_Tercero)))= 1, REPLACE(@pi_Tercero, '.', ' '),@pi_Tercero)

	SET @ps_Nombre = RTRIM(CONCAT(
		IIF((@pi_Primero IS NOT NULL AND LEN(@pi_Primero) > 0 AND @pi_Primero <> 'Sin valor'), @pi_Primero, ''),
		IIF((@pi_Segundo IS NOT NULL AND LEN(@pi_Segundo) > 0 AND @pi_Segundo <> 'Sin valor'), ' ' + @pi_Segundo, ''),
		IIF((@pi_Tercero IS NOT NULL AND LEN(@pi_Tercero) > 0 AND @pi_Tercero <> 'Sin valor'), ' ' + @pi_Tercero, '')
		))

	RETURN @ps_Nombre

END
GO
