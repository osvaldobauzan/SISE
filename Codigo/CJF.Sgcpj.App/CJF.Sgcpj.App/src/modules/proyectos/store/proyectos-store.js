import { defineStore } from "pinia";
import { Utils } from "src/helpers/utils";
import { lista } from "../data/proyectosPage.js";
import { Validaciones } from "src/helpers/validaciones";
import { apiOficialia, apiProyectos } from "boot/axios";
import { ResponseProyectos } from "src/modules/oficialia/data/response-proyectos.js";
//TODO: Eliminar la referencia a apiTest
export const estatusProyecto = {
  SinProyecto: 1,
  ParaRevision: 2,
  NoAprobado: 3,
  ConAjustesDeFondo: 4,
  ConAjustesDeForma: 5,
  Aprobado: 6,
};

export const motivosProyectoKeys = {
  ActosReclamados: 1745,
  Delitos: 48,
  Prestaciones: 89
};

export const motivosProyecto = {
  1745: {
    label: "Actos reclamados",
    title: "Actos Reclamados y Sentidos",
    descLabel: "acto reclamado"
  },
  48: {
    label: "Delitos",
    title: "Delitos y Sentidos",
    descLabel: "delito"
  },
  89: {
    label: "Prestaciones",
    title: "Presentación demandada y Sentidos",
    descLabel: "prestación demandada"
  }
};

export const useProyectosStore = defineStore("ProyectosStore", {
  state: () => ({
    data: new ResponseProyectos(),
    proyectos: lista,
    expediente: [],
    proyectosBase64: [],
    proyectoBase64: "",
    proyectoNombre: "",
    proyectoSeleccionado: null
  }),
  getters: {
    totalProyectos() {
      return this.data?.totalRegistros || 0;
    },
  },
  actions: {
    /**
     * obtiene las promociones
     * @param {*} params object {
     * from: date,
     * to: date,
     * status: 0,
     * text: '',
     * sortBy: 'prop',
     * descending: true,
     * page:1,
     * rowsPerPage:0
     * }
     */
    async obtenerProyectos(params) {
      this.data = (
        await apiProyectos.get("api/proyectos", {
          params: {
            texto: params.text?.trim() || "",
            fechaInicial: params.from,
            fechaFinal: params.to,
            estatus: params.status,
            ordenarPor: params.sortBy,
            descendente: params.descending,
            pagina: params.page,
            registrosPorPagina: params.rowsPerPage,
          },
        })
      )?.data;

      this.data.datos = this.data.datos.map((element) => {
        let difference =
          new Date().getTime() -
          new Date(element.fechaAudiencia ?? new Date()).getTime();
        element.expediente.tipoAsunto = element.expediente.catTipoAsunto;
        element.Transcurridos = Math.round(difference / (1000 * 3600 * 24));
        if (element.estadoProyecto == estatusProyecto.Aprobado)
          element.Transcurridos = null;
        return element;
      });
    },
    async validarExpediente(params) {
      let response = (
        await apiProyectos.get("api/proyecto/validarExpediente", {
          params: {
            numeroExpediente: params.asuntoAlias,
            cuadernoId: params.cuadernoId,
            tipoAsuntoId: params.tipoAsuntoId,
            asuntoNeunId: params.asuntoNeunId,
          },
        })
      )?.data;
      return response;
    },
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
    async addVersion(data) {
      let resultado = [];
      resultado = (
        await apiProyectos.post("api/proyecto/subirConAudiencia", data, {
          headers: { "Content-Type": "multipart/form-data" },
        })
      ).data;
      return resultado;
    },
    async addCorreccionVersion(data) {
      let resultado = [];
      resultado = (
        await apiProyectos.post("api/proyecto/validar", data, {
          headers: { "Content-Type": "multipart/form-data" },
        })
      ).data;
      return resultado;
    },
    async resetProyects() {
      this.proyectosBase64 = [];
    },
    async obtenerProyectoEnBase64(id = 36) {
      const data = (
        await apiProyectos.get("api/proyecto/documento", {
          params: {
            id: id,
            descargar: false
          },
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
    async descargarDocumentos(id) {
      await apiProyectos
        .get("api/proyecto/documento", {
          params: {
            id: id,
            descargar: true,
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
    /**
     * Obtener archivo en base64
     * @param {*} parametros objecto parametros
     */
    async recuperarArchivo(parametros) {
      parametros;
      this.archivoBase64 = "";
      this.archivoBase64 = (
        await apiOficialia.get(
          "api/promociones/documento?asuntoNeunId=30326323&anioPromocion=2024&numeroOrden=454&tipoModulo=1&origen=4&nombre=01800000303263230004540597870014.pdf",
        )
      )?.data?.base64;
    },
    async obtenerHistorial(asuntoNeunId) {
      this.proyectoSeleccionado = (
        await apiProyectos.get("api/proyecto/versiones", {
          params: {
            asuntoNeunId: asuntoNeunId,
          },
        })
      )?.data;

      this.proyectoSeleccionado.archivos.forEach(async (version) => {
        let motivos = (await apiProyectos.get("api/proyectos/motivosPartes", {
          params: {
            proyectoId: version.proyectoId,
          },
        }))?.data.motivos;

        version.partes = Object.groupBy(motivos, ({idParte}) => idParte);

        let partes2 = [];
        for(const parte in version.partes) {
            let key = version.partes[parte].map(motivo => `${motivo.idMotivo}${motivo.idSentido}${motivo.descripcion}`).sort().join('-');
            partes2.push({
                parte,
                key,
                name: version.partes[parte][0].parte,
                value: version.partes[parte]
            });
        };

        version.partes = Object.groupBy(partes2, ({key}) => key);
      });

    },
    async obtenerMotivosProyecto(proyectoId) {
      this.proyectoSeleccionado = (
        await apiProyectos.get("api/proyectos/motivosPartes", {
          params: {
            proyectoId: proyectoId,
          },
        })
      )?.data;
    },
    async reasignarSecretarios(SecretarioNuevoId,ProyectosId) {
      let resultado = [];
      resultado = (
        await apiProyectos.post("api/proyectos/reasignar", null, {
          params: {
            SecretarioNuevoId,
            ProyectosId: JSON.stringify(ProyectosId)
          }
        })
      ).data;
      return resultado;
    }
  },
});
