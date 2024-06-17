import { defineStore } from "pinia";
import { openURL } from "quasar";
import { apiExpedienteElectronico } from "../../../boot/axios";
import { Validaciones } from "../../../helpers/validaciones";
import { apiOficialia } from "../../../boot/axios";

// import _ from "lodash";
import axios from "axios";

export const useExpedienteElectronicoStore = defineStore(
  "expedienteElectronicoStore",
  {
    state: () => ({
      expediente: {},
      expedientes: [],
      expedienteSeleccionado: {},
      parteInformacion: [],
      caracteresPersona: {
        PPromueveNombre: [13],
      },
    }),
    actions: {
      resetExpedientes() {
        this.expedientes = [];
      },
      async buscarExpediente(
        asuntoAlias,
        tipoAsuntoId,
        tipoProcedimiento = null,
        modulo = 1,
      ) {
        const valid = Validaciones.validaNoExpediente(asuntoAlias);
        if (typeof valid === "string") {
          return;
        }
        this.expedientes = (
          await apiOficialia.get("api/asociarexpediente", {
            params: {
              asuntoAlias: asuntoAlias,
              catTipoAsuntoId: tipoAsuntoId,
              catTipoProcedimiento: tipoProcedimiento,
              modulo: modulo,
            },
          })
        )?.data;
        if (this.expedientes?.length > 0) {
          this.expedientes = this.expedientes.map((x) => {
            x.label = `${x.asuntoAlias} (${x.tipoAsunto})`;
            return x;
          });
          // this.expedientes = _.sortBy(this.expedientes, ["tipoAsunto"]);
        }
      },
      async obtenerDatosGenerales(asuntoNeunId) {
        this.expediente[asuntoNeunId] = this.expediente[asuntoNeunId] || {};
        this.expediente[asuntoNeunId].datos = (
          await apiExpedienteElectronico.get("api/datosgenerales", {
            params: {
              asuntoNeunId: asuntoNeunId,
            },
          })
        )?.data;
        return this.expediente[asuntoNeunId].datos;
      },
      async obtenerFichaTecnicaExpedienteElectronico(neun) {
        this.expediente[neun] = this.expediente[neun] || {};
        this.expediente[neun].ficha = (
          await apiExpedienteElectronico.get(
            "api/ObtenerFichaTecnicaExpedienteElectronico",
            {
              params: {
                asuntoNeunId: neun,
              },
            },
          )
        )?.data;
        return this.expediente[neun].ficha;
      },
      async obtenerExpedienteElectronico(neun) {
        this.expediente[neun] = this.expediente[neun] || {};
        this.expediente[neun].acuerdos = (
          await apiExpedienteElectronico.get(
            "api/ObtenerExpedienteElectronico",
            {
              params: {
                asuntoNeunId: neun,
              },
            },
          )
        )?.data;
        return this.expediente[neun].acuerdos;
      },
      getUrlExpedienteElectronico(neun) {
        const options = {
          method: "POST",
          url: `${process.env.DOMINIO_SING}/wsConfiguration/API/login/System/`,
          headers: { "Content-Type": "application/json" },
          data: {
            Password: process.env.FLIPBOOK_PASSWORD,
            UserName: process.env.FLIPBOOK_USERNAME,
          },
        };
        axios
          .request(options)
          .then(function (response) {
            const token = response.data.token;
            let url = `${process.env.DOMINIO_SING}/flipbook/#?param1=${neun}&param2=5&param3=57691&param4=${token}`;
            openURL(url);
          })
          .catch(function (error) {
            throw new Error(error);
          });
      },
      async obtenerParte(idParte) {
        const resultado = (
          await apiExpedienteElectronico.get("api/ObtenerParte", {
            params: { personaId: idParte },
          })
        )?.data;
        return resultado;
      },
      async agregarParte(parametros) {
        const resultado = (
          await apiExpedienteElectronico.put("api/InsertarParte", parametros)
        )?.data;
        return resultado;
      },
      async editarParte(parametros) {
        const resultado = (
          await apiExpedienteElectronico.post("api/ActualizaParte", parametros)
        )?.data;
        return resultado;
      },
      async eliminarParte(parametros) {
        const resultado = (
          await apiExpedienteElectronico.delete("api/EliminarParte ", {
            data: {
              ...parametros,
            },
          })
        )?.data;
        return resultado;
      },
      async obtenerAudiencia(asuntoNeunId, cuadernoId) {
        const resultado = (
          await apiExpedienteElectronico.get("api/audiencia", {
            params: { asuntoNeunId: asuntoNeunId, cuadernoId: cuadernoId },
          })
        )?.data;
        return resultado;
      },
      async obtenerEstadoSentencia(asuntoNeunId) {
        const resultado = (
          await apiExpedienteElectronico.get("api/sentencia", {
            params: { asuntoNeunId: asuntoNeunId },
          })
        )?.data;
        return resultado;
      },
      setExpedienteSeleccionado(exp) {
        this.expedienteSeleccionado = exp;
      },
      setPartes(partes, neun) {
        this.expediente[neun] = this.expediente[neun] || {};
        this.expediente[neun].partes = this.expediente[neun].partes || partes;
      },
      setInformacionParte(datos) {
        this.parteInformacion = [...this.parteInformacion, datos];
      },
      async obtenerParteSeleccionada(params) {
        const resultado = (
          await apiExpedienteElectronico.get("api/parte/captura", {
            params: {
              asuntoNeunId: params.asuntoNeunId,
              personaId: params.personaId,
            },
          })
        )?.data;
        return resultado;
      },
    },
    getters: {
      debeMostrarPregunta: (state) => (pregunta, idCaracterPersona) => {
        const idsPermitidos = state.caracteresPersona[pregunta];
        return idsPermitidos.includes(idCaracterPersona);
      },
      filtrarPorCuadernoId: (state) => (asuntoNeunId, cuadernoId) => {
        if (
          state.expediente &&
          state.expediente[asuntoNeunId] &&
          state.expediente[asuntoNeunId].acuerdos
        ) {
          if (cuadernoId > 0) {
            return state.expediente[asuntoNeunId].acuerdos.filter(
              (item) => item.cuadernoId === cuadernoId,
            );
          } else {
            return state.expediente[asuntoNeunId].acuerdos;
          }
        }
        return [];
      },
    },
  },
);
