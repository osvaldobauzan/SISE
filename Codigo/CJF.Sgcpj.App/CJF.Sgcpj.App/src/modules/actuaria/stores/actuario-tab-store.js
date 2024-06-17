import { defineStore } from "pinia";
import { apiActuaria } from "src/boot/axios";

const DEFAULT_TAB_ACTUARIO = "actuario";

export const useActuarioTabStore = defineStore("ActuarioTabStore", {
  state: () => ({
    tabsActuarios: [],
    tabActive: DEFAULT_TAB_ACTUARIO,
    acuseOficioPdf: { Status: false, Mensaje: "" },
  }),
  actions: {
    addTabExpediente(expediente) {
      expediente.id = expediente.AsuntoNeunId;
      if (
        this.tabsActuarios.filter((tab) => tab.id === expediente.asuntoNeunId)
          .length > 0
      ) {
        this.tabActive = expediente.id;
        return;
      }
      this.tabsActuarios.push(expediente);
      this.tabActive = expediente.id;
    },
    delTabExpediente(index) {
      this.tabsActuarios.splice(index, 1);
      this.tabActive = DEFAULT_TAB_ACTUARIO;
    },

    async getAcuseOficioPorFecha(periodo, empleadoId) {
      this.acuseOficioPdf = (
        await apiActuaria.get("api/actuaria/acuseoficioactuarioporfecha", {
          params: {
            FechaInicio: periodo.from,
            FechaFin: periodo.to,
            ActuarioId: empleadoId,
          },
        })
      )?.data;
    },
  },
});
