import { ref } from "vue";
import { AutoridadJudicial } from "src/data/autoridad-judicial";
import { CatalogoAsunto } from "src/data/catalogo-asunto";
export class FormSolicitarVinculacion {
  constructor() {
    this.organoJurisddicional = null;
    this.tipoAsunto = null;
  }
  organoJurisddicional = ref(new AutoridadJudicial());
  tipoAsunto = ref(new CatalogoAsunto());
}
