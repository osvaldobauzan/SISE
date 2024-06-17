import { ref } from 'vue';

export class FormPromoventes {
  nombre = ref(String);
  descripcion = ref(String);
  tipo = ref(String);
  status = ref(0);
  clasePromovente = ref(0);
  tipoDePromovente = ref(0);
}
