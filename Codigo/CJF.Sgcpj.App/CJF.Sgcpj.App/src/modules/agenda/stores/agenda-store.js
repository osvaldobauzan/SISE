import { defineStore } from "pinia";
import { apiAgenda } from "src/boot/axios";
import { AgendaDto } from "../data/agendadto";

export const useAgendaStore = defineStore("agendaStore", {
  state: () => ({
    eventosAgenda: Array(new AgendaDto()),
  }),
  actions: {
    async obtenerDetalleCarcater(tipoAsuntoId, asuntoNeunId) {
      return (
        (
          await apiAgenda.get("api/caracter/detalle", {
            params: {
              TipoAsuntoId: tipoAsuntoId,
              NeunId: asuntoNeunId,
            },
          })
        )?.data[0] || {}
      );
    },
    async obtenerAgendaAudienciaPorFecha(fechaInicio= null, fechaFin = null, expediente = null, persona = null) {
      this.eventosAgenda = [];
      this.eventosAgenda =
        (
          await apiAgenda.get("api/audiencia/agenda", {
            params: {
              fechaIni: fechaInicio,
              fechaFin: fechaFin,
              expediente: expediente,
              persona: persona
            },
          })
        )?.data || [];
      return this.eventosAgenda;
    },
    async modificarEstado(params) {
      return (await apiAgenda.post("api/Estado/Audiencia", params))?.data;
    },
  },
});
