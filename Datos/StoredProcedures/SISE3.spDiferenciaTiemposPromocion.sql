USE SISE_NEW;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

/****** Object:  StoredProcedure [SISE3].[spDiferenciaTiemposPromocion]    Script Date: 5/20/2024 6:45:00 PM ******/
-- =============================================
-- Author:       Erick Gonzalez
-- Create date:  20/05/2024
-- Description:  Obtiene la lista de promociones únicas junto con la hora y minutos
--               en que fueron dadas de alta y la hora y minuto en que fueron turnadas,
--               considerando solo los registros donde EstatusArchivo es 1,
--               No se filtra explícitamente por organismo, ya que el IdEmpleado previamente
--               fue filtrado.
-- =============================================
ALTER PROCEDURE [SISE3].[spDiferenciaTiemposPromocion]
    @EmpleadoId BIGINT,
    @fechaInicio DATETIME,
    @fechaFin DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    -- Obtiene el año del rango de búsqueda para evitar el NúmeroRegistro
    DECLARE @YearActual INT = YEAR(@fechaFin);

    -- Realizar la consulta principal
    SELECT
        p.RegistroEmpleadoId,
        p.NumeroRegistro,
        p.HoraPresentacion AS HoraMinutoAlta,
        ISNULL(FORMAT(pa.FechaAlta, 'HH:mm'), '00:00') AS HoraMinutoTurnado
    FROM
        Promociones p
    LEFT JOIN
        PromocionArchivos pa ON p.AsuntoNeunId = pa.AsuntoNeunId
                             AND p.NumeroOrden = pa.NumeroOrden
                             AND pa.EstatusArchivo = 1
    WHERE
        p.FechaPresentacion BETWEEN @fechaInicio AND @fechaFin
        AND p.RegistroEmpleadoId = @EmpleadoId
        AND p.YearPromocion = @YearActual
    ORDER BY
        p.FechaAlta;

    SET NOCOUNT OFF;
END
GO