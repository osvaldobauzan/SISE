import { ref } from "vue";

export class Expediente {
  expediente = ref(String);
  tipoAsunto = ref(String);
  tipoProcedimiento = ref(String);
  tipoDocumento = ref(String);
  area = ref(String);
  userName = ref(String);
  puestoDescripcion = ref(String);
}
export class SeguimientoExpediente {
  expediente = ref(String);
  tipoAsunto = ref(String);
  tipoProcedimiento = ref(String);
  tipoDocumento = ref(String);
  area = ref(String);
  userName = ref(String);
  puestoDescripcion = ref(String);
}
export class Acuerdo {
  expediente = ref(String);
  tipoAsunto = ref(String);
  tipoDocumento = ref(String);
  fechaHora_F = ref(String);
}
export class Acuse {
  expediente = ref(String);
  tipoAsunto = ref(String);
  tipoProcedimiento = ref(String);
  tipoDocumento = ref(String);
  fechaHora_F = ref(Date);
}
export class InsertaSeguimiento {
  expediente = ref(String);
  tipoAsunto = ref(String);
  tipoDocumento = ref(String);
  fecha = ref(Date);
}
