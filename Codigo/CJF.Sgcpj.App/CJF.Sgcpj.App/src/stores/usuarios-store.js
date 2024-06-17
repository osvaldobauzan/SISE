import { defineStore } from "pinia";
import { apiUsuarios } from "../boot/axios";
import { Secretario } from "../data/secretario";
import { AutoridadJudicial } from "../data/autoridad-judicial";
import { ParteExistente } from "../data/parte-existente";
import { PromoventeExistente } from "../data/promovente-existente";
import { AutoridadExistente } from "../data/autoridad-existente";

export const useUsuariosStore = defineStore("usuariosStore", {
  state: () => ({
    secretarios: Array(new Secretario()),
    autoridadJudicial: Array(new AutoridadJudicial()),
    autoridadJudicialAfterFindExpediente: Array(new AutoridadJudicial()),
    parteExistente: new Array(new ParteExistente()).splice(0, 0),
    promoventeExistente: new Array(new PromoventeExistente()).splice(0, 0),
    autoridadXExpediente: new Array(new AutoridadExistente()).splice(0, 0),
    autoridadNuevaStore: false,
  }),
  actions: {
    async obtenerSecretarios() {
      this.secretarios = [];
      this.secretarios = (await apiUsuarios.get("api/secretario"))?.data;
    },
    async obtenerAutoridadJudicial(texto) {
      this.autoridadJudicial = [];
      this.autoridadJudicial = (
        await apiUsuarios.get("api/autoridadjudicial", {
          params: { nombre: texto },
        })
      )?.data;
    },
    async obtenerParteExistente(
      asuntoNeunId,
      noExp = null,
      modulo = 1,
      tipoParte = 1,
    ) {
      this.parteExistente = [];
      this.parteExistente = (
        await apiUsuarios.get("api/parteexistente", {
          params: {
            AsuntoNeunId: asuntoNeunId,
            NoExp: noExp,
            modulo: modulo,
            tipoParte: tipoParte,
          },
          cache: false,
        })
      )?.data;
      return this.parteExistente;
    },
    async obtenerPromoventeExistente(asuntoNeunId) {
      this.promoventeExistente = [];
      this.promoventeExistente = (
        await apiUsuarios.get("api/promoventeexistente", {
          params: { AsuntoNeunId: asuntoNeunId },
          cache: false,
        })
      )?.data?.map((x) => {
        x.label = `${x.nombre} ${x.aPaterno} ${x.aMaterno || ""} - ${
          x.promoventeTipo
        }`;
        return x;
      });
    },
    async obtenAutoridad(
      asuntoNeunId,
      noExp = null,
      modulo = 1,
      tipoParte = 1,
    ) {
      this.autoridadXExpediente = [];
      this.autoridadXExpediente = (
        await apiUsuarios.get("api/autoridad", {
          params: {
            AsuntoNeunId: asuntoNeunId,
            NoExp: noExp,
            modulo: modulo,
            tipoParte: tipoParte,
          },
          cache: false,
        })
      )?.data;
    },
    async obtenerSecretarioSugerido(asuntoNeunId) {
      const secretario = (
        await apiUsuarios.get("api/secretario/sugerido", {
          params: {
            AsuntoNeunId: asuntoNeunId,
          },
          cache: false,
        })
      )?.data;
      return secretario;
    },
    async obtenAutoridadXExpediente(asuntoNeunId, noExp) {
      this.autoridadJudicial = [];
      this.autoridadJudicial = (
        await apiUsuarios.get("api/autoridadjudicialasunto", {
          params: {
            AsuntoNeunId: asuntoNeunId,
            NumeroExpediente: noExp,
          },
          cache: false,
        })
      )?.data;
      this.autoridadJudicialAfterFindExpediente = this.autoridadJudicial;
    },
  },
});
