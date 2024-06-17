USE [SISE_NEW]
GO
/****** Object:  StoredProcedure [SISE3].[pcTableroProyectoCatalogoCargo]    Script Date: 18/04/2024 02:54:35 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: Mario A Hernández Román
-- Create date: 20/03/2024
-- Description:	Realiza el conteo y registros de Módulo Proyecto
-- Version: 1.3
-- EXEC [SISE3].[pcTableroProyectoCatalogoCargo] 180, 2  -- 1 = Titulares, 2 = Secretarios
-- =============================================

CREATE   PROCEDURE [SISE3].[pcTableroProyectoCatalogoCargo](
	@pi_CatOrganismoId INT,
	@pi_CargoId INT  -- '5' = Titular  '18,19' = Secretarios
) AS BEGIN

		SET NOCOUNT ON

			DECLARE @pi_sCargoId VARCHAR(100)

			IF (@pi_CargoId = 1) BEGIN
				SET @pi_sCargoId  = '5'
			END

			IF (@pi_CargoId = 2) BEGIN
				SET @pi_sCargoId  = '18,19'
			END

		BEGIN TRY
			-- Consulta la vista de TITULARES ORGANISMOS 
			SELECT 
				viOrganismosTitulares.EmpleadoId,
				[CargoId],
				[CargoDescripcion],
				NombreEmpleado = SISE3.ConcatenarNombres(
					s.Nombre, 
					s.ApellidoPaterno, 
					s.ApellidoMaterno
				)
			FROM 
				[viOrganismosTitulares] with(nolock)
			LEFT JOIN
				CatEmpleados s WITH(NOLOCK) 
			ON 
				s.EmpleadoId = viOrganismosTitulares.EmpleadoId
			WHERE 
				CatOrganismoId = @pi_CatOrganismoId 
			AND 
				viOrganismosTitulares.EmpleadoId NOT IN (
						SELECT 
							EmpleadoId 
						FROM 
							EmpleadoOrganismo WITH(NOLOCK) 
						WHERE 
							CatOrganismoId in (3,1580) 
							AND StatusRegistro = 1
					)
					and not exists(
						SELECT 1 
						FROM 
							EmpleadosNoVisibles WITH(NOLOCK) 
						WHERE fkEmpleadoId = [viOrganismosTitulares].[EmpleadoId] 
							and fkEstatusId = 1
					)
					AND CargoId IN (
						SELECT 
							CAST(value AS INT)
						FROM 
							STRING_SPLIT(@pi_sCargoId , ',')
					)

			  ORDER BY 3 DESC  -- [Nombre Completo]

		END TRY
		BEGIN CATCH
			-- Ejecuta la rutina de recuperacion de errores.
			EXECUTE dbo.usp_GetErrorInfo;
		END CATCH;
		SET NOCOUNT OFF
END;

