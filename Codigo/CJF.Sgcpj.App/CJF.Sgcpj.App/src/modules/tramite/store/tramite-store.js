import { defineStore } from "pinia";
import { apiTramites } from "boot/axios";
import { Utils } from "src/helpers/utils";

export const useTramiteStore = defineStore("tramiteStore", {
  state: () => ({
    dataTramites: [],
    archivoAcuerdo: {},
    acuerdoBase64: "",
    acuerdoNombre: "",
    cambioOficioLibre: false,
    dashboardSelection: {},
    datosAudiencia: {},
  }),
  getters: {},
  actions: {
    /**
     * obtiene los tramites
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
    actualizarOficioLibre(val) {
      this.cambioOficioLibre = val;
    },
    async obtenerTramites(params) {
      this.dataTramites = [];
      this.dataTramites = (
        await apiTramites.get("api/tramites", {
          params: {
            fechaInicial: params.from,
            fechaFinal: params.to,
            estado: params.status,
            //parametros paginacion
            texto: params.text?.trim() || "",
            ordenarPor: params.sortBy,
            descendente: params.descending,
            pagina: params.page,
            registrosPorPagina: params.rowsPerPage, //este va ir en 0 cuando sea todos
            secretario: params.valoresFiltros?.secretario,
            origen: params.valoresFiltros?.origen,
            asunto: params.valoresFiltros?.asunto,
            capturo: params.valoresFiltros?.capturo,
            preautorizo: params.valoresFiltros?.preautorizo,
            autorizo: params.valoresFiltros?.autorizo,
            cancelo: params.valoresFiltros?.cancelo,
          },
        })
      )?.data;
    },
    async subirAcuerdos(params) {
      await apiTramites.post("api/tramite/acuerdo", params, {
        headers: { "Content-Type": "multipart/form-data" },
      });
    },
    async editarAcuerdo(params) {
      await apiTramites.put("api/tramite/acuerdo", params, {
        headers: { "Content-Type": "multipart/form-data" },
      });
    },
    /**
     * Eliminar acuerdo
     * @param {*} parametros
     */
    async eliminarAcuerdo(parametros) {
      await apiTramites.delete("api/tramites", {
        data: {
          ...parametros,
        },
      });
    },
    async obtenerArchivoAcuerdo(params) {
      this.archivoAcuerdo = Object.assign({}, {});
      this.archivoAcuerdo = (
        await apiTramites.get("api/tramite/acuerdo", {
          params: params,
        })
      )?.data;
    },
    async obtenerAcuerdoEnBase64(rutaNas) {
      const data = (
        await apiTramites.get("api/tramite/documento", {
          params: { id: rutaNas },
        })
      )?.data;
      this.acuerdoBase64 = data?.base64;
      this.acuerdoNombre = data?.nombreArchivo;
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
    async cambiarEstadoAcuerdo(params) {
      return await apiTramites.put(
        `api/tramite/estado/${params.tipoUpdate}`,
        params,
      );
    },
    async cambiarEstadoAcuerdoMasivo(params) {
      return await Promise.allSettled(
        params.map((x) =>
          apiTramites.put(`api/tramite/estado/${x.tipoUpdate}`, x),
        ),
      );
    },
    async obtnerDetallesAcuerdos(params) {
      return await Promise.all(
        params.map((x) =>
          apiTramites.get("api/tramite/acuerdo", {
            params: x,
          }),
        ),
      );
    },
    async obtenerAcuerdo(params) {
      const acuerdo = (
        await apiTramites.get("api/tramite/ObtenerAcuerdo", {
          params: params,
        })
      )?.data;
      return acuerdo;
    },
    async obtenerCatalogosFiltros() {
      const filtros = (await apiTramites.get("api/tramite/filtros"))?.data;
      return filtros;
    },
    async obtenerDatosAudiencia(asuntoNeunId) {
      const data = (
        await apiTramites.get("api/obtenerDatosAudiencia", {
          params: { asuntoNeunId: asuntoNeunId },
        })
      )?.data;
      this.datosAudiencia = data;
    },
  },
});
