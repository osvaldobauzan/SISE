import { defineStore } from "pinia";
import { apiSentencias, apiTramites } from "boot/axios";
import { apiOficialia } from "boot/axios";

export const useFirmadorStore = defineStore("firmadorStore", {
  state: () => ({}),
  getters: {},
  actions: {
    async obtenerURLGrafico(params) {
        const respuesta = await apiTramites.post("api/firmador/inicio", params);
        return respuesta.data;
    },
    async obtenerStatusTransaccion() {
      const respuesta = await apiTramites.get(
        "api/firmador/estado/documentos",
        {
          params: { id: localStorage.getItem("cveTransaccion") },
        },
      );
      return respuesta.data;
    },
    async obtenerURLGraficoOficialia(params){
      const respuesta = await apiOficialia.post("api/firmadoroficialia/inicio", params);
      return respuesta.data;
    },
    async obtenerURLGraficoSentencias(params){
      const respuesta = await apiSentencias.post("api/firmadorsentencias/inicio", params);
      return respuesta.data;
    }
  },
});
