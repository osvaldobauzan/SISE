USE [SISE_NEW]
GO
/****** Object:  StoredProcedure [SISE3].[pcTableroProyectoUnitTests]    Script Date: 18/04/2024 03:01:34 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: Mario A Hernández Román
-- Create date: 02/04/2024
-- Description:	Pruebas Unitarias para el Módulo TableroProyecto Sentencias
-- Version: 1.3
-- EXEC [SISE3].[pcTableroProyectoUnitTests] 1
-- =============================================

ALTER   PROCEDURE [SISE3].[pcTableroProyectoUnitTests](
	@borrar_dataBase INT
) AS BEGIN
	
	DECLARE @unitTestsTable TABLE (
		sp_name VARCHAR(500),
		mensaje VARCHAR(500),
		pass BIT
	)

	DECLARE @totalesConteo INT
	DECLARE @totalProyectos INT

	SELECT 
		@totalProyectos = totalProyectos, 
		@totalesConteo = (
				totalSinProyecto 
			+ totalParaRevision
			+ totalNoAprobado
			+ totalConAjustes 
			+ totalAprobado
		)
	FROM
		[SISE3].[fnTableroProyectoConteoAgrupadores](
			180, 
			NULL, 
			NULL, 
			NULL
		)

	IF @totalProyectos = @totalesConteo BEGIN
		INSERT INTO @unitTestsTable
			VALUES (
				'SISE3.peTableroProyecto',
				'Conteos correctos',
				1
			)
	END
	ELSE BEGIN
		INSERT INTO @unitTestsTable
			SELECT 
				'SISE3.peTableroProyecto',
				'Conteos incorrectamentos. Revisar "SISE3.fnTableroProyectoConteoAgrupadores"',
				0
	END

	-- Tablero Cancela Proyecto Version
	EXEC [SISE3].peTableroProyectoCancelaVersion 17
	DECLARE @Proyecto_iStatusReg INT = (
		SELECT 
			iStatusReg 
		FROM 
			SISE3.Proyecto 
		WHERE 
			pkProyectoId = 17
	)

	IF @Proyecto_iStatusReg = 0 BEGIN

		DECLARE @ProyectoArchivo_fkProyectoVersionArchivoId INT = (
			SELECT 
				fkProyectoVersionArchivoId 
			FROM 
				SISE3.Proyecto 
			WHERE 
				pkProyectoId = 17
		)

		DECLARE @ProyectoArchivo_iStatusReg INT = (
			SELECT 
				iStatusReg 
			FROM 
				SISE3.ProyectoArchivo 
			WHERE 
				pkProyectoArchivoId = @ProyectoArchivo_fkProyectoVersionArchivoId
		)

		IF @ProyectoArchivo_iStatusReg = 0 BEGIN
			INSERT INTO @unitTestsTable
				SELECT 
					'SISE3.peTableroProyectoCancelaVersion',
					'Borra correctamente',
					1
		END
		ELSE BEGIN
			INSERT INTO @unitTestsTable
				SELECT 
					'SISE3.peTableroProyectoCancelaVersion',
					'Borra incorrectamente. Revisar borrado en "SISE3.ProyectoArchivo"',
					0
		END

	END
	ELSE BEGIN
		INSERT INTO @unitTestsTable
			SELECT 
				'SISE3_peTableroProyectoCancelaVersion',
				'Borra incorrectamente. Revisar borrado en "SISE3.Proyecto"',
				0
	END

	-- Agrega 1ra Version sin proyecto
	EXEC [SISE3].[piTableroProyectoInsertar] 
		180, 
		5232, 
		34556, 
		45712, 
		1, 
		2, 
		'UNIT_TEST_DACD', 
		45712, 
		'UNIT_TEST_DACD.docx', 
		'UNIT_TEST_DACD'

	-- Agrega 2da version de proyecto
	EXEC [SISE3].[piTableroProyectoInsertar] 		
		180, 
		5232, 
		34556, 
		45712, 
		1, 
		2, 
		'UNIT_TEST_DACD', 
		45712, 
		'UNIT_TEST_DACD.docx', 
		'UNIT_TEST_DACD'

	-- Agrega 3ra version de proyecto
	EXEC [SISE3].[piTableroProyectoInsertar] 
		180, 
		5232, 
		34556, 
		45712, 
		1, 
		2, 
		'UNIT_TEST_DACD', 
		45712, 
		'UNIT_TEST_DACD.docx', 
		'UNIT_TEST_DACD'

	-- Valida que el boton proyecto sin audiencia no deje pasar un registro ya en el tablero
	DECLARE @PuedeIngestar BIT 
	DECLARE @MotivoNoIngesta VARCHAR(1000)

	SELECT 
		@MotivoNoIngesta = MotivoNoIngesta, 
		@PuedeIngestar = PuedeIngestar
	FROM
		[SISE3].[fnTableroProyectoValidaIngesta](
			180,
			2,  -- Cuaderno
			NULL,
			NULL,
			5232
		)

	IF @PuedeIngestar = 0 BEGIN
		INSERT INTO @unitTestsTable
			SELECT 
				'SISE3_fnTableroProyectoValidaIngesta',
				'Funciona correctamente',
				1
	END
	ELSE BEGIN
		INSERT INTO @unitTestsTable
			SELECT 
				'SISE3.fnTableroProyectoValidaIngesta',
				'Funciona incorrectamente. Revisar borrado en "fnTableroProyectoValidaIngesta"',
				0
	END

	-- Inserta proyecto sin Audiencia NEUN 30314859
	EXEC [SISE3].[piTableroProyectoInsertar] 
		180, 
		30314859, 
		34556, 
		45712, 
		1, 
		2, 
		'UNIT_TEST_DACD', 
		45712, 
		'UNIT_TEST_DACD.docx', 
		'UNIT_TEST_DACD'

	-- Valida SP [paTableroProyectoVersion]
	DECLARE @pk_idProyecto INT = (
		SELECT 
			MAX(pkProyectoId)
		FROM 
			SISE3.Proyecto
		WHERE 
			AsuntoNeunId = 30314859
			AND iStatusReg = 1
	)

	EXEC [SISE3].[paTableroProyectoVersion]
		@pk_idProyecto, 
		'UNIT_TEST_DACD', 
		'UNIT_TEST_DACD_CORRECCION.docx', 
		3, 
		'UNIT_TEST_DACD'

	DECLARE @pk_fkCorreccionArchivoId INT = (
		SELECT 
			fkCorreccionArchivoId
		FROM 
			SISE3.Proyecto
		WHERE 
			AsuntoNeunId = 30314859
			AND iStatusReg = 1
	)

	DECLARE @pk_pkProyectoArchivoIdTitular INT = (
		SELECT 
			pkProyectoArchivoId
		FROM 
			SISE3.ProyectoArchivo
		WHERE 
			sNombreArchivoReal = 'UNIT_TEST_DACD_CORRECCION.docx'
	)

	IF @pk_fkCorreccionArchivoId = @pk_pkProyectoArchivoIdTitular BEGIN
		INSERT INTO @unitTestsTable
			SELECT 
				'SISE3_paTableroProyectoVersion',
				'Funciona correctamente',
				1
	END
	ELSE BEGIN
		INSERT INTO @unitTestsTable
			SELECT 
				'SISE3.paTableroProyectoVersion',
				'Funciona incorrectamente. No hacen Match los ids fkCorreccionArchivoId "paTableroProyectoVersion"',
				0
	END

	-- SP validacion [SISE3].[pcTableroProyectoObtieneInfoArchivo]

	DECLARE @ut_fkProyectoVersionArchivoId INT
	DECLARE @ut_fkCorreccionArchivoId INT

	SELECT 
		@ut_fkProyectoVersionArchivoId = fkProyectoVersionArchivoId,
		@ut_fkCorreccionArchivoId = fkCorreccionArchivoId
	FROM
		Proyecto
	WHERE 
		AsuntoNeunId = 30314859
		AND iStatusReg = 1

	DECLARE @ut_proyNeun INT
	DECLARE @ut_corrNeun INT

	SELECT 
		@ut_proyNeun = AsuntoNeunId
	FROM
		[SISE3].[fnTableroProyectoObtieneInfoArchivo](
			@ut_fkProyectoVersionArchivoId
		)

	SELECT 
		@ut_corrNeun = AsuntoNeunId
	FROM
		[SISE3].[fnTableroProyectoObtieneInfoArchivo](
			@ut_fkCorreccionArchivoId
		)

	IF @ut_corrNeun = @ut_proyNeun BEGIN
		INSERT INTO @unitTestsTable
			SELECT 
				'SISE3.fnTableroProyectoObtieneInfoArchivo',
				'Funciona correctamente',
				1
	END
	ELSE BEGIN
		INSERT INTO @unitTestsTable
			SELECT 
				'SISE3.fnTableroProyectoObtieneInfoArchivo',
				'Funciona incorrectamente. Los AsuntoNeunId deben ser los mismos: Proyecto (Secretario) y sus Correcciones (Titular)',
				0
	END

	DECLARE @conteo INT = (
		SELECT 
			COUNT(AsuntoNeunId)
		FROM
			[SISE3].[fnTableroProyectoObtieneInfoArchivo](
				@ut_fkCorreccionArchivoId
		)
	)

	IF @conteo = 1 BEGIN
		INSERT INTO @unitTestsTable
			SELECT 
				'SISE3.fnTableroProyectoObtieneInfoArchivo',
				'Funciona correctamente conteo',
				1
	END
	ELSE BEGIN
		INSERT INTO @unitTestsTable
			SELECT 
				'SISE3.fnTableroProyectoObtieneInfoArchivo',
				'Funciona incorrectamenten conteo. Solo debe existir asignado 1 archivo por neun',
				0
	END

	-- NUEVA VERSION 2 NEUN 30314859
	EXEC [SISE3].[piTableroProyectoInsertar] 
		180, 
		30314859, 
		34556, 
		45712, 
		1, 
		2, 
		'UNIT_TEST_DACD', 
		45712, 
		'UNIT_TEST_DACD.docx', 
		'UNIT_TEST_DACD'

	DECLARE @pk_idProyecto_v2 INT = (
		SELECT 
			MAX(pkProyectoId)
		FROM 
			SISE3.Proyecto
		WHERE 
			AsuntoNeunId = 30314859
			AND iStatusReg = 1
	)

	-- APRUEBA
	EXEC [SISE3].[paTableroProyectoVersion]
		@pk_idProyecto_v2, 
		'UNIT_TEST_DACD_APROBADO', 
		'UNIT_TEST_DACD_CORRECCION_APROBADO.docx', 
		6, 
		'UNIT_TEST_DACD'

	DECLARE @pk_idProyecto_aprobado INT = (
		SELECT 
			MAX(pkProyectoId)
		FROM 
			SISE3.Proyecto
		WHERE 
			AsuntoNeunId = 30314859
			AND iStatusReg = 1
	)

	-- DESAPRUEBA PROYECTO
	EXEC [SISE3].[paTableroProyectoVersion]
		@pk_idProyecto_aprobado, 
		'UNIT_TEST_DACD_DESAPROBADO', 
		'UNIT_TEST_DACD_CORRECCION_DESAPROBADO.docx', 
		3, 
		'UNIT_TEST_DACD'

	DECLARE @pk_idProyecto_desaprobado INT = (
		SELECT 
			MAX(pkProyectoId)
		FROM 
			SISE3.Proyecto
		WHERE 
			AsuntoNeunId = 30314859
			AND iStatusReg = 1
	)

	-- NUEVA VERSION 3 NEUN 30314859
	EXEC [SISE3].[piTableroProyectoInsertar] 
		180, 
		30314859, 
		34556, 
		45712, 
		1, 
		2, 
		'UNIT_TEST_DACD', 
		45712, 
		'UNIT_TEST_DACD_V3.docx', 
		'UNIT_TEST_DACD_V3'

	DECLARE @pk_idProyecto_1er_comentario_fondo INT = (
		SELECT 
			MAX(pkProyectoId)
		FROM 
			SISE3.Proyecto
		WHERE 
			AsuntoNeunId = 30314859
			AND iStatusReg = 1
	)

		DECLARE @conteo_versiones INT = (
			SELECT 
				COUNT(AsuntoNeunId)
			FROM
				SISE3.Proyecto
			WHERE
				AsuntoNeunId = 30314859
				AND iStatusReg = 1
	)

	-- COMENTARIOS FONDO
	EXEC [SISE3].[paTableroProyectoVersion]
		@pk_idProyecto_1er_comentario_fondo, 
		'UNIT_TEST_DACD_FONDO', 
		'UNIT_TEST_DACD_CORRECCION_FONDO.docx', 
		3, 
		'UNIT_TEST_DACD'

	DECLARE @pk_idProyecto_Modificaciones_al_1er_comentario_fondo INT = (
		SELECT 
			MAX(pkProyectoId)
		FROM 
			SISE3.Proyecto
		WHERE 
			AsuntoNeunId = 30314859
			AND iStatusReg = 1
	)

	-- Modifica comentario
	EXEC [SISE3].[paTableroProyectoVersion]
		@pk_idProyecto_Modificaciones_al_1er_comentario_fondo, 
		'UNIT_TEST_DACD_FONDO_MODIFICA_A_FORMA', 
		'UNIT_TEST_DACD_FONDO_MODIFICA_A_FORMA.docx', 
		4, 
		'UNIT_TEST_DACD'

	DECLARE @pk_idProyecto_Modificacion_al_2do_comentario_fondo INT = (
		SELECT 
			MAX(pkProyectoId)
		FROM 
			SISE3.Proyecto
		WHERE 
			AsuntoNeunId = 30314859
			AND iStatusReg = 1
	)

	-- Modifica 2do comentario
	EXEC [SISE3].[paTableroProyectoVersion]
		@pk_idProyecto_Modificacion_al_2do_comentario_fondo, 
		'UNIT_TEST_DACD_FONDO_MODIFICA_A_FORMA_REGRESA_A_FONDO', 
		'UNIT_TEST_DACD__FONDO_MODIFICA_A_FORMA_REGRESA_A_FONDO.docx', 
		3, 
		'UNIT_TEST_DACD'

	-- Tienen que tener 3 versiones despues del ciclos de revisiones
	-- Incluyendo la desaprobación

	IF @conteo_versiones = 3 BEGIN
		INSERT INTO @unitTestsTable
			SELECT 
				'Ciclo conteo de versiones. Incluyendo desaprobacion',
				'Funciona correctamente conteo',
				1
	END
	ELSE BEGIN
		INSERT INTO @unitTestsTable
			SELECT 
				'Ciclo conteo de versiones. Incluyendo desaprobacion',
				'Funciona incorrectamente conteo. Deben de existir 3 versiones',
				0
	END

	-- EXEC [SISE3].[pcTableroProyectoUnitTests] 1
	SELECT 
		* 
	FROM
		@unitTestsTable

	-- Borrado de registros de UNIT_TEST_DACD


	IF @borrar_dataBase = 1 BEGIN

		DELETE FROM [SISE3].[ProyectoArchivo]
			WHERE sIPUsuario LIKE '%UNIT_TEST_DACD%'

		DELETE FROM [SISE3].[Proyecto]
			WHERE sSintesis LIKE '%UNIT_TEST_DACD%'

	END


END;