import { defineStore } from "pinia";
import { apiActuaria } from "boot/axios";
import { ResponseNotificaciones } from "../data/response-notificaciones";

export const useActuariaDetalleNotificacionesStore = defineStore(
  "actuariaDetalleNotificacionesStore",
  {
    state: () => ({
      notificaciones: new ResponseNotificaciones(),
      detalleAcuerdo: [],
      actuarioNotificaciones: new ResponseNotificaciones(),
    }),
    getters: {},
    actions: {
      async getDetalleAcuerdo(params) {
        this.detalleAcuerdo = (
          await apiActuaria.get("api/actuaria/acuerdodetalle", {
            params: {
              asuntoNeunId: params.asuntoNeunId,
              sintesisOrden: params.sintesisOrden,
              asuntoDocumentoId: params.asuntoDocumentoId,
            },
          })
        )?.data;
      },
      async getNotificaciones(params) {
        this.notificaciones = Object.assign({}, {});
        this.notificaciones = (
          await apiActuaria.get("api/actuaria/notificaciones", {
            params: {
              filtroTipo: params.status,
              //parametros paginacion
              texto: params.text?.trim() || "",
              ordenarPor: params.sortBy,
              tipoOrden: params.descending,
              numeroPagina: params.page,
              tamanioPagina: params.rowsPerPage, //este va ir en 0 cuando sea todos
              asuntoNeunId: params.asuntoNeunId,
              asuntoDocumentoID: params.asuntoDocumentoID,
              primeraCarga: params.primeraCargaNotificaciones,
              filtroTipoParteID: params.valoresFiltros?.filtroTipoParteID,
              filtroTipoNotificacionID:
                params.valoresFiltros?.filtroTipoNotificacionID,
              filtroActuarioID: params.valoresFiltros?.filtroActuarioID,
            },
          })
        )?.data;
      },
      async getNotificacionesFiltros() {
        const Filtrosnotificaciones = (
          await apiActuaria.get("api/actuaria/notificaciones/filtros")
        )?.data;
        return Filtrosnotificaciones;
      },
      async getNotificacionesPorActuario(params) {
        this.actuarioNotificaciones = Object.assign({}, {});
        this.actuarioNotificaciones = (
          await apiActuaria.get("api/actuaria/actuarionotificaciones", {
            params: {
              fechaInicial: params.from,
              fechaFinal: params.to,
              filtroTipo: params.status,
              texto: params.text?.trim() || "",
              ordenarPor: params.sortBy,
              tipoOrden: params.descending,
              numeroPagina: params.page,
              tamanioPagina: params.rowsPerPage, //este va ir en 0 cuando sea todos
              filtroTipoParteID: params.valoresFiltros?.filtroTipoParteID,
              filtroTipoNotificacionID:
                params.valoresFiltros?.filtroTipoNotificacionID,
              filtroActuarioID: params.valoresFiltros?.filtroActuarioID,
            },
          })
        )?.data;
      },
      /**
       * Asignar a actuario
       * @param {*} params objeto con parametros para asignar
       * @returns true or false
       */
      async postAgregarActuario(params) {
        const correcto = await apiActuaria.post(
          "api/actuaria/actuario",
          params,
        );
        return correcto.data;
      },
      /**
       * Asignar a actuario masivo
       * @param {*} params objeto con parametros para asignar
       * @returns true or false
       */
      async postAgregarActuarioMasivo(params) {
        const correcto = await apiActuaria.post(
          "api/actuaria/actuariomultiple",
          params,
        );
        return correcto.data;
      },
      /**
       * Asignar a actuario
       * @param {*} params objeto con parametros para asignar
       * @returns true or false
       */
      async putAgregarActuario(params) {
        const correcto = await apiActuaria.put("api/actuaria/actuario", params);
        return correcto.data;
      },
      async subirAcuse(params) {
        const correcto = await apiActuaria.post(
          "api/actuaria/notificaciones/acuse",
          params,
        );
        return correcto.data;
      },
      async subirAcuseMasivo(params) {
        const correcto = await apiActuaria.post(
          "api/actuaria/notificaciones/acusemultiple",
          params,
        );
        return correcto.data;
      },
    },
  },
);
