import { defineStore } from "pinia";
import { apiAlertas } from "boot/axios";

export const useAlertasStore = defineStore("alertas", {
  state: () => ({
    alertas: [],
  }),
  actions: {
    async obtenerAlertasPorUsuario() {
      this.alertas = (await apiAlertas.get(`api/alertas`))?.data || [];
    },
    async eliminarAlerta(idAlerta) {
      await apiAlertas.delete(`api/alertas?alertaId=${idAlerta}`);
    },
    async postConexion(idConexion) {
      await apiAlertas.post(`api/connections`, {
        currentConnection: idConexion,
        pastConnection: localStorage.getItem("wsConnection") || null,
      });
      localStorage.setItem("wsConnection", idConexion);
    },
  },
});
