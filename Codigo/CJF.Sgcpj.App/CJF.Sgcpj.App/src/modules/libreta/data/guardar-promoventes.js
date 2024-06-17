import { ref } from "vue";

export class GuardarPromoventes {
  asuntoNeunId = ref(0);
  tipo = ref(0);
  nombre = ref("");
  aPaterno = ref("");
  aMaterno = "";
  personaId = 0; //se obtiene de personas asuntos
  registroEmpleadoId = 0;
  numeroOrden = ref(0);
}
