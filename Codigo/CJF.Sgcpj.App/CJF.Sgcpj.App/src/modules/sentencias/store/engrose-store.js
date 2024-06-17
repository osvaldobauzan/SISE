import { defineStore } from 'pinia';
import { Sentencia, SentenciaVP } from '../data/sentencias';
import { apiSentencias } from 'src/boot/axios';

export const useEngroseStore = defineStore('EngroseStore', {
  state: () => ({
    sentenciaData: new Sentencia(),
    sentenciaVP: new SentenciaVP(),
    file: null,
  }),
  actions: {
    async addSentencia(data) {
      let resultado = [];
      resultado = (
        await apiSentencias.post("api/sentencias", data, {
          headers: { "Content-Type": "multipart/form-data" },
        })
      ).data;
      return resultado;
    }
  },
});
