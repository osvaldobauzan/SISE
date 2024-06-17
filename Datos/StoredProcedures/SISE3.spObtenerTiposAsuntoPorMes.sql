
USE SISE_NEW;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

/****** Object:  StoredProcedure [SISE3].[spObtenerTiposAsuntoPorMes]    Script Date: 5/20/2024 6:30:00 PM ******/
-- =============================================
-- Author:       Erick Gonzalez
-- Create date:  20/05/2024
-- Description:  Obtiene el conteo de tipos de asunto por mes en los últimos 12 meses,
--               relacionados con las promociones, para un EmpleadoId dado.
-- =============================================
CREATE PROCEDURE [SISE3].[spObtenerTiposAsuntoPorMes]
    @EmpleadoId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    -- Calcular la fecha de inicio y fin para los últimos 12 meses
    DECLARE @fechaFin DATETIME = GETDATE();
    DECLARE @fechaInicio DATETIME = DATEADD(MONTH, -11, @fechaFin);

    -- Realizar la consulta principal
    SELECT
        DATENAME(MONTH, p.FechaPresentacion) AS Mes,
        cta.Descripcion AS TipoAsunto,
        COUNT(*) AS Total
    FROM
        Promociones p
    INNER JOIN
        Asuntos a ON p.AsuntoNeunId = a.AsuntoNeunId
    INNER JOIN
        CatTiposAsunto cta ON a.CatTipoAsuntoId = cta.CatTipoAsuntoId
    WHERE
        p.FechaPresentacion BETWEEN @fechaInicio AND @fechaFin
        AND p.RegistroEmpleadoId = @EmpleadoId
    GROUP BY
        DATENAME(MONTH, p.FechaPresentacion),
        DATEPART(MONTH, p.FechaPresentacion),
        cta.Descripcion
    ORDER BY
        DATEPART(MONTH, p.FechaPresentacion),
        cta.Descripcion;

    SET NOCOUNT OFF;
END
GO