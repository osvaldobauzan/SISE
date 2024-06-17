SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Proyecto: SISE3
-- Autor: Sergio Orozco - MS
-- Alter Date: 11/01/2024
-- Objetivo: Carga el detalle de notificaciones electrónicas en detalle de actuaría
-- EXEC SISE3.pcTableroDetalleNotificaciones 180, 1000, 1, 30315077, 9, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
-- Notas: 
--      Pendiente de implementar quien asigna al actuario y fecha de asignacion, no existe aun en la tabla
--      Pendiente de agregar las notificaciones a Autoridades Judiciales y Promoventes, no existen en la tabla
--          Se propone generar una vista donde se incluyan los datos de las notificaciones a Autoridades Judiciales y Promoventes
--          y se incluya en la consulta de este SP
--      Pendiente filtrar por ese tipo de parte o tipo de figura
-- @pi_CatOrganismoId = 180
-- @pi_TamanoPagina = 1000
-- @pi_NumeroPagina = 1
-- @pi_AsuntoNeunId = 30315469
-- @pi_AsuntoDocumentoID = 1
-- @pi_Texto = NULL
-- @pi_OrdenarPor = NULL
-- @pi_TipoOrden = NULL
-- @pi_FiltroTipo = NULL
-- @pi_FiltroTipoParteID = NULL
-- @pi_FiltroTipoNotificacionID = NULL
-- @pi_FiltroActuarioID = NULL
-- @pi_primeraCarga = 0
-- =============================================

-- EXEC SISE3.pcTableroDetalleNotificaciones 180, 1000, 1, 30316269, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL

-- EXEC SISE3.pcTableroDetalleNotificaciones 180, 1000, 1, 30316166, 15, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL



CREATE OR ALTER PROCEDURE [SISE3].[pcTableroDetalleNotificaciones]
    (
    -- REPRESENTA EL IDENTIFICADOR DEL ORGANISMO
    @pi_CatOrganismoId INT,
    -- REPRESENTA EL TAMAÑO DE LA PÁGINA DE LA PAGINACIÓN
    @pi_TamanoPagina INT = NULL,
    --REPRESENTA EL NUMERO DE PÁGINA DE LA PAGINACIÓN
    @pi_NumeroPagina INT,
    -- REPRESENTA EL IDENTIFICADOR DEL EXPEDIENTE
    @pi_AsuntoNeunId BIGINT,
    -- REPRESENTA EL IDENTIFICADOR DEL DOCUMENTO
    @pi_AsuntoDocumentoID BIGINT,
    -- REPRESENTA EL TEXTO A BUSCAR EN EL EXPEDIENTE
    @pi_Texto VARCHAR(MAX) = NULL,
    -- Recibe valor para ordenamiento de la página, PUEDE SER NULO, si es nulo ordena por fecha, de lo contrario por el campo recibido
    @pi_OrdenarPor VARCHAR(128) = NULL,
    -- Recibe configuración de ordenamiento Ascendente o Descendente? 1=Descendente 0=Ascendente
    @pi_TipoOrden INT = NULL,
    --Recibe parámetro del tipo de filtro
    -- Estado opciones, 0=VerTodas, 1=Pendiente, 2=En Proceso, 3=Notificados
    @pi_FiltroTipo INT = 0 ,
    --Recibe parámetro del tipo de parte 1=Partes, 2=Promoventes, 3=Autoridades Judiciales
    @pi_FiltroTipoParteID INT = NULL,
    --Recibe parámetro del tipo de notificación
    @pi_FiltroTipoNotificacionID INT = NULL,
    --Recibe parámetro del id de empleado actuario
    @pi_FiltroActuarioID BIGINT = NULL,
    --Es la primera carga de tablero o no 0=No, 1=Si
    @pi_primeraCarga INT = 0
)
AS
BEGIN

