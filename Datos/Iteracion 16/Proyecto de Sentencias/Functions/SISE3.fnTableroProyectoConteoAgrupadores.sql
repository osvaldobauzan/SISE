USE [SISE_NEW]
GO
/****** Object:  UserDefinedFunction [SISE3].[fnTableroProyectoConteoAgrupadores]    Script Date: 18/04/2024 02:57:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: Mario A Hernández Román
-- Create date: 11/03/2024
-- Version: 2
-- Description:	Realiza el conteo de los filtros agrupadores del TableroProyectoSentencias
-- SELECT * FROM [SISE3].[fnTableroProyectoConteoAgrupadores](180, NULL, NULL, NULL)
-- =============================================

CREATE   FUNCTION [SISE3].[fnTableroProyectoConteoAgrupadores](	
	@pi_CatOrganismoId INT,	
	@pi_FechaPresentacionIni DATE=NULL,
	@pi_FechaPresentacionFin DATE=NULL,
	@pi_Texto VARCHAR(MAX)=NULL
)
RETURNS @TableroProyectoConteos TABLE (
	TotalProyectos INT,
	TotalSinProyecto INT,
	TotalParaRevision INT,
	TotalNoAprobado INT,
	TotalConAjustes INT,
	TotalAprobado INT 
)
AS BEGIN

	DECLARE @TotalProyectos INT
	DECLARE @TotalSinProyecto INT
	DECLARE @TotalParaRevision INT
	DECLARE @TotalNoAprobado INT
	DECLARE @TotalConAjustes INT
	DECLARE @TotalAprobado INT 

	DECLARE @pi_Estados VARCHAR(30) 
	
	-- Conteo Sin proyecto
	SET @pi_Estados = '1'
	SET @TotalSinProyecto = (
		SELECT 
			COUNT(ftp.AsuntoNeunId) 
		FROM 
			[SISE3].[fnTableroProyectoV3](	
				@pi_CatOrganismoId,
				@pi_Estados,
				@pi_Texto,
				@pi_FechaPresentacionIni,
				@pi_FechaPresentacionFin
			) ftp
	)

	-- Conteo Para revision
	SET @pi_Estados = '2'
	SET @TotalParaRevision = (
		SELECT 
			COUNT(AsuntoNeunId)
		FROM 
			[SISE3].[fnTableroProyectoV3](	
				@pi_CatOrganismoId,
				@pi_Estados,
				@pi_Texto,
				@pi_FechaPresentacionIni,
				@pi_FechaPresentacionFin
			)
	)

	-- Conteo No aprobados
	SET @pi_Estados = '3'
	SET @TotalNoAprobado = (
		SELECT 
			COUNT(AsuntoNeunId)
		FROM 
			[SISE3].[fnTableroProyectoV3](	
				@pi_CatOrganismoId,
				@pi_Estados,
				@pi_Texto,
				@pi_FechaPresentacionIni,
				@pi_FechaPresentacionFin
			)
	)

	-- Conteo Con Ajuste de Fondo y Forma
	SET @pi_Estados = '4,5'
	SET @TotalConAjustes = (
		SELECT 
			COUNT(AsuntoNeunId)
		FROM 
			[SISE3].[fnTableroProyectoV3](	
				@pi_CatOrganismoId,
				@pi_Estados,
				@pi_Texto,
				@pi_FechaPresentacionIni,
				@pi_FechaPresentacionFin
			)
	)

	-- Conteo Aprobados
	SET @pi_Estados = '6'
	SET @TotalAprobado = (
		SELECT 
			COUNT(AsuntoNeunId)
		FROM 
			[SISE3].[fnTableroProyectoV3](	
				@pi_CatOrganismoId,
				@pi_Estados,
				@pi_Texto,
				@pi_FechaPresentacionIni,
				@pi_FechaPresentacionFin
			)
	)

	-- Conteo Total
	SET @pi_Estados = '1,2,3,4,5,6'
	SET @TotalProyectos = (	
		SELECT 
			COUNT(ftp.AsuntoNeunId) 
		FROM 
			[SISE3].[fnTableroProyectoV3](	
				@pi_CatOrganismoId,
				@pi_Estados,
				@pi_Texto,
				@pi_FechaPresentacionIni,
				@pi_FechaPresentacionFin
			) ftp
	)

	-- Conteos de los agrupadores
	INSERT INTO @TableroProyectoConteos
		SELECT 
			@TotalProyectos AS totalProyectos, 
			@TotalSinProyecto AS totalSinProyecto, 
			@TotalParaRevision AS totalParaRevision, 
			@TotalNoAprobado AS totalNoAprobado,
			@TotalConAjustes AS totalConAjustes, 
			@TotalAprobado AS totalAprobado

	RETURN 
END
