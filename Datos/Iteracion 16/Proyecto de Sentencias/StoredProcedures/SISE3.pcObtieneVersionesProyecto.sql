USE [SISE_NEW]
GO
/****** Object:  StoredProcedure [SISE3].[pcObtieneVersionesProyecto]    Script Date: 18/04/2024 02:53:59 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- =============================================
-- Author:	Daniel A. Rangel Gavia - DGETD
-- Alter date: 19/03/2024
-- Objetivo: Obtiene el listado de las versiones de proyectos cargadas y revisadas de un NeunId dado.
-- EXEC  [SISE3].[pcObtieneVersionesProyecto] 16181909   30315604
-- =============================================
CREATE   PROCEDURE [SISE3].[pcObtieneVersionesProyecto]
    (
    @pi_AsuntoNeunId BIGINT
)

AS
BEGIN
	
	DECLARE @pi_TipoAsuntoId INT = 1
	DECLARE @pi_TipoAsunto VARCHAR(250) = 'Amparo Indirecto'
	DECLARE @pi_CuadernoId INT = 1
	DECLARE @pi_Cuaderno VARCHAR(250) = 'Principal'

	SELECT 
		ast.AsuntoNeunId,
		ast.AsuntoAlias,
		TipoAsuntoId = @pi_TipoAsuntoId,
		TipoAsunto = @pi_TipoAsunto,
		CuadernoId = @pi_CuadernoId,
		Cuaderno = @pi_Cuaderno,
		p.pkProyectoId,
		p.[iVersion],
		P.[fFechaAlta],
		p.[iEstado],
		p.[iTitular], 
		(
			SELECT 
				SISE3.ConcatenarNombres(
					e.Nombre,
					e.ApellidoPaterno,
					e.ApellidoMaterno
		) FROM 
				dbo.CatEmpleados e WITH(NOLOCK)
			WHERE 
				e.EmpleadoId = p.[iTitular] 
		) AS NombreTitular,
		p.[iSecretario],
		(SELECT SISE3.ConcatenarNombres(es.Nombre,es.ApellidoPaterno, es.ApellidoMaterno) FROM dbo.CatEmpleados es WITH(NOLOCK) WHERE  es.EmpleadoId = p.[iSecretario] ) AS NombreSecretario ,
		(SELECT pe.[sProyectoEstado] FROM SISE3.[CAT_ProyectoEstado] pe WITH(NOLOCK) WHERE pe.[pkProyectoEstadoId] = p.[iEstado]) AS EstadoProyecto,
		p.[iSentidoId],
		(SELECT s.[sSentido] FROM SISE3.[CAT_Sentido] s WITH(NOLOCK) WHERE s.[pkSentidoId] = p.[iSentidoId]) AS SentidoSentencia,
		p.[iTipoSentenciaId],
		(SELECT t.[sTipoSentencia] FROM SISE3.[CAT_TipoSentencia] t WITH(NOLOCK) WHERE t.[pkTipoSentenciaId] = p.[iTipoSentenciaId]) AS TipoSentencia,
		p.[sSintesis] AS ComentarioSecretario,
		p.[sCorreccionComentario] AS ComentarioTitular,
		p.[fkProyectoVersionArchivoId],
		P.[fkCorreccionArchivoId],
		(SELECT ca.[sNombreArchivo] FROM [SISE3].[ProyectoArchivo] ca WITH(NOLOCK) WHERE ca.[pkProyectoArchivoId] = p.[fkCorreccionArchivoId] AND ca.[iStatusReg] = 1) AS CorreccionArchivo,
		pa.pkProyectoArchivoId,
		pa.sNombreArchivo,
		pa.sNombreArchivoReal,
		pa.iRutaArchivoNAS,
		pa.fFechaAlta AS fFechaAltaArchivoProyecto,
		pa.iRegistroEmpleadoId,
		pa.sIPUsuario,
		pa.iStatusReg,
		pa.sAnioRuta,
		pa.CatOrganismoId

	FROM 
		SISE3.[Proyecto] p WITH(NOLOCK)
		CROSS APPLY SISE3.fnExpediente(p.AsuntoNeunId) ast
		INNER JOIN SISE3.[ProyectoArchivo] pa ON
			pa.pkProyectoArchivoId = p.fkProyectoVersionArchivoId
	WHERE p.[AsuntoNeunId] = @pi_AsuntoNeunId
	AND p.[iStatusReg] = 1
	ORDER BY p.[iVersion] DESC
	
END
