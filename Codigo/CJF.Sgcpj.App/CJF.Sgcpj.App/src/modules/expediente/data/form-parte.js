export class FormParte {
  constructor() {}
  personaId = 0;
  asuntoNeunId = 0;
  usuarioCaptura = 0;
  idOrganoPlenos = 0;
  personaAsunto = new PersonaAsunto();
}

export class PersonaAsunto {
  constructor() {}
  nombre = "";
  aPaterno = "";
  aMaterno = "";
  catTipoPersonaId = 1;
  catCaracterPersonaAsuntoId = 0; //mapear json en guardado
  sexo = 0; //mapear json en guardado
  mayorEdad = 0;
  catTipoPersonaJuridicaId = 0; //mapear json en guardado
  denominacionDeAutoridad = "";
  clasificaAutoridadGenericaId = 0; //mapear json en guardado
  sujetoDerechoAgrario = 0;
  aceptaOponePublicarDatos = 0;
  fechaAceptaOponePublicarDatos = "";
  foraneo = 0;
  recurrente = 0;
  caracterPromueveNombre = 0; //mapear json en guardado
  victimaOfendidoDelito = 0;
  parteAdhesivaApelacion = 0;
  alias = "";
  esParteGrupoVulnerable = 0;
  grupoVulnerable = 0; //mapear json en guardado
  edadMenor = 0; //mapear json en guardado
  hablaLengua = 0;
  lengua = 0; //mapear json en guardado
  traductor = 0;
}