--Declara variables de conteos
DECLARE @Todos              INT
DECLARE @Notificados        INT
DECLARE @Pendientes         INT
DECLARE @EnProceso          INT
DECLARE @pagina             INT
DECLARE @totalPaginas       INT
DECLARE @totalRegistros     INT


--Limpiar variables de entrada

IF @pi_TamanoPagina IS NULL
BEGIN
    SET @pi_TamanoPagina = 0
    SET @pi_NumeroPagina = iif(@pi_TamanoPagina=0,0x7ffffff,@pi_TamanoPagina)
END

IF @pi_Texto IS NOT NULL
BEGIN
    SET @pi_Texto = LTRIM(RTRIM(@pi_Texto))
END

IF @pi_OrdenarPor IS NOT NULL
BEGIN
    SET @pi_OrdenarPor = LTRIM(RTRIM(@pi_OrdenarPor))
END

--Validar Filtros existentes
IF @pi_FiltroTipo IS NULL
BEGIN
	SET @pi_FiltroTipo = 0
END

IF @pi_TipoOrden IS NULL
BEGIN
    SET @pi_TipoOrden = 0
END

IF @pi_FiltroTipo NOT IN (0,1,2,3)
BEGIN
	SET @pi_FiltroTipo = 0
END

IF @pi_FiltroTipoParteID IS NULL
BEGIN
    SET @pi_FiltroTipoParteID = 0
END

IF @pi_FiltroTipoNotificacionID IS NULL
BEGIN
    SET @pi_FiltroTipoNotificacionID = 0
END

IF @pi_FiltroActuarioID IS NULL
BEGIN
    SET @pi_FiltroActuarioID = 0
END

IF @pi_primeraCarga IS NULL
BEGIN
    SET @pi_primeraCarga = 0
END


SELECT 
    ad.NombreDocumento as NombreArchivo
    ,DATEDIFF(DD,ad.FechaAutoriza, GETDATE()) AS Transcurrido
    ,a.AsuntoAlias as No_Exp
    ,cto.Descripcion As TipoAsuntoDescripcion
    ,dbo.funRecuperaCatalogoDependienteDescripcion(527, ad.TipoCuaderno) as TipoCuaderno
    , a.TipoProcedimiento as TipoProcedimiento
    FROM AsuntosDocumentos ad WITH(NOLOCK) 
        CROSS APPLY SISE3.fnExpediente(ad.AsuntoNeunId) a
        JOIN CatTiposAsunto cto WITH (NOLOCK) on a.CatTipoAsuntoId = cto.CatTipoAsuntoId
WHERE 
    ad.AsuntoNeunId = @pi_AsuntoNeunId
    AND ad.AsuntoDocumentoID = @pi_AsuntoDocumentoID


-- Crear una funcion para calcular el estado 
-- Pendiente es que no tiene ningun tipo de notificacion y no se ha trabajo 
-- Pendiente es cuando no tiene asignado en actuarioID nullo o 10273 y no tiene fecha de notificacion 
-- En Proceso es que Tiene un actuario y no tiene acuse o el tipo de acuse no es "verde" (segun analisis) o valido
-- notificado es que cuenta archivo y tipo de acuse es un acuse valido

-- Si existe un actuario, es porque esta asignado la notificacion 

-- tabla de notificaciones con joins se inserta a #TMP_NOTIFICACIONES
        SELECT
            1 as TipoParte, 
            CASE
                WHEN pas.CatTipoPersonaId = 1 THEN CONCAT(pas.Nombre,' ', pas.APaterno, ' ', pas.AMaterno)
                WHEN pas.CatTipoPersonaId <> 1 THEN pas.Nombre
            END AS Parte
            , nep.PersonaId as ParteID  
            , DomicilioParte = IIF(dom.ParteId is not NULL, CONCAT(dom.TipoVialidadId, ' ',dom.NombreVialidad,' ', dom.NumeroExterior, ' INT ', dom.NumeroInterior, ' ', dom.TipoAsentamientoId, ' ',' ', dom.CP, ' ', dom.MunicipioId, ' ', dom.EstadoId), 'Domicilio no registrado')
