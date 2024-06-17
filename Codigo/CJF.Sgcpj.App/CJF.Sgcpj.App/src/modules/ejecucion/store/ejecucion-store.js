import { defineStore } from "pinia";
import { apiOficialia } from "boot/axios";
import { ejecucion } from "src/modules/ejecucion/data/ejecucionData.js";

export const useEjecucionStore = defineStore("EjecucionStore", {
  state: () => ({
    ejecucion: ejecucion,
    expediente: [],
  }),
  actions: {
    async buscarExpediente(asuntoAlias, tipoAsuntoId, modulo = 1) {
      this.expediente = Object.assign([], []);
      const valid = Validaciones.validaNoExpediente(asuntoAlias);
      if (typeof valid === "string") {
        return;
      }
      this.expediente = (
        await apiOficialia.get("api/asociarexpediente", {
          params: {
            asuntoAlias: asuntoAlias,
            catTipoAsuntoId: tipoAsuntoId,
            modulo: modulo,
          },
        })
      )?.data;
      if (this.expediente?.length > 0) {
        this.expediente = this.expediente.map((x) => {
          x.label = `${x.asuntoAlias} (${x.tipoAsunto})`;
          return x;
        });
      }
    },
  },
});
