import { defineStore } from "pinia";
// import { tabla } from "../data/actuariaPage.js";
import { apiActuaria } from "boot/axios";
import { filter, uniqBy } from "lodash";

export const useActuariaStore = defineStore("actuariaStore", {
  state: () => ({
    dataActuaria: [],
    notificaciones: [],
    acuerdos: [],
    acuses: [],
  }),
  getters: {},
  actions: {
    async getAcuerdos(params) {
      this.dataActuaria = Object.assign({}, {});
      this.dataActuaria = (
        await apiActuaria.get("api/actuaria", {
          params: {
            fechaInicial: params.from,
            fechaFinal: params.to,
            tipoFiltro: params.typeFilter,
            //parametros paginacion
            texto: params.text?.trim() || "",
            ordenarPor: params.sortBy,
            descendente: params.descending,
            pagina: params.page,
            registrosPorPagina: params.rowsPerPage, //este va ir en 0 cuando sea todos
            estado: params.valoresFiltros?.estado,
            contenido: params.valoresFiltros?.contenido,
          },
        })
      )?.data;
      return this.dataActuaria;
    },
    getNotificaciones(neun) {
      const asunto = filter(this.acuerdos, function (item) {
        return item.AsuntoNeunId === neun;
      });
      if (asunto.length === 0) return [];
      this.notificaciones = asunto[0].Notificaciones;
    },
    getActuarios() {
      return uniqBy(
        this.notificaciones.map(({ Actuario }) => ({ Actuario })),
        "Actuario",
      );
    },
    getQuejosos() {
      return uniqBy(
        this.notificaciones.map(({ Parte }) => ({ Parte })),
        "Actuario",
      );
    },
    getCaracter() {
      return uniqBy(
        this.notificaciones.map(({ Caracter }) => ({ Caracter })),
        "Actuario",
      );
    },
    addAcuerdo(expediente) {
      expediente.id = expediente.expediente.asuntoNeunId;
      this.acuerdos.push(expediente);
    },

    addNotificacion(expediente) {
      expediente.id = expediente.expediente.asuntoNeunId;
      this.notificaciones.push(expediente);
    },

    delAcuerdo(index) {
      this.acuerdos.splice(index, 1);
    },
    async obtenerCatalogosFiltros() {
      const filtros = (await apiActuaria.get("api/actuaria/filtros"))?.data;
      return filtros;
    },

    // Comunicaciones Oficiales Enviadas
    async insertarCOE(coe) {
      const bool = await apiActuaria.post("api/actuaria/agregarCOE", coe);
      return bool;
    },

    async consultarCOE(NotificacionElectronicaId) {
      const infoCOE = (
        await apiActuaria.get("api/actuaria/obtenerCOE", {
          params: {
            NotificacionElectronicaId: NotificacionElectronicaId,
          },
        })
      )?.data;
      return infoCOE;
    },

    async obtenerArchivoB64(params) {
      const archivoB64 = (
        await apiActuaria.get("api/actuaria/documento", {
          params: params,
        })
      )?.data;
      return archivoB64;
    },

    async obtenerAcusesNas(params) {
      const acuse = (
        await apiActuaria.get("api/actuaria/archivos", {
          params: params,
        })
      )?.data;
      this.acuses.push(acuse);
    },
  },
});
