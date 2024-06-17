import { defineStore } from "pinia";
import { apiActuaria } from "src/boot/axios";

export const useActuariaOficiosStore = defineStore("actuariaOficiosStore", {
  state: () => ({
    oficios: [],
  }),
  actions: {
    addOficio(expediente) {
      expediente.id = expediente.expediente.asuntoNeunId;
      this.oficios.push(expediente);
    },

    delOficio(index) {
      this.oficios.splice(index, 1);
    },
    async buscarOficio(folio) {
      const response = await apiActuaria.get("api/actuaria/oficios", {
        params: { folio: folio },
      });
      return response?.data;
    },
    async recibirOficio(params) {
      const response = await apiActuaria.post("api/actuaria/oficios", params);
      return response?.data;
    },
  },
});
