USE SISE_NEW
GO

-- =============================================
-- Proyecto: SISE3
-- Autor: Erick Gonzalez
-- Creado: [2024-06-07]
-- Objetivo: Obtener las notificaciones por tipo y por semana del mes seleccionado
-- =============================================
ALTER PROCEDURE [SISE3].[sp_ObtenerActuariaDetalleMes]
    @pi_CatOrganismoId INT,          -- Identificador del organismo
    @pi_FiltroActuarioID BIGINT,     -- Identificador del actuario (puede ser 0 para no filtrar)
    @pi_FechaInicial DATE,           -- Fecha inicial del rango de búsqueda
    @pi_FechaFinal DATE,             -- Fecha final del rango de búsqueda
    @MesSeleccionado INT             -- Mes seleccionado para el filtro
AS
BEGIN
    SET NOCOUNT ON;

        CREATE TABLE #TMP_NOTIFICACIONES (
        AsuntoNeunId INT,                -- Identificador del asunto
        No_Exp VARCHAR(255),             -- Número de expediente
        TipoAsunto VARCHAR(255),         -- Tipo de asunto
        TipoCuaderno VARCHAR(255),       -- Tipo de cuaderno
        AsuntoDocumentoId INT,           -- Identificador del documento del asunto
        SintesisOrden VARCHAR(255),      -- Síntesis de la orden
        DocumentoAcuerdo VARCHAR(255),   -- Documento de acuerdo
        TipoParte INT,                   -- Tipo de parte
        ParteID INT,                     -- Identificador de la parte
        EstadoId INT,                    -- Identificador del estado
        EstadoDescripcion VARCHAR(255),  -- Descripción del estado
        EstadoFecha DATE,                -- Fecha del estado
		FechaAlta DATE,
		HoraNotificacion DATE,
        Tipo VARCHAR(255),               -- Tipo de notificación
        TipoId INT,                      -- Identificador del tipo de notificación
        AsignadoActuario VARCHAR(255),   -- Nombre del actuario asignado
        AsignadoZona VARCHAR(255),       -- Zona asignada
        ArchivoAcuse VARCHAR(255),       -- Archivo de acuse
        ActuarioId INT,                  -- Identificador del actuario
        NotElecId INT,                   -- Identificador de notificación electrónica
        TipoCuadernoDesc VARCHAR(255),   -- Descripción del tipo de cuaderno
        FechaAutoriza DATE,              -- Fecha de autorización
        FechaAuto_F VARCHAR(10),         -- Fecha de autorización (formato)
        Transcurrido INT                 -- Días transcurridos desde la autorización
    );

    -- Insertar datos en la tabla temporal
    INSERT INTO #TMP_NOTIFICACIONES
    SELECT 
        ad.AsuntoNeunId,                   -- 1. Identificador del asunto
        a.AsuntoAlias AS No_Exp,           -- 2. Número de expediente
        a.CatTipoAsunto AS TipoAsunto,     -- 3. Tipo de asunto
        ad.TipoCuaderno,                   -- 4. Tipo de cuaderno
        ad.AsuntoDocumentoId,              -- 5. Identificador del documento del asunto
        ad.SintesisOrden,                  -- 6. Síntesis de la orden
        ad.NombreDocumento AS DocumentoAcuerdo, -- 7. Documento de acuerdo
        1 AS TipoParte,                    -- 8. Tipo de parte (constante 1)
        nep.PersonaId AS ParteID,          -- 10. Identificador de la parte
        CASE
            WHEN (nep.FechaNotificacion IS NULL) THEN 1
            WHEN (nep.TipoConstanciaId IN ('5726', '5731', '5732', '1440') OR nea.ArchivoId IS NULL) THEN 2
            WHEN nep.ActuarioId IS NOT NULL AND (nep.TipoConstanciaId IS NOT NULL AND nep.TipoConstanciaId NOT IN ('5726', '5731', '5732', '1440')) AND nea.ArchivoId IS NOT NULL THEN 3
            ELSE NULL
        END AS EstadoId,                   -- 13. Identificador del estado
        ctn.DESCRIPCION AS EstadoDescripcion, -- 14. Descripción del estado
        nep.FechaNotificacion AS EstadoFecha, -- 15. Fecha del estado
		nep.FechaAlta,
		nep.HoraNotificacion,
        cnt.sDescripcionCorta AS Tipo,     -- 16. Tipo de notificación
        nep.TipoNotificacion AS TipoId,    -- 17. Identificador del tipo de notificación
        CASE
            WHEN nep.ActuarioId <> 10273 THEN CONCAT(emp.Nombre, ' ', emp.ApellidoPaterno, ' ', emp.ApellidoMaterno)
            ELSE NULL
        END AS AsignadoActuario,           -- 18. Nombre del actuario asignado
        ar.Nombre AS AsignadoZona,         -- 19. Zona asignada
        nea.NombreArchivo AS ArchivoAcuse, -- 20. Archivo de acuse
        nep.ActuarioId,                    -- 21. Identificador del actuario
        nep.NotElecId,                     -- 22. Identificador de notificación electrónica
        dbo.funRecuperaCatalogoDependienteDescripcion(527, ad.TipoCuaderno) AS TipoCuadernoDesc, -- 26. Descripción del tipo de cuaderno
        ad.FechaAutoriza,                  -- 27. Fecha de autorización
        ISNULL(CONVERT(VARCHAR(10), ad.FechaAutoriza, 103), '') AS FechaAuto_F, -- 28. Fecha de autorización (formato)
        DATEDIFF(DD, ad.FechaAutoriza, GETDATE()) AS Transcurrido -- 29. Días transcurridos desde la autorización
    FROM AsuntosDocumentos ad WITH (NOLOCK)
    CROSS APPLY SISE3.fnExpediente(ad.AsuntoNeunId) a
    INNER JOIN NotificacionElectronica_Personas nep ON ad.AsuntoID = nep.AsuntoId AND ad.AsuntoNeunId = nep.AsuntoNeunId AND ad.SintesisOrden = nep.SintesisOrden
    LEFT JOIN dbo.Areas ar ON ar.EmpleadoId = nep.ActuarioId
    LEFT JOIN dbo.CatNotificaciones cnt ON cnt.kIdCatNotificaciones = nep.TipoNotificacion
    LEFT JOIN dbo.CatEmpleados emp ON emp.EmpleadoId = nep.ActuarioId
    LEFT JOIN (
        SELECT ID, DESCRIPCION, Elementos
        FROM viCatalogos a WITH (NOLOCK)
        INNER JOIN Catalogos b WITH (NOLOCK) ON a.Catalogo = b.CatalogoId
        WHERE CatalogoPadre = 6867 AND CatalogoPadre > 0
    ) ctn ON ctn.ID = nep.TipoConstanciaId
    LEFT JOIN NotificacionElectronica_Archivos nea ON nep.NotElecId = nea.NotElecId
    WHERE nep.StatusReg = 1 AND ad.StatusReg = 1
    AND nep.TipoNotificacion IN (1, 3, 5, 6, 11, 12)
    AND a.CatOrganismoId = @pi_CatOrganismoId
    AND (nep.ActuarioId = @pi_FiltroActuarioID OR @pi_FiltroActuarioID = 0)
    AND CAST(nep.FechaAlta AS DATE) BETWEEN @pi_FechaInicial AND @pi_FechaFinal

    -- Consulta para obtener las notificaciones por tipo y por semana del mes seleccionado
    SELECT 
        DATEPART(WEEK, FechaAlta) - DATEPART(WEEK, DATEADD(MONTH, DATEDIFF(MONTH, 0, FechaAlta), 0)) + 1 AS Semana,
        Tipo,
        COUNT(*) AS Total
    FROM
        #TMP_NOTIFICACIONES
    WHERE
        MONTH(FechaAlta) = @MesSeleccionado
    GROUP BY
        DATEPART(WEEK, FechaAlta) - DATEPART(WEEK, DATEADD(MONTH, DATEDIFF(MONTH, 0, FechaAlta), 0)) + 1,
        Tipo
    ORDER BY
        Semana;

    -- Eliminar la tabla temporal
    DROP TABLE #TMP_NOTIFICACIONES;
END;
GO
