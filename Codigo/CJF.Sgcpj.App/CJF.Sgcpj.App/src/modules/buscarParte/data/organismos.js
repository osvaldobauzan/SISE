const organismos = [
  {
    catOrganismoId: 4,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 5,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Civil del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 6,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia de Trabajo del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 7,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Penal del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 8,
    nombreOficial:
      "Primer Tribunal Unitario en Materias Civil, Administrativa y Especializados en Competencia Económica, Radiodifusión y Telecomunicaciones",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 9,
    nombreOficial:
      "Primer Tribunal Unitario en Materia Penal del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 10,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia Administrativa en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 12,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia Civil en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 15,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 16,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Civil del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 17,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia de Trabajo del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 18,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Penal del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 19,
    nombreOficial:
      "Segundo Tribunal Unitario en Materias Civil, Administrativa y Especializados en Competencia Económica, Radiodifusión y Telecomunicaciones",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 20,
    nombreOficial:
      "Segundo Tribunal Unitario en Materia Penal del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 23,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia Civil en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 25,
    nombreOficial:
      "Juzgado Segundo de Distrito de Procesos Penales Federales en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 26,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 27,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia Civil del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 28,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia de Trabajo del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 29,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia Penal del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 30,
    nombreOficial:
      "Tercer Tribunal Unitario en Materia Penal del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 33,
    nombreOficial:
      "Juzgado Tercero de Distrito en Materia Civil en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 35,
    nombreOficial:
      "Juzgado Tercero de Distrito de Procesos Penales Federales en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 36,
    nombreOficial:
      "Cuarto Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 37,
    nombreOficial:
      "Cuarto Tribunal Colegiado en Materia Civil del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 38,
    nombreOficial:
      "Cuarto Tribunal Colegiado en Materia de Trabajo del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 39,
    nombreOficial:
      "Cuarto Tribunal Colegiado en Materia Penal del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 40,
    nombreOficial:
      "Cuarto Tribunal Unitario en Materia Penal del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 41,
    nombreOficial:
      "Juzgado Cuarto de Distrito en Materia Administrativa en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 43,
    nombreOficial:
      "Juzgado Cuarto de Distrito en Materia Civil en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 44,
    nombreOficial:
      "Juzgado Cuarto de Distrito de Procesos Penales Federales en la Ciudad de México (02/04/2001 - 24/06/2015)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 45,
    nombreOficial:
      "Quinto Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 46,
    nombreOficial:
      "Quinto Tribunal Colegiado en Materia Civil del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 47,
    nombreOficial:
      "Quinto Tribunal Colegiado en Materia de Trabajo del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 48,
    nombreOficial:
      "Quinto Tribunal Colegiado en Materia Penal del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 51,
    nombreOficial:
      "Juzgado Quinto de Distrito en Materia Civil en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 52,
    nombreOficial:
      "Juzgado Quinto de Distrito de Procesos Penales Federales en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 53,
    nombreOficial:
      "Sexto Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 54,
    nombreOficial:
      "Sexto Tribunal Colegiado en Materia Civil del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 55,
    nombreOficial:
      "Sexto Tribunal Colegiado en Materia de Trabajo del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 56,
    nombreOficial:
      "Sexto Tribunal Colegiado en Materia Penal del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 59,
    nombreOficial:
      "Juzgado Sexto de Distrito en Materia Civil en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 60,
    nombreOficial:
      "Juzgado Sexto de Distrito de Procesos Penales Federales en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 61,
    nombreOficial:
      "Séptimo Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 62,
    nombreOficial:
      "Séptimo Tribunal Colegiado en Materia Civil del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 63,
    nombreOficial:
      "Séptimo Tribunal Colegiado en Materia de Trabajo del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 64,
    nombreOficial:
      "Juzgado Séptimo de Distrito en Materia Administrativa en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 67,
    nombreOficial:
      "Octavo Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 68,
    nombreOficial:
      "Octavo Tribunal Colegiado en Materia Civil del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 69,
    nombreOficial:
      "Octavo Tribunal Colegiado en Materia de Trabajo del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 71,
    nombreOficial:
      "Juzgado Octavo de Distrito en Materia Civil en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 73,
    nombreOficial:
      "Noveno Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 74,
    nombreOficial:
      "Noveno Tribunal Colegiado en Materia Civil del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 75,
    nombreOficial:
      "Noveno Tribunal Colegiado en Materia de Trabajo del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 76,
    nombreOficial:
      "Juzgado Noveno de Distrito en Materia Administrativa en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 77,
    nombreOficial:
      "Juzgado Noveno de Distrito en Materia Civil en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 78,
    nombreOficial:
      "Juzgado Noveno de Distrito de Procesos Penales Federales en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 79,
    nombreOficial:
      "Décimo Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 80,
    nombreOficial:
      "Décimo Tribunal Colegiado en Materia Civil del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 81,
    nombreOficial:
      "Décimo Tribunal Colegiado en Materia de Trabajo del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 83,
    nombreOficial:
      "Juzgado Décimo de Distrito en Materia Civil en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 84,
    nombreOficial:
      "Décimo Primer Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 85,
    nombreOficial:
      "Décimo Primer Tribunal Colegiado en Materia Civil del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 86,
    nombreOficial:
      "Décimo Primer Tribunal Colegiado en Materia de Trabajo del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 87,
    nombreOficial:
      "Juzgado Décimo Primero de Distrito en Materia Civil en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 88,
    nombreOficial:
      "Décimo Segundo Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 89,
    nombreOficial:
      "Décimo Segundo Tribunal Colegiado en Materia Civil del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 90,
    nombreOficial:
      "Décimo Segundo Tribunal Colegiado en Materia de Trabajo del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 91,
    nombreOficial:
      "Juzgado Décimo Segundo de Distrito en Materia Civil en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 92,
    nombreOficial:
      "Décimo Tercer Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 93,
    nombreOficial:
      "Décimo Tercer Tribunal Colegiado en Materia Civil del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 94,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Administrativa del Segundo Circuito",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 5,
  },
  {
    catOrganismoId: 95,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Civil del Segundo Circuito",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 96,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia de Trabajo del Segundo Circuito",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 97,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Penal del Segundo Circuito",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 100,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 5,
  },
  {
    catOrganismoId: 102,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Administrativa del Segundo Circuito",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 5,
  },
  {
    catOrganismoId: 103,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Civil del Segundo Circuito",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 104,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Penal del Segundo Circuito",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 107,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 5,
  },
  {
    catOrganismoId: 109,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia Civil del Segundo Circuito",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 110,
    nombreOficial:
      "Tercer Tribunal Unitario del Segundo Circuito, con residencia en Nezahualcoyotl (2/4/2001) - (15/12/2022)   ",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 3,
  },
  {
    catOrganismoId: 111,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 5,
  },
  {
    catOrganismoId: 115,
    nombreOficial: "Juzgado Sexto de Distrito en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 3,
  },
  {
    catOrganismoId: 116,
    nombreOficial: "Juzgado Séptimo de Distrito en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 5,
  },
  {
    catOrganismoId: 118,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Administrativa del Tercer Circuito",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 119,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Civil del Tercer Circuito",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 120,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia de Trabajo del Tercer Circuito",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 121,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Penal del Tercer Circuito",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 122,
    nombreOficial:
      "Primer Tribunal Unitario del Tercer Circuito (2/4/2001) - (30/11/2022)   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 125,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Colima",
    catCircuitoId: 109,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 126,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia Penal en el Estado de Jalisco (02/04/2001 - 15/10/2012)   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 127,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Administrativa del Tercer Circuito",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 128,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Civil del Tercer Circuito",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 129,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia de Trabajo del Tercer Circuito",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 130,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Penal del Tercer Circuito",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 131,
    nombreOficial:
      "Segundo Tribunal Unitario del Tercer Circuito (2/4/2001) - (30/11/2022)   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 134,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Colima",
    catCircuitoId: 109,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 135,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia Penal en el Estado de Jalisco (02/04/2001 - 15/10/2012)   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 136,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia Civil del Tercer Circuito",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 137,
    nombreOficial:
      "Tercer Tribunal Unitario del Tercer Circuito (2/4/2001) - (30/11/2022)   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 139,
    nombreOficial:
      "Juzgado Decimosegundo de Distrito en Materias Administrativa, Civil y de Trabajo en el Estado de Jalisco, con residencia en Zapopan.   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 140,
    nombreOficial:
      "Juzgado Tercero de Distrito en Materia Penal en el Estado de Jalisco (02/04/2001 - 15/10/2012)   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 141,
    nombreOficial:
      "Cuarto Tribunal Colegiado en Materia Civil del Tercer Circuito",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 142,
    nombreOficial:
      "Juzgado Decimotercero de Distrito en Materias Administrativa, Civil y de Trabajo en el Estado de Jalisco, con residencia en Zapopan.   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 143,
    nombreOficial:
      "Juzgado Cuarto de Distrito en Materia Penal en el Estado de Jalisco (02/04/2001 - 15/10/2012)   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 144,
    nombreOficial:
      "Juzgado Quinto de Distrito en Materia Penal en el Estado de Jalisco (02/04/2001 - 17/10/2012)   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 145,
    nombreOficial:
      "Juzgado Sexto de Distrito en Materia Penal en el Estado de Jalisco (02/04/2001 - 15/10/2012)   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 146,
    nombreOficial:
      "Juzgado Séptimo de Distrito en Materia Penal en el Estado de Jalisco (02/04/2001 - 15/10/2012)   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 147,
    nombreOficial:
      "Juzgado Primero de Distrito de Amparo en Materia Penal en el Estado de Jalisco, con residencia en Zapopan",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 148,
    nombreOficial:
      "Juzgado Segundo de Distrito de Amparo en Materia Penal en el Estado de Jalisco, con residencia en Zapopan",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 149,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Administrativa del Cuarto Circuito",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 150,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Civil del Cuarto Circuito",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 151,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia de Trabajo del Cuarto Circuito",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 152,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Penal del Cuarto Circuito",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 153,
    nombreOficial:
      "Primer Tribunal Unitario del Cuarto Circuito (02/04/2001 - 30/09/2017)   ",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 155,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Administrativa del Cuarto Circuito",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 156,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Civil del Cuarto Circuito",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 157,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia de Trabajo del Cuarto Circuito",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 158,
    nombreOficial:
      "Segundo Tribunal Unitario del Cuarto Circuito (2/4/2001) - (15/12/2022)   ",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 160,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia de Trabajo del Cuarto Circuito",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 167,
    nombreOficial: "Primer Tribunal Unitario del Quinto Circuito",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 168,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Sonora",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 170,
    nombreOficial: "Segundo Tribunal Unitario del Quinto Circuito",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 171,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Sonora",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 173,
    nombreOficial: "Tercer Tribunal Unitario del Quinto Circuito",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 174,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Sonora",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 175,
    nombreOficial: "Cuarto Tribunal Unitario del Quinto Circuito",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 176,
    nombreOficial: "Juzgado Cuarto de Distrito en el Estado de Sonora",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 11,
  },
  {
    catOrganismoId: 177,
    nombreOficial: "Juzgado Quinto de Distrito en el Estado de Sonora",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 11,
  },
  {
    catOrganismoId: 179,
    nombreOficial: "Juzgado Séptimo de Distrito en el Estado de Sonora",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 10,
  },
  {
    catOrganismoId: 180,
    nombreOficial: "Juzgado Noveno de Distrito en el Estado de Sonora",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 68,
  },
  {
    catOrganismoId: 181,
    nombreOficial: "Juzgado Décimo de Distrito en el Estado de Sonora",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 182,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Administrativa del Sexto Circuito",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 183,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Civil del Sexto Circuito",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 184,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia de Trabajo del Sexto Circuito",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 12,
  },
  {
    catOrganismoId: 185,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Penal del Sexto Circuito",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 186,
    nombreOficial: "Primer Tribunal Unitario del Sexto Circuito",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 187,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Tlaxcala",
    catCircuitoId: 50,
    catEstadoId: 29,
    catCiudadId: 75,
  },
  {
    catOrganismoId: 188,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Administrativa del Sexto Circuito",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 189,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Civil del Sexto Circuito",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 191,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Penal del Sexto Circuito",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 192,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Tlaxcala",
    catCircuitoId: 50,
    catEstadoId: 29,
    catCiudadId: 75,
  },
  {
    catOrganismoId: 193,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia Administrativa del Sexto Circuito",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 194,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia Civil del Sexto Circuito",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 195,
    nombreOficial:
      "Juzgado Tercero de Distrito en el Estado de Puebla (02/04/2001 - 31/05/2015)   ",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 200,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Civil del Séptimo Circuito",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 202,
    nombreOficial:
      "Primer Tribunal Unitario del Séptimo Circuito (2/4/2001) - (15/12/2022)   ",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 14,
  },
  {
    catOrganismoId: 203,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Veracruz",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 205,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Civil del Séptimo Circuito",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 207,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Veracruz",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 209,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Veracruz",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 14,
  },
  {
    catOrganismoId: 210,
    nombreOficial: "Juzgado Cuarto de Distrito en el Estado de Veracruz",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 14,
  },
  {
    catOrganismoId: 211,
    nombreOficial: "Juzgado Quinto de Distrito en el Estado de Veracruz",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 14,
  },
  {
    catOrganismoId: 212,
    nombreOficial: "Juzgado Sexto de Distrito en el Estado de Veracruz",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 14,
  },
  {
    catOrganismoId: 213,
    nombreOficial: "Juzgado Séptimo de Distrito en el Estado de Veracruz",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 15,
  },
  {
    catOrganismoId: 214,
    nombreOficial: "Juzgado Octavo de Distrito en el Estado de Veracruz",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 15,
  },
  {
    catOrganismoId: 216,
    nombreOficial:
      "Primer Tribunal Unitario del Octavo Circuito (2/4/2001) - (30/11/2022)   ",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 18,
  },
  {
    catOrganismoId: 217,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Coahuila",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 17,
  },
  {
    catOrganismoId: 219,
    nombreOficial:
      "Segundo Tribunal Unitario del Octavo Circuito (2/4/2001) - (30/11/2022)   ",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 18,
  },
  {
    catOrganismoId: 220,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Coahuila",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 17,
  },
  {
    catOrganismoId: 222,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Coahuila",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 19,
  },
  {
    catOrganismoId: 223,
    nombreOficial: "Juzgado Cuarto de Distrito en el Estado de Coahuila",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 20,
  },
  {
    catOrganismoId: 224,
    nombreOficial:
      "Primer Tribunal Colegiado del Noveno Circuito (02/04/2001 - 31/12/2015)   ",
    catCircuitoId: 9,
    catEstadoId: 24,
    catCiudadId: 22,
  },
  {
    catOrganismoId: 225,
    nombreOficial:
      "Tribunal Unitario del Noveno Circuito (2/4/2001) - (15/12/2022)   ",
    catCircuitoId: 9,
    catEstadoId: 24,
    catCiudadId: 22,
  },
  {
    catOrganismoId: 226,
    nombreOficial:
      "Juzgado Primero de Distrito en el Estado de San Luis Potosí",
    catCircuitoId: 9,
    catEstadoId: 24,
    catCiudadId: 22,
  },
  {
    catOrganismoId: 227,
    nombreOficial:
      "Segundo Tribunal Colegiado del Noveno Circuito (02/04/2001 -31/12/2015)   ",
    catCircuitoId: 9,
    catEstadoId: 24,
    catCiudadId: 22,
  },
  {
    catOrganismoId: 228,
    nombreOficial:
      "Juzgado Segundo de Distrito en el Estado de San Luis Potosí",
    catCircuitoId: 9,
    catEstadoId: 24,
    catCiudadId: 22,
  },
  {
    catOrganismoId: 229,
    nombreOficial:
      "Juzgado Tercero de Distrito en el Estado de San Luis Potosí",
    catCircuitoId: 9,
    catEstadoId: 24,
    catCiudadId: 22,
  },
  {
    catOrganismoId: 230,
    nombreOficial: "Juzgado Cuarto de Distrito en el Estado de San Luis Potosí",
    catCircuitoId: 9,
    catEstadoId: 24,
    catCiudadId: 22,
  },
  {
    catOrganismoId: 232,
    nombreOficial:
      "Primer Tribunal Unitario del Décimo Circuito (2/4/2001) - (15/12/2022)   ",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 233,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Tabasco",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 235,
    nombreOficial:
      "Segundo Tribunal Unitario del Décimo Circuito (2/4/2001) - (15/12/2022)   ",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 237,
    nombreOficial:
      "Tercer Tribunal Colegiado del Décimo Circuito (02/04/2001 - 31/01/2009)   ",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 238,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Tabasco",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 239,
    nombreOficial: "Juzgado Noveno de Distrito en el Estado de Veracruz",
    catCircuitoId: 10,
    catEstadoId: 30,
    catCiudadId: 24,
  },
  {
    catOrganismoId: 240,
    nombreOficial: "Juzgado Décimo de Distrito en el Estado de Veracruz",
    catCircuitoId: 10,
    catEstadoId: 30,
    catCiudadId: 24,
  },
  {
    catOrganismoId: 242,
    nombreOficial:
      "Primer Tribunal Unitario del Décimo Primer Circuito (2/4/2001) - (30/11/2022)   ",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 243,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Michoacán",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 245,
    nombreOficial:
      "Segundo Tribunal Unitario del Décimo Primer Circuito (2/4/2001) - (30/11/2022)   ",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 246,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Michoacán",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 247,
    nombreOficial:
      "Tercer Tribunal Colegiado del Décimo Primer Circuito (02/04/2001 - 14/11/2008)   ",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 248,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Michoacán",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 249,
    nombreOficial: "Juzgado Cuarto de Distrito en el Estado de Michoacán",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 250,
    nombreOficial: "Juzgado Quinto de Distrito en el Estado de Michoacán",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 26,
  },
  {
    catOrganismoId: 251,
    nombreOficial: "Juzgado Sexto de Distrito en el Estado de Michoacán",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 26,
  },
  {
    catOrganismoId: 253,
    nombreOficial:
      "Primer Tribunal Colegiado del Décimo Segundo Circuito (02/04/2001 - 16/08/2014)   ",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 254,
    nombreOficial: "Primer Tribunal Unitario del Décimo Segundo Circuito",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 255,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Sinaloa",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 256,
    nombreOficial:
      "Segundo Tribunal Colegiado del Décimo Segundo Circuito (02/04/2001 - 16/08/2014)   ",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 257,
    nombreOficial: "Segundo Tribunal Unitario del Décimo Segundo Circuito",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 258,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Sinaloa",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 259,
    nombreOficial:
      "Tercer Tribunal Colegiado del Décimo Segundo Circuito (02/04/2001 - 16/08/2014)   ",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 261,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Sinaloa",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 262,
    nombreOficial:
      "Cuarto Tribunal Colegiado del Décimo Segundo Circuito (02/04/2001 - 16/08/2014)   ",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 263,
    nombreOficial: "Juzgado Cuarto de Distrito en el Estado de Sinaloa",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 265,
    nombreOficial: "Juzgado Quinto de Distrito en el Estado de Sinaloa",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 29,
  },
  {
    catOrganismoId: 266,
    nombreOficial: "Juzgado Sexto de Distrito en el Estado de Sinaloa",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 29,
  },
  {
    catOrganismoId: 267,
    nombreOficial: "Juzgado Séptimo de Distrito en el Estado de Sinaloa",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 29,
  },
  {
    catOrganismoId: 268,
    nombreOficial: "Juzgado Octavo de Distrito en el Estado de Sinaloa",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 269,
    nombreOficial: "Juzgado Noveno de Distrito en el Estado de Sinaloa",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 270,
    nombreOficial: "Juzgado Décimo de Distrito en el Estado de Sinaloa",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 272,
    nombreOficial: "Primer Tribunal Unitario del Décimo Tercer Circuito",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 80,
  },
  {
    catOrganismoId: 273,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Oaxaca",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 80,
  },
  {
    catOrganismoId: 275,
    nombreOficial: "Segundo Tribunal Unitario del Décimo Tercer Circuito",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 80,
  },
  {
    catOrganismoId: 276,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Oaxaca",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 80,
  },
  {
    catOrganismoId: 278,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Oaxaca",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 80,
  },
  {
    catOrganismoId: 279,
    nombreOficial: "Juzgado Cuarto de Distrito en el Estado de Oaxaca",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 80,
  },
  {
    catOrganismoId: 281,
    nombreOficial: "Juzgado Sexto de Distrito en el Estado de Oaxaca",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 33,
  },
  {
    catOrganismoId: 282,
    nombreOficial: "Juzgado Séptimo de Distrito en el Estado de Oaxaca",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 33,
  },
  {
    catOrganismoId: 284,
    nombreOficial:
      "Tribunal Unitario del Décimo Cuarto Circuito (2/4/2001) - (15/12/2022)   ",
    catCircuitoId: 38,
    catEstadoId: 31,
    catCiudadId: 34,
  },
  {
    catOrganismoId: 285,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Campeche",
    catCircuitoId: 47,
    catEstadoId: 4,
    catCiudadId: 35,
  },
  {
    catOrganismoId: 288,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Campeche",
    catCircuitoId: 47,
    catEstadoId: 4,
    catCiudadId: 35,
  },
  {
    catOrganismoId: 290,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Yucatán",
    catCircuitoId: 38,
    catEstadoId: 31,
    catCiudadId: 34,
  },
  {
    catOrganismoId: 291,
    nombreOficial: "Primer Tribunal Colegiado del Décimo Quinto Circuito",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 292,
    nombreOficial: "Primer Tribunal Unitario del Décimo Quinto Circuito",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 293,
    nombreOficial:
      "Juzgado Primero de Distrito en el Estado de Baja California",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 294,
    nombreOficial: "Segundo Tribunal Colegiado del Décimo Quinto Circuito",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 295,
    nombreOficial: "Segundo Tribunal Unitario del Décimo Quinto Circuito",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 296,
    nombreOficial:
      "Juzgado Segundo de Distrito en el Estado de Baja California",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 297,
    nombreOficial: "Tercer Tribunal Colegiado del Décimo Quinto Circuito",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 298,
    nombreOficial: "Tercer Tribunal Unitario del Décimo Quinto Circuito",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 300,
    nombreOficial: "Cuarto Tribunal Unitario del Décimo Quinto Circuito",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 302,
    nombreOficial: "Quinto Tribunal Unitario del Décimo Quinto Circuito",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 303,
    nombreOficial:
      "Juzgado Quinto de Distrito en el Estado de Baja California (02/04/2001 - 30/11/2013)   ",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 304,
    nombreOficial:
      "Juzgado Sexto de Distrito en el Estado de Baja California (02/04/2001 - 30/11/2013)   ",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 306,
    nombreOficial:
      "Juzgado Octavo de Distrito en el Estado de Baja California (02/04/2001 - 30/11/2013)   ",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 307,
    nombreOficial:
      "Juzgado Noveno de Distrito en el Estado de Baja California (02/04/2001 - 30/11/2013)   ",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 308,
    nombreOficial:
      "Juzgado Décimo de Distrito en el Estado de Baja California (02/04/2001-15/01/2014)   ",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 40,
  },
  {
    catOrganismoId: 309,
    nombreOficial:
      "Juzgado Décimo Primero de Distrito en el Estado de Baja California (02/04/2001-15/01/2014)   ",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 40,
  },
  {
    catOrganismoId: 312,
    nombreOficial:
      "Primer Tribunal Unitario del Décimo Sexto Circuito (2/4/2001) - (30/11/2022)   ",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 313,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Guanajuato",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 315,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Guanajuato",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 317,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Guanajuato",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 42,
  },
  {
    catOrganismoId: 319,
    nombreOficial: "Juzgado Cuarto de Distrito en el Estado de Guanajuato",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 42,
  },
  {
    catOrganismoId: 321,
    nombreOficial: "Juzgado Quinto de Distrito en el Estado de Guanajuato",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 43,
  },
  {
    catOrganismoId: 322,
    nombreOficial: "Juzgado Sexto de Distrito en el Estado de Guanajuato",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 43,
  },
  {
    catOrganismoId: 324,
    nombreOficial:
      "Primer Tribunal Unitario del Décimo Séptimo Circuito (2/4/2001) - (30/11/2022)   ",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 44,
  },
  {
    catOrganismoId: 325,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Chihuahua",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 44,
  },
  {
    catOrganismoId: 327,
    nombreOficial:
      "Segundo Tribunal Unitario del Décimo Séptimo Circuito (2/4/2001) - (30/11/2022)   ",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 44,
  },
  {
    catOrganismoId: 328,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Chihuahua",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 44,
  },
  {
    catOrganismoId: 330,
    nombreOficial:
      "Tercer Tribunal Unitario del Décimo Séptimo Circuito (2/4/2001) - (30/11/2022)   ",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 45,
  },
  {
    catOrganismoId: 334,
    nombreOficial: "Juzgado Sexto de Distrito en el Estado de Chihuahua",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 45,
  },
  {
    catOrganismoId: 335,
    nombreOficial: "Juzgado Séptimo de Distrito en el Estado de Chihuahua",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 45,
  },
  {
    catOrganismoId: 336,
    nombreOficial:
      "Primer Tribunal Colegiado del Décimo Octavo Circuito (02/04/2001 - 29/02/2016)   ",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 337,
    nombreOficial:
      "Primer Tribunal Unitario del Décimo Octavo Circuito (2/4/2001) - (30/11/2022)   ",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 338,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Morelos",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 339,
    nombreOficial:
      "Segundo Tribunal Colegiado del Décimo Octavo Circuito (02/04/2001 - 29/02/2016)   ",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 340,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Morelos",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 341,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Morelos",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 342,
    nombreOficial: "Juzgado Cuarto de Distrito en el Estado de Morelos",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 344,
    nombreOficial:
      "Primer Tribunal Unitario del Décimo Noveno Circuito (2/4/2001) - (15/12/2022)   ",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 50,
  },
  {
    catOrganismoId: 345,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Tamaulipas",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 50,
  },
  {
    catOrganismoId: 347,
    nombreOficial:
      "Segundo Tribunal Unitario del Décimo Noveno Circuito (2/4/2001) - (15/12/2022)   ",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 48,
  },
  {
    catOrganismoId: 348,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Tamaulipas",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 50,
  },
  {
    catOrganismoId: 350,
    nombreOficial:
      "Tercer Tribunal Unitario del Décimo Noveno Circuito (2/4/2001) - (15/12/2022)   ",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 51,
  },
  {
    catOrganismoId: 351,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Tamaulipas",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 47,
  },
  {
    catOrganismoId: 353,
    nombreOficial:
      "Cuarto Tribunal Unitario del Décimo Noveno Circuito (2/4/2001) - (15/12/2022)   ",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 47,
  },
  {
    catOrganismoId: 358,
    nombreOficial: "Juzgado Octavo de Distrito en el Estado de Tamaulipas",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 51,
  },
  {
    catOrganismoId: 359,
    nombreOficial: "Juzgado Noveno de Distrito en el Estado de Tamaulipas",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 49,
  },
  {
    catOrganismoId: 360,
    nombreOficial: "Juzgado Décimo de Distrito en el Estado de Tamaulipas",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 49,
  },
  {
    catOrganismoId: 362,
    nombreOficial:
      "Primer Tribunal Colegiado del Vigésimo Circuito (02/04/2001 - 15/11/2015)   ",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 363,
    nombreOficial:
      "Primer Tribunal Unitario del Vigésimo Circuito (2/4/2001) - (15/12/2022)   ",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 65,
  },
  {
    catOrganismoId: 364,
    nombreOficial:
      "Juzgado Primero de Distrito en el Estado de Chiapas (02/04/2001 - 13/03/2016)   ",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 365,
    nombreOficial:
      "Segundo Tribunal Colegiado del Vigésimo Circuito (02/04/2001 - 15/11/2015)   ",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 366,
    nombreOficial:
      "Segundo Tribunal Unitario del Vigésimo Circuito (2/4/2001) - (15/12/2022)   ",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 65,
  },
  {
    catOrganismoId: 367,
    nombreOficial:
      "Juzgado Segundo de Distrito en el Estado de Chiapas (02/04/2001 - 13/03/2016)   ",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 369,
    nombreOficial:
      "Juzgado Tercero de Distrito en el Estado de Chiapas (02/04/2001 - 13/03/2016)   ",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 53,
  },
  {
    catOrganismoId: 370,
    nombreOficial:
      "Juzgado Cuarto de Distrito en el Estado de Chiapas (02/04/2001 - 13/03/2016)   ",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 53,
  },
  {
    catOrganismoId: 372,
    nombreOficial:
      "Juzgado Sexto de Distrito en el Estado de Chiapas (02/04/2001 - 13/03/2016)   ",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 374,
    nombreOficial:
      "Primer Tribunal Unitario del Vigésimo Primer Circuito (2/4/2001) - (30/11/2022)   ",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 54,
  },
  {
    catOrganismoId: 375,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Guerrero",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 54,
  },
  {
    catOrganismoId: 377,
    nombreOficial:
      "Segundo Tribunal Unitario del Vigésimo Primer Circuito (2/4/2001) - (30/11/2022)   ",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 55,
  },
  {
    catOrganismoId: 378,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Guerrero",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 55,
  },
  {
    catOrganismoId: 380,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Guerrero",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 55,
  },
  {
    catOrganismoId: 382,
    nombreOficial: "Juzgado Cuarto de Distrito en el Estado de Guerrero",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 55,
  },
  {
    catOrganismoId: 383,
    nombreOficial: "Juzgado Quinto de Distrito en el Estado de Guerrero",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 56,
  },
  {
    catOrganismoId: 384,
    nombreOficial: "Juzgado Sexto de Distrito en el Estado de Guerrero",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 55,
  },
  {
    catOrganismoId: 385,
    nombreOficial:
      "Primer Tribunal Colegiado del Vigésimo Segundo Circuito (02/04/2001 - 13/03/2016)   ",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 386,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Hidalgo",
    catCircuitoId: 49,
    catEstadoId: 14,
    catCiudadId: 58,
  },
  {
    catOrganismoId: 387,
    nombreOficial:
      "Segundo Tribunal Colegiado del Vigésimo Segundo Circuito (02/04/2001 - 13/03/2016)   ",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 389,
    nombreOficial: "Primer Tribunal Colegiado del Vigésimo Noveno Circuito",
    catCircuitoId: 49,
    catEstadoId: 14,
    catCiudadId: 58,
  },
  {
    catOrganismoId: 391,
    nombreOficial: "Segundo Tribunal Colegiado del Vigésimo Noveno Circuito",
    catCircuitoId: 49,
    catEstadoId: 14,
    catCiudadId: 58,
  },
  {
    catOrganismoId: 392,
    nombreOficial: "Primer Tribunal Colegiado del Vigésimo Tercer Circuito",
    catCircuitoId: 56,
    catEstadoId: 32,
    catCiudadId: 59,
  },
  {
    catOrganismoId: 393,
    nombreOficial:
      "Tribunal Unitario del Vigésimo Tercer Circuito (2/4/2001) - (15/12/2022)   ",
    catCircuitoId: 56,
    catEstadoId: 32,
    catCiudadId: 59,
  },
  {
    catOrganismoId: 394,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Aguascalientes",
    catCircuitoId: 30,
    catEstadoId: 1,
    catCiudadId: 60,
  },
  {
    catOrganismoId: 395,
    nombreOficial: "Primer Tribunal Colegiado del Trigésimo Circuito",
    catCircuitoId: 30,
    catEstadoId: 1,
    catCiudadId: 60,
  },
  {
    catOrganismoId: 396,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Zacatecas",
    catCircuitoId: 56,
    catEstadoId: 32,
    catCiudadId: 59,
  },
  {
    catOrganismoId: 397,
    nombreOficial: "Segundo Tribunal Colegiado del Trigésimo Circuito",
    catCircuitoId: 30,
    catEstadoId: 1,
    catCiudadId: 60,
  },
  {
    catOrganismoId: 398,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Aguascalientes",
    catCircuitoId: 30,
    catEstadoId: 1,
    catCiudadId: 60,
  },
  {
    catOrganismoId: 399,
    nombreOficial: "Primer Tribunal Colegiado del Vigésimo Cuarto Circuito",
    catCircuitoId: 48,
    catEstadoId: 18,
    catCiudadId: 30,
  },
  {
    catOrganismoId: 400,
    nombreOficial:
      "Primer Tribunal Unitario del Vigésimo Cuarto Circuito  (2/4/2001) - (30/11/2022)   ",
    catCircuitoId: 48,
    catEstadoId: 18,
    catCiudadId: 30,
  },
  {
    catOrganismoId: 403,
    nombreOficial: "Primer Tribunal Colegiado del Vigésimo Quinto Circuito",
    catCircuitoId: 52,
    catEstadoId: 10,
    catCiudadId: 21,
  },
  {
    catOrganismoId: 404,
    nombreOficial:
      "Tribunal Unitario del Vigésimo Quinto Circuito (2/4/2001) - (30/11/2022)   ",
    catCircuitoId: 52,
    catEstadoId: 10,
    catCiudadId: 21,
  },
  {
    catOrganismoId: 405,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Durango",
    catCircuitoId: 52,
    catEstadoId: 10,
    catCiudadId: 21,
  },
  {
    catOrganismoId: 406,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Durango",
    catCircuitoId: 52,
    catEstadoId: 10,
    catCiudadId: 21,
  },
  {
    catOrganismoId: 407,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Durango",
    catCircuitoId: 52,
    catEstadoId: 10,
    catCiudadId: 21,
  },
  {
    catOrganismoId: 408,
    nombreOficial: "Primer Tribunal Colegiado del Vigésimo Sexto Circuito",
    catCircuitoId: 55,
    catEstadoId: 3,
    catCiudadId: 31,
  },
  {
    catOrganismoId: 409,
    nombreOficial: "Tribunal Unitario del Vigésimo Sexto Circuito",
    catCircuitoId: 55,
    catEstadoId: 3,
    catCiudadId: 31,
  },
  {
    catOrganismoId: 410,
    nombreOficial:
      "Juzgado Primero de Distrito en el Estado de Baja California Sur",
    catCircuitoId: 55,
    catEstadoId: 3,
    catCiudadId: 31,
  },
  {
    catOrganismoId: 411,
    nombreOficial:
      "Juzgado Segundo de Distrito en el Estado de Baja California Sur",
    catCircuitoId: 55,
    catEstadoId: 3,
    catCiudadId: 31,
  },
  {
    catOrganismoId: 412,
    nombreOficial: "Juzgado Octavo de Distrito en el Estado de Sonora",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 10,
  },
  {
    catOrganismoId: 413,
    nombreOficial:
      "Segundo Tribunal Unitario del Décimo Sexto Circuito (2/4/2001) - (30/11/2022)   ",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 415,
    nombreOficial:
      "Juzgado Primero de Distrito en el Estado de Puebla (02/04/2001 - 31/05/2015)   ",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 417,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Hidalgo",
    catCircuitoId: 49,
    catEstadoId: 14,
    catCiudadId: 58,
  },
  {
    catOrganismoId: 418,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Aguascalientes",
    catCircuitoId: 30,
    catEstadoId: 1,
    catCiudadId: 60,
  },
  {
    catOrganismoId: 419,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Quintana Roo",
    catCircuitoId: 54,
    catEstadoId: 23,
    catCiudadId: 37,
  },
  {
    catOrganismoId: 420,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Yucatán",
    catCircuitoId: 38,
    catEstadoId: 31,
    catCiudadId: 34,
  },
  {
    catOrganismoId: 422,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Yucatán",
    catCircuitoId: 38,
    catEstadoId: 31,
    catCiudadId: 34,
  },
  {
    catOrganismoId: 425,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Quintana Roo",
    catCircuitoId: 54,
    catEstadoId: 23,
    catCiudadId: 36,
  },
  {
    catOrganismoId: 426,
    nombreOficial:
      "Cuarto Tribunal Colegiado en Materia Civil del Segundo Circuito",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 427,
    nombreOficial:
      "Tribunal Unitario del Vigésimo Segundo Circuito (2/4/2001) - (15/12/2022)   ",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 428,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia Administrativa del Segundo Circuito",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 5,
  },
  {
    catOrganismoId: 459,
    nombreOficial: "Juzgado Primero de Distrito en el Estado de Zacatecas",
    catCircuitoId: 56,
    catEstadoId: 32,
    catCiudadId: 59,
  },
  {
    catOrganismoId: 460,
    nombreOficial: "Juzgado Octavo de Distrito en el Estado de Michoacán",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 26,
  },
  {
    catOrganismoId: 461,
    nombreOficial: "Juzgado Cuarto de Distrito en el Estado de Yucatán",
    catCircuitoId: 38,
    catEstadoId: 31,
    catCiudadId: 34,
  },
  {
    catOrganismoId: 462,
    nombreOficial: "Primer Tribunal Colegiado del Vigésimo Séptimo Circuito",
    catCircuitoId: 54,
    catEstadoId: 23,
    catCiudadId: 36,
  },
  {
    catOrganismoId: 463,
    nombreOficial: "Juzgado Séptimo de Distrito en el Estado de Guanajuato",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 42,
  },
  {
    catOrganismoId: 464,
    nombreOficial:
      "Órgano Jurisdiccional Virtual para Pruebas de Nuevos Desarrollos",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 465,
    nombreOficial:
      "Primer Tribunal Unitario del Vigésimo Séptimo Circuito (6/8/2001) - (15/12/2022)   ",
    catCircuitoId: 54,
    catEstadoId: 23,
    catCiudadId: 36,
  },
  {
    catOrganismoId: 466,
    nombreOficial: "Juzgado Octavo de Distrito en el Estado de Oaxaca",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 80,
  },
  {
    catOrganismoId: 467,
    nombreOficial: "Juzgado Cuarto de Distrito en el Estado de Tabasco",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 468,
    nombreOficial:
      "Juzgado Décimo Tercero de Distrito en el Estado de Baja California (03/09/2001 - 30/11/2013)   ",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 469,
    nombreOficial:
      "Juzgado Séptimo de Distrito en el Estado de Puebla (08/10/2001 - 31/05/2015)   ",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 470,
    nombreOficial: "Sexto Tribunal Unitario del Décimo Quinto Circuito",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 471,
    nombreOficial: "Juzgado Quinto de Distrito en el Estado de San Luis Potosí",
    catCircuitoId: 9,
    catEstadoId: 24,
    catCiudadId: 62,
  },
  {
    catOrganismoId: 473,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia Administrativa del Tercer Circuito",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 475,
    nombreOficial:
      "Quinto Tribunal Colegiado en Materia Civil del Tercer Circuito",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 476,
    nombreOficial: "Juzgado Quinto de Distrito en el Estado de Chihuahua",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 45,
  },
  {
    catOrganismoId: 477,
    nombreOficial:
      "Juzgado Primero de Distrito en el Estado de Querétaro (05/11/2001 - 30/09/2014)   ",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 478,
    nombreOficial:
      "Cuarto Tribunal Unitario del Tercer Circuito (5/11/2001) - (30/11/2022)   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 479,
    nombreOficial:
      "Cuarto Tribunal Unitario del Décimo Séptimo Circuito (26/11/2001) - (30/11/2022)   ",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 45,
  },
  {
    catOrganismoId: 480,
    nombreOficial: "Segundo Órgano Jurisdiccional Virtual para Pruebas",
    catCircuitoId: 110,
    catEstadoId: 6,
    catCiudadId: 45,
  },
  {
    catOrganismoId: 482,
    nombreOficial:
      "Juzgado Tercero de Distrito en el Estado de Querétaro (05/11/2001 - 30/09/2014)   ",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 483,
    nombreOficial: "Juzgado Quinto de Distrito en el Estado de Oaxaca",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 80,
  },
  {
    catOrganismoId: 484,
    nombreOficial: "Primer Tribunal Colegiado del Decimoséptimo Circuito",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 45,
  },
  {
    catOrganismoId: 486,
    nombreOficial:
      "Juzgado Décimo de Distrito de Procesos Penales Federales en la Ciudad de México (19/11/2001 - 01/04/2019)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 488,
    nombreOficial:
      "Juzgado Décimo Segundo de Distrito de Procesos Penales Federales en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 490,
    nombreOficial:
      "Juzgado Décimo Cuarto de Distrito de Procesos Penales Federales en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 491,
    nombreOficial:
      "Juzgado Décimo Quinto de Distrito de Procesos Penales Federales en la Ciudad de México (14/12/2001 - 24/06/2015)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 493,
    nombreOficial:
      "Juzgado Décimo Séptimo de Distrito de Procesos Penales Federales en la Ciudad de México (14/12/2001 - 24/06/2015)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 494,
    nombreOficial:
      "Juzgado Décimo Octavo de Distrito de Procesos Penales Federales en la Ciudad de México (14/12/2001 - 01/04/2019)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 495,
    nombreOficial:
      "Décimo Tercer Tribunal Colegiado en Materia de Trabajo del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 496,
    nombreOficial:
      "Décimo Cuarto Tribunal Colegiado en Materia de Trabajo del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 497,
    nombreOficial:
      "Décimo Quinto Tribunal Colegiado en Materia de Trabajo del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 498,
    nombreOficial:
      "Décimo Cuarto Tribunal Colegiado en Materia Civil del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 499,
    nombreOficial:
      "Séptimo Tribunal Colegiado en Materia Penal del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 500,
    nombreOficial:
      "Octavo Tribunal Colegiado en Materia Penal del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 501,
    nombreOficial:
      "Noveno Tribunal Colegiado en Materia Penal del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 506,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia Civil del Cuarto Circuito",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 507,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Penal del Cuarto Circuito",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 508,
    nombreOficial: "Primer Tribunal Colegiado del Decimonoveno Circuito",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 51,
  },
  {
    catOrganismoId: 510,
    nombreOficial:
      "Juzgado Tercero de Distrito en el Estado de Baja California",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 511,
    nombreOficial:
      "Juzgado Séptimo de Distrito de Procesos Penales Federales en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 512,
    nombreOficial:
      "Juzgado Octavo de Distrito de Procesos Penales Federales en la Ciudad de México (14/12/2001 - 01/04/2019)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 513,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materias Administrativa, Civil y de Trabajo en el Estado de Jalisco, con residencia en Zapopan.   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 516,
    nombreOficial:
      "Juzgado Sexto de Distrito en el Estado de Puebla (08/10/2001 - 31/05/2015)   ",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 517,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Tabasco",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 518,
    nombreOficial:
      "Juzgado Cuarto de Distrito en el Estado de Baja California (03/09/2001 - 30/11/2013)   ",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 519,
    nombreOficial: "Juzgado Séptimo de Distrito en el Estado de Tamaulipas",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 51,
  },
  {
    catOrganismoId: 527,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia de Trabajo en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 534,
    nombreOficial:
      "Juzgado Tercero de Distrito en Materia Administrativa en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 535,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia de Trabajo en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 555,
    nombreOficial:
      "Juzgado Primero de Distrito de Procesos Penales Federales en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 556,
    nombreOficial:
      "Juzgado Séptimo de Distrito en el Estado de Baja California (12/11/2001 - 30/11/2013)   ",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 557,
    nombreOficial: "Juzgado Cuarto de Distrito en el Estado de Chihuahua",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 45,
  },
  {
    catOrganismoId: 558,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia Penal en el Estado de Nuevo León",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 559,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia Penal en el Estado de Nuevo León",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 560,
    nombreOficial:
      "Juzgado Tercero de Distrito en Materia Penal en el Estado de Nuevo León",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 561,
    nombreOficial:
      "Juzgado Cuarto de Distrito en Materia Penal en el Estado de Nuevo León",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 562,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia Administrativa en el Estado de Nuevo León",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 569,
    nombreOficial:
      "Juzgado Tercero de Distrito en Materias Civil y de Trabajo en el Estado de Nuevo León",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 574,
    nombreOficial:
      "Tribunal Unitario del Vigésimo Octavo Circuito (30/10/2002) - (15/12/2022)   ",
    catCircuitoId: 50,
    catEstadoId: 29,
    catCiudadId: 75,
  },
  {
    catOrganismoId: 575,
    nombreOficial: "Primer Tribunal Colegiado del Vigésimo Octavo Circuito",
    catCircuitoId: 50,
    catEstadoId: 29,
    catCiudadId: 75,
  },
  {
    catOrganismoId: 576,
    nombreOficial:
      "Juzgado Décimo Primero de Distrito en Materia Administrativa en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 583,
    nombreOficial:
      "Tribunal Unitario del Vigésimo Noveno Circuito (13/12/2002) - (15/12/2022)   ",
    catCircuitoId: 49,
    catEstadoId: 14,
    catCiudadId: 58,
  },
  {
    catOrganismoId: 585,
    nombreOficial:
      "Juzgado Décimo Primero de Distrito en el Estado de Veracruz",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 63,
  },
  {
    catOrganismoId: 586,
    nombreOficial:
      "Décimo Cuarto Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 601,
    nombreOficial:
      "Juzgado Quinto de Distrito en el Estado de Chiapas (08/07/2003 - 13/03/2016)   ",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 647,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia Administrativa del Cuarto Circuito",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 648,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Civil y de Trabajo del Decimoséptimo Circuito",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 44,
  },
  {
    catOrganismoId: 649,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Penal y Administrativa del Décimo Séptimo Circuito",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 44,
  },
  {
    catOrganismoId: 650,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Civil y de Trabajo del Decimoséptimo Circuito",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 44,
  },
  {
    catOrganismoId: 651,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Penal y Administrativa del Décimo Séptimo Circuito",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 44,
  },
  {
    catOrganismoId: 652,
    nombreOficial:
      "Juzgado Cuarto de Distrito en Materia de Trabajo en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 653,
    nombreOficial: "Juzgado Octavo de Distrito en el Estado de Chihuahua",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 44,
  },
  {
    catOrganismoId: 654,
    nombreOficial: "Juzgado Noveno de Distrito en el Estado de Chihuahua",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 45,
  },
  {
    catOrganismoId: 655,
    nombreOficial:
      "Tercer Tribunal Colegiado del Vigésimo Circuito (07/10/2003 - 15/11/2015)   ",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 658,
    nombreOficial:
      "Quinto Tribunal Unitario en Materia Penal del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 659,
    nombreOficial:
      "Juzgado Primero de Distrito de Procesos Penales Federales en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 660,
    nombreOficial:
      "Juzgado Segundo de Distrito de Procesos Penales Federales en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 661,
    nombreOficial:
      "Juzgado Tercero de Distrito de Procesos Penales Federales en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 662,
    nombreOficial:
      "Juzgado Cuarto de Distrito de Procesos Penales Federales en el Estado de México (28/10/2003 - 30/09/2018)   ",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 663,
    nombreOficial:
      "Juzgado Quinto de Distrito de Procesos Penales Federales en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 664,
    nombreOficial:
      "Juzgado Sexto de Distrito de Procesos Penales Federales en el Estado de México (28/10/2003 - 15/10/2019)   ",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 665,
    nombreOficial:
      "Juzgado Primero de Distrito en Materias de Amparo y Juicios Federales en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 666,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materias de Amparo y Juicios Federales en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 667,
    nombreOficial:
      "Juzgado Tercero de Distrito en Materias de Amparo y Juicios Federales en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 668,
    nombreOficial:
      "Juzgado Cuarto de Distrito en Materias de Amparo y Juicios Federales en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 670,
    nombreOficial:
      "Segundo Tribunal Unitario del Segundo Circuito (27/10/2003) - (15/12/2022)   ",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 671,
    nombreOficial:
      "Cuarto Tribunal Unitario del Segundo Circuito (27/10/2003) - (15/12/2022)   ",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 672,
    nombreOficial:
      "Quinto Tribunal Unitario del Segundo Circuito (27/10/2003) - (15/12/2022)   ",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 673,
    nombreOficial: "Juzgado Noveno de Distrito en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 3,
  },
  {
    catOrganismoId: 674,
    nombreOficial:
      "Juzgado Décimo Segundo de Distrito en el Estado de Baja California (05/01/2004-15/01/2014)   ",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 675,
    nombreOficial:
      "Juzgado Décimo Cuarto de Distrito en el Estado de Baja California (05/01/2004-15/01/2014)   ",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 679,
    nombreOficial:
      "Juzgado Tercero de Distrito en Materias Administrativa, Civil y de Trabajo en el Estado de Jalisco, con residencia en Zapopan.   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 680,
    nombreOficial:
      "Juzgado Quinto de Distrito en Materias Administrativa, Civil y de Trabajo en el Estado de Jalisco, con residencia en Zapopan.   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 681,
    nombreOficial:
      "Juzgado Decimocuarto de Distrito en Materias Administrativa, Civil y de Trabajo en el Estado de Jalisco, con residencia en Zapopan.   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 682,
    nombreOficial:
      "Juzgado Quinto de Distrito en Materia Penal en el Estado de Nuevo León",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 683,
    nombreOficial:
      "Juzgado Cuarto de Distrito en Materias Civil y de Trabajo en el Estado de Nuevo León",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 684,
    nombreOficial:
      "Juzgado Segundo de Distrito en el Estado de Puebla (15/03/2004 - 31/05/2015)   ",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 685,
    nombreOficial:
      "Juzgado Cuarto de Distrito en el Estado de Puebla (15/03/2004 - 31/05/2015)   ",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 686,
    nombreOficial:
      "Juzgado Quinto de Distrito en el Estado de Puebla (15/03/2004 - 31/05/2015)   ",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 687,
    nombreOficial:
      "Juzgado Octavo de Distrito en el Estado de Puebla (15/03/2004 - 31/05/2015)   ",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 688,
    nombreOficial:
      "Juzgado Noveno de Distrito en el Estado de Puebla (15/03/2004 - 31/05/2015)   ",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 689,
    nombreOficial:
      "Juzgado Décimo de Distrito en el Estado de Puebla (15/03/2004 - 31/05/2015)   ",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 690,
    nombreOficial: "Juzgado Octavo de Distrito en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 5,
  },
  {
    catOrganismoId: 691,
    nombreOficial: "Juzgado Quinto de Distrito en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 3,
  },
  {
    catOrganismoId: 692,
    nombreOficial:
      "Juzgado Décimo de Distrito en Materias Administrativa, Civil y de Trabajo en el Estado de Jalisco, con residencia en Zapopan.   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 693,
    nombreOficial:
      "Juzgado Decimoprimero de Distrito en Materias Administrativa, Civil y de Trabajo en el Estado de Jalisco, con residencia en Zapopan.   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 694,
    nombreOficial:
      "Juzgado Primero de Distrito en Materias Administrativa, Civil y de Trabajo en el Estado de Jalisco, con residencia en Zapopan.   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 695,
    nombreOficial:
      "Juzgado Cuarto de Distrito en Materias Administrativa, Civil y de Trabajo en el Estado de Jalisco, con residencia en Zapopan.   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 696,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia Civil y de Trabajo en el Estado de Nuevo León",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 697,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia Civil y de Trabajo en el Estado de Nuevo León",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 698,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia Administrativa en el estado de Nuevo León",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 700,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Chihuahua",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 44,
  },
  {
    catOrganismoId: 702,
    nombreOficial: "Quinto Tribunal Unitario del Quinto Circuito",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 703,
    nombreOficial: "Cuarto Tribunal Colegiado del Décimo Quinto Circuito",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 704,
    nombreOficial: "Séptimo Tribunal Unitario del Décimo Quinto Circuito",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 706,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia Penal del Segundo Circuito",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 707,
    nombreOficial: "Juzgado Noveno de Distrito en el Estado de Michoacán",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 708,
    nombreOficial:
      "Décimo Quinto Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 709,
    nombreOficial:
      "Juzgado Décimo Segundo de Distrito en el Estado de Veracruz",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 64,
  },
  {
    catOrganismoId: 712,
    nombreOficial: "Juzgado Primero de Distrito en La Laguna",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 18,
  },
  {
    catOrganismoId: 713,
    nombreOficial: "Juzgado Tercero de Distrito en La Laguna",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 18,
  },
  {
    catOrganismoId: 714,
    nombreOficial:
      "Juzgado Décimo Tercero de Distrito en el Estado de Veracruz",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 63,
  },
  {
    catOrganismoId: 717,
    nombreOficial: "Juzgado Decimocuarto de Distrito en el Estado de Veracruz",
    catCircuitoId: 10,
    catEstadoId: 30,
    catCiudadId: 24,
  },
  {
    catOrganismoId: 721,
    nombreOficial: "Juzgado Séptimo de Distrito en el Estado de Guerrero",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 54,
  },
  {
    catOrganismoId: 722,
    nombreOficial: "Juzgado Octavo de Distrito en el Estado de Guerrero",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 55,
  },
  {
    catOrganismoId: 726,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia Administrativa en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 727,
    nombreOficial:
      "Juzgado Quinto de Distrito en Materia Administrativa en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 728,
    nombreOficial:
      "Juzgado Sexto de Distrito en Materia Administrativa en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 729,
    nombreOficial:
      "Juzgado Octavo de Distrito en Materia Administrativa en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 730,
    nombreOficial:
      "Juzgado Décimo de Distrito en Materia Administrativa en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 731,
    nombreOficial:
      "Juzgado Décimo Segundo de Distrito en Materia Administrativa en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 732,
    nombreOficial:
      "Juzgado Décimo Tercero de Distrito en Materia Administrativa en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 733,
    nombreOficial:
      "Juzgado Décimo Cuarto de Distrito en Materia Administrativa en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 734,
    nombreOficial:
      "Juzgado Décimo Quinto de Distrito en Materia Administrativa en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 735,
    nombreOficial:
      "Juzgado Décimo Sexto de Distrito en Materia Administrativa en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 736,
    nombreOficial:
      "Juzgado Segundo de  Distrito en el Estado de Querétaro (08/11/2004 - 30/09/2014)   ",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 737,
    nombreOficial:
      "Juzgado Cuarto de Distrito en el Estado de Querétaro (08/11/2004 - 30/09/2014)   ",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 744,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Administrativa y Civil del Décimo Noveno Circuito",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 50,
  },
  {
    catOrganismoId: 745,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Administrativa y Civil del Décimo Noveno Circuito",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 50,
  },
  {
    catOrganismoId: 746,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Penal y de Trabajo del Décimo Noveno Circuito",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 50,
  },
  {
    catOrganismoId: 747,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Penal y de Trabajo del Décimo Noveno Circuito",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 50,
  },
  {
    catOrganismoId: 749,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Civil y de Trabajo del Vigésimo Primer Circuito",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 54,
  },
  {
    catOrganismoId: 750,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Civil y de Trabajo del Vigésimo Primer Circuito",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 54,
  },
  {
    catOrganismoId: 751,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Penal y Administrativa del Vigésimo Primer Circuito",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 55,
  },
  {
    catOrganismoId: 752,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Penal y Administrativa del Vigésimo Primer Circuito",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 55,
  },
  {
    catOrganismoId: 753,
    nombreOficial: "Juzgado Quinto de Distrito en el Estado de Morelos",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 758,
    nombreOficial:
      "Juzgado Séptimo de Distrito en Materia Civil en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 759,
    nombreOficial:
      "Juzgado Decimotercero de Distrito en Materia Civil en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 760,
    nombreOficial: "Juzgado Decimoquinto de Distrito en el Estado de Veracruz",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 761,
    nombreOficial: "Juzgado Décimo de Distrito en el Estado de Chihuahua",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 44,
  },
  {
    catOrganismoId: 762,
    nombreOficial:
      "Juzgado Primero de Distrito de Amparo en Materia Penal en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 763,
    nombreOficial:
      "Juzgado Segundo de Distrito de Amparo en Materia Penal en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 764,
    nombreOficial:
      "Juzgado Tercero de Distrito de Amparo en Materia Penal en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 765,
    nombreOficial:
      "Juzgado Cuarto de Distrito de Amparo en Materia Penal en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 766,
    nombreOficial:
      "Juzgado Quinto de Distrito de Amparo en Materia Penal en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 767,
    nombreOficial:
      "Juzgado Sexto de Distrito de Amparo en Materia Penal en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 768,
    nombreOficial:
      "Juzgado Séptimo de Distrito de Amparo en Materia Penal en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 769,
    nombreOficial:
      "Juzgado Octavo de Distrito de Amparo en Materia Penal en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 770,
    nombreOficial:
      "Juzgado Noveno de Distrito de Amparo en Materia Penal en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 771,
    nombreOficial:
      "Juzgado Décimo de Distrito de Amparo en Materia Penal en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 772,
    nombreOficial:
      "Juzgado Decimoprimero de Distrito de Amparo en Materia Penal en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 773,
    nombreOficial:
      "Juzgado Decimosegundo de Distrito de Amparo en Materia Penal en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 774,
    nombreOficial:
      "Tercer Tribunal Colegiado del Noveno Circuito (24/10/2005 - 31/12/2015)   ",
    catCircuitoId: 9,
    catEstadoId: 24,
    catCiudadId: 22,
  },
  {
    catOrganismoId: 775,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Penal y Administrativa del Quinto Circuito",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 776,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Penal y Administrativa del Quinto Circuito",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 777,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materias Penal y Administrativa del Quinto Circuito",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 778,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Civil y de Trabajo del Quinto Circuito",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 779,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Civil y de Trabajo del Quinto Circuito",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 780,
    nombreOficial:
      "Juzgado Décimo Primero de Distrito en el Estado de Tamaulipas",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 50,
  },
  {
    catOrganismoId: 781,
    nombreOficial:
      "Juzgado Tercero de Distrito en Materia de Trabajo en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 782,
    nombreOficial:
      "Juzgado Quinto de Distrito en Materia de Trabajo en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 784,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Administrativa y de Trabajo del Decimosexto Circuito (16/01/2006 - 15/11/2013)   ",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 786,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Penal del Decimosexto Circuito",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 788,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Administrativa y de Trabajo del Decimosexto Circuito (16/01/2006 - 15/11/2013)   ",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 790,
    nombreOficial: "Juzgado Segundo de Distrito en el Estado de Quintana Roo",
    catCircuitoId: 54,
    catEstadoId: 23,
    catCiudadId: 36,
  },
  {
    catOrganismoId: 791,
    nombreOficial: "Juzgado Cuarto de Distrito en el Estado de Quintana Roo",
    catCircuitoId: 54,
    catEstadoId: 23,
    catCiudadId: 36,
  },
  {
    catOrganismoId: 792,
    nombreOficial:
      "Decimosexto Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 793,
    nombreOficial:
      "Sexto Tribunal Unitario en Materia Penal del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 794,
    nombreOficial:
      "Cuarto Tribunal Colegiado en Materia Penal del Segundo Circuito",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 795,
    nombreOficial:
      "Cuarto Tribunal Colegiado en Materia Administrativa del Tercer Circuito",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 796,
    nombreOficial:
      "Segundo Tribunal Unitario del Séptimo Circuito (1/8/2006) - (15/12/2022)   ",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 14,
  },
  {
    catOrganismoId: 798,
    nombreOficial:
      "Juzgado Decimoquinto de Distrito en el Estado de Baja California (01/08/2006-15/01/2014)   ",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 799,
    nombreOficial:
      "Juzgado Decimosexto de Distrito en el Estado de Baja California (01/08/2006 - 30/11/2013)   ",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 800,
    nombreOficial:
      "Tercer Tribunal Colegiado del Vigésimo Segundo Circuito (01/08/2006 - 13/03/2016)   ",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 801,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Hidalgo",
    catCircuitoId: 49,
    catEstadoId: 14,
    catCiudadId: 58,
  },
  {
    catOrganismoId: 802,
    nombreOficial: "Segundo Tribunal Colegiado del Decimonoveno Circuito",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 51,
  },
  {
    catOrganismoId: 803,
    nombreOficial:
      "Juzgado Quinto de Distrito en Materias de Amparo y Juicios Federales en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 931,
    nombreOficial: "Juzgado Segundo de Distrito en La Laguna",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 18,
  },
  {
    catOrganismoId: 932,
    nombreOficial: "Juzgado Cuarto de Distrito en La Laguna",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 18,
  },
  {
    catOrganismoId: 933,
    nombreOficial:
      "Decimoséptimo Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 934,
    nombreOficial:
      "Tercer Tribunal Unitario en Materias Civil, Administrativa y Especializados en Competencia Económica, Radiodifusión y Telecomunicaciones",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 935,
    nombreOficial:
      "Juzgado Sexto de Distrito en Materia de Trabajo en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 936,
    nombreOficial: "Juzgado Decimosexto de Distrito en el Estado de Veracruz",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 64,
  },
  {
    catOrganismoId: 938,
    nombreOficial:
      "Juzgado Decimotercero de Distrito de Amparo en Materia Penal en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 939,
    nombreOficial:
      "Juzgado Decimocuarto de Distrito de Amparo en Materia Penal en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 940,
    nombreOficial: "Juzgado Decimoprimero de Distrito en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 5,
  },
  {
    catOrganismoId: 941,
    nombreOficial: "Juzgado Séptimo de Distrito en el Estado de Michoacán",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 942,
    nombreOficial: "Juzgado Cuarto de Distrito en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 5,
  },
  {
    catOrganismoId: 943,
    nombreOficial: "Juzgado Décimo de Distrito en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 5,
  },
  {
    catOrganismoId: 944,
    nombreOficial: "Segundo Tribunal Colegiado del Vigésimo Séptimo Circuito",
    catCircuitoId: 54,
    catEstadoId: 23,
    catCiudadId: 36,
  },
  {
    catOrganismoId: 946,
    nombreOficial:
      "Tercer Tribunal Colegiado del Decimoctavo Circuito (15/01/2007 - 29/02/2016)   ",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 949,
    nombreOficial:
      "Juzgado Primero de Distrito del Centro Auxiliar de la Primera Región, con residencia en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 950,
    nombreOficial:
      "Tribunal Colegiado en Materias Penal y Administrativa del Decimocuarto Circuito",
    catCircuitoId: 38,
    catEstadoId: 31,
    catCiudadId: 34,
  },
  {
    catOrganismoId: 951,
    nombreOficial:
      "Tribunal Colegiado en Materias Civil y Administrativa del Decimocuarto Circuito",
    catCircuitoId: 38,
    catEstadoId: 31,
    catCiudadId: 34,
  },
  {
    catOrganismoId: 952,
    nombreOficial:
      "Tribunal Colegiado en Materias de Trabajo y Administrativa del Decimocuarto Circuito",
    catCircuitoId: 38,
    catEstadoId: 31,
    catCiudadId: 34,
  },
  {
    catOrganismoId: 953,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Penal y de Trabajo del Séptimo Circuito (16/05/2007 - 30/11/2014)   ",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 14,
  },
  {
    catOrganismoId: 954,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Penal y de Trabajo del Séptimo Circuito (16/05/2007 - 30/11/2014)   ",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 14,
  },
  {
    catOrganismoId: 955,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materias Penal y de Trabajo del Séptimo Circuito (16/05/2007 - 30/11/2014)   ",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 956,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Administrativa del Séptimo Circuito",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 14,
  },
  {
    catOrganismoId: 957,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Administrativa del Séptimo Circuito",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 14,
  },
  {
    catOrganismoId: 964,
    nombreOficial:
      "Juzgado Segundo de Distrito del Centro Auxiliar de la Primera Región, con residencia en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 967,
    nombreOficial:
      "Juzgado Primero de Distrito del Centro Auxiliar de la Segunda Región (01/11/2007 - 31/10/2018)   ",
    catCircuitoId: 33,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 968,
    nombreOficial:
      "Juzgado Segundo de Distrito del Centro Auxiliar de la Segunda Región (01/12/2007 - 31/10/2018)   ",
    catCircuitoId: 33,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 970,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Civil y de Trabajo del Decimosexto Circuito (01/12/2007 - 15/11/2013)   ",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 971,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Civil y de Trabajo del Decimosexto Circuito (01/12/2007 - 15/11/2013)   ",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 973,
    nombreOficial:
      "Juzgado Séptimo de Distrito en el Estado de Chiapas (01/03/2008 - 13/03/2016)   ",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 974,
    nombreOficial: "Juzgado Sexto de Distrito en el Estado de San Luis Potosí",
    catCircuitoId: 9,
    catEstadoId: 24,
    catCiudadId: 22,
  },
  {
    catOrganismoId: 979,
    nombreOficial:
      "Juzgado Tercero de Distrito del Centro Auxiliar de la Segunda Región (16/04/2008 - 31/10/2018)   ",
    catCircuitoId: 33,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 980,
    nombreOficial:
      "Juzgado Cuarto de Distrito del Centro Auxiliar de la Segunda Región (16/04/2008 - 31/05/2015)   ",
    catCircuitoId: 33,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 981,
    nombreOficial:
      "Juzgado Quinto de Distrito del Centro Auxiliar de la Segunda Región (16/04/2008 - 27/01/2012)   ",
    catCircuitoId: 33,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 982,
    nombreOficial:
      "Juzgado Sexto de Distrito del Centro Auxiliar de la Segunda Región (16/04/2008 - 31/10/2018)   ",
    catCircuitoId: 33,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 983,
    nombreOficial:
      "Primer Tribunal Colegiado de Circuito del Centro Auxiliar de la Segunda Región",
    catCircuitoId: 33,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 984,
    nombreOficial:
      "Segundo Tribunal Colegiado de Circuito del Centro Auxiliar de la Segunda Región",
    catCircuitoId: 33,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 985,
    nombreOficial:
      "Tercer Tribunal Colegiado de Circuito del Centro Auxiliar de la Segunda Región",
    catCircuitoId: 33,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 986,
    nombreOficial:
      "Cuarto Tribunal Colegiado de Circuito del Centro Auxiliar de la Segunda Región (16/11/2008 - 29/11/2013)   ",
    catCircuitoId: 33,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 989,
    nombreOficial: "Segundo Tribunal Unitario del Sexto Circuito",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 990,
    nombreOficial:
      "Juzgado Séptimo de Distrito del Centro Auxiliar de la Segunda Región (16/05/2008 - 31/08/2014)   ",
    catCircuitoId: 33,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 991,
    nombreOficial:
      "Juzgado Octavo de Distrito del Centro Auxiliar de la Segunda Región (16/05/2008 - 31/08/2014)   ",
    catCircuitoId: 33,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 992,
    nombreOficial:
      "Juzgado Noveno de Distrito del Centro Auxiliar de la Segunda Región (16/05/2008 - 20/04/2012)   ",
    catCircuitoId: 33,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 996,
    nombreOficial:
      "Primer Tribunal Colegiado de Circuito del Centro Auxiliar de la Tercera Región (16/08/2008 - 15/11/2013)   ",
    catCircuitoId: 35,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 997,
    nombreOficial:
      "Segundo Tribunal Colegiado de Circuito del Centro Auxiliar de la Tercera Región (16/08/2008 - 15/02/2013)   ",
    catCircuitoId: 35,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 998,
    nombreOficial:
      "Primer Tribunal Unitario de Circuito del Centro Auxiliar de la Tercera Región (16/08/2008 - 30/11/2016)   ",
    catCircuitoId: 35,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 999,
    nombreOficial:
      "Segundo Tribunal Unitario de Circuito del Centro Auxiliar de la Tercera Región (16/08/2008 - 30/11/2016)   ",
    catCircuitoId: 35,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1000,
    nombreOficial: "Juzgado Octavo de Distrito en el Estado de Guanajuato",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 43,
  },
  {
    catOrganismoId: 1001,
    nombreOficial:
      "Juzgado Primero de Distrito del Centro Auxiliar de la Tercera Región",
    catCircuitoId: 35,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1002,
    nombreOficial:
      "Juzgado Segundo de Distrito del Centro Auxiliar de la Tercera Región",
    catCircuitoId: 35,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1003,
    nombreOficial:
      "Juzgado Tercero de Distrito del Centro Auxiliar de la Tercera Región (16/08/2008 - 06/05/2012)   ",
    catCircuitoId: 35,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1004,
    nombreOficial:
      "Primer Tribunal Colegiado de Circuito del Centro Auxiliar de la Cuarta Región",
    catCircuitoId: 31,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 1005,
    nombreOficial:
      "Juzgado Primero de Distrito del Centro Auxiliar de la Cuarta Región (16/08/2008 - 31/03/2016)   ",
    catCircuitoId: 31,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 1006,
    nombreOficial:
      "Juzgado Segundo de Distrito del Centro Auxiliar de la Cuarta Región (16/08/2008 - 31/03/2016)   ",
    catCircuitoId: 31,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 1007,
    nombreOficial:
      "Juzgado Tercero de Distrito del Centro Auxiliar de la Cuarta Región",
    catCircuitoId: 31,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 1008,
    nombreOficial:
      "Juzgado Cuarto de Distrito del Centro Auxiliar de la Cuarta Región (16/08/2008 - 15/01/2019)   ",
    catCircuitoId: 31,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 1009,
    nombreOficial: "Quinto Tribunal Colegiado del Décimo Quinto Circuito",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 1010,
    nombreOficial:
      "Tribunal Unitario del Trigésimo Circuito (16/8/2008) - (15/12/2022)   ",
    catCircuitoId: 30,
    catEstadoId: 1,
    catCiudadId: 60,
  },
  {
    catOrganismoId: 1012,
    nombreOficial:
      "Segundo Tribunal Colegiado de Circuito del Centro Auxiliar de la Cuarta Región",
    catCircuitoId: 31,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 1014,
    nombreOficial: "Juzgado Decimosegundo de Distrito en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 3,
  },
  {
    catOrganismoId: 1015,
    nombreOficial:
      "Tercer Tribunal Unitario del  Decimosexto Circuito (16/8/2008) - (30/11/2022)   ",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1016,
    nombreOficial:
      "Juzgado Quinto de Distrito del Centro Auxiliar de la Cuarta Región (16/08/2008 - 15/01/2019)   ",
    catCircuitoId: 31,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 1020,
    nombreOficial:
      "Juzgado Tercero Federal Penal Especializado en Cateos, Arraigos e Intervención de Comunicaciones con Competencia en toda la República  y Residencia en la Ciudad de México (05/01/2009 - 16/05/2017)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1021,
    nombreOficial:
      "Juzgado Cuarto Federal Penal Especializado en Cateos, Arraigos e Intervención de Comunicaciones con Competencia en toda la República y Residencia en la Ciudad de México (05/01/2009 - 16/05/2017)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1022,
    nombreOficial:
      "Juzgado Quinto Federal Penal Especializado en Cateos, Arraigos e Intervención de Comunicaciones con Competencia en toda la República y Residencia en la Ciudad de México (05/01/2009 - 16/05/2017)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1023,
    nombreOficial:
      "Juzgado Sexto Federal Penal Especializado en Cateos, Arraigos e Intervención de Comunicaciones con Competencia en toda la República y Residencia en la Ciudad de México (05/01/2009 - 16/05/2017)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1024,
    nombreOficial:
      "Juzgado Primero de Distrito del Centro Auxiliar de la Séptima Región",
    catCircuitoId: 36,
    catEstadoId: 13,
    catCiudadId: 55,
  },
  {
    catOrganismoId: 1025,
    nombreOficial:
      "Juzgado Segundo de Distrito del Centro Auxiliar de la Séptima Región (16/11/2008 - 14/11/2018)   ",
    catCircuitoId: 36,
    catEstadoId: 13,
    catCiudadId: 55,
  },
  {
    catOrganismoId: 1026,
    nombreOficial:
      "Juzgado Tercero de Distrito del Centro Auxiliar de la Séptima Región (16/11/2008 - 14/11/2018)   ",
    catCircuitoId: 36,
    catEstadoId: 13,
    catCiudadId: 55,
  },
  {
    catOrganismoId: 1027,
    nombreOficial:
      "Juzgado Primero de Distrito del Centro Auxiliar de la Quinta Región, con residencia en Culiacán, Sinaloa",
    catCircuitoId: 32,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 1028,
    nombreOficial:
      "Juzgado Segundo de Distrito del Centro Auxiliar de la Quinta Región, con residencia en Culiacán, Sinaloa",
    catCircuitoId: 32,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 1029,
    nombreOficial:
      "Juzgado Tercero de Distrito del Centro Auxiliar de la Quinta Región, con residencia en Culiacán, Sinaloa",
    catCircuitoId: 32,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 1030,
    nombreOficial:
      "Juzgado Cuarto de Distrito del Centro Auxiliar de la Quinta Región, con residencia en Culiacán, Sinaloa",
    catCircuitoId: 32,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 1031,
    nombreOficial:
      "Juzgado Quinto de Distrito del Centro Auxiliar de la Quinta Región, con residencia en Culiacán, Sinaloa",
    catCircuitoId: 32,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 1032,
    nombreOficial:
      "Primer Tribunal Colegiado del Centro Auxiliar de la Quinta Región",
    catCircuitoId: 32,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 1033,
    nombreOficial:
      "Primer Tribunal Unitario del Centro Auxiliar de la Quinta Región",
    catCircuitoId: 32,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 1034,
    nombreOficial: "Tercer Tribunal Unitario del Decimosegundo Circuito",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 1035,
    nombreOficial:
      "Tercer Tribunal Unitario de Circuito del Centro Auxiliar de la Tercera Región (16/11/2008 - 30/11/2016)   ",
    catCircuitoId: 35,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1036,
    nombreOficial:
      "Cuarto Tribunal Unitario de Circuito del Centro Auxiliar de la Tercera Región",
    catCircuitoId: 35,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1037,
    nombreOficial:
      "Tribunal Colegiado de Circuito del Centro Auxiliar de la Sexta Región (16/11/2008 - 28/02/2014)   ",
    catCircuitoId: 34,
    catEstadoId: 6,
    catCiudadId: 44,
  },
  {
    catOrganismoId: 1038,
    nombreOficial:
      "Tribunal Unitario del Centro Auxiliar de la Sexta Región (16/11/2008 - 15/12/2018)   ",
    catCircuitoId: 34,
    catEstadoId: 6,
    catCiudadId: 44,
  },
  {
    catOrganismoId: 1039,
    nombreOficial:
      "Quinto Tribunal Colegiado de Circuito del Centro Auxiliar de la Tercera Región, con Residencia en Morelia, Michoacán (16/11/2008 - 31/10/2014)   ",
    catCircuitoId: 116,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 1040,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Civil del Decimoprimer Circuito",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 1041,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Administrativa y de Trabajo del Decimoprimer Circuito",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 1042,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Administrativa y de Trabajo del Decimoprimer Circuito",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 1043,
    nombreOficial:
      "Tribunal Colegiado en Materia Penal del Decimoprimer Circuito",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 1044,
    nombreOficial:
      "Tribunal Unitario del Trigésimo Primer Circuito (16/2/2009) - (15/12/2022)   ",
    catCircuitoId: 47,
    catEstadoId: 4,
    catCiudadId: 35,
  },
  {
    catOrganismoId: 1045,
    nombreOficial: "Tribunal Colegiado del Trigésimo Primer Circuito",
    catCircuitoId: 47,
    catEstadoId: 4,
    catCiudadId: 35,
  },
  {
    catOrganismoId: 1057,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Civil y Administrativa del Decimotercer Circuito, con residencia en San Bartolo Coyotepec, Oaxaca",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 32,
  },
  {
    catOrganismoId: 1058,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Penal y de Trabajo del Decimotercer Circuito, con residencia en San Bartolo Coyotepec, Oaxaca",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 80,
  },
  {
    catOrganismoId: 1059,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Penal y de Trabajo del Decimotercer Circuito, con residencia en San Bartolo Coyotepec, Oaxaca",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 80,
  },
  {
    catOrganismoId: 1062,
    nombreOficial:
      "Tribunal Colegiado en Materia Civil del Décimo Circuito, con residencia en Villahermosa, Tabasco",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 1063,
    nombreOficial:
      "Tribunal Colegiado en Materia Penal del Décimo Circuito, con residencia en Villahermosa, Tabasco",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 1064,
    nombreOficial:
      "Tribunal Colegiado en Materia Administrativa del Décimo Circuito, con residencia en Villa Hermosa, Tabasco",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 1065,
    nombreOficial:
      "Juzgado Séptimo Federal Penal Especializado en Cateos, Arraigos e Intervención de Comunicaciones con Competencia en toda la República y Residencia en la Ciudad de México (16/06/2009 - 20/06/2016)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1072,
    nombreOficial:
      "Juzgado Tercero de Distrito del Centro Auxiliar de la Primera Región",
    catCircuitoId: 57,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1073,
    nombreOficial:
      "Juzgado Cuarto de Distrito del Centro Auxiliar de la Primera Región (01/11/2009 - 08/08/2013)   ",
    catCircuitoId: 57,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1074,
    nombreOficial:
      "Juzgado Quinto de Distrito del Centro Auxiliar de la Primera Región (01/11/2009 - 08/08/2013)   ",
    catCircuitoId: 57,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1075,
    nombreOficial:
      "Primer Tribunal Colegiado de Circuito del Centro Auxiliar de la Primera Región",
    catCircuitoId: 57,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1076,
    nombreOficial:
      "Segundo Tribunal Colegiado de Circuito del Centro Auxiliar de la Primera Región (16/11/2009 - 08/08/2013)   ",
    catCircuitoId: 57,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1080,
    nombreOficial: "Segundo Tribunal Colegiado del Vigésimo Cuarto Circuito",
    catCircuitoId: 48,
    catEstadoId: 18,
    catCiudadId: 30,
  },
  {
    catOrganismoId: 1081,
    nombreOficial:
      "Juzgado de Distrito del Centro Auxiliar de la Sexta Región (01/08/2009 - 15/12/2018)   ",
    catCircuitoId: 34,
    catEstadoId: 6,
    catCiudadId: 44,
  },
  {
    catOrganismoId: 1082,
    nombreOficial:
      "Juzgado Sexto de Distrito en Materia Penal en el Estado de Nuevo León",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 1083,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia Penal en el Estado de Nayarit",
    catCircuitoId: 48,
    catEstadoId: 18,
    catCiudadId: 30,
  },
  {
    catOrganismoId: 1084,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia Penal en el Estado de Nayarit",
    catCircuitoId: 48,
    catEstadoId: 18,
    catCiudadId: 30,
  },
  {
    catOrganismoId: 1085,
    nombreOficial:
      "Cuarto Tribunal Colegiado en Materia Administrativa del Segundo Circuito",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 5,
  },
  {
    catOrganismoId: 1088,
    nombreOficial:
      "Tribunal Unitario del Trigésimo Segundo Circuito (16/10/2009) - (30/11/2022)   ",
    catCircuitoId: 109,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 1089,
    nombreOficial: "Tribunal Colegiado del Trigésimo Segundo Circuito",
    catCircuitoId: 109,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 1092,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia de Amparo Civil, Administrativo y de Trabajo y de Juicios Federales en el Estado de Nayarit",
    catCircuitoId: 48,
    catEstadoId: 18,
    catCiudadId: 30,
  },
  {
    catOrganismoId: 1093,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia de Amparo Civil, Administrativo y de Trabajo y de Juicios Federales en el Estado de Nayarit",
    catCircuitoId: 48,
    catEstadoId: 18,
    catCiudadId: 30,
  },
  {
    catOrganismoId: 1094,
    nombreOficial:
      "Quinto Tribunal Unitario de Circuito del Centro Auxiliar de la Tercera Región (01/10/2009 - 31/08/2014)   ",
    catCircuitoId: 35,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1095,
    nombreOficial:
      "Segundo Tribunal Colegiado de Circuito del Centro Auxiliar de la Quinta Región",
    catCircuitoId: 32,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 1096,
    nombreOficial:
      "Juzgado Sexto de Distrito del Centro Auxiliar de la Quinta Región, con residencia en Culiacán, Sinaloa (01/10/2009 - 30/09/2015)   ",
    catCircuitoId: 32,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 1097,
    nombreOficial:
      "Juzgado Séptimo de Distrito del Centro Auxiliar de la Quinta Región, con residencia en Culiacán, Sinaloa (01/10/2009 - 30/09/2015)   ",
    catCircuitoId: 32,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 1098,
    nombreOficial:
      "Tribunal Colegiado de Circuito del Centro Auxiliar de la Séptima Región",
    catCircuitoId: 36,
    catEstadoId: 13,
    catCiudadId: 55,
  },
  {
    catOrganismoId: 1099,
    nombreOficial:
      "Tribunal Unitario de Circuito del Centro Auxiliar de la Séptima Región",
    catCircuitoId: 36,
    catEstadoId: 13,
    catCiudadId: 55,
  },
  {
    catOrganismoId: 1100,
    nombreOficial:
      "Séptimo Tribunal Colegiado de Circuito del Centro Auxiliar de la Primera Región, con Residencia en Naucalpan de Juárez, Estado de México",
    catCircuitoId: 113,
    catEstadoId: 11,
    catCiudadId: 5,
  },
  {
    catOrganismoId: 1101,
    nombreOficial:
      "Octavo Tribunal Colegiado de Circuito del Centro Auxiliar de la Primera Región, con Residencia en Naucalpan de Juárez, Estado de México",
    catCircuitoId: 113,
    catEstadoId: 11,
    catCiudadId: 5,
  },
  {
    catOrganismoId: 1103,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia de Trabajo del Segundo Circuito",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 1106,
    nombreOficial:
      "Séptimo Tribunal Unitario de Circuito del Centro Auxiliar de la Tercera Región, con residencia en Guadalajara, Jalisco (16/10/2009 - 31/08/2017)   ",
    catCircuitoId: 115,
    catEstadoId: 15,
    catCiudadId: 6,
  },
  {
    catOrganismoId: 1107,
    nombreOficial:
      "Juzgado Cuarto de Distrito del Centro Auxiliar de la Tercera Región, con residencia en Guadalajara, Jalisco (16/10/2009 - 15/10/2012)   ",
    catCircuitoId: 115,
    catEstadoId: 15,
    catCiudadId: 6,
  },
  {
    catOrganismoId: 1108,
    nombreOficial:
      "Juzgado Quinto de Distrito del Centro Auxiliar de la Tercera Región, con residencia en Guadalajara, Jalisco (16/10/2009 - 15/10/2012)   ",
    catCircuitoId: 115,
    catEstadoId: 15,
    catCiudadId: 6,
  },
  {
    catOrganismoId: 1122,
    nombreOficial:
      "Juzgado Primero de Distrito de Procesos Penales Federales en el Estado de Tamaulipas",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 48,
  },
  {
    catOrganismoId: 1123,
    nombreOficial:
      "Juzgado Segundo de Distrito de Procesos Penales Federales en el Estado de Tamaulipas",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 48,
  },
  {
    catOrganismoId: 1124,
    nombreOficial:
      "Juzgado de Distrito en Materias de Amparo y Juicios Federales en el Estado de Tamaulipas",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 48,
  },
  {
    catOrganismoId: 1125,
    nombreOficial:
      "Juzgado Primero de Distrito del Centro Auxiliar de la Novena Región",
    catCircuitoId: 111,
    catEstadoId: 32,
    catCiudadId: 59,
  },
  {
    catOrganismoId: 1126,
    nombreOficial:
      "Juzgado Segundo de Distrito del Centro Auxiliar de la Novena Región",
    catCircuitoId: 111,
    catEstadoId: 32,
    catCiudadId: 59,
  },
  {
    catOrganismoId: 1127,
    nombreOficial:
      "Juzgado Tercero de Distrito del Centro Auxiliar de la Novena Región",
    catCircuitoId: 111,
    catEstadoId: 32,
    catCiudadId: 59,
  },
  {
    catOrganismoId: 1128,
    nombreOficial:
      "Juzgado Cuarto de Distrito del Centro Auxiliar de la Novena Región",
    catCircuitoId: 111,
    catEstadoId: 32,
    catCiudadId: 59,
  },
  {
    catOrganismoId: 1129,
    nombreOficial:
      "Juzgado Quinto de Distrito del Centro Auxiliar de la Novena Región (01/11/2009 - 15/08/2015)   ",
    catCircuitoId: 111,
    catEstadoId: 32,
    catCiudadId: 59,
  },
  {
    catOrganismoId: 1130,
    nombreOficial:
      "Juzgado Sexto de Distrito del Centro Auxiliar de la Novena Región (01/11/2009 - 31/08/2014)   ",
    catCircuitoId: 111,
    catEstadoId: 32,
    catCiudadId: 59,
  },
  {
    catOrganismoId: 1131,
    nombreOficial:
      "Juzgado Séptimo de Distrito del Centro Auxiliar de la Novena Región (01/11/2009 - 31/08/2014)   ",
    catCircuitoId: 111,
    catEstadoId: 32,
    catCiudadId: 59,
  },
  {
    catOrganismoId: 1132,
    nombreOficial:
      "Primer Tribunal Colegiado de Circuito del Centro Auxiliar de la Novena Región (01/11/2009 - 31/10/2019)   ",
    catCircuitoId: 111,
    catEstadoId: 32,
    catCiudadId: 59,
  },
  {
    catOrganismoId: 1135,
    nombreOficial:
      "Juzgado Primero de Distrito del Centro Auxiliar de la Décima Región",
    catCircuitoId: 108,
    catEstadoId: 7,
    catCiudadId: 17,
  },
  {
    catOrganismoId: 1136,
    nombreOficial:
      "Juzgado Segundo de Distrito del Centro Auxiliar de la Décima Región",
    catCircuitoId: 108,
    catEstadoId: 7,
    catCiudadId: 17,
  },
  {
    catOrganismoId: 1138,
    nombreOficial:
      "Primer Tribunal Colegiado de Circuito del Centro Auxiliar de la Décima Región",
    catCircuitoId: 108,
    catEstadoId: 7,
    catCiudadId: 17,
  },
  {
    catOrganismoId: 1139,
    nombreOficial:
      "Segundo Tribunal Colegiado de Circuito del Centro Auxiliar de la Décima Región",
    catCircuitoId: 108,
    catEstadoId: 7,
    catCiudadId: 17,
  },
  {
    catOrganismoId: 1140,
    nombreOficial:
      "Juzgado Primero de Distrito del Centro Auxiliar de la Octava Región (01/11/2009 - 17/12/2013)   ",
    catCircuitoId: 110,
    catEstadoId: 23,
    catCiudadId: 36,
  },
  {
    catOrganismoId: 1141,
    nombreOficial:
      "Juzgado Segundo de Distrito del Centro Auxiliar de la Octava Región (01/11/2009 - 16/03/2012)   ",
    catCircuitoId: 110,
    catEstadoId: 23,
    catCiudadId: 36,
  },
  {
    catOrganismoId: 1143,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Penal y Administrativa del Octavo Circuito",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 18,
  },
  {
    catOrganismoId: 1144,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Civil y de Trabajo del Octavo Circuito",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 18,
  },
  {
    catOrganismoId: 1145,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Penal y Administrativa del Octavo Circuito",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 18,
  },
  {
    catOrganismoId: 1146,
    nombreOficial:
      "Tribunal Colegiado en Materias Penal y de Trabajo del Octavo Circuito",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 17,
  },
  {
    catOrganismoId: 1147,
    nombreOficial:
      "Tribunal Colegiado en Materias Administrativa y Civil del Octavo Circuito",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 17,
  },
  {
    catOrganismoId: 1148,
    nombreOficial:
      "Décimo Octavo Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1150,
    nombreOficial:
      "Primer Tribunal Colegiado del Segundo Circuito, con residencia en Nezahualcóyotl, Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 3,
  },
  {
    catOrganismoId: 1153,
    nombreOficial:
      "Juzgado Tercero de Distrito del Centro Auxiliar de la Primera Región, con residencia en la Ciudad de México, con jurisdicción en la República Mexicana, especializado en materia de Extinción de Dominio",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1154,
    nombreOficial:
      "Juzgado de Distrito del Complejo Penitenciario Islas Marías y Auxiliar en toda la República (01/07/2010 - 28/02/2013)   ",
    catCircuitoId: 110,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 1155,
    nombreOficial:
      "Juzgado de Distrito del Complejo Penitenciario Islas Marías y Auxiliar en toda la República, en su carácter de Ordinario (01/07/2010 - 28/02/2013)   ",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 1159,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materias Civil y de Trabajo del Decimosexto Circuito (01/10/2010 - 15/11/2013)   ",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1160,
    nombreOficial:
      "Tercer Tribunal Colegiado de Circuito del Centro Auxiliar de la Primera Región (01/10/2010 - 08/08/2013)   ",
    catCircuitoId: 57,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1161,
    nombreOficial:
      "Cuarto Tribunal Colegiado de Circuito del Centro Auxiliar de la Primera Región",
    catCircuitoId: 57,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1162,
    nombreOficial:
      "Primer Tribunal Colegiado del Décimo Circuito, con residencia en Coatzacoalcos, Veracruz",
    catCircuitoId: 10,
    catEstadoId: 30,
    catCiudadId: 24,
  },
  {
    catOrganismoId: 1163,
    nombreOficial:
      "Sexto Tribunal Colegiado de Circuito del Centro Auxiliar de la Tercera Región, con Residencia en Morelia, Michoacán (16/10/2010 - 31/10/2017)   ",
    catCircuitoId: 116,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 1165,
    nombreOficial:
      "Segundo Tribunal Unitario de Circuito del Centro Auxiliar de la Quinta Región",
    catCircuitoId: 32,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 1166,
    nombreOficial:
      "Cuarto Tribunal Colegiado del Decimoctavo Circuito (01/11/2010 - 29/02/2016)   ",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 1168,
    nombreOficial:
      "Sexto Tribunal Colegiado de Circuito del Centro Auxiliar de la Primera Región, con residencia en Cuernavaca, Morelos (01/11/2010 - 31/03/2018)   ",
    catCircuitoId: 114,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 1173,
    nombreOficial: "Juzgado Sexto de Distrito en el Estado de Morelos",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 1174,
    nombreOficial:
      "Juzgado Sexto de Distrito del Centro Auxiliar de la Primera Región, con residencia en Cuernavaca, Morelos (01/12/2010 - 31/03/2018)   ",
    catCircuitoId: 114,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 1175,
    nombreOficial:
      "Juzgado Séptimo de Distrito del Centro Auxiliar de la Primera Región, con residencia en Cuernavaca, Morelos (01/12/2010 - 09/08/2012)   ",
    catCircuitoId: 114,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 1176,
    nombreOficial:
      "Primer Tribunal Colegiado de Circuito del Centro Auxiliar de la Octava Región (01/11/2010 - 16/11/2013)   ",
    catCircuitoId: 110,
    catEstadoId: 23,
    catCiudadId: 36,
  },
  {
    catOrganismoId: 1177,
    nombreOficial:
      "Segundo Tribunal Colegiado de Circuito del Centro Auxiliar de la Octava Región",
    catCircuitoId: 110,
    catEstadoId: 31,
    catCiudadId: 34,
  },
  {
    catOrganismoId: 1178,
    nombreOficial:
      "Juzgado Tercero de Distrito en Materia Penal en el Estado de Nayarit",
    catCircuitoId: 48,
    catEstadoId: 18,
    catCiudadId: 30,
  },
  {
    catOrganismoId: 1182,
    nombreOficial: "Juzgado Quinto de Distrito en el Estado de Quintana Roo",
    catCircuitoId: 54,
    catEstadoId: 23,
    catCiudadId: 36,
  },
  {
    catOrganismoId: 1183,
    nombreOficial:
      "Segundo Tribunal Unitario del Vigésimo Cuarto Circuito (1/12/2010) - (30/11/2022)   ",
    catCircuitoId: 48,
    catEstadoId: 18,
    catCiudadId: 30,
  },
  {
    catOrganismoId: 1185,
    nombreOficial:
      "Juzgado Tercero de Distrito en el Estado de Baja California Sur",
    catCircuitoId: 55,
    catEstadoId: 3,
    catCiudadId: 31,
  },
  {
    catOrganismoId: 1186,
    nombreOficial:
      "Tribunal Unitario del Centro Auxiliar de la Cuarta Región, con Residencia en Xalapa Veracruz (16/06/2011 - 06/08/2012)   ",
    catCircuitoId: 31,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 1187,
    nombreOficial:
      "Juzgado Decimoquinto de Distrito en Materias Administrativa, Civil y de Trabajo en el Estado de Jalisco, con residencia en Zapopan.   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 1188,
    nombreOficial:
      "Juzgado Decimoprimero de Distrito en el Estado de Puebla (01/06/2011 - 31/05/2015)   ",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 1189,
    nombreOficial:
      "Quinto Tribunal Unitario del Tercer Circuito (1/3/2012) - (30/11/2022)   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 1190,
    nombreOficial:
      "Séptimo Tribunal Colegiado de Circuito del Centro Auxiliar de la Tercera Región con Jurisdicción en toda la República y Competencia Mixta (16/10/2012 - 31/03/2018)   ",
    catCircuitoId: 115,
    catEstadoId: 15,
    catCiudadId: 6,
  },
  {
    catOrganismoId: 1192,
    nombreOficial:
      "Tercer Tribunal Colegiado de Circuito del Centro Auxiliar de la Décima Región con Jurisdicción en toda la República y Competencia Mixta (01/11/2011-15/12/2023)   ",
    catCircuitoId: 108,
    catEstadoId: 7,
    catCiudadId: 17,
  },
  {
    catOrganismoId: 1193,
    nombreOficial:
      "Cuarto Tribunal Colegiado de Circuito del Centro Auxiliar de la Décima Región con Jurisdicción en toda la República y Competencia Mixta",
    catCircuitoId: 108,
    catEstadoId: 7,
    catCiudadId: 17,
  },
  {
    catOrganismoId: 1194,
    nombreOficial:
      "Juzgado Primero de Distrito Especializado en Ejecución de Penas",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1195,
    nombreOficial:
      "Juzgado Segundo de Distrito Especializado en Ejecución de Penas",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1196,
    nombreOficial:
      "Juzgado Tercero de Distrito Especializado en Ejecución de Penas",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1200,
    nombreOficial: "Organo de Pruebas - Juzgado de Distrito",
    catCircuitoId: 109,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 1201,
    nombreOficial: "Organo de Pruebas - Tribunal Unitario de Circuito",
    catCircuitoId: 109,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 1202,
    nombreOficial: "Organo de Pruebas - Tribunal Colegiado de Circuito",
    catCircuitoId: 109,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 1203,
    nombreOficial: "Organo de Pruebas - Juzgado de Distrito Auxiliar",
    catCircuitoId: 35,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 1204,
    nombreOficial: "Organo de Pruebas - Tribunal Colegiado Auxiliar",
    catCircuitoId: 35,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 1205,
    nombreOficial: "Organo de Pruebas - Tribunal Unitario Auxiliar",
    catCircuitoId: 35,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 1206,
    nombreOficial:
      "Juzgado Cuarto de Distrito del Centro Auxiliar de la Séptima Región",
    catCircuitoId: 36,
    catEstadoId: 13,
    catCiudadId: 55,
  },
  {
    catOrganismoId: 1207,
    nombreOficial:
      "Juzgado Décimo de Distrito del Centro Auxiliar de la Segunda Región (16/09/2011 - 12/12/2012)   ",
    catCircuitoId: 33,
    catEstadoId: 21,
    catCiudadId: 12,
  },
  {
    catOrganismoId: 1208,
    nombreOficial:
      "Cuarto Tribunal Colegiado en Materias Penal y de Trabajo del Séptimo Circuito (01/10/2011 - 30/11/2014)   ",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 1209,
    nombreOficial:
      "Juzgado Primero de Distrito del Centro Auxiliar de la Decimoprimera Región, con residencia en Coatzacoalcos, Veracruz (01/10/2011 - 15/06/2018)   ",
    catCircuitoId: 112,
    catEstadoId: 30,
    catCiudadId: 24,
  },
  {
    catOrganismoId: 1210,
    nombreOficial:
      "Juzgado Segundo de Distrito del Centro Auxiliar de la Decimoprimera Región, con residencia en Coatzacoalcos, Veracruz (01/10/2011 - 15/06/2018)   ",
    catCircuitoId: 112,
    catEstadoId: 30,
    catCiudadId: 24,
  },
  {
    catOrganismoId: 1212,
    nombreOficial:
      "Segundo Tribunal Colegiado de Circuito del Centro Auxiliar de la Decimoprimera Región, con residencia en Coatzacoalcos, Veracruz (01/12/2011 - 30/11/2018)   ",
    catCircuitoId: 112,
    catEstadoId: 30,
    catCiudadId: 24,
  },
  {
    catOrganismoId: 1215,
    nombreOficial:
      "Juzgado Noveno de Distrito en el Estado de Guerrero, con residencia en Iguala",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 56,
  },
  {
    catOrganismoId: 1216,
    nombreOficial:
      "Juzgado Séptimo de Distrito en el Estado de San Luis Potosí",
    catCircuitoId: 9,
    catEstadoId: 24,
    catCiudadId: 62,
  },
  {
    catOrganismoId: 1217,
    nombreOficial:
      "Juzgado Tercero de Distrito del Centro Auxiliar de la Décima Región",
    catCircuitoId: 108,
    catEstadoId: 7,
    catCiudadId: 17,
  },
  {
    catOrganismoId: 1218,
    nombreOficial:
      "Juzgado Cuarto de Distrito del Centro Auxiliar de la Décima Región (16/11/2011 - 25/05/2012)   ",
    catCircuitoId: 108,
    catEstadoId: 7,
    catCiudadId: 17,
  },
  {
    catOrganismoId: 1219,
    nombreOficial:
      "Tercer Tribunal Colegiado de Circuito del Centro Auxiliar de la Quinta Región",
    catCircuitoId: 32,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 1220,
    nombreOficial:
      "Cuarto Tribunal Colegiado de Circuito del Centro Auxiliar de la Quinta Región",
    catCircuitoId: 32,
    catEstadoId: 25,
    catCiudadId: 29,
  },
  {
    catOrganismoId: 1221,
    nombreOficial:
      "Quinto Tribunal Colegiado de Circuito del Centro Auxiliar de la Quinta Región",
    catCircuitoId: 32,
    catEstadoId: 3,
    catCiudadId: 31,
  },
  {
    catOrganismoId: 1222,
    nombreOficial:
      "Juzgado Sexto de Distrito del Centro Auxiliar de la Tercera Región, con residencia en Uruapan, Michoacán",
    catCircuitoId: 116,
    catEstadoId: 16,
    catCiudadId: 26,
  },
  {
    catOrganismoId: 1224,
    nombreOficial:
      "Juzgado Primero de Distrito de Procesos Penales Federales en el Estado de Jalisco, con residencia en Puente Grande, Municipio de Juanacatlán",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 1225,
    nombreOficial:
      "Juzgado Segundo de Distrito de Procesos Penales Federales en el Estado de Jalisco, con residencia en Puente Grande, Municipio de Juanacatlán",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 1226,
    nombreOficial:
      "Juzgado Tercero de Distrito de Procesos Penales Federales en el Estado de Jalisco, con residencia en Puente Grande, Municipio de Juanacatlán",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 1227,
    nombreOficial:
      "Juzgado Cuarto de Distrito de Procesos Penales Federales en el Estado de Jalisco, con residencia en Puente Grande, Municipio de Juanacatlán",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 1228,
    nombreOficial:
      "Juzgado Quinto de Distrito de Procesos Penales Federales en el Estado de Jalisco, con residencia en Puente Grande (16/10/2012 - 31/01/2018)   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 1229,
    nombreOficial:
      "Juzgado Sexto de Distrito de Procesos Penales Federales en el Estado de Jalisco, con residencia en Puente Grande, Municipio de Juanacatlán (16/10/2012 - 11/11/2018)   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 1231,
    nombreOficial:
      "Juzgado Octavo de Distrito en Materia Penal en el Estado de Jalisco (02/04/2001 - 15/10/2012)   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 1232,
    nombreOficial:
      "Juzgado Noveno de Distrito en Materia Penal en el Estado de Jalisco (02/04/2001 - 15/10/2012)   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 1233,
    nombreOficial:
      "Juzgado Tercero de Distrito de Amparo en Materia Penal en el Estado de Jalisco, con residencia en Zapopan",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 1234,
    nombreOficial:
      "Juzgado Cuarto de Distrito de Amparo en Materia Penal en el Estado de Jalisco, con residencia en Zapopan",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 1235,
    nombreOficial:
      "Juzgado Quinto de Distrito en el Estado de Querétaro (01/12/2011 - 30/09/2014)   ",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 1236,
    nombreOficial: "Juzgado Sexto de Distrito en el Estado de Quintana Roo",
    catCircuitoId: 54,
    catEstadoId: 23,
    catCiudadId: 37,
  },
  {
    catOrganismoId: 1237,
    nombreOficial: "Octavo Tribunal Unitario del Decimoquinto Circuito",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 1238,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia Mercantil Federal en el Estado de Puebla, Especializado en Juicios Orales",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 1239,
    nombreOficial:
      "Juzgado de Distrito en Materia Mercantil Federal en el Estado de Quintana Roo, Especializado en Juicios Orales",
    catCircuitoId: 54,
    catEstadoId: 23,
    catCiudadId: 36,
  },
  {
    catOrganismoId: 1241,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia de Trabajo del Tercer Circuito",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 1242,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Tlaxcala",
    catCircuitoId: 50,
    catEstadoId: 29,
    catCiudadId: 75,
  },
  {
    catOrganismoId: 1243,
    nombreOficial: "Juzgado Noveno de Distrito en el Estado de Guanajuato",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 71,
  },
  {
    catOrganismoId: 1245,
    nombreOficial: "Juzgado Décimo de Distrito en el Estado de Guanajuato",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 71,
  },
  {
    catOrganismoId: 1246,
    nombreOficial:
      "Juzgado Quinto de Distrito en el Estado de Coahuila de Zaragoza, con residencia en Monclova",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 20,
  },
  {
    catOrganismoId: 1248,
    nombreOficial:
      "Cuarto Tribunal Colegiado del Vigésimo Circuito (16/06/2012 - 15/11/2015)   ",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 1250,
    nombreOficial:
      "Juzgado Sexto de Distrito en Materias Administrativa, Civil y de Trabajo en el Estado de Jalisco, con residencia en Zapopan.   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 1253,
    nombreOficial:
      "Juzgado Séptimo de Distrito en el Estado de Morelos, con residencia en Cuernavaca",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 1254,
    nombreOficial:
      "Tercer Tribunal Unitario del Séptimo Circuito, con residencia en Xalapa, Veracruz (16/8/2012) - (15/12/2022)   ",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 1255,
    nombreOficial:
      "Tribunal Colegiado en Materias de Trabajo y Administrativa en el Cuarto Circuito, con residencia en Monterrey (16/10/2012 - 05/05/2014)   ",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 1257,
    nombreOficial:
      "Cuarto Tribunal Colegiado en Materia de Trabajo del Tercer Circuito, con residencia en Zapopan, Jalisco",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 1258,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia Penal del Tercer Circuito, con residencia en Zapopan, Jalisco",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 1260,
    nombreOficial: "Órgano de Pruebas JACG - Juzgado de Distrito",
    catCircuitoId: 109,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 1261,
    nombreOficial: "Órgano de Pruebas SiBAP - 1 Juzgado de Distrito",
    catCircuitoId: 109,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 1262,
    nombreOficial: "Órgano de Pruebas SiBAP  - 2 Juzgado de Distrito",
    catCircuitoId: 109,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 1263,
    nombreOficial:
      "Tercer Tribunal Unitario del Octavo Circuito, con residencia en Saltillo, Coahuila de Zaragoza (1/11/2012) - (30/11/2022)   ",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 17,
  },
  {
    catOrganismoId: 1265,
    nombreOficial: "Órgano de Pruebas SiBAP -  Tribunal Unitario",
    catCircuitoId: 109,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 1268,
    nombreOficial:
      "Juzgado Quinto de Distrito en Materia Civil y de Trabajo en el Estado de Nuevo León, con residencia en Monterrey",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 1269,
    nombreOficial: "Juzgado Decimotercero de Distrito en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 5,
  },
  {
    catOrganismoId: 1270,
    nombreOficial: "Juzgado Decimocuarto de Distrito en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 5,
  },
  {
    catOrganismoId: 1271,
    nombreOficial:
      "Segundo Tribunal Colegiado del Segundo Circuito, con residencia en Nezahualcóyotl, Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 3,
  },
  {
    catOrganismoId: 1273,
    nombreOficial:
      "Juzgado Cuarto de Distrito en el Estado de Hidalgo, con residencia en Pachuca",
    catCircuitoId: 49,
    catEstadoId: 14,
    catCiudadId: 58,
  },
  {
    catOrganismoId: 1274,
    nombreOficial:
      "Juzgado Octavo de Distrito del Centro Auxiliar de la Quinta Región, con residencia en Mazatlán, Sinaloa",
    catCircuitoId: 32,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 1278,
    nombreOficial:
      "Cuarto Tribunal Colegiado en Materias Civil y de Trabajo del Decimosexto Circuito (16/02/2013 - 15/11/2013)   ",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1280,
    nombreOficial:
      "Juzgado Octavo de Distrito del Centro Auxiliar de la Quinta Región, con residencia en Mazatlán, Sinaloa, en su carácter de Ordinario",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 1281,
    nombreOficial:
      "Juzgado Séptimo de Distrito del Centro Auxiliar de la Segunda Región, en su carácter de Ordinario (22/03/2013 - 31/08/2014)   ",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 1282,
    nombreOficial:
      "Juzgado Sexto de Distrito del Centro Auxiliar de la Quinta Región, con residencia en Culiacán, Sinaloa, en su carácter de Ordinario (01/03/2013 - 30/09/2015)   ",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 1283,
    nombreOficial:
      "Juzgado Séptimo de Distrito del Centro Auxiliar de la Quinta Región, con residencia en Culiacán, Sinaloa, en su carácter de Ordinario (01/03/2013 - 30/09/2015)   ",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 1287,
    nombreOficial:
      "Juzgado Tercero de Distrito en Materia Mercantil, Especializado en Juicios de Cuantía Menor, con residencia en Zapopan, Jalisco",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 1294,
    nombreOficial:
      "Primer Tribunal Colegiado de Circuito del Centro Auxiliar de la Segunda Región, en su carácter de Ordinario",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 1295,
    nombreOficial:
      "Segundo Tribunal Colegiado de Circuito del Centro Auxiliar de la Segunda Región, en su carácter de Ordinario",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 1298,
    nombreOficial:
      "Quinto Tribunal Colegiado del Décimo Octavo Circuito (16/05/2013 - 29/02/2016)   ",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 1300,
    nombreOficial:
      "Juzgado Séptimo de Distrito en Materias Administrativa, Civil y de Trabajo en el Estado de Jalisco, con residencia en Zapopan.   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 1301,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia Administrativa, Especializado en Competencia Económica, Radiodifusión y Telecomunicaciones, con residencia en la Ciudad de México y Jurisdicción en toda la República",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1302,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia Administrativa, Especializado en Competencia Económica, Radiodifusión y Telecomunicaciones, con residencia en la Ciudad de México y Jurisdicción en toda la República",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1304,
    nombreOficial:
      "Primer Tribunal Colegiado de Circuito en Materia Administrativa, Especializado en Competencia Económica, Radiodifusión y Telecomunicaciones, con residencia en la Ciudad de México y Jurisdicción en toda la República",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1305,
    nombreOficial:
      "Segundo Tribunal Colegiado de Circuito en Materia Administrativa, Especializado en Competencia Económica, Radiodifusión y Telecomunicaciones, con residencia en la Ciudad de México y Jurisdicción en toda la República",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1308,
    nombreOficial:
      "Segundo Tribunal Unitario del Vigésimo Noveno Circuito (01/10/2013 - 15/08/2018)   ",
    catCircuitoId: 49,
    catEstadoId: 14,
    catCiudadId: 58,
  },
  {
    catOrganismoId: 1311,
    nombreOficial:
      "Tercer Tribunal Colegiado de Circuito del Centro Auxiliar de la Segunda Región, en su carácter de Ordinario",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 1312,
    nombreOficial:
      "Juzgado Quinto de Distrito de Amparo en Materia Penal en el Estado de Jalisco, con residencia en Puente Grande",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 1313,
    nombreOficial:
      "Juzgado Sexto de Distrito de Amparo en Materia Penal en el Estado de Jalisco, con residencia en Puente Grande",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 1314,
    nombreOficial:
      "Juzgado Decimoséptimo de Distrito en el Estado de Veracruz, con residencia en Xalapa",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 1316,
    nombreOficial:
      "Sexto Tribunal Unitario del Segundo Circuito (1/12/2013) - (15/12/2022)   ",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 1318,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia de Trabajo del Decimosexto Circuito, con residencia en la Ciudad de Guanajuato",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1319,
    nombreOficial:
      "Tercer Tribunal Colegiado del Vigésimo Séptimo Circuito, con residencia en Cancún, Quintana Roo",
    catCircuitoId: 54,
    catEstadoId: 23,
    catCiudadId: 36,
  },
  {
    catOrganismoId: 1320,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Administrativa del Decimosexto Circuito, con residencia en Guanajuato, Gto",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1321,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Administrativa del Decimosexto Circuito, con residencia en Guanajuato, Gto",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1323,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Civil del Decimosexto Circuito, con residencia en Guanajuato, Gto",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1324,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Civil del Decimosexto Circuito, con residencia en Guanajuato, Gto",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1325,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia Civil del Decimosexto Circuito, con residencia en Guanajuato, Gto",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1326,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia de Trabajo del Decimosexto Circuito, con residencia en Guanajuato, Gto",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1330,
    nombreOficial:
      "Juzgado Décimo de Distrito en el Estado de Guerrero, con residencia en Chilpancingo, Gro",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 54,
  },
  {
    catOrganismoId: 1331,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Civil y de Trabajo del Octavo Circuito, con residencia en Torreón, Coah.   ",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 18,
  },
  {
    catOrganismoId: 1332,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia de Trabajo del Sexto Circuito, con residencia en Puebla, Puebla",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 12,
  },
  {
    catOrganismoId: 1335,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia Penal del Sexto Circuito, con residencia en San Andrés Cholula, Puebla",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 1336,
    nombreOficial:
      "Juzgado Primero de Distrito de Procesos Penales Federales en el Estado de Baja California, con residencia en Tijuana",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 1337,
    nombreOficial:
      "Juzgado Segundo de Distrito de Procesos Penales Federales en el Estado de Baja California, con residencia en Tijuana",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 1338,
    nombreOficial:
      "Juzgado Tercero de Distrito de Procesos Penales Federales en el Estado de Baja California, con residencia en Tijuana",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 1341,
    nombreOficial:
      "Juzgado Sexto de Distrito de Procesos Penales Federales en el Estado de Baja California, con residencia en Tijuana (01/12/2013 - 15/10/2018)   ",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 1343,
    nombreOficial:
      "Juzgado Décimo de Distrito en el Estado de Baja California, con residencia en Tijuana",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 1344,
    nombreOficial:
      "Juzgado Decimoprimero de Distrito en el Estado de Baja California, con residencia en Tijuana",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 1345,
    nombreOficial:
      "Juzgado Decimosegundo de Distrito en el Estado de Baja California, con residencia en Tijuana",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 1348,
    nombreOficial:
      "Décimo Tribunal Unitario del Decimoquinto Circuito con sede en Mexicali, Baja California",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 1350,
    nombreOficial:
      "Juzgado Séptimo de Distrito en el Estado de Quintana Roo, con residencia en Cancún",
    catCircuitoId: 54,
    catEstadoId: 23,
    catCiudadId: 36,
  },
  {
    catOrganismoId: 1351,
    nombreOficial:
      "Juzgado Decimotercero de Distrito en el Estado de Baja California, con residencia en Tijuana",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 1353,
    nombreOficial:
      "Juzgado Quinto de Distrito en el Estado de Yucatán, con residencia en Mérida",
    catCircuitoId: 38,
    catEstadoId: 31,
    catCiudadId: 34,
  },
  {
    catOrganismoId: 1354,
    nombreOficial:
      "Juzgado Noveno de Distrito en el Estado de Baja California, con residencia en Ensenada",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 40,
  },
  {
    catOrganismoId: 1355,
    nombreOficial:
      "Juzgado Octavo de Distrito del Centro Auxiliar de la Segunda Región, en su carácter de Ordinario (10/01/2014 - 31/08/2014)   ",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 1357,
    nombreOficial:
      "Juzgado Cuarto de Distrito en el Estado de Baja California, con residencia en Mexicali",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 1358,
    nombreOficial:
      "Juzgado Quinto de Distrito en el Estado de Baja California, con residencia en Mexicali",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 1359,
    nombreOficial:
      "Juzgado Sexto de Distrito en el Estado de Baja California, con residencia en Mexicali",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 1360,
    nombreOficial:
      "Juzgado Séptimo de Distrito en el Estado de Baja California, con residencia en Ensenada",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 40,
  },
  {
    catOrganismoId: 1361,
    nombreOficial:
      "Juzgado Octavo de Distrito en el Estado de Baja California, con residencia en Ensenada",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 40,
  },
  {
    catOrganismoId: 1362,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materias Civil y de Trabajo del Decimoséptimo Circuito",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 44,
  },
  {
    catOrganismoId: 1364,
    nombreOficial:
      "Juzgado Decimoprimero de Distrito en el Estado de Guanajuato",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 42,
  },
  {
    catOrganismoId: 1365,
    nombreOficial:
      "Juzgado Quinto de Distrito del Centro Auxiliar de la Séptima Región",
    catCircuitoId: 36,
    catEstadoId: 13,
    catCiudadId: 55,
  },
  {
    catOrganismoId: 1366,
    nombreOficial:
      "Cuarto Tribunal Colegiado en Materia de Trabajo del Cuarto Circuito",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 1367,
    nombreOficial:
      "Juzgado Primero de Distrito del Centro Auxiliar de la Primera Región, con residencia en la Ciudad de México.   ",
    catCircuitoId: 57,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1368,
    nombreOficial:
      "Juzgado Segundo de Distrito del Centro Auxiliar de la Primera Región, con residencia en la Ciudad de México.   ",
    catCircuitoId: 57,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1372,
    nombreOficial:
      "Primer Tribunal Colegiado de Circuito del Centro Auxiliar de la Primera Región, en su carácter de ordinario",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1374,
    nombreOficial:
      "Cuarto Tribunal Colegiado de Circuito del Centro Auxiliar de la Primera Región, en su carácter de ordinario",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1376,
    nombreOficial:
      "Juzgado Tercero de Distrito de Procesos Penales Federales en el Estado de Tamaulipas",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 48,
  },
  {
    catOrganismoId: 1377,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Penal del Decimosegundo Circuito",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 1378,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Administrativa del Decimosegundo Circuito",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 1379,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Administrativa del Decimosegundo Circuito",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 1380,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Civil del Decimosegundo Circuito",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 1381,
    nombreOficial:
      "Tribunal Colegiado en Materia de Trabajo del Decimosegundo Circuito",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 1383,
    nombreOficial:
      "Cuarto Tribunal Unitario del Decimosexto Circuito (1/9/2014) - (30/11/2022)   ",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1384,
    nombreOficial: "Juzgado Octavo de Distrito en el Estado de Morelos",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 1386,
    nombreOficial:
      "Quinto Tribunal Colegiado en Materia Administrativa del Tercer Circuito",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 1387,
    nombreOficial: "Sexto Tribunal Colegiado del Decimoquinto Circuito",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 1388,
    nombreOficial:
      "Cuarto Tribunal Colegiado del Vigésimo Segundo Circuito (01/10/2014 - 13/03/2016)   ",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 1389,
    nombreOficial:
      "Juzgado Primero de Distrito de Procesos Penales Federales en el Estado de Querétaro (01/10/2014 - 15/10/2019)   ",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 1391,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia de Amparo Civil, Administrativo y de Trabajo y de Juicios Federales en el Estado de Querétaro",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 1392,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia de Amparo Civil, Administrativo y de Trabajo y de Juicios Federales en el Estado de Querétaro",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 1393,
    nombreOficial:
      "Juzgado Tercero de Distrito en Materia de Amparo Civil, Administrativo y de Trabajo y de Juicios Federales en el Estado de Querétaro",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 1395,
    nombreOficial:
      "Juzgado Cuarto de Distrito en Materia de Amparo Civil, Administrativo y de Trabajo y de Juicios Federales en el Estado de Querétaro",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 1396,
    nombreOficial:
      "Juzgado Quinto de Distrito en Materia de Amparo Civil, Administrativo y de Trabajo y de Juicios Federales en el Estado de Querétaro",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 1403,
    nombreOficial:
      "Tercer Tribunal Unitario del Cuarto Circuito (1/11/2014) - (15/12/2022)   ",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 1404,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materias Civil y de Trabajo del Vigésimo Primer Circuito",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 54,
  },
  {
    catOrganismoId: 1407,
    nombreOficial:
      "Cuarto Tribunal Unitario del Séptimo Circuito (1/11/2014) - (15/12/2022)   ",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 61,
  },
  {
    catOrganismoId: 1408,
    nombreOficial:
      "Juzgado Octavo de Distrito en Materias Administrativa, Civil y de Trabajo en el Estado de Jalisco, con residencia en Zapopan.   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 1409,
    nombreOficial:
      "Juzgado Decimosexto de Distrito en Materias Administrativa, Civil y de Trabajo en el Estado de Jalisco, con residencia en Zapopan.   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 1410,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Civil del Decimoprimer Circuito",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 1412,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia Penal del Séptimo Circuito, con residencia en Boca del Río, Veracruz",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 14,
  },
  {
    catOrganismoId: 1413,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Penal del Séptimo Circuito, con residencia en Boca del Río, Veracruz",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 14,
  },
  {
    catOrganismoId: 1414,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia de Trabajo del Séptimo Circuito, con residencia en Xalapa, Veracruz",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 1415,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia de Trabajo del Séptimo Circuito, con residencia en Xalapa, Veracruz",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 1417,
    nombreOficial:
      "Decimosexto Tribunal Colegiado en Materia de Trabajo del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1421,
    nombreOficial: "Juzgado Quinto de Distrito en el Estado de Tabasco",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 1422,
    nombreOficial: "Juzgado Sexto de Distrito en el Estado de Tabasco",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 1424,
    nombreOficial:
      "Juzgado Decimocuarto de Distrito en Materia Civil en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1425,
    nombreOficial:
      "Juzgado Séptimo de Distrito en Materia de Trabajo en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1426,
    nombreOficial:
      "Juzgado Octavo de Distrito en Materia de Trabajo en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1427,
    nombreOficial:
      "Juzgado Noveno de Distrito en Materia de Trabajo en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1438,
    nombreOficial:
      "Juzgado Primero de Distrito Especializado en el Sistema Penal Acusatorio del Centro de Justicia Penal Federal en el Estado de Yucatán, con residencia en la Ciudad de Mérida",
    catCircuitoId: 38,
    catEstadoId: 31,
    catCiudadId: 34,
  },
  {
    catOrganismoId: 1439,
    nombreOficial:
      "Juzgado Segundo de Distrito Especializado en el Sistema Penal Acusatorio del Centro de Justicia Penal Federal en el Estado de Yucatán, con residencia en la Ciudad de Mérida",
    catCircuitoId: 38,
    catEstadoId: 31,
    catCiudadId: 34,
  },
  {
    catOrganismoId: 1441,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materias Civil y de Trabajo del Quinto Circuito",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 1442,
    nombreOficial:
      "Decimonoveno Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1443,
    nombreOficial:
      "Vigésimo Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1444,
    nombreOficial:
      "Juzgado de Distrito de Procesos Penales Federales en el Estado de Puebla, con residencia en San Andrés Cholula (01/06/2015 - 15/05/2023)   ",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 1447,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia de Amparo Civil, Administrativa y de Trabajo y de Juicios Federales en el Estado de Puebla",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 1448,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia de Amparo Civil, Administrativa y de Trabajo y Juicios Federales en el Estado de Puebla",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 1449,
    nombreOficial:
      "Juzgado Tercero de Distrito en Materia de Amparo Civil, Administrativa y de Trabajo y Juicios Federales en el Estado de Puebla",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 1450,
    nombreOficial:
      "Juzgado Cuarto de Distrito en Materia de Amparo Civil, Administrativa y de Trabajo y Juicios Federales en el Estado de Puebla",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 1451,
    nombreOficial:
      "Juzgado Quinto de Distrito en Materia de Amparo Civil, Administrativa y de Trabajo y Juicios Federales en el Estado de Puebla",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 1452,
    nombreOficial:
      "Juzgado Sexto de Distrito en Materia de Amparo Civil, Administrativa y de Trabajo y Juicios Federales en el Estado de Puebla",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 1453,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia Penal en el Estado de Puebla",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 1455,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia Penal en el Estado de Puebla",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 1456,
    nombreOficial:
      "Juzgado Tercero de Distrito en Materia Penal en el Estado de Puebla",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 1457,
    nombreOficial:
      "Juzgado Cuarto de Distrito en Materia Penal en el Estado de Puebla",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 12,
  },
  {
    catOrganismoId: 1458,
    nombreOficial:
      "Juzgado Quinto de Distrito en Materia Penal en el Estado de Puebla",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 12,
  },
  {
    catOrganismoId: 1460,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Puebla, con residencia en la ciudad del mismo nombre",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 12,
  },
  {
    catOrganismoId: 1461,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Durango, con residencia en la ciudad del mismo nombre",
    catCircuitoId: 52,
    catEstadoId: 10,
    catCiudadId: 21,
  },
  {
    catOrganismoId: 1462,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Yucatán, con residencia en Mérida",
    catCircuitoId: 38,
    catEstadoId: 31,
    catCiudadId: 34,
  },
  {
    catOrganismoId: 1463,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Zacatecas, con residencia en la ciudad del mismo nombre",
    catCircuitoId: 56,
    catEstadoId: 32,
    catCiudadId: 59,
  },
  {
    catOrganismoId: 1467,
    nombreOficial:
      "Juzgado Decimoquinto de Distrito de Amparo en Materia Penal en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1468,
    nombreOficial:
      "Juzgado Decimosexto de Distrito de Amparo en Materia Penal en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1475,
    nombreOficial:
      "Centro de Justicia Penal Federal para pruebas de nuevos Desarrollos",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1476,
    nombreOficial:
      "Segundo Tribunal Unitario del Vigésimo Tercer Circuito (16/08/2015 - 30/11/2018)   ",
    catCircuitoId: 56,
    catEstadoId: 32,
    catCiudadId: 59,
  },
  {
    catOrganismoId: 1478,
    nombreOficial: "Segundo Tribunal Colegiado del Vigésimo Quinto Circuito",
    catCircuitoId: 52,
    catEstadoId: 10,
    catCiudadId: 21,
  },
  {
    catOrganismoId: 1480,
    nombreOficial: "Centro de Justicia Penal Federal para Pruebas 1 (DF)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1481,
    nombreOficial:
      "Juzgado Tercero de Distrito en Materia Administrativa para Presentación 3 (DF)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1483,
    nombreOficial:
      "Centro de Justicia Penal Federal para Presentación 1 (DF)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1484,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de San Luis Potosí, con residencia en la ciudad del mismo nombre",
    catCircuitoId: 9,
    catEstadoId: 24,
    catCiudadId: 22,
  },
  {
    catOrganismoId: 1485,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Guanajuato, con residencia en la ciudad del mismo nombre",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1486,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Querétaro, con residencia en la ciudad del mismo nombre",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 1487,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Baja California Sur, con residencia en La Paz",
    catCircuitoId: 55,
    catEstadoId: 3,
    catCiudadId: 31,
  },
  {
    catOrganismoId: 1489,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia Administrativa para Presentación 1 (DF)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1490,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia Administrativa para Presentación 2 (DF)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1492,
    nombreOficial:
      "Centro de Justicia Penal Federal para Presentación 2 (DF)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1494,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia Administrativa para Pruebas 1 (CDMX)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1495,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia Administrativa para Pruebas 2 (CDMX)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1497,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Nayarit, con residencia en Tepic",
    catCircuitoId: 48,
    catEstadoId: 18,
    catCiudadId: 30,
  },
  {
    catOrganismoId: 1498,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Tlaxcala, con residencia en Apizaco",
    catCircuitoId: 50,
    catEstadoId: 29,
    catCiudadId: 75,
  },
  {
    catOrganismoId: 1499,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Chiapas, con residencia en Cintalapa de Figueroa",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 65,
  },
  {
    catOrganismoId: 1500,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Oaxaca, con residencia en San Bartolo Coyotepec",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 80,
  },
  {
    catOrganismoId: 1501,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Coahuila, con residencia en Torreón",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 18,
  },
  {
    catOrganismoId: 1502,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Chihuahua, con residencia en la ciudad del mismo nombre",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 44,
  },
  {
    catOrganismoId: 1503,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Chihuahua, con residencia en Ciudad Juárez",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 45,
  },
  {
    catOrganismoId: 1504,
    nombreOficial:
      "Centro de Justicia Penal Federal en la Ciudad de México, Reclusorio Norte",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1505,
    nombreOficial:
      "Centro de Justicia Penal Federal en la Ciudad de México, Reclusorio Oriente",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1506,
    nombreOficial:
      "Centro de Justicia Penal Federal en la Ciudad de México, Reclusorio Sur",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1507,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Chiapas, con residencia en Tapachula",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 53,
  },
  {
    catOrganismoId: 1508,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Campeche, con residencia en la ciudad del mismo nombre",
    catCircuitoId: 47,
    catEstadoId: 4,
    catCiudadId: 35,
  },
  {
    catOrganismoId: 1509,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Colima, con residencia en la ciudad del mismo nombre",
    catCircuitoId: 109,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 1510,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Guerrero, con residencia en Acapulco",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 55,
  },
  {
    catOrganismoId: 1511,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Hidalgo, con residencia en Pachuca",
    catCircuitoId: 49,
    catEstadoId: 14,
    catCiudadId: 58,
  },
  {
    catOrganismoId: 1512,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Jalisco, con residencia en Puente Grande",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 1513,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Michoacán, con residencia en Morelia",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 1514,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Nuevo León, con residencia en Cadereyta",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 1515,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Morelos, con residencia en Xochitepec",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 81,
  },
  {
    catOrganismoId: 1516,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Quintana Roo, con residencia en Cancún",
    catCircuitoId: 54,
    catEstadoId: 23,
    catCiudadId: 36,
  },
  {
    catOrganismoId: 1517,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Tabasco, con residencia en Villahermosa",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 1518,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Aguascalientes, con residencia en la ciudad del mismo nombre",
    catCircuitoId: 30,
    catEstadoId: 1,
    catCiudadId: 60,
  },
  {
    catOrganismoId: 1519,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Baja California, con residencia en Tijuana",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 1520,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Baja California, con residencia en Mexicali",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 1521,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Baja California, con residencia en Ensenada",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 40,
  },
  {
    catOrganismoId: 1523,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de México, con residencia en Nezahualcóyotl",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 3,
  },
  {
    catOrganismoId: 1524,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Sinaloa, con residencia en Culiacán",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 1526,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Tamaulipas, con residencia en Reynosa",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 51,
  },
  {
    catOrganismoId: 1527,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Tamaulipas, con residencia en Ciudad Victoria",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 50,
  },
  {
    catOrganismoId: 1528,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Veracruz, con residencia en Coatzacoalcos",
    catCircuitoId: 10,
    catEstadoId: 30,
    catCiudadId: 24,
  },
  {
    catOrganismoId: 1529,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Veracruz, con residencia en Emiliano Zapata",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 1530,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de Sonora, con residencia en Hermosillo",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 1532,
    nombreOficial:
      "Juzgado Tercero de Distrito en Materia Administrativa para Pruebas 3 (CDMX)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1533,
    nombreOficial: "Juzgado Decimoprimero de Distrito en el Estado de Sonora",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 1534,
    nombreOficial: "Juzgado Decimosegundo de Distrito en el Estado de Sonora",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 1535,
    nombreOficial:
      "Tribunal Colegiado en Materia Administrativa del Vigésimo Circuito, con residencia en Tuxtla Gutiérrez, Chiapas",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 1536,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Penal y Civil del Vigésimo Circuito, con residencia en Tuxtla Gutiérrez, Chiapas",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 1537,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Penal y Civil del Vigésimo Circuito, con residencia en Tuxtla Gutiérrez, Chiapas",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 1538,
    nombreOficial:
      "Tribunal Colegiado en Materia de Trabajo del Vigésimo Circuito, con residencia en Tuxtla Gutiérrez, Chiapas",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 1555,
    nombreOficial: "Juzgado Cuarto de Distrito en el Estado de Aguascalientes",
    catCircuitoId: 30,
    catEstadoId: 1,
    catCiudadId: 60,
  },
  {
    catOrganismoId: 1556,
    nombreOficial: "Juzgado Quinto de Distrito en el Estado de Aguascalientes",
    catCircuitoId: 30,
    catEstadoId: 1,
    catCiudadId: 60,
  },
  {
    catOrganismoId: 1557,
    nombreOficial: "Tribunal Colegiado en Materia Penal del Noveno Circuito",
    catCircuitoId: 9,
    catEstadoId: 24,
    catCiudadId: 22,
  },
  {
    catOrganismoId: 1558,
    nombreOficial:
      "Tribunal Colegiado en Materia de Trabajo del Noveno Circuito",
    catCircuitoId: 9,
    catEstadoId: 24,
    catCiudadId: 22,
  },
  {
    catOrganismoId: 1560,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Civil y Administrativa del Noveno Circuito",
    catCircuitoId: 9,
    catEstadoId: 24,
    catCiudadId: 22,
  },
  {
    catOrganismoId: 1561,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Civil y Administrativa del Noveno Circuito",
    catCircuitoId: 9,
    catEstadoId: 24,
    catCiudadId: 22,
  },
  {
    catOrganismoId: 1563,
    nombreOficial:
      "Segundo Tribunal Unitario del Decimoctavo Circuito (1/3/2016) - (30/11/2022)   ",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 1571,
    nombreOficial:
      "Juzgado Decimoctavo de Distrito en el Estado de Veracruz, con residencia en Xalapa",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 1573,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia de Trabajo del Decimoctavo Circuito, con Residencia en Cuernavaca Morelos",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 1574,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia de Trabajo del Decimoctavo Circuito, con Residencia en Cuernavaca Morelos",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 1576,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Penal y Administrativa del Decimoctavo Circuito, con Residencia en Cuernavaca Morelos",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 1577,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Penal y Administrativa del Decimoctavo Circuito, con Residencia en Cuernavaca Morelos",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 1579,
    nombreOficial:
      "Tribunal Colegiado en Materia Civil del Decimoctavo Circuito, con residencia en Cuernavaca, Morelos",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 1582,
    nombreOficial:
      "Juzgado Primero de Distrito de Amparo y Juicios Federales en el Estado de Chiapas, con residencia en Tuxtla Gutiérrez",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 1583,
    nombreOficial:
      "Juzgado Segundo de Distrito de Amparo y Juicios Federales en el Estado de Chiapas, con residencia en Tuxtla Gutiérrez",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 1584,
    nombreOficial:
      "Juzgado Tercero de Distrito de Amparo y Juicios Federales en el Estado de Chiapas, con residencia en Tuxtla Gutiérrez",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 1585,
    nombreOficial:
      "Juzgado Cuarto de Distrito de Amparo y Juicios Federales en el Estado de Chiapas, con residencia en Tuxtla Gutiérrez",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 1586,
    nombreOficial:
      "Juzgado Quinto de Distrito de Amparo y Juicios Federales en el Estado de Chiapas, con residencia en Tuxtla Gutiérrez",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 1587,
    nombreOficial:
      "Tribunal Colegiado en Materias Penal y Administrativa del Vigésimo Segundo Circuito, con residencia en Querétaro, Querétaro",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 1588,
    nombreOficial:
      "Tribunal Colegiado en Materias Administrativa y de Trabajo del Vigésimo Segundo Circuito, con residencia en Querétaro, Querétaro",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 1589,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Administrativa y Civil del Vigésimo Segundo Circuito, con residencia en Querétaro, Querétaro",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 1590,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Administrativa y Civil del Vigésimo Segundo Circuito, con residencia en Querétaro, Querétaro",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 1591,
    nombreOficial:
      "Juzgado Primero de Distrito en el Estado de Chiapas, con residencia en Tapachula",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 53,
  },
  {
    catOrganismoId: 1592,
    nombreOficial:
      "Juzgado Segundo de Distrito en el Estado de Chiapas, con residencia en Tapachula",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 53,
  },
  {
    catOrganismoId: 1608,
    nombreOficial:
      "Juez (Pruebas) de Control del Centro Nacional de Justicia Especializado.   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1662,
    nombreOficial:
      "Cuarto Tribunal Unitario del Cuarto Circuito (16/5/2016) - (15/12/2022)   ",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 1663,
    nombreOficial:
      "Quinto Tribunal Unitario del Decimonoveno Circuito (16/5/2016) - (15/12/2022)   ",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 48,
  },
  {
    catOrganismoId: 1664,
    nombreOficial:
      "Juzgado Sexto de Distrito en Materia de Amparo Civil, Administrativo y de Trabajo y de Juicios Federales en el Estado de Querétaro",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 1665,
    nombreOficial:
      "Juzgado Séptimo de Distrito en Materia de Amparo Civil, Administrativo y de Trabajo y de Juicios Federales en el Estado de Querétaro",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 1666,
    nombreOficial:
      "Juzgado Noveno de Distrito en el Estado de Oaxaca, con residencia en San Bartolo Coyotepec",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 80,
  },
  {
    catOrganismoId: 1667,
    nombreOficial:
      "Juzgado Séptimo de Distrito de Amparo en Materia Penal en el Estado de Jalisco, con residencia en Puente Grande",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 1668,
    nombreOficial:
      "Juzgado Decimosegundo de Distrito en el Estado de Tamaulipas, con residencia en Ciudad Victoria",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 50,
  },
  {
    catOrganismoId: 1669,
    nombreOficial:
      "Juzgado Décimo Tercero de Distrito en el Estado de Tamaulipas",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 77,
  },
  {
    catOrganismoId: 1671,
    nombreOficial:
      "Décimo Tribunal Colegiado en Materia Penal del Primer Circuito.   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1673,
    nombreOficial:
      "Primer Tribunal Colegiado en Materias Civil y de Trabajo del Decimoquinto Circuito",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 1674,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Civil y de Trabajo del Decimoquinto Circuito",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 1676,
    nombreOficial:
      "Juzgado Primero de Distrito Especializado en Medidas Cautelares y Control de Técnicas de Investigación, con competencia en toda la República y residencia en la Ciudad de México (21/06/2016 - 16/05/2017)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1677,
    nombreOficial:
      "Juzgado Segundo de Distrito Especializado en Medidas Cautelares y Control de Técnicas de Investigación, con competencia en toda la República y residencia en la Ciudad de México (21/06/2016 - 16/05/2017)   ",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1711,
    nombreOficial: "Juzgado Quinto de Distrito en La Laguna",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 18,
  },
  {
    catOrganismoId: 1712,
    nombreOficial: "Juzgado Sexto de Distrito en La Laguna",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 18,
  },
  {
    catOrganismoId: 1886,
    nombreOficial:
      "Sexto Tribunal Colegiado en Materia Administrativa del Tercer Circuito",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 1887,
    nombreOficial:
      "Séptimo Tribunal Colegiado en Materia Administrativa del Tercer Circuito",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 1888,
    nombreOficial:
      "Quinto Tribunal Colegiado en Materia de Trabajo del Tercer Circuito, con residencia en Zapopan, Jalisco",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 1889,
    nombreOficial:
      "Juzgado Décimo de Distrito en el Estado de Oaxaca, con residencia en San Bartolo Coyotepec",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 80,
  },
  {
    catOrganismoId: 1890,
    nombreOficial:
      "Juzgado Decimoprimero de Distrito en el Estado de Oaxaca, con residencia en San Bartolo Coyotepec",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 80,
  },
  {
    catOrganismoId: 1891,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Penal del Decimosexto Circuito",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 1893,
    nombreOficial:
      "Juzgado Sexto de Distrito de Amparo y Juicios Federales en el Estado de Chiapas, con residencia en Tuxtla Gutiérrez",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 1894,
    nombreOficial:
      "Juzgado Séptimo de Distrito de Amparo y Juicios Federales en el Estado de Chiapas, con residencia en Tuxtla Gutiérrez",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 1895,
    nombreOficial:
      "Juzgado Cuarto de Distrito en Materia Mercantil, Especializado en Juicios de Cuantía Menor, con sede en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 1896,
    nombreOficial:
      "Juzgado Quinto de Distrito en Materia Mercantil, Especializado en Juicios de Cuantía Menor, con sede en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2071,
    nombreOficial: "Juzgado Octavo de Distrito en el Estado de San Luis Potosí",
    catCircuitoId: 9,
    catEstadoId: 24,
    catCiudadId: 22,
  },
  {
    catOrganismoId: 2072,
    nombreOficial: "Juzgado Primero Pruebas de Instituto",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2073,
    nombreOficial: "Juzgado Segundo Pruebas de Instituto",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2074,
    nombreOficial: "Juzgado Tercero Pruebas de Instituto",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2075,
    nombreOficial: "Primer Tribunal Colegiado Pruebas de Instituto",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2076,
    nombreOficial: "Segundo Tribunal Colegiado Pruebas de Instituto",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2077,
    nombreOficial: "Tercer Tribunal Colegiado Pruebas de Instituto",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2078,
    nombreOficial: "Primer Tribunal Unitario Pruebas de Instituto",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2079,
    nombreOficial: "Segundo Tribunal Unitario Pruebas de Instituto",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2080,
    nombreOficial: "Tercer Tribunal Unitario Pruebas de Instituto",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2081,
    nombreOficial:
      "Primer Centro de Justicia Penal Federal Pruebas de Instituto",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2082,
    nombreOficial:
      "Segundo Centro de Justicia Penal Federal Pruebas de Instituto",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2083,
    nombreOficial:
      "Tercer Centro de Justicia Penal Federal Pruebas de Instituto",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2316,
    nombreOficial:
      "Juez Primero de Control del Centro Nacional de Justicia Especializado en Control de Técnicas de Investigación, Arraigo e Intervención de Comunicaciones, con residencia en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2317,
    nombreOficial:
      "Juez Segundo de Control del Centro Nacional de Justicia Especializado en Control de Técnicas de Investigación, Arraigo e Intervención de Comunicaciones, con residencia en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2318,
    nombreOficial:
      "Juez Tercero de Control del Centro Nacional de Justicia Especializado en Control de Técnicas de Investigación, Arraigo e Intervención de Comunicaciones, con residencia en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2319,
    nombreOficial:
      "Juez Cuarto de Control del Centro Nacional de Justicia Especializado en Control de Técnicas de Investigación, Arraigo e Intervención de Comunicaciones, con residencia en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2320,
    nombreOficial:
      "Juez Quinto de Control del Centro Nacional de Justicia Especializado en Control de Técnicas de Investigación, Arraigo e Intervención de Comunicaciones, con residencia en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2321,
    nombreOficial:
      "Juez Sexto de Control del Centro Nacional de Justicia Especializado en Control de Técnicas de Investigación, Arraigo e Intervención de Comunicaciones, con residencia en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2322,
    nombreOficial:
      "Centro de Justicia Penal Federal en el Estado de México, con residencia en el municipio de Almoloya de Juárez (Altiplano)   ",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 2404,
    nombreOficial:
      "Sexto Tribunal Unitario del Quinto Circuito, con residencia en Hermosillo Sonora",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 2406,
    nombreOficial: "Juzgado Decimoprimero de Distrito en Estado de Chihuahua",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 44,
  },
  {
    catOrganismoId: 2407,
    nombreOficial:
      "Sexto Tribunal Unitario del Tercer Circuito (1/9/2017) - (30/11/2022)   ",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 6,
  },
  {
    catOrganismoId: 2408,
    nombreOficial:
      "Tribunal Unitario Especializado en Materia Penal del Segundo Circuito (1/10/2017) - (15/12/2022)   ",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 2409,
    nombreOficial: "Segundo Tribunal Colegiado del Decimoséptimo Circuito",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 45,
  },
  {
    catOrganismoId: 2411,
    nombreOficial:
      "Juzgado Sexto de Distrito en Materias de Amparo y Juicios Federales en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 2412,
    nombreOficial: "Segundo Tribunal Colegiado del Vigésimo Octavo Circuito",
    catCircuitoId: 50,
    catEstadoId: 29,
    catCiudadId: 75,
  },
  {
    catOrganismoId: 2414,
    nombreOficial:
      "Juzgado Noveno de Distrito en Materias Administrativa, Civil y de Trabajo en el Estado de Jalisco, con residencia en Zapopan",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 2415,
    nombreOficial: "Tercer Tribunal Colegiado del Vigésimo Quinto Circuito",
    catCircuitoId: 52,
    catEstadoId: 10,
    catCiudadId: 21,
  },
  {
    catOrganismoId: 2416,
    nombreOficial:
      "Tribunal Unitario especializado en Materia Penal del Cuarto Circuito  (1/10/2017) - (15/12/2022)   ",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 76,
  },
  {
    catOrganismoId: 2417,
    nombreOficial: "Juzgado Sexto de Distrito en el Estado de Aguascalientes",
    catCircuitoId: 30,
    catEstadoId: 1,
    catCiudadId: 60,
  },
  {
    catOrganismoId: 2418,
    nombreOficial:
      "Juzgado Cuarto de Distrito Especializado en Ejecución de Penas",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2419,
    nombreOficial:
      "Juzgado Quinto de Distrito Especializado en Ejecución de Penas",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2420,
    nombreOficial: "Tercer Tribunal Colegiado del Trigésimo Circuito",
    catCircuitoId: 30,
    catEstadoId: 1,
    catCiudadId: 60,
  },
  {
    catOrganismoId: 2422,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materias Administrativa y Civil del Vigésimo Segundo Circuito, con residencia en Querétaro, Querétaro",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 2423,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materias Administrativa y de Trabajo del Decimoprimer Circuito",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 2424,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Colima",
    catCircuitoId: 109,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 2425,
    nombreOficial:
      "Juzgado Tercero de Distrito en Materia Administrativa en el Estado de Nuevo León",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 2426,
    nombreOficial:
      "Tercer Tribunal Unitario del Decimoprimer Circuito, con residencia en Morelia, Michoacán (1/11/2017) - (30/11/2022)   ",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 2456,
    nombreOficial:
      "Vigésimo Primer Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 2741,
    nombreOficial:
      "Juzgado de Distrito en Materia Mercantil Federal en el Estado de México, con residencia en Naucalpan de Juárez",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 5,
  },
  {
    catOrganismoId: 2742,
    nombreOficial:
      "Juzgado Sexto de Distrito en Materia Mercantil, Especializado en Juicios de Cuantía Menor, con residencia en Zapopan, Jalisco",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 2743,
    nombreOficial:
      "Juzgado Decimosegundo de Distrito en el Estado de Guanajuato",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 42,
  },
  {
    catOrganismoId: 2744,
    nombreOficial:
      "Séptimo Tribunal Unitario del Segundo Circuito  (1/1/2018) - (15/12/2022)   ",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 2745,
    nombreOficial:
      "Juzgado Octavo de Distrito de Amparo en Materia Penal en el Estado de Jalisco, con residencia en Puente Grande",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 2748,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materias Penal y Administrativa del Decimoctavo Circuito, con Residencia en Cuernavaca Morelos",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 2885,
    nombreOficial:
      "Sexto Tribunal Colegiado en Materia Civil del Tercer Circuito",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 6,
  },
  {
    catOrganismoId: 2886,
    nombreOficial: "Juzgado Séptimo de Distrito en el Estado de Tabasco",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 2887,
    nombreOficial: "Cuarto Tribunal Colegiado del Vigésimo Quinto Circuito",
    catCircuitoId: 52,
    catEstadoId: 10,
    catCiudadId: 21,
  },
  {
    catOrganismoId: 2951,
    nombreOficial:
      "Juzgado de Distrito en Materia Mercantil Federal en el Estado de Veracruz, con residencia en Boca del Río",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 14,
  },
  {
    catOrganismoId: 2952,
    nombreOficial:
      "Sexto Tribunal Unitario del Decimonoveno Circuito (1/7/2018) - (15/12/2022)   ",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 51,
  },
  {
    catOrganismoId: 2953,
    nombreOficial:
      "Juzgado Octavo de Distrito en el Estado de Quintana Roo, con residencia en Cancún",
    catCircuitoId: 54,
    catEstadoId: 23,
    catCiudadId: 36,
  },
  {
    catOrganismoId: 2954,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materias Penal y Administrativa del Octavo Circuito",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 18,
  },
  {
    catOrganismoId: 2956,
    nombreOficial: "Juzgado Décimo Noveno de Distrito en el Estado de Veracruz",
    catCircuitoId: 10,
    catEstadoId: 30,
    catCiudadId: 24,
  },
  {
    catOrganismoId: 2990,
    nombreOficial:
      "Octavo Tribunal Unitario del Segundo Circuito, con residencia en Nezahualcóyotl (1/8/2018) - (15/12/2022)   ",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 3,
  },
  {
    catOrganismoId: 2992,
    nombreOficial: "Tercer Tribunal Colegiado del Vigésimo Noveno Circuito",
    catCircuitoId: 49,
    catEstadoId: 14,
    catCiudadId: 58,
  },
  {
    catOrganismoId: 3444,
    nombreOficial:
      "Juez Séptimo de Control del Centro Nacional de Justicia Especializado en Control de Técnicas de Investigación, Arraigo e Intervención de Comunicaciones, con residencia en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 3445,
    nombreOficial:
      "Juez Octavo de Control del Centro Nacional de Justicia Especializado en Control de Técnicas de Investigación, Arraigo e Intervención de Comunicaciones, con residencia en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 3447,
    nombreOficial: "Cuarto Tribunal Colegiado del Trigésimo Circuito",
    catCircuitoId: 30,
    catEstadoId: 1,
    catCiudadId: 60,
  },
  {
    catOrganismoId: 3448,
    nombreOficial:
      "Juzgado de Distrito en Materia Mercantil Federal en el Estado de Oaxaca",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 80,
  },
  {
    catOrganismoId: 3449,
    nombreOficial:
      "Juzgado Séptimo de Distrito en Materia de Amparo y Juicios Federales en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 3450,
    nombreOficial:
      "Juzgado de Distrito en Materia Mercantil Federal en el Estado de Yucatán",
    catCircuitoId: 38,
    catEstadoId: 31,
    catCiudadId: 34,
  },
  {
    catOrganismoId: 3454,
    nombreOficial:
      "Segundo Tribunal Unitario del Vigésimo Séptimo Circuito (1/10/2018) - (15/12/2022)   ",
    catCircuitoId: 54,
    catEstadoId: 23,
    catCiudadId: 36,
  },
  {
    catOrganismoId: 3456,
    nombreOficial:
      "Juzgado Séptimo de Distrito en Materia de Amparo Civil, Administrativa y de Trabajo y Juicios Federales en el Estado de Puebla",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 3457,
    nombreOficial:
      "Juzgado Octavo de Distrito en Materia de Amparo Civil, Administrativa y de Trabajo y Juicios Federales en el Estado de Puebla",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 3458,
    nombreOficial:
      "Cuarto Tribunal Colegiado en Materia Penal del Tercer Circuito, con residencia en Guadalajara, Jalisco",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 6,
  },
  {
    catOrganismoId: 3459,
    nombreOficial:
      "Juzgado de Distrito en Materia Mercantil Federal en el Estado de San Luis Potosí",
    catCircuitoId: 9,
    catEstadoId: 24,
    catCiudadId: 22,
  },
  {
    catOrganismoId: 3460,
    nombreOficial:
      "Juzgado Decimocuarto de Distrito en el Estado de Baja California, con residencia en Tijuana",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 3822,
    nombreOficial:
      "Juzgado de Distrito en Materia Mercantil Federal en la Laguna, con residencia en Torreón, Coahuila de Zaragoza",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 18,
  },
  {
    catOrganismoId: 3823,
    nombreOficial:
      "Juzgado de Distrito en Materia Mercantil Federal en el Estado de Guerrero, con residencia en Acapulco",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 55,
  },
  {
    catOrganismoId: 3824,
    nombreOficial:
      "Juzgado de Distrito en Materia Mercantil Federal en el Estado de México, con residencia en Toluca",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 3825,
    nombreOficial:
      "Juzgado Noveno de Distrito de Amparo en Materia Penal en el Estado de Jalisco, con residencia en Puente Grande",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 67,
  },
  {
    catOrganismoId: 3826,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia Mercantil Federal en el Estado de Tabasco",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 3832,
    nombreOficial: "Juzgado Tercero de Distrito en el Estado de Zacatecas",
    catCircuitoId: 56,
    catEstadoId: 32,
    catCiudadId: 59,
  },
  {
    catOrganismoId: 3833,
    nombreOficial:
      "Juzgado Séptimo de Distrito en Materia Mercantil Especializado en Juicios de Cuantía Menor, con residencia en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 3835,
    nombreOficial:
      "Juzgado Decimoquinto de Distrito en el Estado de México, con residencia en Naucalpan de Juárez",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 5,
  },
  {
    catOrganismoId: 3836,
    nombreOficial:
      "Juzgado Decimosexto de Distrito en el Estado de México, con residencia en Naucalpan de Juárez",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 5,
  },
  {
    catOrganismoId: 3837,
    nombreOficial:
      "Juzgado Noveno de Distrito en el Estado de Morelos, con residencia en Cuernavaca",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 3838,
    nombreOficial:
      "Juzgado Tercero de Distrito en el Estado de Campeche, con residencia en Ciudad del Carmen (QUEDA SIN EFECTOS)   ",
    catCircuitoId: 47,
    catEstadoId: 4,
    catCiudadId: 78,
  },
  {
    catOrganismoId: 3840,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia Mercantil Federal en el Estado de Baja California, con residencia en Mexicali",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 3841,
    nombreOficial:
      "Segundo Tribunal Colegiado del Décimo Circuito, con residencia en Coatzacoalcos, Veracruz",
    catCircuitoId: 10,
    catEstadoId: 30,
    catCiudadId: 24,
  },
  {
    catOrganismoId: 3842,
    nombreOficial:
      "Juzgado de Distrito en Materia Mercantil Federal en el Estado de Veracruz, con residencia en Xalapa",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 3843,
    nombreOficial:
      "Séptimo Tribunal Unitario del Decimonoveno Circuito, con residencia en Tampico (16/1/2019) - (15/12/2022)   ",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 49,
  },
  {
    catOrganismoId: 3844,
    nombreOficial:
      "Décimo Quinto Tribunal Colegiado en Materia Civil del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 3845,
    nombreOficial:
      "Juzgado Tercero de Distrito en el Estado de Chiapas, con residencia en Tapachula",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 53,
  },
  {
    catOrganismoId: 3849,
    nombreOficial:
      "Juzgado de Distrito en Materia Mercantil Federal en el Estado de Tlaxcala",
    catCircuitoId: 50,
    catEstadoId: 29,
    catCiudadId: 75,
  },
  {
    catOrganismoId: 3850,
    nombreOficial:
      "Juzgado de Distrito en Materia Mercantil Federal en el Estado de Chiapas, con residencia en Tuxtla Gutiérrez",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 52,
  },
  {
    catOrganismoId: 3851,
    nombreOficial: "Juzgado Decimotercero de Distrito en el Estado de Sonora",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 3852,
    nombreOficial:
      "Primer Tribunal Colegiado en Materia de Trabajo del Décimo Circuito, con residencia en Villahermosa, Tabasco",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 3853,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia de Trabajo del Décimo Circuito, con residencia en Villahermosa, Tabasco",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 3858,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materias Civil y Administrativa del Decimotercer Circuito, con residencia en Oaxaca, Oaxaca",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 32,
  },
  {
    catOrganismoId: 3861,
    nombreOficial:
      "Juzgado Séptimo de Distrito en La Laguna, con residencia en Torreón",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 18,
  },
  {
    catOrganismoId: 3874,
    nombreOficial:
      "Juzgado Octavo de Distrito en Materias de Amparo y Juicios Federales en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 3875,
    nombreOficial:
      "Juzgado Noveno de Distrito en Materias de Amparo y Juicios Federales en el Estado de México",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 3876,
    nombreOficial:
      "Juzgado Primero de Distrito de Procesos Penales Federales y de Amparo en Materia Penal en el Estado de Veracruz, con residencia en Villa Aldama",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 74,
  },
  {
    catOrganismoId: 3877,
    nombreOficial:
      "Juzgado Segundo de Distrito de Procesos Penales Federales y de Amparo en Materia Penal en el Estado de Veracruz, con residencia en Villa Aldama",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 74,
  },
  {
    catOrganismoId: 3878,
    nombreOficial:
      "Juzgado Decimoséptimo de Distrito en Materias Administrativa, Civil y de Trabajo en el Estado de Jalisco, con residencia en Zapopan",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 3879,
    nombreOficial:
      "Juzgado Decimoctavo de Distrito en Materias Administrativa, Civil y de Trabajo en el Estado de Jalisco, con residencia en Zapopan",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 3880,
    nombreOficial:
      "Juzgado Decimonoveno de Distrito en Materias Administrativa, Civil y de Trabajo en el Estado de Jalisco, con residencia en Zapopan",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 3881,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia Penal en el Estado de Querétaro",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 3882,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia Penal en el Estado de Querétaro",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 3886,
    nombreOficial:
      "Juzgado Decimoquinto de Distrito en el Estado de Baja California, con residencia en Tijuana",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 3887,
    nombreOficial:
      "Juzgado Decimosexto de Distrito en el Estado de Baja California, con residencia en Tijuana",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 3888,
    nombreOficial:
      "Juzgado Primero de Distrito de Procesos Penales Federales y de Amparo en Materia Penal en el Estado de Chiapas, con sede en Cintalapa de Figueroa",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 65,
  },
  {
    catOrganismoId: 3889,
    nombreOficial:
      "Juzgado Segundo de Distrito de Procesos Penales Federales y de Amparo en Materia Penal en el Estado de Chiapas, con sede en Cintalapa de Figueroa",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 65,
  },
  {
    catOrganismoId: 3890,
    nombreOficial:
      "Juzgado Tercero de Distrito en Materia de Amparo Civil, Administrativo y de Trabajo y de Juicios Federales en el Estado de Nayarit, con residencia en el Rincón, Municipio de Tepic",
    catCircuitoId: 48,
    catEstadoId: 18,
    catCiudadId: 30,
  },
  {
    catOrganismoId: 3892,
    nombreOficial:
      "Cuarto Tribunal Unitario en Materias Civil, Administrativa y Especializados en Competencia Económica, Radiodifusión y Telecomunicaciones",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 3896,
    nombreOficial: "Segundo Tribunal Colegiado del Vigésimo Tercer Circuito",
    catCircuitoId: 56,
    catEstadoId: 32,
    catCiudadId: 59,
  },
  {
    catOrganismoId: 3898,
    nombreOficial:
      "Juzgado Octavo de Distrito en Materia Mercantil Especializado en Juicios de Cuantía Menor, con residencia en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 3900,
    nombreOficial:
      "Juzgado Noveno de Distrito en Materia de Amparo Civil, Administrativo y de Trabajo y de Juicios Federales en el Estado de Puebla",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 3977,
    nombreOficial: "Centro de Justicia Penal Federal para Pruebas 2",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 3993,
    nombreOficial:
      "Juzgado Decimosegundo de Distrito en el Estado de Chihuahua",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 44,
  },
  {
    catOrganismoId: 3997,
    nombreOficial:
      "Sexto Tribunal Colegiado en Materia de Trabajo del Tercer Circuito, con residencia en Zapopan, Jalisco",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 3998,
    nombreOficial:
      "Juzgado Cuarto de Distrito en el Estado de Chiapas, con residencia en Tapachula",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 53,
  },
  {
    catOrganismoId: 3999,
    nombreOficial:
      "Juzgado Sexto de Distrito Especializado en Ejecución de Penas",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4000,
    nombreOficial: "Juzgado Séptimo de Distrito en el Estado de Aguascalientes",
    catCircuitoId: 30,
    catEstadoId: 1,
    catCiudadId: 60,
  },
  {
    catOrganismoId: 4001,
    nombreOficial: "Juzgado Octavo de Distrito en el Estado de Tabasco",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 4002,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia de Extinción de Dominio con Competencia en la República Mexicana y Especializado en Juicios Orales Mercantiles en el Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4003,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia de Extinción de Dominio con Competencia en la República Mexicana y Especializado en Juicios Orales Mercantiles en el Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4004,
    nombreOficial:
      "Juzgado Tercero de Distrito en Materia de Extinción de Dominio con Competencia en la República Mexicana y Especializado en Juicios Orales Mercantiles en el Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4005,
    nombreOficial:
      "Juzgado Cuarto de Distrito en Materia de Extinción de Dominio con Competencia en la República Mexicana y Especializado en Juicios Orales Mercantiles en el Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4006,
    nombreOficial:
      "Juzgado Quinto de Distrito en Materia de Extinción de Dominio con Competencia en la República Mexicana y Especializado en Juicios Orales Mercantiles en el Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4007,
    nombreOficial:
      "Juzgado Sexto de Distrito en Materia de Extinción de Dominio con Competencia en la República Mexicana y Especializado en Juicios Orales Mercantiles en el Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4009,
    nombreOficial:
      "Juez Noveno de Control del Centro Nacional de Justicia Especializado en Control de Técnicas de Investigación, Arraigo e Intervención de Comunicaciones, con residencia en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4010,
    nombreOficial:
      "Juez Décimo de Control del Centro Nacional de Justicia Especializado en Control de Técnicas de Investigación, Arraigo e Intervención de Comunicaciones, con residencia en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4012,
    nombreOficial:
      "Juzgado Decimoséptimo de Distrito en Materia Administrativa en la Ciudad de México",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4093,
    nombreOficial:
      "Quinto Tribunal Colegiado en Materia de Trabajo del Cuarto Circuito",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 4137,
    nombreOficial:
      "Juzgado Decimoséptimo de Distrito en el Estado de México, con residencia en Nezahualcóyotl",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 3,
  },
  {
    catOrganismoId: 4138,
    nombreOficial:
      "Juzgado Tercero de Distrito en Materia Administrativa, Especializado en Competencia Económica, Radiodifusión y Telecomunicaciones, con residencia en la Ciudad de México y Jurisdicción en toda la República",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4144,
    nombreOficial:
      "Juzgado de Distrito en Materia Mercantil Federal en el estado de Sinaloa, con residencia en Mazatlán",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 4145,
    nombreOficial:
      "Juzgado Quinto de Distrito en el estado de Coahuila de Zaragoza, con residencia en Saltillo",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 17,
  },
  {
    catOrganismoId: 4147,
    nombreOficial:
      "Juzgado Décimo de Distrito en el Estado de Morelos, con residencia en Cuernavaca",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 46,
  },
  {
    catOrganismoId: 4157,
    nombreOficial:
      "Juzgado Primero de Distrito en Materia de Concursos Mercantiles, con residencia en la Ciudad de México y jurisdicción en toda la República Mexicana",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4158,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia de Concursos Mercantiles, con residencia en la Ciudad de México y jurisdicción en toda la República Mexicana",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4236,
    nombreOficial:
      "Juzgado Cuarto de Distrito en Materia Administrativa en el Estado de Nuevo León",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 4237,
    nombreOficial: "Órgano de pruebas DGETD - Juzgado de Distrito",
    catCircuitoId: 109,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 4238,
    nombreOficial: "Órgano de pruebas DGETD - Tribunal Unitario de Circuito",
    catCircuitoId: 109,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 4239,
    nombreOficial: "Órgano de pruebas DGETD - Tribunal Colegiado de Circuito",
    catCircuitoId: 109,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 4240,
    nombreOficial: "Órgano de pruebas DGETD - Juzgado de Distrito Auxiliar",
    catCircuitoId: 35,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 4241,
    nombreOficial: "Órgano de pruebas DGETD - Tribunal Colegiado Auxiliar",
    catCircuitoId: 35,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 4242,
    nombreOficial: "Órgano de pruebas DGETD - Tribunal Unitario Auxiliar",
    catCircuitoId: 35,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 4243,
    nombreOficial: "Órgano de pruebas DGETD - Centro de Justicia Penal Federal",
    catCircuitoId: 109,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4259,
    nombreOficial:
      "Juzgado Decimocuarto de Distrito en el Estado de Sonora, con residencia en Hermosillo",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 4327,
    nombreOficial:
      "Vigésimo Cuarto Tribunal Colegiado en Materia Administrativa del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4328,
    nombreOficial: "Tribunal Colegiado de Apelación de Pruebas",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4339,
    nombreOficial: "Órgano de Pruebas- Tribunal Colegiado de Circuito 2",
    catCircuitoId: 109,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 4341,
    nombreOficial:
      "Primer Tribunal Colegiado de Apelación en Materias Civil, Administrativa y Especializado en Competencia Económica, Radiodifusión y Telecomunicaciones del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4342,
    nombreOficial:
      "Segundo Tribunal Colegiado de Apelación en Materias Civil, Administrativa y Especializado en Competencia Económica, Radiodifusión y Telecomunicaciones del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4343,
    nombreOficial:
      "Primer Tribunal Colegiado de Apelación en Materia Penal del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4344,
    nombreOficial:
      "Segundo Tribunal Colegiado de Apelación en Materia Penal del Primer Circuito",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4345,
    nombreOficial:
      "Primer Tribunal Colegiado de Apelación del Segundo Circuito, con residencia en Toluca",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 4346,
    nombreOficial:
      "Segundo Tribunal Colegiado de Apelación del Segundo Circuito, con residencia en Toluca",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 4347,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Segundo Circuito, con residencia en Nezahualcóyotl",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 3,
  },
  {
    catOrganismoId: 4348,
    nombreOficial:
      "Primer Tribunal Colegiado de Apelación del Tercer Circuito, con residencia en Zapopan, Jalisco",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 4349,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Cuarto Circuito, con residencia en Cadereyta, Nuevo León",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 76,
  },
  {
    catOrganismoId: 4350,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Quinto Circuito, con residencia en Hermosillo",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 4351,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Sexto Circuito, con residencia en San Andrés Cholula",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 4352,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Séptimo Circuito, con residencia en Xalapa, Veracruz",
    catCircuitoId: 7,
    catEstadoId: 30,
    catCiudadId: 16,
  },
  {
    catOrganismoId: 4353,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Octavo Circuito, con residencia en Torreón",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 18,
  },
  {
    catOrganismoId: 4354,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Noveno Circuito, con residencia en San Luis Potosí, San Luis Potosí",
    catCircuitoId: 9,
    catEstadoId: 24,
    catCiudadId: 22,
  },
  {
    catOrganismoId: 4355,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Décimo Circuito, con residencia en Villahermosa, Tabasco",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 4356,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Decimoprimer, Circuito, con residencia en Morelia",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 4357,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Decimosegundo Circuito, con residencia en Culiacán",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 27,
  },
  {
    catOrganismoId: 4358,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Decimotercer Circuito, con residencia en San Bartolo Coyotepec",
    catCircuitoId: 46,
    catEstadoId: 20,
    catCiudadId: 80,
  },
  {
    catOrganismoId: 4359,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Decimocuarto Circuito, con residencia en Mérida, Yucatán",
    catCircuitoId: 38,
    catEstadoId: 31,
    catCiudadId: 34,
  },
  {
    catOrganismoId: 4360,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Decimoquinto Circuito, con residencia en Tijuana",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 39,
  },
  {
    catOrganismoId: 4361,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Decimosexto Circuito, con residencia en Guanajuato, Guanajuato",
    catCircuitoId: 45,
    catEstadoId: 12,
    catCiudadId: 41,
  },
  {
    catOrganismoId: 4362,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Decimoséptimo Circuito, con residencia en Chihuahua",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 44,
  },
  {
    catOrganismoId: 4363,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Decimoctavo Circuito, con residencia en Xochitepec, Morelos",
    catCircuitoId: 40,
    catEstadoId: 17,
    catCiudadId: 81,
  },
  {
    catOrganismoId: 4364,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Decimonoveno Circuito, con residencia en Matamoros, Tamaulipas",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 48,
  },
  {
    catOrganismoId: 4365,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Vigésimo Circuito, con residencia en Cintalapa de Figueroa, Chiapas",
    catCircuitoId: 20,
    catEstadoId: 5,
    catCiudadId: 65,
  },
  {
    catOrganismoId: 4366,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Vigésimo Primer Circuito, con residencia en Acapulco",
    catCircuitoId: 51,
    catEstadoId: 13,
    catCiudadId: 55,
  },
  {
    catOrganismoId: 4367,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Vigésimo Segundo Circuito, con residencia en Querétaro, Querétaro",
    catCircuitoId: 53,
    catEstadoId: 22,
    catCiudadId: 57,
  },
  {
    catOrganismoId: 4368,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Vigésimo Tercer Circuito, con residencia en Zacatecas, Zacatecas",
    catCircuitoId: 56,
    catEstadoId: 32,
    catCiudadId: 59,
  },
  {
    catOrganismoId: 4369,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Vigésimo Cuarto Circuito, con residencia en Tepic, Nayarit",
    catCircuitoId: 48,
    catEstadoId: 18,
    catCiudadId: 30,
  },
  {
    catOrganismoId: 4370,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Vigésimo Quinto Circuito, con residencia en Durango, Durango",
    catCircuitoId: 52,
    catEstadoId: 10,
    catCiudadId: 21,
  },
  {
    catOrganismoId: 4371,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Vigésimo Sexto Circuito, con residencia en La Paz",
    catCircuitoId: 55,
    catEstadoId: 3,
    catCiudadId: 31,
  },
  {
    catOrganismoId: 4372,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Vigésimo Séptimo Circuito, con residencia en Cancún, Quintana Roo",
    catCircuitoId: 54,
    catEstadoId: 23,
    catCiudadId: 36,
  },
  {
    catOrganismoId: 4373,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Vigésimo Octavo Circuito, con residencia en Apizaco, Tlaxcala",
    catCircuitoId: 50,
    catEstadoId: 29,
    catCiudadId: 75,
  },
  {
    catOrganismoId: 4374,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Vigésimo Noveno Circuito, con residencia en Pachuca, Hidalgo",
    catCircuitoId: 49,
    catEstadoId: 14,
    catCiudadId: 58,
  },
  {
    catOrganismoId: 4375,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Trigésimo Circuito, con residencia en Aguascalientes, Aguascalientes",
    catCircuitoId: 30,
    catEstadoId: 1,
    catCiudadId: 60,
  },
  {
    catOrganismoId: 4376,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Trigésimo Primer Circuito, con residencia en San Francisco de Campeche, Campeche",
    catCircuitoId: 47,
    catEstadoId: 4,
    catCiudadId: 82,
  },
  {
    catOrganismoId: 4377,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Trigésimo Segundo Circuito, con residencia en Colima",
    catCircuitoId: 109,
    catEstadoId: 8,
    catCiudadId: 7,
  },
  {
    catOrganismoId: 4378,
    nombreOficial: "Tribunal Colegiado de Apelación_Pruebas2",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4379,
    nombreOficial: "Tribunal Colegiado de Apelación_Pruebas3",
    catCircuitoId: 1,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4382,
    nombreOficial: "PLENO REGIONAL PRUEBAS1",
    catCircuitoId: 164,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4383,
    nombreOficial: "PLENO REGIONAL PRUEBAS2",
    catCircuitoId: 165,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4384,
    nombreOficial: "PLENO REGIONAL PRUEBAS3",
    catCircuitoId: 166,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4385,
    nombreOficial:
      "Pleno Regional en Materias Penal y de Trabajo de la Región Centro-Norte, con residencia en la Ciudad de México.   ",
    catCircuitoId: 164,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4386,
    nombreOficial:
      "Pleno Regional en Materias Administrativa y Civil de la Región Centro-Norte, con residencia en la Ciudad de México.   ",
    catCircuitoId: 164,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4387,
    nombreOficial:
      "Pleno Regional en Materia Civil de la Región Centro-Norte, con residencia en la Ciudad de México (16/01/2023-15/01/2024)   ",
    catCircuitoId: 164,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4388,
    nombreOficial:
      "Pleno Regional en Materia de Trabajo de la Región Centro-Norte, con residencia en Monterrey, Nuevo León (16/01/2023-15/01/2024)   ",
    catCircuitoId: 164,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4389,
    nombreOficial:
      "Pleno Regional en Materia Penal de la Región Centro-Sur, con residencia en San Andrés Cholula, Puebla (16/01/2023-15/01/2024)   ",
    catCircuitoId: 165,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4390,
    nombreOficial:
      "Pleno Regional en Materia Administrativa de la Región Centro-Sur, con residencia en Cuernavaca, Morelos (16/01/2023-15/01/2024)   ",
    catCircuitoId: 165,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4391,
    nombreOficial:
      "Pleno Regional en Materia Civil de la Región Centro-Sur, con residencia en Guadalajara, Jalisco (16/01/2023-15/01/2024)   ",
    catCircuitoId: 165,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4392,
    nombreOficial:
      "Pleno Regional en Materias Penal y de Trabajo de la Región Centro-Sur, con residencia en la Ciudad de México.   ",
    catCircuitoId: 165,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4393,
    nombreOficial:
      "Pleno Regional Especializado en Competencia Económica, Radiodifusión y Telecomunicaciones",
    catCircuitoId: 166,
    catEstadoId: 9,
    catCiudadId: 1,
  },
  {
    catOrganismoId: 4396,
    nombreOficial:
      "Segundo Tribunal Colegiado de Apelación del Tercer Circuito, con residencia en Zapopan, Jalisco",
    catCircuitoId: 3,
    catEstadoId: 15,
    catCiudadId: 70,
  },
  {
    catOrganismoId: 4397,
    nombreOficial:
      "Segundo Tribunal Colegiado en Materia Civil del Decimosegundo Circuito",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 28,
  },
  {
    catOrganismoId: 4399,
    nombreOficial:
      "Tribunal Colegiado de Apelación del Decimoséptimo Circuito, con residencia en Ciudad Juárez",
    catCircuitoId: 44,
    catEstadoId: 6,
    catCiudadId: 45,
  },
  {
    catOrganismoId: 4401,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia Mercantil Federal en el Estado de Tabasco",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 4403,
    nombreOficial:
      "Juzgado Sexto de Distrito en el Estado de Yucatán, con residencia en Mérida",
    catCircuitoId: 38,
    catEstadoId: 31,
    catCiudadId: 34,
  },
  {
    catOrganismoId: 4405,
    nombreOficial:
      "Segundo Tribunal Colegiado del Vigésimo Sexto Circuito, con residencia en La Paz, Baja California Sur",
    catCircuitoId: 55,
    catEstadoId: 3,
    catCiudadId: 31,
  },
  {
    catOrganismoId: 4408,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia de Trabajo del Segundo Circuito",
    catCircuitoId: 2,
    catEstadoId: 11,
    catCiudadId: 2,
  },
  {
    catOrganismoId: 4409,
    nombreOficial:
      "Juzgado de Distrito en Materia Mercantil Federal en el Estado de Michoacán, con residencia en Morelia",
    catCircuitoId: 41,
    catEstadoId: 16,
    catCiudadId: 25,
  },
  {
    catOrganismoId: 4416,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia Mercantil Federal en el Estado de Baja California, con residencia en Mexicali",
    catCircuitoId: 42,
    catEstadoId: 2,
    catCiudadId: 38,
  },
  {
    catOrganismoId: 4418,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materias Penal y de Trabajo del Décimo Noveno Circuito",
    catCircuitoId: 39,
    catEstadoId: 28,
    catCiudadId: 50,
  },
  {
    catOrganismoId: 4420,
    nombreOficial:
      "Juzgado Segundo de Distrito en Materia Mercantil Federal en el Estado de Puebla, Especializado en Juicios Orales",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 4422,
    nombreOficial:
      "Juzgado Décimo de Distrito en Materia de Amparo Civil, Administrativo y de Trabajo y de Juicios Federales en el Estado de Puebla",
    catCircuitoId: 6,
    catEstadoId: 21,
    catCiudadId: 69,
  },
  {
    catOrganismoId: 4426,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materia de Trabajo del Décimo Circuito, con residencia en Villahermosa, Tabasco",
    catCircuitoId: 10,
    catEstadoId: 27,
    catCiudadId: 23,
  },
  {
    catOrganismoId: 4428,
    nombreOficial:
      "Centro de Justicia Penal Federal con residencia en los Mochis, Sinaloa",
    catCircuitoId: 43,
    catEstadoId: 25,
    catCiudadId: 29,
  },
  {
    catOrganismoId: 4431,
    nombreOficial: "Tercer Tribunal Colegiado del Vigésimo Cuarto Circuito",
    catCircuitoId: 48,
    catEstadoId: 18,
    catCiudadId: 30,
  },
  {
    catOrganismoId: 4438,
    nombreOficial:
      "Juzgado Cuarto de Distrito en Materia Penal en el Estado de Nayarit, con residencia en el Rincón, Municipio de Tepic",
    catCircuitoId: 48,
    catEstadoId: 18,
    catCiudadId: 30,
  },
  {
    catOrganismoId: 4439,
    nombreOficial:
      "Juzgado Cuarto de Distrito en Materia de Amparo Civil, Administrativo y de Trabajo y de Juicios Federales en el Estado de Nayarit",
    catCircuitoId: 48,
    catEstadoId: 18,
    catCiudadId: 30,
  },
  {
    catOrganismoId: 4564,
    nombreOficial:
      "Juzgado de Distrito en Materia Mercantil Federal en el Estado de Nuevo León, con residencia en Monterrey",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 4565,
    nombreOficial:
      "Juzgado Sexto de Distrito en Materias Civil y de Trabajo en el Estado de Nuevo León, con residencia en Monterrey",
    catCircuitoId: 4,
    catEstadoId: 19,
    catCiudadId: 8,
  },
  {
    catOrganismoId: 4571,
    nombreOficial:
      "Cuarto Tribunal Colegiado en Materias Penal y Administrativa del Quinto Circuito",
    catCircuitoId: 5,
    catEstadoId: 26,
    catCiudadId: 9,
  },
  {
    catOrganismoId: 4575,
    nombreOficial:
      "Tercer Tribunal Colegiado en Materias Civil y de Trabajo del Octavo Circuito, con residencia en Torreón, Coahuila de Zaragoza",
    catCircuitoId: 8,
    catEstadoId: 7,
    catCiudadId: 18,
  },
  {
    catOrganismoId: 4612,
    nombreOficial:
      "Pleno Regional en Materias Administrativa y Civil de la Región Centro-Sur, con residencia en la Ciudad de México.   ",
    catCircuitoId: 165,
    catEstadoId: 9,
    catCiudadId: 1,
  },
];


