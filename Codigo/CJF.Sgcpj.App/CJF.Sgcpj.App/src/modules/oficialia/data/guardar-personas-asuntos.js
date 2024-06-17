import { ref } from "vue";

export class GuardarPersonasAsuntos {
  asuntoNeunId = ref(0);
  usuarioCaptura = 0;
  nombre = ref("");
  aPaterno = ref("");
  aMaterno = ref("");
  catTipoPersonaId = ref(0);
  catCaracterPersonaAsuntoId = ref(0);
  denominacionDeAutoridad = ref("");
  personaId = 0;
  numeroOrden = ref(0);
}
