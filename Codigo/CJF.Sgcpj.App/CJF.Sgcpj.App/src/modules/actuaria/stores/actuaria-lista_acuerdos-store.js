import { defineStore } from "pinia";
import { uniqBy } from "lodash";
import { apiActuaria } from "src/boot/axios";

export const useActuariaListaStore = defineStore("actuariaListaStore", {
  state: () => ({
    listaAcuerdos: [],
    actuarios: [],
    listaSintesisManual: [],
  }),
  getters: {},
  actions: {
    getTitulares() {
      return uniqBy(
        this.listaAcuerdos.map(({ titularId, titular }) => ({
          titularId,
          titular,
        })),
        "titularId",
      );
    },
    getActuarios() {
      this.actuarios = uniqBy(
        this.listaAcuerdos.map(({ actuarioId, actuario }) => ({
          actuarioId,
          actuario,
        })),
        "actuarioId",
      );
      return this.actuarios;
    },
    getQuejosos() {
      return uniqBy(
        this.listaAcuerdos.map(({ quejosoId, quejoso }) => ({
          quejosoId,
          quejoso,
        })),
        "quejosoId",
      );
    },
    getAutoridades() {
      return uniqBy(
        this.listaAcuerdos.map(({ autoridadId, autoridad }) => ({
          autoridadId,
          autoridad,
        })),
        "autoridadId",
      );
    },
    async getListaAcuerdos(fInicio, fFin) {
      await apiActuaria
        .get("api/actuaria/listaacuerdos", {
          params: {
            fechaInicio: fInicio,
            fechaFin: fFin,
          },
        })
        .then(({ data }) => {
          this.listaAcuerdos = data;
        });
    },
    async addAcuerdoManual(expediente) {
      return (
        await apiActuaria.post("api/actuaria/agregarsintesismanual", expediente)
      ).data;
    },

    async getListaSintesisManual(fecha) {
      this.listaSintesisManual = (
        await apiActuaria.get(
          `api/actuaria/listasintesismanual?FechaPublicacion=${fecha}`,
        )
      ).data;
    },
    delAcuerdo(index) {
      this.acuerdos.splice(index, 1);
    },
  },
});
