import { ref } from "vue";
import { Promocion } from "./promocion";
export class ResponsePromociones {
  constructor() {
    this.datos.value = [];
  }
  datos = ref(Array(new Promocion()));
  metaDatos = {
    totalPromociones: 0,
    totalSinCaptura: 0,
    totalCapturadas: 0,
    enviadasAMesa: 0,
  };
  pagina = 1;
  totalPaginas = 0;
  totalRegistros = 0;
}