--            , 'Domicilio' as DomicilioParte
            ,cpa.Descripcion as Caracter
            ,nep.TipoConstanciaId
            ,CASE
                -- Pendiente es cuando no tiene asignado en actuarioID nullo o 10273 y no tiene fecha de notificacion 
                WHEN (
                        (nep.ActuarioId IS NULL or nep.ActuarioId=10273) 
                        AND nep.FechaNotificacion IS NULL 
                        --AND nea.NombreArchivo IS NULL 
                    ) THEN 1
--                        OR nep.TipoConstanciaId in ('5736') THEN 1
                -- En Proceso es que Tiene un actuario y no tiene acuse Entonces debe ser "Tipo de Acuse"
                WHEN nep.ActuarioId IS NOT NULL AND (nep.TipoConstanciaId in ('5726','5731','5732','1440') OR nea.ArchivoId IS NULL)
                THEN 2
                -- Notificado es que cuenta conAcuse y ser de tipo de acuse valido (verde)
                -- No debe ser por presencia de archivo, sino por tipo de acuse Verde o valido (confirmar con abogados)
--                WHEN nep.ActuarioId IS NOT NULL AND (nep.TipoConstanciaId is not null and nep.TipoConstanciaId not in ('5726','5732','5736'))
                -- Se remueve tipo constancia notificacion por lista. 5736
                WHEN nep.ActuarioId IS NOT NULL AND (nep.TipoConstanciaId is not null and nep.TipoConstanciaId not in ('5726','5731','5732','1440'))
                    AND nea.ArchivoId IS NOT NULL
                    THEN 3
                ELSE NULL
            END as EstadoId
            ,ctn.DESCRIPCION as EstadoDescripcion
            ,nep.FechaNotificacion as EstadoFecha
            ,cnt.sDescripcion as Tipo --tipo de notificacion
            ,nep.TipoNotificacion as TipoId
            -- Pendientes de obtener pues no existen en la tabla de notificaciones
            ,'Pendiente obtener asignó Persona' as AsignoPersona
            ,'Pendiente obtener asignó fecha' as AsignoFecha
            ,CASE 
                WHEN nep.ActuarioId <> 10273 
                    THEN CONCAT(emp.Nombre, ' ', emp.ApellidoPaterno, ' ', emp.ApellidoMaterno)
                ELSE NULL
            END as AsignadoActuario
            ,ar.Nombre as AsignadoZona
            ,nea.NombreArchivo as archivoAcuse
            ,nep.ActuarioId
            ,0 as TieneCOE
        INTO #TMP_NOTIFICACIONES
        FROM AsuntosDocumentos ad WITH(NOLOCK)
            -- se hace join para lograr cruce con CatCaracterPersonaAsunto
            CROSS APPLY SISE3.fnExpediente(ad.AsuntoNeunId) a
            INNER JOIN NotificacionElectronica_Personas nep ON ad.AsuntoID=nep.AsuntoId AND ad.AsuntoNeunId=nep.AsuntoNeunId AND  ad.SintesisOrden = nep.SintesisOrden
            INNER join PersonasAsunto  pas ON  ad.AsuntoId = pas.AsuntoId and ad.AsuntoNeunId = pas.AsuntoNeunId AND nep.PersonaId = pas.PersonaId
            -- Join para obtener Caracter
            LEFT join dbo.CatCaracterPersonaAsunto	cpa ON pas.CatCaracterPersonaAsuntoId = cpa.CatCaracterPersonaAsuntoId and cpa.CatTipoAsuntoId = a.CatTipoAsuntoId
            -- Join para obtener Zona de actuario
            LEFT join dbo.Areas ar on ar.EmpleadoId = nep.ActuarioId 
            -- Trae descripcion de tipo de notificacion
            LEFT join dbo.CatNotificaciones cnt on cnt.kIdCatNotificaciones = nep.TipoNotificacion
            -- Join para obtener nombre de actuario
            LEFT join dbo.CatEmpleados emp on emp.EmpleadoId = nep.ActuarioId
            -- Join para obtener tipo de persona
            LEFT join dbo.CatTiposPersona ctp on pas.CatTipoPersonaId = ctp.CatTipoPersonaId
            -- Join para catalogo de tipo de acuse
            LEFT join 
            (
                SELECT      ID
                    ,DESCRIPCION
                    ,Elementos
                FROM  viCatalogos a with(nolock) INNER JOIN Catalogos b with(nolock) ON a.Catalogo = b.CatalogoId 
                WHERE CatalogoPadre = 6867 AND
                CatalogoPadre > 0
            ) ctn on ctn.ID = nep.TipoConstanciaId
            -- Join para obtener nombre de archivo
            LEFT JOIN NotificacionElectronica_Archivos nea ON nep.NotElecId = nea.NotElecId
            left join dbo.DomicilioPartes dom ON pas.PersonaId = dom.ParteId
        WHERE ad.StatusReg=1 
        AND nep.StatusReg=1
            --Filtra solo notificaciones para actuaría
		AND nep.TipoNotificacion IN (1, 3, 5, 6, 11,12) 
        AND ad.AsuntoNeunId = @pi_AsuntoNeunId
        AND ad.AsuntoDocumentoID = @pi_AsuntoDocumentoID;
    
    -- Crea tabla temporal para filtrar notificaciones
    SELECT 
    [TipoParte],
    [Parte],
    [ParteId],
    [DomicilioParte],
    [Caracter],
    [EstadoId],
    [TipoConstanciaId] as TipoDeAcuse,
    CASE WHEN EstadoId = 1 THEN 'Pendiente'
        WHEN EstadoId = 2 THEN [EstadoDescripcion]
        WHEN EstadoId = 3 THEN 'Notificado'
        ELSE NULL
    END as Estado,
    [EstadoFecha],
    [Tipo],
    [TipoId],
    [AsignoPersona],
    [AsignoFecha],
    [AsignadoActuario],
    [AsignadoZona],
    [archivoAcuse],
    0 as [TieneCOE]
    INTO #TMP_NOTIFICACIONES_FILTRADAS
    FROM #TMP_NOTIFICACIONES
    WHERE  
        (
            TRIM(ISNULL(@pi_Texto, '')) = ''
            OR CONCAT(Parte, Caracter, EstadoDescripcion, Tipo, TipoID, AsignoPersona, AsignadoActuario, AsignadoZona, archivoAcuse) LIKE '%' + TRIM(ISNULL(@pi_Texto, '')) + '%'
        )
        AND (
            -- 1=Partes, 2=Promoventes, 3=Autoridades Judiciales
            (@pi_FiltroTipoParteID = 0 AND 1 = 1)
            OR (@pi_FiltroTipoParteID = TipoParte)

        )
        AND (
            (@pi_FiltroTipoNotificacionID = 0 AND 1 = 1)
            OR (@pi_FiltroTipoNotificacionID = TipoId )
        )
        AND (
            (@pi_FiltroActuarioID = 0 AND 1 = 1)
            OR (@pi_FiltroActuarioID = ActuarioId )
        )

