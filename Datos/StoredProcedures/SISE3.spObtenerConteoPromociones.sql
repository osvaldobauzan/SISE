USE SISE_NEW;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

/****** Object:  StoredProcedure [SISE3].[spObtenerConteoPromociones]    Script Date: 5/17/2024 2:30:00 PM ******/
-- =============================================
-- Author:       Erick Gonzalez
-- Create date:  17/05/2024
-- Description:  Obtiene el conteo de promociones en un rango de fechas determinado,
--               ligadas a un oficial y su OrganismoId, y cuenta cuántas de esas 
--               promociones han sido turnadas, así como el total de promociones
--               del año actual por oficial, teniendo en cuenta NumeroOrden y AsuntoNeunId.
-- =============================================
ALTER PROCEDURE [SISE3].[spObtenerConteoPromociones]
    @fechaInicio DATETIME,
    @fechaFin DATETIME,
    @pi_CatOrganismoId INT,
    @pi_CargoId INT = 22
AS
BEGIN
    SET NOCOUNT ON;

    -- Crear una tabla temporal para almacenar los oficiales
    CREATE TABLE #Oficiales (
        EmpleadoId BIGINT,
        CargoId INT,
        NombreOficial VARCHAR(255),
        Permisos VARCHAR(255),
        UserName VARCHAR(255)
    );

    -- Insertar los oficiales en la tabla temporal
    INSERT INTO #Oficiales (EmpleadoId, CargoId, NombreOficial, Permisos, UserName)
    EXEC [SISE3].[pcObtieneCatalogoOficiales] @pi_CatOrganismoId, @pi_CargoId;

    -- Obtener el año actual
    DECLARE @YearActual INT = YEAR(GETDATE());

    -- Crear una tabla temporal para las promociones únicas
    CREATE TABLE #PromocionesUnicas (
        EmpleadoId BIGINT,
        AsuntoNeunId BIGINT,
        NumeroOrden INT,
		FechaAlta DATETIME
    );

    -- Insertar las promociones únicas en la tabla temporal
    INSERT INTO #PromocionesUnicas (EmpleadoId, AsuntoNeunId, NumeroOrden,FechaAlta)
    SELECT DISTINCT
        p.RegistroEmpleadoId,
        p.AsuntoNeunId,
        p.NumeroOrden,
		p.FechaAlta
    FROM
        dbo.Promociones p
    WHERE
        p.FechaPresentacion BETWEEN @fechaInicio AND @fechaFin
        AND p.CatOrganismoId = @pi_CatOrganismoId;

	    -- Calcular el número de días en el rango especificado
    DECLARE @NumeroDias INT;
    SET @NumeroDias = DATEDIFF(DAY, @fechaInicio, @fechaFin) + 1;

    -- Realizar la consulta principal
    SELECT
        o.EmpleadoId,
        o.NombreOficial,
        o.UserName,
        COUNT(pu.NumeroOrden) AS TotalPromociones,
        SUM(CASE WHEN pa.EstatusArchivo = 1 THEN 1 ELSE 0 END) AS PromocionesTurnadas,
        (SELECT COUNT(*)
         FROM dbo.Promociones
         WHERE RegistroEmpleadoId = o.EmpleadoId
           AND FechaPresentacion >= @YearActual
           AND CatOrganismoId = @pi_CatOrganismoId) AS TotalPromocionesAnoActual,
		CAST(SUM(CASE WHEN pa.EstatusArchivo = 1 THEN 1 ELSE 0 END) AS FLOAT) / @NumeroDias AS PromedioPromocionesTurnadasPorDia,
		AVG(CASE WHEN pa.EstatusArchivo = 1 THEN DATEDIFF(MINUTE, pu.FechaAlta, pa.FechaAlta) ELSE NULL END) AS TiempoPromedioMins
    FROM
        #PromocionesUnicas pu
    INNER JOIN
        #Oficiales o ON pu.EmpleadoId = o.EmpleadoId
    LEFT JOIN
        dbo.PromocionArchivos pa ON pu.AsuntoNeunId = pa.AsuntoNeunId AND pu.NumeroOrden = pa.NumeroOrden AND pu.EmpleadoId = pa.RegistroEmpleadoSISEId
        AND pa.EstatusArchivo = 1
    GROUP BY
        o.EmpleadoId, o.NombreOficial, o.UserName
    ORDER BY
        o.NombreOficial;

    -- Eliminar las tablas temporales
    DROP TABLE #Oficiales;
    DROP TABLE #PromocionesUnicas;

    SET NOCOUNT OFF;
END
GO