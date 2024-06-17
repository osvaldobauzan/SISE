import { defineStore } from "pinia";
import { apiUsuarios } from "boot/axios";
import {
  organismos,
  circuitos,
  estados,
  ciudades,
} from "../data/organismos.js";
import _ from "lodash";

// import { apiCatalogos } from "boot/axios";

export const useBuscarParteStore = defineStore("BuscarParteStore", {
  state: () => ({
    buscarPartes: [],
    organismos: [],
    groupedResult: [],
    circuitos: circuitos,
    estados: estados,
    ciudades: ciudades,
  }),
  getters: {
    getTipoAsuntoList() {
      const tipoAsuntos = Object.keys(this.groupedResult);
      const result = tipoAsuntos.map((tipoAsunto) => {
        return {
          count: this.groupedResult[tipoAsunto].length,
          label: tipoAsunto,
        };
      });
      return result;
    },
    getResultByTipoAsunto() {
      return (tipoAsunto) => this.groupedResult[tipoAsunto];
    }
  },
  actions: {
    async getOrganismos() {
      this.organismos = [];
      this.organismos = organismos;
      // this.organismos = (
      //   await apiCatalogos.get("api/organosJurisdiccionales")
      // )?.data;
    },
    async BuscarPartes(params) {
      this.buscarPartes = [];
      this.buscarPartes = (
        await apiUsuarios.get("api/busquedaParte", params)
      )?.data;
      this.groupedResult = _.groupBy(
        this.buscarPartes,
        "tipoAsuntoDescripcion",
      );
    },
  },
});