const circuitos = [
  { label: "PRIMER CIRCUITO", value: 1 },
  { label: "SEGUNDO CIRCUITO", value: 2 },
  { label: "TERCER CIRCUITO", value: 3 },
  { label: "CUARTO CIRCUITO", value: 4 },
  { label: "QUINTO CIRCUITO", value: 5 },
  { label: "SEXTO CIRCUITO", value: 6 },
  { label: "SÉPTIMO CIRCUITO", value: 7 },
  { label: "OCTAVO CIRCUITO", value: 8 },
  { label: "NOVENO CIRCUITO", value: 9 },
  { label: "DÉCIMO CIRCUITO", value: 10 },
  { label: "DÉCIMO PRIMER CIRCUITO", value: 41 },
  { label: "DÉCIMO SEGUNDO CIRCUITO", value: 43 },
  { label: "DÉCIMO TERCER CIRCUITO", value: 46 },
  { label: "DÉCIMO CUARTO CIRCUITO", value: 38 },
  { label: "DÉCIMO QUINTO CIRCUITO", value: 42 },
  { label: "DÉCIMO SEXTO CIRCUITO", value: 45 },
  { label: "DÉCIMO SÉPTIMO CIRCUITO", value: 44 },
  { label: "DÉCIMO OCTAVO CIRCUITO", value: 40 },
  { label: "DÉCIMO NOVENO CIRCUITO", value: 39 },
  { label: "VIGÉSIMO CIRCUITO", value: 20 },
  { label: "VIGÉSIMO PRIMER CIRCUITO", value: 51 },
  { label: "VIGÉSIMO SEGUNDO CIRCUITO", value: 53 },
  { label: "VIGÉSIMO TERCER CIRCUITO", value: 56 },
  { label: "VIGÉSIMO CUARTO CIRCUITO", value: 48 },
  { label: "VIGÉSIMO QUINTO CIRCUITO", value: 52 },
  { label: "VIGÉSIMO SEXTO CIRCUITO", value: 55 },
  { label: "VIGÉSIMO SÉPTIMO CIRCUITO", value: 54 },
  { label: "VIGÉSIMO OCTAVO CIRCUITO", value: 50 },
  { label: "VIGÉSIMO NOVENO CIRCUITO", value: 49 },
  { label: "TRIGÉSIMO CIRCUITO", value: 30 },
  { label: "TRIGÉSIMO PRIMER CIRCUITO", value: 47 },
  { label: "TRIGÉSIMO SEGUNDO CIRCUITO", value: 109 },
];

