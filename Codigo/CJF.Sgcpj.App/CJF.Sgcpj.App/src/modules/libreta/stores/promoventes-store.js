import { defineStore } from "pinia";
import { apiPromoventes } from "boot/axios";

export const usePromoventesStore = defineStore("promoventes", {
  state: () => ({
    personaId: 0,
  }),
  actions: {
    /**
     * Crea promovente
     * @param {*} params objecto promovente
     */
    async crearPromovente(params) {
      await apiPromoventes.post("api/promoventes", params);
    },
    /**
     * Crea una Autoridad Judicial
     * @param {*} params objecto Autordad Judicial
     */
    async crearAutoridadJudicial(params) {
      await apiPromoventes.post("api/autoridadjudicial", params);
    },
    /**
     * Crea personas asunto de los promoventes
     * @param {*} params objecto personas asunto
     */
    async crearPersonasAsunto(params) {
      this.personaId = 0;
      this.personaId = (
        await apiPromoventes.post("api/personas", params)
      )?.data;
    },
  },
});
