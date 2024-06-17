USE [SISE_NEW]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------------------------------------------------------------------------------
-- =============================================
-- Author:  Erick Gonzalezs
-- Alter date:  09/05/2024
-- Description: Obtiene la información de los oficiales para un organismo y cargo específico
-- EXEC [SISE3].[pcObtieneCatalogoOficiales] 180, 17
-- =============================================
ALTER PROCEDURE [SISE3].[pcObtieneCatalogoOficiales]
    -- REPRESENTA EL IDENTIFICADOR DEL ORGANISMO
    @pi_CatOrganismoId INT,
    -- REPRESENTA LOS IDENTIFICADORES DEL CARGO COMO UNA LISTA SEPARADA POR COMAS
    @pi_CargoId NVARCHAR(MAX) = '22'
AS
BEGIN
    -- Selecciona información única de los empleados que tienen un cargo específico en un organismo específico
    SELECT DISTINCT
        ce.EmpleadoId,
        eo.CargoId,
        nombreOficial = LTRIM(UPPER(ISNULL(ce.Nombre + ISNULL(' ' + ce.ApellidoPaterno, '') + ISNULL(' ' + ce.ApellidoMaterno, ''), ''))),
        ce.Permisos,
        ce.UserName
    FROM CatEmpleados ce
    INNER JOIN EmpleadoOrganismo eo ON ce.EmpleadoId = eo.EmpleadoId
    INNER JOIN [SISE3].SplitString(@pi_CargoId, ',') cargos ON eo.CargoId = CAST(cargos.Item AS INT)
    WHERE
        eo.CatOrganismoId = @pi_CatOrganismoId
        AND ce.FechaActivacion IS NOT NULL
       -- AND ce.OnLine = 1 Se documenta flag Online pués se consideró para un borrado o activación lógica, cuando en realidad es un campo heradado de sise2
    ORDER BY nombreOficial;
END