-- Obtiene conteos de tabla filtrada
    Select @Todos = COUNT(*) from #TMP_NOTIFICACIONES_FILTRADAS
    Select @Notificados = COUNT(*) from #TMP_NOTIFICACIONES_FILTRADAS where Estado = 'Notificado'
    Select @Pendientes = COUNT(*) from #TMP_NOTIFICACIONES_FILTRADAS where Estado = 'Pendiente'	
    Select @EnProceso = COUNT(*) from #TMP_NOTIFICACIONES_FILTRADAS where Estado not in  ('Notificado', 'Pendiente')
    Select @pagina = @pi_NumeroPagina
    Select @totalPaginas = CEILING(CAST(@Todos as FLOAT) / iif(@pi_TamanoPagina=0,0x7ffffff,@pi_TamanoPagina))
    Select @totalRegistros = @Todos


    Select 
        @Todos as Vertodo, 
        @Pendientes as Pendiente, 
        @EnProceso as EnProceso, 
        @Notificados as Notificados,
        @pagina as pagina,
        @totalPaginas as totalPaginas,
        @totalRegistros as totalRegistros

    -- Regresa dataset filtrado y con orden aplicado
    Select * from #TMP_NOTIFICACIONES_FILTRADAS
    where 
         (
            (@pi_FiltroTipo = 0 AND 1 = 1)
            OR (@pi_FiltroTipo = 1 AND Estado = 'Pendiente')
            OR (@pi_FiltroTipo = 2 AND Estado NOT IN ('Pendiente', 'Notificado'))
            OR (@pi_FiltroTipo = 3 AND Estado = 'Notificado')
        )
    ORDER BY 
  			CASE WHEN (@pi_OrdenarPor= 'Parte' and @pi_TipoOrden = 0) THEN Parte END ASC,
			CASE WHEN (@pi_OrdenarPor= 'Parte' and @pi_TipoOrden = 1) THEN Parte END DESC,
  			CASE WHEN (@pi_OrdenarPor= 'Estado' and @pi_TipoOrden = 0) THEN EstadoId END ASC,
			CASE WHEN (@pi_OrdenarPor= 'Estado' and @pi_TipoOrden = 1) THEN EstadoId END DESC,
  			CASE WHEN (@pi_OrdenarPor= 'Tipo' and @pi_TipoOrden = 0) THEN Tipo END ASC,
			CASE WHEN (@pi_OrdenarPor= 'Tipo' and @pi_TipoOrden = 1) THEN Tipo END DESC,
  			CASE WHEN (@pi_OrdenarPor= 'AsignoPersona' and @pi_TipoOrden = 0) THEN AsignoPersona END ASC,
			CASE WHEN (@pi_OrdenarPor= 'AsignoPersona' and @pi_TipoOrden = 1) THEN AsignoPersona END DESC,
  			CASE WHEN (@pi_OrdenarPor= 'AsignadoActuario' and @pi_TipoOrden = 0) THEN AsignadoActuario END ASC,
			CASE WHEN (@pi_OrdenarPor= 'AsignadoActuario' and @pi_TipoOrden = 1) THEN AsignadoActuario END DESC,
  			CASE WHEN (@pi_OrdenarPor= 'ArchivoAcuse' and @pi_TipoOrden = 0) THEN ArchivoAcuse END ASC,
			CASE WHEN (@pi_OrdenarPor= 'ArchivoAcuse' and @pi_TipoOrden = 1) THEN ArchivoAcuse END DESC,
	    	CASE WHEN (@pi_OrdenarPor not in('Parte','Estado','Tipo','ActuarioAsigno','ArchivoAcuse')) THEN EstadoId END ASC, 
            CASE WHEN (@pi_OrdenarPor not in('Parte','Estado','Tipo','ActuarioAsigno','ArchivoAcuse')) THEN EstadoFecha END ASC,
			-- En primera carga ordenar por estados y despues por fecha
            CASE WHEN (@pi_primeraCarga = 1) THEN EstadoId END ASC,
            CASE WHEN (@pi_primeraCarga = 1) THEN EstadoFecha END ASC
            OFFSET @pi_TamanoPagina * (@pi_NumeroPagina - 1) ROWS 
			FETCH NEXT iif(@pi_TamanoPagina=0,0x7ffffff,@pi_TamanoPagina)  ROWS ONLY


-- Limpia tablas temporales
    DROP TABLE #TMP_NOTIFICACIONES
    DROP TABLE #TMP_NOTIFICACIONES_FILTRADAS

END;
