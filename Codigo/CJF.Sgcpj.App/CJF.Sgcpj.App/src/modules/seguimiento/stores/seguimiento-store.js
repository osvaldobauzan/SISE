import { defineStore } from "pinia";
import { apiSeguimiento } from "boot/axios";
import {
  Expediente,
  Acuerdo,
  Acuse,
  InsertaSeguimiento,
  SeguimientoExpediente,
} from "src/data/seguimiento";

export const useSeguimientoStore = defineStore("seguimientoStore", {
  state: () => ({
    expedienteArray: Array(new Expediente()),
    mostrarExpedienteArray: Array(new SeguimientoExpediente()),
    AcuerdoArray: Array(new Acuerdo()),
    AcuseArray: Array(new Acuse()),
    InsertaSeguimiento: Array(new InsertaSeguimiento()),
  }),
  actions: {
    async mostrarSeguimientoExpediente(
      expediente,
      tipoAsunto,
      tipoProcedimiento,
    ) {
      const response = await apiSeguimiento.get(
        "api/ObtenerComboXAliasTipoAsunto",
        {
          params: { expediente, tipoAsunto, tipoProcedimiento },
        },
      );
      this.mostrarExpedienteArray.value = response?.data;
    },

    async buscarExpediente(expediente) {
      this.expedienteArray = (
        await apiSeguimiento.get("api/ObtenerSeguimientoXExpediente", {
          params: { expediente },
        })
      )?.data;
      if (this.expedienteArray?.length > 0) {
        this.expedienteArray = this.expedienteArray.map((x) => {
          x.label = `${x.expediente} (${x.tipoAsunto})`;
          return x;
        });
      }
    },

    async buscarExpedientes(expediente, tipoDocumento) {
      this.expedienteArray = (
        await apiSeguimiento.get("api/ObtenerComboExpediente", {
          params: { expediente, tipoDocumento },
        })
      )?.data;
      if (this.expedienteArray?.length > 0) {
        this.expedienteArray = this.expedienteArray.map((x) => {
          x.label = `${x.expediente} (${x.tipoAsunto})`;
          return x;
        });
      }
    },

    async buscarAcuerdos(
      expediente,
      tipoAsunto,
      tipoProcedimiento,
      tipoDocumento,
    ) {
      this.AcuerdoArray = (
        await apiSeguimiento.get("api/ObtenerComboAsunto", {
          params: { expediente, tipoAsunto, tipoProcedimiento, tipoDocumento },
        })
      )?.data;
      if (this.AcuerdoArray?.length > 0) {
        this.AcuerdoArray = this.AcuerdoArray.map((x) => {
          x.label = `${x.expediente} (${x.tipoAsunto})`;
          return x;
        });
      }
    },

    async buscarAcuses(
      expediente,
      tipoAsunto,
      tipoProcedimiento,
      tipoDocumento,
      fecha,
    ) {
      this.AcuseArray = (
        await apiSeguimiento.get("api/ObtenerComboPartes", {
          params: {
            expediente,
            tipoAsunto,
            tipoProcedimiento,
            tipoDocumento,
            fecha,
          },
        })
      )?.data;
      if (this.AcuseArray?.length > 0) {
        this.AcuseArray = this.AcuseArray.map((x) => {
          x.label = `${x.expediente} (${x.tipoAsunto})`;
          return x;
        });
      }
    },

    async insertarSeguimiento(QrString) {
      this.InsertaSeguimiento = (
        await apiSeguimiento.post("api/InsertarSeguimiento", { QrString })
      )?.data;
      if (this.InsertaSeguimiento?.length > 0) {
        this.InsertaSeguimiento = this.InsertaSeguimiento.map((x) => {
          x.label = `${x.expediente} (${x.tipoAsunto})`;
          return x;
        });
      }
    },
  },
});
