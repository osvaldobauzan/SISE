USE [SISE_NEW]
GO

-- =====================================================================================
-- Procedimiento Almacenado: spDiferenciaTiemposNotificacion
-- Descripción: Este procedimiento obtiene información de notificaciones electrónicas
--              para un actuario específico dentro de un rango de fechas. Calcula la
--              diferencia en días entre la fecha de asignación y la fecha de notificación,
--              y también incluye el día del mes de ambas fechas.
-- =====================================================================================
-- Parámetros de Entrada:
--   @EmpleadoId      - Identificador del empleado (actuario).
--   @fechaInicio     - Fecha de inicio del rango a consultar.
--   @fechaFin        - Fecha de fin del rango a consultar.
-- =====================================================================================
-- Resultados:
--   AsuntoAlias      - Alias del asunto.
--   ActuarioId       - Identificador del actuario.
--   NumeroOrden      - Número de orden.
--   FechaAsigna      - Fecha y hora de asignación (temporalmente tomada de HoraNotificacion).
--   FechaNotifica    - Fecha de notificación final.
--   DiferenciaDias   - Diferencia en días entre FechaAsigna y FechaNotifica.
--   DiaAsigna        - Número del día del mes de FechaAsigna.
--   DiaNotifica      - Número del día del mes de FechaNotifica.
-- =====================================================================================
ALTER PROCEDURE [SISE3].[spDiferenciaTiemposNotificacion]
    @EmpleadoId BIGINT,
    @fechaInicio DATETIME,
    @fechaFin DATETIME
AS
BEGIN
    -- Evitar mensajes adicionales que no sean el conjunto de resultados
    SET NOCOUNT ON;

    -- Consulta principal
    SELECT
        a.AsuntoAlias,  -- Alias del asunto
        nep.ActuarioId, -- Identificador del actuario
        nep.NumeroOrden, -- Número de orden
        nep.HoraNotificacion AS FechaAsigna, -- Fecha y hora de asignación (temporalmente tomada de HoraNotificacion)
        nep.FechaNotificacion AS FechaNotifica, -- Fecha de notificación final
        DATEDIFF(DAY, nep.HoraNotificacion, nep.FechaNotificacion) AS DiferenciaDias, -- Diferencia en días entre FechaAsigna y FechaNotifica
        DAY(nep.HoraNotificacion) AS DiaAsigna, -- Número del día del mes de FechaAsigna
        DAY(nep.FechaNotificacion) AS DiaNotifica -- Número del día del mes de FechaNotifica

    FROM
        NotificacionElectronica_Personas nep
        INNER JOIN Asuntos a ON nep.AsuntoNeunId = a.AsuntoNeunId 
    WHERE 
        CAST(nep.FechaAlta AS DATE) BETWEEN @fechaInicio AND @fechaFin -- Filtrar por rango de fechas
        AND nep.ActuarioId = @EmpleadoId -- Filtrar por actuario
    ORDER BY
        nep.FechaNotificacion; -- Ordenar por fecha de notificación

    -- Permitir mensajes adicionales después de que se ha devuelto el conjunto de resultados
    SET NOCOUNT OFF;
END
GO
