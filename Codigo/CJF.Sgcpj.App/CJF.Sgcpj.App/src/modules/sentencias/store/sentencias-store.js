import { defineStore } from "pinia";
import { lista } from "../data/sentenciaPage.js";
import { Validaciones } from "src/helpers/validaciones";
import { apiOficialia, apiSentencias, apiTramites } from "boot/axios";
import { ResponseSentencias } from "src/modules/oficialia/data/response-sentencias.js";
import { Utils } from "src/helpers/utils";

export const estatusProyecto = {
  Todos: 0,
  SinEngrose: 1,
  Capturado: 2,
  Cancelado: 3,
  Preautorizado: 4,
  Autorizado: 5,
};

export const useSentenciasStore = defineStore("SentenciasStore", {
  state: () => ({
    data: new ResponseSentencias(),
    sentencias: lista,
    expediente: [],
    proyectosBase64: [],
    proyectoBase64: "",
    proyectoNombre: "",
  }),
  actions: {
    async buscarExpediente(asuntoAlias, tipoAsuntoId, modulo = 1) {
      this.expediente = Object.assign([], []);
      const valid = Validaciones.validaNoExpediente(asuntoAlias);
      if (typeof valid === "string") {
        return;
      }
      this.expediente = (
        await apiOficialia.get("api/asociarexpediente", {
          params: {
            asuntoAlias: asuntoAlias,
            catTipoAsuntoId: tipoAsuntoId,
            modulo: modulo,
          },
        })
      )?.data;
      if (this.expediente?.length > 0) {
        this.expediente = this.expediente.map((x) => {
          x.label = `${x.asuntoAlias} (${x.tipoAsunto})`;
          return x;
        });
      }
    },
    async obtenerSentencias(params) {
      this.data = (
        await apiSentencias.get("api/sentencias", {
          params: {            
            fecha: params.date.from,
            fechaFin: params.date.to,
          },
        })
      )?.data;

      this.data.metaDatos = {
        [`${estatusProyecto.SinEngrose}`]: 0,
        [`${estatusProyecto.Capturado}`]: 0,
        [`${estatusProyecto.Cancelado}`]: 0,
        [`${estatusProyecto.Preautorizado}`]: 0,
        [`${estatusProyecto.Autorizado}`]: 0,
      };


      this.data.datos = this.data.datos.map((element) => {
        element.expediente.tipoAsunto = element.expediente.catTipoAsunto;
        this.data.metaDatos[element.estadoSentenciaId] += 1;
        element.selected = false;
        return element;
      });
    },
    addVersion(expediente, data) {
      const index = this.sentencias.findIndex(
        (item) => item.AsuntoNeunId === expediente.AsuntoNeunId,
      );

      if (index < 0) {
        //nuevo expediente
        const newexpediente = {
          AsuntoAlias: expediente.asuntoAlias,
          AsuntoNeunId: expediente.asuntoNeunId,
          CatTipoAsunto: expediente.tipoAsunto,
          Cuaderno: cuaderno.value ? cuaderno.value.cuaderno : "",
          FechaAudiencia: "",
          Transcurridos: 0,
          Secretario: expediente.secretario,
          SecretarioNombre: secretarioSelected.value.value,
          TitularNombre: titularSelected.value.value,
          Mesa: expediente.mesa,
          Estado: "Para revisión",
          Sentido: tipoSentidoSelected.value.value,
          TipoSentencia: tipoSentenciaSelected.value.value,
          FechaAlta: new Date(),
        };
        data.archivo = "1";
        data.version = `Versión 1`;
        newexpediente.Historico = [data];
        this.sentencias.push(newexpediente);
      } else {
        //expediente existente
        if (!this.sentencias[index].Historico) {
          this.sentencias[index].Historico = [];
        }
        data.archivo = this.sentencias[index].Historico.length + 1;
        data.version = `Versión ${this.sentencias[index].Historico.length + 1}`;
        this.sentencias[index].Estado = "Para revisión";
        this.sentencias[index].Sentido = data.sentido;
        this.sentencias[index].TipoSentencia = data.tipoSentencia;
        this.sentencias[index].FechaAlta = data.fecha;
        this.sentencias[index].TitularNombre = data.titular;
        this.sentencias[index].SecretarioNombre = data.secretario;
        this.sentencias[index].Historico = [
          data,
          ...this.sentencias[index].Historico,
        ];
      }
    },
    addCorreccion(expediente, data) {
      const index = this.sentencias.findIndex(
        (item) => item.AsuntoNeunId === expediente.AsuntoNeunId,
      );
      const historico = this.sentencias[index].Historico[0];
      historico.comentarioTitular = data.comentarioTitular;
      historico.correccionFecha = data.correccionFecha;
      historico.correccionArchivo = data.correccionArchivo;
    },   
    async obtenerProyectoEnBase64(params) {
      const data = (
        await apiSentencias.get("api/sentencias/documento", {
          params: params
        })
      )?.data;
      this.proyectoBase64 = data?.base64;
      this.proyectoNombre = data?.nombreArchivo;
      return (
        this.proyectosBase64.push({
          archivoBase64: this.proyectoBase64,
          proyectoNombre: this.proyectoNombre,
        }) - 1
      );
    }, 
    async descargarDocumentos(guidDoc) {
      await apiTramites
        .get("api/tramite/documento", {
          params: {
            id: guidDoc,
            esDescarga: true,
          },
        })
        .then((response) => {
          let url = null;
          if (response.data.nombreArchivo.includes(".pdf")) {
            url = window.URL.createObjectURL(
              Utils.base64ToUrlObj(response.data.base64, true),
            );
          } else {
            url = window.URL.createObjectURL(
              Utils.base64ToBlobWord(response.data.base64),
            );
          }

          const link = document.createElement("a");
          link.href = url;
          link.setAttribute("download", response.data.nombreArchivo);
          document.body.appendChild(link);
          link.click();
          document.body.removeChild(link);
        });
    },
    async preautorizarSinFirma(params) {
      await apiSentencias.post("api/sentencias/preautorizar", params);
    }
  },
});
