USE SISE_NEW
GO

-- ====================================================================================
-- Author:        Erick Gonzales
-- Create date:   06/06/2024
-- Description:   Este procedimiento almacenado obtiene una lista de empleados junto con 
--                su información asociada a un organismo específico y cargo(s) específico(s).
-- ====================================================================================
ALTER PROCEDURE [SISE3].[sp_ObtenerEmpleadosPorOrganismoYCargo]
    @pi_CatOrganismoId INT,        -- Identificador del organismo
    @pi_CargoId NVARCHAR(MAX) = '15'      -- Lista de identificadores de cargos separados por comas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT
        ce.EmpleadoId,
        eo.CargoId,
        nombreOficial = LTRIM(UPPER(ISNULL(ce.Nombre + ISNULL(' ' + ce.ApellidoPaterno, '') + ISNULL(' ' + ce.ApellidoMaterno, ''), ''))),
        ce.UserName,
        area.Nombre
    FROM CatEmpleados ce
    INNER JOIN EmpleadoOrganismo eo ON ce.EmpleadoId = eo.EmpleadoId
    INNER JOIN [SISE3].SplitString(@pi_CargoId, ',') cargos ON eo.CargoId = CAST(cargos.Item AS INT)
    LEFT JOIN Areas area ON eo.EmpleadoId = area.EmpleadoId
    WHERE
        eo.CatOrganismoId = @pi_CatOrganismoId
        AND ce.FechaActivacion IS NOT NULL
        AND area.Nombre IS NOT NULL
    ORDER BY nombreOficial;
END
GO
