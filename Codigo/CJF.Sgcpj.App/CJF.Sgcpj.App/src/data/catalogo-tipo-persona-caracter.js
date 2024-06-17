import { ref } from "vue";

export class CatalogoTipoPersonaCaracter {
  caracterPersonaId = ref(Number);
  caracterPersona = ref(String);
  tipoAsuntoId = ref(Number);
  tipoAsunto = ref(String);
  orden = ref(Number);
}
