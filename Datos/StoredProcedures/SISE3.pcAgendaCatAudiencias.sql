SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================= 
-- Autor: Anabel Gonzalez
-- Fecha de Creación: 14 de Junio 2024
-- Descripción: Catalogo de audiencias por tipo de organismo y tipo de asunto
-- Ejemplo : EXEC [SISE3].[pcAgendaCatAudiencias]  340,124,1
-- ============================================= 
CREATE PROCEDURE [SISE3].[pcAgendaCatAudiencias]
(
 @pi_CatTipoOrganismoId INT
,@pi_IdTipoAsunto INT
,@pi_IdTipoAgenda INT
)
AS
BEGIN
	SET NOCOUNT ON
		BEGIN TRY
			SELECT 
			CONVERT(INT,cat.IdTipoAudiencia) IdAudiencia
			,cat.Descripcion
			,FAud
			,HAud
			,FDif
			,HDif
			,UsaPartes
			FROM AUD_CatTipoAudiencia cat WITH(NOLOCK)
			INNER JOIN AUD_TipoAudienciaPorAsunto rel WITH(NOLOCK) ON cat.IdTipoAudiencia = rel.IdTipoAudiencia 
			WHERE rel.IdTipoAsunto = @piIdTipoAsunto
			AND cat.IdTipoAgenda = @piIdTipoAgenda
			AND rel.CatTipoOrganismoId=@piCatTipoOrganismoId
			AND rel.Activo = 1
			GROUP By cat.IdTipoAudiencia,cat.Descripcion,FAud,HAud,FDif,HDif,UsaPartes
		END TRY 
		BEGIN CATCH
			EXECUTE dbo.usp_GetErrorInfo;
		END CATCH;
		SET NOCOUNT OFF
END