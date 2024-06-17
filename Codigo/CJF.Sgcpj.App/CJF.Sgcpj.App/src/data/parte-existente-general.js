import { ref } from "vue";

export class ParteExistenteGeneral {
  nombre = ref(String);
  descripcion = ref(String);
  tipo = ref(String);
  status = ref(0);
  clasePromovente = ref(0);
  tipoDePromovente = ref(0);
}