const estados = [
  { label: "Aguascalientes", value: 1 },
  { label: "Baja California", value: 2 },
  { label: "Baja California Sur", value: 3 },
  { label: "Campeche", value: 4 },
  { label: "Chiapas", value: 5 },
  { label: "Chihuahua", value: 6 },
  { label: "Coahuila", value: 7 },
  { label: "Colima", value: 8 },
  { label: "Ciudad de México", value: 9 },
  { label: "Durango", value: 10 },
  { label: "Estado de México", value: 11 },
  { label: "Guanajuato", value: 12 },
  { label: "Guerrero", value: 13 },
  { label: "Hidalgo", value: 14 },
  { label: "Jalisco", value: 15 },
  { label: "Michoacán", value: 16 },
  { label: "Morelos", value: 17 },
  { label: "Nayarit", value: 18 },
  { label: "Nuevo León", value: 19 },
  { label: "Oaxaca", value: 20 },
  { label: "Puebla", value: 21 },
  { label: "Querétaro", value: 22 },
  { label: "Quintana Roo", value: 23 },
  { label: "San Luis Potosí", value: 24 },
  { label: "Sinaloa", value: 25 },
  { label: "Sonora", value: 26 },
  { label: "Tabasco", value: 27 },
  { label: "Tamaulipas", value: 28 },
  { label: "Tlaxcala", value: 29 },
  { label: "Veracruz", value: 30 },
  { label: "Yucatán", value: 31 },
  { label: "Zacatecas", value: 32 },
];

