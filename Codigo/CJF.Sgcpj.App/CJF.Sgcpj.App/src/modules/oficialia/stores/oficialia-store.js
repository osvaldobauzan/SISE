import { defineStore } from "pinia";
import { apiOficialia } from "../../../boot/axios";
import { ResponsePromociones } from "../data/response-promociones";
import { Expediente } from "../data/promocion";
import { Validaciones } from "../../../helpers/validaciones";
import { ArchivosAnexos } from "../../../data/archivos-anexos";
import { DetallePromocion } from "../data/detalle-promocion";
import { DetalleGruposMes } from "../data/response-grupos-mes";
import { DetalleIntervalosTiempo } from "../data/response-intervalos";

export const useOficialiaStore = defineStore("oficialiaStore", {
  state: () => ({
    data: new ResponsePromociones(),
    countMonthData: Array(new DetalleGruposMes()),
    intervalTimeData: Array(new DetalleIntervalosTiempo()),
    expedientePdf: "",
    noRegistro: 1,
    expediente: Array(new Expediente()),
    promocion: new DetallePromocion(),
    archivos: new ArchivosAnexos(),
    archivoBase64: "",
    asuntoNeunId: 0,
    numeroOrden: 0,
    promocionesXExpediente: [],
    textoBuscar: "",
  }),
  getters: {
    totalPromociones() {
      return this.data?.totalRegistros || 0;
    },
    promocionesAsignadas() {
      return this.data?.datos?.filter((x) => x.estado === 4)?.length || 0;
    },
  },
  actions: {
    actualizaTextoBuscar(texto) {
      this.textoBuscar = texto;
    },
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
    async obtenerPromociones(params) {
      this.listaPromociones = Object.assign([], []);
      this.data = (
        await apiOficialia.get("api/promociones", {
          params: {
            fechaInicial: params.from,
            fechaFinal: params.to,
            estado: params.status,
            texto: params.text?.trim() || "",
            ordenarPor: params.sortBy,
            descendente: params.descending,
            pagina: params.page,
            registrosPorPagina: params.rowsPerPage,
            origen: params.valoresFiltros?.origen,
            secretario: params.valoresFiltros?.secretario,
            capturo: params.valoresFiltros?.capturo,
          },
        })
      )?.data;
    },
    /**
     *obtine expediente de promocion pdf
     * @param {*} id es asuntoNeunId
     */
    async obtenerExpediente(
      asuntoNeunId,
      numeroRegistro,
      numeroOrder,
      anioPromocion,
    ) {
      this.expedientePdf = "";
      this.expedientePdf = (
        await apiOficialia.get(`api/promociones/expediente/`, {
          params: { asuntoNeunId, numeroRegistro, numeroOrder, anioPromocion },
        })
      )?.data?.base64;
    },
    async obtenerConteoMes(
      EmpleadoId
    ) {
      this.countMonthData = (
        await apiOficialia.get(`api/promociones/gruposmeses/`, {
          params: { EmpleadoId: EmpleadoId }
        })
      )?.data;
    },
    async obtenerTiemposTurnos(
      FechaInicial,
      FechaFinal,
      EmpleadoId,
    ) {
      this.intervalTimeData = (
        await apiOficialia.get(`api/promociones/intervalos/`, {
          params: { FechaInicial: FechaInicial, FechaFinal: FechaFinal, EmpleadoId: EmpleadoId }
        })
      )?.data;
    },
    /**
     *obtine expediente de promocion pdf
     * @param {*} asuntoNeunId es asuntoNeunId
     */
    async obtenerArchivosYAnexos(
      asuntoNeunId,
      anioPromocion,
      numeroOrden,
      tipoModulo,
      AsuntoDocumentoId,
      origen,
      kIdElectronica,
    ) {
      const res = await apiOficialia.get(`api/promociones/archivos`, {
        params: {
          asuntoNeunId: asuntoNeunId,
          anioPromocion: anioPromocion,
          numeroOrden: numeroOrden,
          tipoModulo: tipoModulo,
          AsuntoDocumentoId: AsuntoDocumentoId,
          origen: origen,
          kIdElectronica: kIdElectronica,
        },
      });
      this.archivos = res.data;
      return res.data;
    },
    /**
     * Calcula el numero de registro consecutivo
     */
    async calculaRegistro() {
      this.noRegistro = 1;
      this.noRegistro = (
        await apiOficialia.get("api/registro", {
          params: {},
        })
      )?.data?.registro;
    },
    /**
     * Crea promocion
     * @param {*} params objecto promocion
     */
    async crearPromocion(params) {
      this.asuntoNeunId = 0;
      const resultado = (await apiOficialia.post("api/promociones", params))
        .data;
      this.asuntoNeunId = resultado?.asuntoNeunId;
      this.numeroOrden = resultado?.numeroOrden;
    },
    /**
     * Edita promocion
     * @param {*} params objecto promocion
     */
    async editarPromocion(params) {
      await apiOficialia.put("api/promociones", params);
    },
    /**
     * Busca expediente
     * @param {*} asuntoAlias numero expediente
     */
    async buscarExpediente(
      asuntoAlias,
      tipoAsuntoId,
      tipoProcedimiento = null,
      modulo = 1,
    ) {
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
            catTipoProcedimiento: tipoProcedimiento,
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
    /**
     * Subir archivos
     * @param {*} params form data
     */
    async subirArchivo(params) {
      await apiOficialia.post("api/promociones/archivo", params, {
        headers: { "Content-Type": "multipart/form-data" },
      });
    },
    /**
     * Subir archivos
     * @param {*} params form data
     */
    async subirArchivos(params) {
      let resultado = [];
      resultado = (
        await apiOficialia.post("api/promociones/archivos", params, {
          headers: { "Content-Type": "multipart/form-data" },
        })
      ).data;
      return resultado;
    },
    /**
     * Subir anexos
     * @param {*} params objecto anexos
     */
    async subirAnexos(params) {
      let anexos = [];
      anexos = (
        await apiOficialia.post("api/promociones/anexos", params, {
          headers: { "Content-Type": "multipart/form-data" },
        })
      ).data;
      return anexos;
    },
    /**
     * Obtiene el detalle de la promocion
     * @param {*} parametros
     */
    async detallePromocion(parametros) {
      this.promocion = {};
      const response = (
        await apiOficialia.get("api/promociondetalle", {
          params: {
            ...parametros,
          },
        })
      )?.data.datos;
      if (response.length > 0) {
        this.promocion = response[0];
        this.promocion.anexos = JSON.parse(this.promocion.anexos);
      }
    },

    /**
     * Obtener archivo en base64
     * @param {*} parametros objecto parametros
     */
    async recuperarArchivo(parametros) {
      this.archivoBase64 = "";
      this.archivoBase64 = (
        await apiOficialia.get("api/promociones/documento", {
          params: { ...parametros },
        })
      )?.data?.base64;
    },
    /**
     * Elimina la promocion
     * @param {*} parametros
     */
    async eliminarPromocion(parametros) {
      await apiOficialia.delete("api/promociones", {
        data: {
          ...parametros,
        },
      });
    },
    /**
     * Elimina la promocion
     * @param {*} parametros
     */
    eliminarAnexo(parametros) {
      return apiOficialia.delete("api/anexos", {
        data: {
          ...parametros,
        },
      });
    },
    /**
     * Asociar promocion
     * @param {*} params objecto promocion
     */
    async asociarPromocion(params) {
      this.numeroOrden = 0;
      this.numeroOrden = (
        await apiOficialia.post("api/promocionesinsertar", params)
      ).data;
    },
    /**
     * Promociones por expediente
     * @param {*} parametros objecto parametros
     */
    async promocionesPorExpediente(parametros) {
      this.promocionesXExpediente = [];
      this.promocionesXExpediente = (
        await apiOficialia.get("api/promocion/expediente", {
          params: parametros,
        })
      ).data;
    },
    async revisarNumeroPromocion(parametros) {
      const promocionExiste = (
        await apiOficialia.get("api/promociones/estatus", {
          params: {
            NumeroRegistro: parametros.numeroPromocion,
            YearPromocion: parametros.anioPromocion,
          },
        })
      ).data;
      return promocionExiste.existe;
    },
    async expedienteInsertarPromocion(parametros) {
      const promocionNueva = (
        await apiOficialia.post("api/expedientesinsertar", {
          catTipoAsuntoId: parametros.catTipoAsuntoId,
          numeroOCC: parametros.numeroOCC,
          noExpediente: parametros.noExpediente,
          tipoProcedimiento: parametros.tipoProcedimiento,
          piAsuntoNeunId: parametros.piAsuntoNeunId,
          esActualizacion: parametros.esActualizacion,
        })
      ).data;
      return promocionNueva;
    },
    /**
     * Calcula el numero de expediente consecutivo
     */
    async calculaNumeroExpediente(tipoAsunto, tipoProcedimiento = null) {
      const noExpediente = (
        await apiOficialia.get("api/expediente/numero", {
          params: {
            IdTipoAsunto: tipoAsunto,
            IdTipoProcedimiento: tipoProcedimiento,
          },
        })
      )?.data?.asuntoAlias;
      return noExpediente;
    },
    async obtenerCatalogosFiltros() {
      const filtros = (await apiOficialia.get("api/promociones/filtros"))?.data;
      return filtros;
    },
  },
});
