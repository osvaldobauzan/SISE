-- Scripts de inserción para habilitar la funcionalidad de privilegios para ciertos roles de usuario sobre el módulo de proyectos de sentencias

------------------- PASO #1 SISE3.CatPrivilegio -----------------------------------------------------------
INSERT INTO SISE3.CatPrivilegio (sNombrePrivilegio, sDescripcion, sModulo, bEstatus) VALUES('Proyecto', 'Panel de Proyecto', 'Panel',1);
INSERT INTO SISE3.CatPrivilegio (sNombrePrivilegio, sDescripcion, sModulo, bEstatus) VALUES('Subir proyecto con audiencia', 'Permite Subir proyecto con audiencia', 'Proyecto',1);
INSERT INTO SISE3.CatPrivilegio (sNombrePrivilegio, sDescripcion, sModulo, bEstatus) VALUES('Subir proyecto sin audiencia', 'Permite subir proyecto sin audiencia', 'Proyecto',1);
INSERT INTO SISE3.CatPrivilegio (sNombrePrivilegio, sDescripcion, sModulo, bEstatus) VALUES('Validar proyecto', 'Se valida proyecto para determinar si se aprueba o no o se se�alan ajustes', 'Proyecto',1);
INSERT INTO SISE3.CatPrivilegio (sNombrePrivilegio, sDescripcion, sModulo, bEstatus) VALUES('Validar expediente', 'Se valida si el expediente es susceptible para creaci�n de proyecto', 'Proyecto',1);
INSERT INTO SISE3.CatPrivilegio (sNombrePrivilegio, sDescripcion, sModulo, bEstatus) VALUES('Validar expediente', 'Se obtienen las versiones de un proyecto', 'Proyecto',1);
INSERT INTO SISE3.CatPrivilegio (sNombrePrivilegio, sDescripcion, sModulo, bEstatus) VALUES('Obtener documento', 'Obtiene el documento proyecto de sentencia o documento de observaciones', 'Proyecto',1);

 ------------------- PASO #2 SISE3.CatAPI -----------------------------------------------------------
Insert into SISE3.CatAPI (sDescripcion, sURL, bEstatus) values ('Obtiene tablero proyectos', '/api/proyectos', 1);
Insert into SISE3.CatAPI (sDescripcion, sURL, bEstatus) values ('Subir proyecto con audiencia', '/api/proyecto/subirConAudiencia', 1);
Insert into SISE3.CatAPI (sDescripcion, sURL, bEstatus) values ('Subir proyecto sin audiencia', '/api/proyecto/subirSinAudiencia', 1);
Insert into SISE3.CatAPI (sDescripcion, sURL, bEstatus) values ('Se valida proyecto para determinar si se aprueba o no o se se�alan ajustes', '/api/proyecto/Validar', 1);
Insert into SISE3.CatAPI (sDescripcion, sURL, bEstatus) values ('Se valida si el expediente es susceptible para creaci�n de proyecto', '/api/proyecto/ValidarExpediente', 1);
Insert into SISE3.CatAPI (sDescripcion, sURL, bEstatus) values ('Catálogos de secretario y titular', '/api/proyecto/empleados', 1);
Insert into SISE3.CatAPI (sDescripcion, sURL, bEstatus) values ('Catálogos de tipo de sentencia', '/api/proyecto/tipoSentencia', 1);
Insert into SISE3.CatAPI (sDescripcion, sURL, bEstatus) values ('Catálogos de tipo de sentido', '/api/proyecto/tipoSentido', 1);
Insert into SISE3.CatAPI (sDescripcion, sURL, bEstatus) values ('Se obtienen las versiones de un proyecto', '/api/proyecto/versiones', 1);
Insert into SISE3.CatAPI (sDescripcion, sURL, bEstatus) values ('Obtener documento de un PS', '/api/proyecto/documento', 1);
 ------------------- PASO #3  SISE3.REL_PrivilegioXRol -----------------------------------------------------------

Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (5, 68, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (7, 68, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (12, 68, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (13, 68, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (14, 68, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (15, 68, 1, getdate());

Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (5, 69, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (7, 69, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (15, 69, 1, getdate());

Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (5, 70, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (7, 70, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (15, 70, 1, getdate());

Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (12, 71, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (13, 71, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (14, 71, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (15, 71, 1, getdate());

Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (5, 72, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (7, 72, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (15, 72, 1, getdate());

Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (7, 74, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (5, 74, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (12, 74, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (13, 74, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (14, 74, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (15, 74, 1, getdate());

Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (7, 75, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (5, 75, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (12, 75, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (13, 75, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (14, 75, 1, getdate());
Insert into SISE3.REL_PrivilegioXRol (IdRol, IdPrivilegio, bEstatus, fFechaAlta) VALUES (15, 75, 1, getdate());

INSERT INTO [SISE3].[REL_PrivilegioXRol](IdRol,IdPrivilegio,bEstatus,fFechaAlta)
SELECT 12,IdPrivilegio,bEstatus,fFechaAlta FROM [SISE3].[REL_PrivilegioXRol]
WHERE IdRol = 15
AND IdPrivilegio IN (8,56,63,64,65,66,67)

------------------- PASO #4  SISE3.REL_RolAPi -----------------------------------------------------------
Insert into SISE3.REL_RolAPi ( IdPrivilegio, IdAPI, sVerbo) values (68,88,'GET');
Insert into SISE3.REL_RolAPi ( IdPrivilegio, IdAPI, sVerbo) values (69,90,'POST');
Insert into SISE3.REL_RolAPi ( IdPrivilegio, IdAPI, sVerbo) values (70,91,'POST');
Insert into SISE3.REL_RolAPi ( IdPrivilegio, IdAPI, sVerbo) values (71,92,'POST');
Insert into SISE3.REL_RolAPi ( IdPrivilegio, IdAPI, sVerbo) values (72,93,'GET');
Insert into SISE3.REL_RolAPi ( IdPrivilegio, IdAPI, sVerbo) values (69,94,'GET');
Insert into SISE3.REL_RolAPi ( IdPrivilegio, IdAPI, sVerbo) values (69,95,'GET');
Insert into SISE3.REL_RolAPi ( IdPrivilegio, IdAPI, sVerbo) values (69,96,'GET');
Insert into SISE3.REL_RolAPi ( IdPrivilegio, IdAPI, sVerbo) values (74,98,'GET');
Insert into SISE3.REL_RolAPi ( IdPrivilegio, IdAPI, sVerbo) values (75,99,'GET');