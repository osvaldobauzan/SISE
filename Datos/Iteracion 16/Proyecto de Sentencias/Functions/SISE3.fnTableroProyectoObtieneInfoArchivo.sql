USE [SISE_NEW]
GO
/****** Object:  UserDefinedFunction [SISE3].[fnTableroProyectoObtieneInfoArchivo]    Script Date: 18/04/2024 02:57:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: Mario A Hernández Román
-- Create date: 09/04/2024
-- Description:	Obtiene informaci'on del archivo y el NEun Asociado
-- Version: 1.3
-- SELECT * FROM [SISE3].[fnTableroProyectoObtieneInfoArchivo](418)
-- =============================================

CREATE   FUNCTION [SISE3].[fnTableroProyectoObtieneInfoArchivo](	
	@pi_pkProyectoArchivoId BIGINT
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT DISTINCT 
		AsuntoNeunId = IIF(
			pap.AsuntoNeunId IS NULL, 
			pac.AsuntoNeunId, 
			pap.AsuntoNeunId
		),
		pa.pkProyectoArchivoId,
		pa.sNombreArchivo,
		pa.sNombreArchivoReal,
		pa.iRutaArchivoNAS,
		pa.fFechaAlta,
		pa.iRegistroEmpleadoId,
		pa.sIPUsuario,
		pa.iStatusReg,
		pa.sAnioRuta,
		pa.CatOrganismoId
	FROM 
		SISE3.ProyectoArchivo pa  WITH(NOLOCK)
		LEFT JOIN SISE3.Proyecto pap  WITH(NOLOCK) ON
		pa.pkProyectoArchivoId = pap.fkProyectoVersionArchivoId
		LEFT JOIN SISE3.Proyecto pac  WITH(NOLOCK) ON
		pa.pkProyectoArchivoId = pac.fkCorreccionArchivoId
	WHERE 
		pkProyectoArchivoId = @pi_pkProyectoArchivoId
		AND pa.iStatusReg = 1
)
