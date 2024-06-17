import { ref } from "vue";
export class InsertarPromocion {
  asuntoNeunId = ref(0);
  tipoCuaderno = ref(0);
  fechaPresentacion = ref("");
  horaPresentacion = ref("");
  clasePromocion = ref(0);
  clasePromovente = ref(0);
  tipoPromovente = 0;
  tipoContenido = ref(0);
  numeroCopias = ref(0);
  numeroAnexo = ref(0);
  secretario = ref(0);
  registroEmpleadoId = 0;
  observaciones = "";
  ipUsuario = "";
  origenPromocion = ref(0);
  numeroRegistro = ref(0);
  numeroOrden = 0;
  conExpedienteElectronico = false;
}
