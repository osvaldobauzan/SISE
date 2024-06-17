import { CatalogoAnexos } from "../../../data/catalogo-anexos";
import { ref } from "vue";

export class FormAnexos {
  constructor() {
    this.descripcion = null;
    this.tipoAnexo = null;
    this.caracter = null;
  }
  id = ref(0);
  descripcion = ref(new CatalogoAnexos());
  tipoAnexo = ref(new CatalogoAnexos());
  caracter = ref(new CatalogoAnexos());
  file = ref(null);
  guardadoEnBD = ref(false);
  consecutivo = ref(0);
  archivoBase64 = ref("");
  nombreArchivo = ref("");
}
