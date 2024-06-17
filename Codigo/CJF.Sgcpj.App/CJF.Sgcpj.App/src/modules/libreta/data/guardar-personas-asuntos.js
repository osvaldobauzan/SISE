import { ref } from "vue";

export class GuardarPersonasAsuntos {
  asuntoNeunId = ref(0);
  usuarioCaptura = 0; //seteo del claims
  nombre = ref("");
  aPaterno = ref("");
  aMaterno = ref("");
  catTipoPersonaId = ref(0);
  catCaracterPersonaAsuntoId = ref(0);
  denominacionDeAutoridad = ""; //no se sabe que oruga
  personaId = 0; //output
  numeroOrden = ref(0);
}
