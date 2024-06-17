import { ref } from "vue";

export class ArchivosAnexos {
  archivos = ref(Array(new ArchivoVm()));
  anexos = ref(Array(new AnexoVm()));
}

export class ArchivoVm {
  nombre = ref("archivo");
  descripcion = ref("");
  ruta = ref("");
}
export class AnexoVm {
  nombre = ref("anexo");
  descripcion = ref("");
  ruta = ref("");
}