const ciudades = [
  { label: "Acapulco", value: 55 },
  { label: "Agua Prieta", value: 68 },
  { label: "Aguascalientes", value: 60 },
  { label: "Apizaco", value: 75 },
  { label: "Boca del Río", value: 14 },
  { label: "Cadereyta Jiménez", value: 76 },
  { label: "Campeche", value: 35 },
  { label: "Cancún", value: 36 },
  { label: "Celaya", value: 43 },
  { label: "Chetumal", value: 37 },
  { label: "Chihuahua", value: 44 },
  { label: "Chilpancingo", value: 54 },
  { label: "Cintalapa de Figueroa", value: 65 },
  { label: "Ciudad del Carmen", value: 78 },
  { label: "Ciudad Juárez", value: 45 },
  { label: "Ciudad Madero", value: 77 },
  { label: "Ciudad Nezahualcóyotl", value: 3 },
  { label: "Ciudad Obregón", value: 10 },
  { label: "Ciudad Valles", value: 62 },
  { label: "Ciudad Victoria", value: 50 },
  { label: "Coatzacoalcos", value: 24 },
  { label: "Colima", value: 7 },
  { label: "Córdoba", value: 64 },
  { label: "Cuernavaca", value: 46 },
  { label: "Culiacán", value: 27 },
  { label: "Durango", value: 21 },
  { label: "Ensenada", value: 40 },
  { label: "Guadalajara", value: 6 },
  { label: "Guadalupe", value: 66 },
  { label: "Guanajuato", value: 41 },
  { label: "Hermosillo", value: 9 },
  { label: "Iguala", value: 56 },
  { label: "Irapuato", value: 71 },
  { label: "La Paz", value: 31 },
  { label: "León", value: 42 },
  { label: "Los Cabos", value: 72 },
  { label: "Los Mochis", value: 29 },
  { label: "Matamoros", value: 48 },
  { label: "Mazatlán", value: 28 },
  { label: "Mérida", value: 34 },
  { label: "Mexicali", value: 38 },
  { label: "México", value: 1 },
  { label: "Monclova", value: 20 },
  { label: "Monterrey", value: 8 },
  { label: "Morelia", value: 25 },
  { label: "Naucalpan de Juárez", value: 5 },
  { label: "Nogales", value: 11 },
  { label: "Nuevo Laredo", value: 47 },
  { label: "Oaxaca", value: 32 },
  { label: "Pachuca de Soto", value: 58 },
  { label: "Perote", value: 73 },
  { label: "Piedras Negras", value: 19 },
  { label: "Poza Rica", value: 63 },
  { label: "Puebla", value: 12 },
  { label: "Puente Grande", value: 67 },
  { label: "Querétaro", value: 57 },
  { label: "Reynosa", value: 51 },
  { label: "Salina Cruz", value: 33 },
  { label: "Saltillo", value: 17 },
  { label: "San Andrés Cholula", value: 69 },
  { label: "San Bartolo Coyotepec", value: 80 },
  { label: "San Francisco de Campeche", value: 82 },
  { label: "San Luis Potosí", value: 22 },
  { label: "Tampico", value: 49 },
  { label: "Tapachula", value: 53 },
  { label: "Tepic", value: 30 },
  { label: "Tijuana", value: 39 },
  { label: "Tlalnepantla", value: 4 },
  { label: "Tlaxcala", value: 13 },
  { label: "Toluca", value: 2 },
  { label: "Torreón", value: 18 },
  { label: "Tuxpan", value: 15 },
  { label: "Tuxtla Gutiérrez", value: 52 },
  { label: "Uruapan", value: 26 },
  { label: "Veracruz", value: 61 },
  { label: "Villa Aldama", value: 74 },
  { label: "Villahermosa", value: 23 },
  { label: "Xalapa", value: 16 },
  { label: "Xochitepec", value: 81 },
  { label: "Zacatecas", value: 59 },
  { label: "Zapopan", value: 70 },
  { label: "Zihuatanejo de Azueta", value: 79 },
];

