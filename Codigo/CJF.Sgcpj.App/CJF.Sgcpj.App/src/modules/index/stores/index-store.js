import { defineStore } from "pinia";
import { apiOficialia, apiTramites, apiActuaria} from "boot/axios";
import { DetalleIndicador } from "../../oficialia/data/response-indicadores";

export const useIndexStore = defineStore("oficialiaIndexStore", {
  state: () => ({
    oficialiaData: [],
    tramitesData: {},
    kpiSummaryData: Array(new DetalleIndicador()),
    notificacionesPendientesPorDias: [],
    totalNotificaciones: {},
    notificacionesPorTipo: [],
    detalleActuario: [],
    detalleMes: [],
    detalleSemanas: [],
    diferenciasDias: []
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
      this.oficialiaData = (
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
    async obtenerTramites(params) {
      this.tramitesData = Object.assign({}, {});
      this.tramitesData = (
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
    /**
     * obtiene los indicadores
     * @param {*} params object {
     * CatOrganismoId: int,
     * FiltroActuarioId: int,
     * FechaInicial: date,
     * FechaFinal: date
     * }
     */
    async obtenerIndicadores(params) {
      const response = await apiActuaria.get("api/actuaria/obtenerIndicadores", {
        params: {
          CatOrganismoId: params.CatOrganismoId,
          FiltroActuarioId: params.FiltroActuarioId,
          FechaInicial: params.FechaInicial,
          FechaFinal: params.FechaFinal
        }
      });
      this.notificacionesPendientesPorDias = response.data.notificacionesPendientesPorDias;
      this.totalNotificaciones = response.data.totalNotificaciones;
      this.notificacionesPorTipo = response.data.notificacionesPorTipo;
    },
    /**
 * Método para obtener los KPIs de promociones en un rango de fechas específico.
 * 
 * @param {string} FechaInicial - La fecha de inicio del rango para obtener los KPIs.
 * @param {string} FechaFinal - La fecha de finalización del rango para obtener los KPIs.
 * 
 * @returns {Promise<void>} - Actualiza `kpiSummaryData` con los datos obtenidos de la API.
 */
    async obtenerKpis(
      FechaInicial,
      FechaFinal,
    ) {
      this.kpiSummaryData = (
        await apiOficialia.get(`api/promociones/indicadores/`, {
          params: { FechaInicial: FechaInicial, FechaFinal: FechaFinal }
        })
      )?.data;
    },
    /**
 * Método para obtener los indicadores de zonas en un rango de fechas específico y para un organismo en particular.
 * 
 * @param {string} FechaInicial - La fecha de inicio del rango para obtener los indicadores.
 * @param {string} FechaFinal - La fecha de finalización del rango para obtener los indicadores.
 * @param {number} CatOrganismoId - El ID del organismo para el cual obtener los indicadores.
 * 
 * @returns {Promise<void>} - Actualiza `detalleActuario` con los datos obtenidos de la API.
 */
    async obtenerIndicadoresZonas(
      FechaInicial,
      FechaFinal,
      CatOrganismoId,
    ) {
      const response = (
        await apiActuaria.get(`api/actuaria/indicadoresZonas`, {
          params: { FechaInicial: FechaInicial, FechaFinal: FechaFinal, CatOrganismoId: CatOrganismoId }
        })
      )?.data;
      this.detalleActuario = response;
    },
   /**
 * Método para obtener el desglose de notificaciones por tipo y mes en un rango de fechas específico para un organismo y actuario en particular.
 * 
 * @param {string} FechaInicial - La fecha de inicio del rango para obtener el desglose.
 * @param {string} FechaFinal - La fecha de finalización del rango para obtener el desglose.
 * @param {number} CatOrganismoId - El ID del organismo para el cual obtener el desglose.
 * @param {number} FiltroActuarioId - El ID del actuario para el cual obtener el desglose.
 * 
 * @returns {Promise<void>} - Actualiza `detalleMes` con los datos obtenidos de la API.
 */  
    async obtenerDesglosesMes(
      FechaInicial,
      FechaFinal,
      CatOrganismoId,
      FiltroActuarioId
    ) {
      const response = (
        await apiActuaria.get(`api/actuaria/notificacionesTipoYMesFunction`, {
          params: { FechaInicial: FechaInicial, FechaFinal: FechaFinal, CatOrganismoId: CatOrganismoId, FiltroActuarioId: FiltroActuarioId }
        })
      )?.data;
      this.detalleMes = response;
    },
    /**
 * Método para obtener el desglose de notificaciones por tipo y semana en un rango de fechas específico para un organismo, actuario y mes seleccionado.
 * 
 * @param {number} CatOrganismoId - El ID del organismo para el cual obtener el desglose.
 * @param {number} FiltroActuarioId - El ID del actuario para el cual obtener el desglose.
 * @param {string} FechaInicial - La fecha de inicio del rango para obtener el desglose.
 * @param {string} FechaFinal - La fecha de finalización del rango para obtener el desglose.
 * @param {number} MesSeleccionado - El mes seleccionado para el cual obtener el desglose.
 * 
 * @returns {Promise<void>} - Actualiza `detalleSemanas` con los datos obtenidos de la API.
 */
    async obtenerDesglosesSemanas(
      CatOrganismoId,
      FiltroActuarioId,
      FechaInicial,
      FechaFinal,
      MesSeleccionado 
    ) {
      const response = (
        await apiActuaria.get(`api/actuaria/notificacionesTipoSemanaFunction`, {
          params: {CatOrganismoId: CatOrganismoId, FiltroActuarioId :FiltroActuarioId,  FechaInicial: FechaInicial, FechaFinal: FechaFinal, MesSeleccionado: MesSeleccionado }
        })
      )?.data;
      this.detalleSemanas = response.notificaciones;
    },
    /**
 * Método para obtener las diferencias en días entre notificaciones en un rango de fechas específico para un actuario en particular.
 * 
 * @param {number} FiltroActuarioId - El ID del actuario para el cual obtener las diferencias en días.
 * @param {string} FechaInicial - La fecha de inicio del rango para obtener las diferencias.
 * @param {string} FechaFinal - La fecha de finalización del rango para obtener las diferencias.
 * 
 * @returns {Promise<void>} - Actualiza `diferenciasDias` con los datos obtenidos de la API.
 */
    async obtenerDiferenciasDias(
      FiltroActuarioId,
      FechaInicial,
      FechaFinal,
    ) {
      const response = (
        await apiActuaria.get(`api/actuaria/obtenerIntervalosNotifica`, {
          params: {FiltroActuarioId :FiltroActuarioId,  FechaInicial: FechaInicial, FechaFinal: FechaFinal }
        })
      )?.data;
      this.diferenciasDias = response.diferencias;
    }
  },
});
