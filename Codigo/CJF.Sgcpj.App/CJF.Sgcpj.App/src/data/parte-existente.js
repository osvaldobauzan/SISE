import { ref } from "vue";

export class ParteExistente {
  descripcionTipoPersona = ref(String);
  denominacionDeAutoridad = ref(String);
  descripcionCaracterPersona = ref(String);
  descripcionClasificaAutoridadGenerica = ref(String);
  personaId = ref(Number);
  nombre = ref(String);
  aMaterno = ref(String);
  aPaterno = ref(String);
  catCaracterPersonaAsuntoId = ref(Number);
  foraneo = ref(Number);
  tipo = ref(Number);
  personaTipo = ref(String);
  noty = ref("1");
}