const regiones = [
  { label: "PRIMERA REGIÓN (D.F.)", value: "57" },
  { label: "Región Centro-Norte", value: "164" },
  { label: "Región Centro-Sur", value: "165" },
  { label: "Pleno Regional Especializado CERYT", value: "166" },
  { label: "PRIMERA REGIÓN (EDO. MÉXICO)", value: "113" },
  { label: "TERCERA REGIÓN (JALISCO)", value: "115" },
  { label: "SEGUNDA REGIÓN", value: "33" },
  { label: "CUARTA REGIÓN", value: "31" },
  { label: "DÉCIMA REGIÓN", value: "108" },
  { label: "DÉCIMOPRIMERA REGIÓN", value: "112" },
  { label: "TERCERA REGIÓN (MICHOACAN)", value: "116" },
  { label: "QUINTA REGIÓN", value: "32" },
  { label: "TERCERA REGIÓN (GUANAJUATO)", value: "35" },
  { label: "SEXTA REGIÓN", value: "34" },
  { label: "PRIMERA REGIÓN (MORELOS)", value: "114" },
  { label: "SÉPTIMA REGIÓN", value: "36" },
  { label: "NOVENA REGIÓN", value: "111" },
  { label: "OCTAVA REGIÓN", value: "110" },
  { label: "DÉCIMO SEGUNDA REGIÓN", value: "163" },
];

export {
  circuitos,
  estados,
  ciudades,
  regiones,
  organismos
};
