import { ref } from "vue";
import { Proyecto } from "./proyecto";
export class ResponseProyectos {
  constructor() {
    this.datos.value = [];
  }
  datos = ref(Array(new Proyecto()));
  metaDatos = {
    totalProyectos: 0,
    totalSinProyecto: 0,
    totalParaRevision: 0,
    totalNoAprobado: 0,
    totalConAjustes: 0,
    totalAprobado: 0,
  };
  pagina = 1;
  totalPaginas = 0;
  totalRegistros = 0;
}
