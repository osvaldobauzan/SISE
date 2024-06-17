import { defineStore } from "pinia";
import { apiCatalogos, apiProyectos } from "../boot/axios";
import { CatalogoAsunto } from "../data/catalogo-asunto";
import { Contenido } from "../data/contenido";
import { Cuaderno } from "../data/cuaderno";
import { Procedimiento } from "../data/procedimiento";
import { CatalogoTipo } from "../data/catalogo-tipo";
import { CatalogoPromovente } from "../data/catalogo-promovente";
import { CatalogoAnexos } from "../data/catalogo-anexos";
import { CatalogoDependiente } from "../data/catalogo-depeniente";
import { CatalogoTipoPersona } from "../data/catalogo-tipo-persona";
import { AmparoEnRevision } from "src/data/amparo-revision";
import { CatalogoOficiales } from "src/data/catalogo-oficiales";
// import { CatalogoTipoPersonaCaracter } from "src/data/catalogo-tipo-persona-caracter";

export const useCatalogosStore = defineStore("catalogosStore", {
  state: () => ({
    asuntos: Array(new CatalogoAsunto()),
    cuadernos: Array(new Cuaderno()),
    amparoEnRevision: Array(new AmparoEnRevision()),
    contenidos: Array(new Contenido()),
    procedimientos: Array(new Procedimiento()),
    tipos: Array(new CatalogoTipo()),
    tiposPromovente: Array(new CatalogoPromovente()),
    tiposAnexo: Array(new CatalogoAnexos()),
    descripcionesAnexo: Array(new CatalogoAnexos()),
    caracteresAnexo: Array(new CatalogoAnexos()),
    tipoPersona: Array(new CatalogoTipoPersona()),
    listaOficiales: Array(new CatalogoOficiales()),
    tipoPersonaCaracter: [],
    tipoPersonaJuridica: [],
    clasificacionAutoridadGenerica: [],
    zonas: [],
    contenidosTramite: [],
    sexo: [],
    tipoComunicacion: [],
    organismosOcc: [],
    titulares: [],
    secretarios: [],
    tipoSentencia: [],
    sentidosSentencia: [],
    proyectoMotivos: [],
    audiencias:[],
    resultadosAudiencia: [],
    catalogoDependiente: Array(new CatalogoDependiente()),
  }),
  actions: {
    async obtenerAsuntos() {
      this.asuntos = [];
      this.asuntos = (await apiCatalogos.get("api/asuntos"))?.data;
    },
    async obtenerOficiales(organismoId, cargoOficial) {
      this.listaOficiales = [];
      this.listaOficiales = (
        await apiCatalogos.get("api/oficiales", {
          params: { cargoId: cargoOficial, organismoId: organismoId },
        })
      )?.data;
    },
    async obtenerContenidos(catTipoAsuntoId) {
      this.contenidos = [];
      this.contenidos = (
        await apiCatalogos.get("api/contenido", {
          params: { catTipoAsuntoId: catTipoAsuntoId, CatTipoOrganismoId: 2 },
        })
      )?.data;
    },
    async obtenerCuadernos(catTipoAsuntoId, asuntoNeunId) {
      this.cuadernos = [];
      this.cuadernos = (
        await apiCatalogos.get("api/cuaderno", {
          params: { TipoAsuntoId: catTipoAsuntoId, AsuntoNeunId: asuntoNeunId },
        })
      )?.data;
    },
    async obtenerAmparoEnRevision(catalogoId) {
      this.amparoEnRevision = [];
      this.amparoEnRevision = (
        await apiCatalogos.get("api/generico", {
          params: { catalogoId: catalogoId },
        })
      )?.data;
    },
    async obtenerProcedimientos(tipoAsuntoId) {
      this.procedimientos = [];
      this.procedimientos = (
        await apiCatalogos.get("api/procedimientos", {
          params: { tipoAsuntoId: tipoAsuntoId, CuadernoId: 0 },
        })
      )?.data;
    },
    async obtenerPromoventes(tipoAsuntoId) {
      this.tiposPromovente = [];
      this.tiposPromovente = (
        await apiCatalogos.get("api/tipopromovente", {
          params: {
            catTipoAsuntoId: tipoAsuntoId,
            catTipoOrganismoId: 0,
            catOrganismoId: 1,
          },
        })
      )?.data;
    },
    async obtenerTipos() {
      this.tipos = [];
      this.tipos = (await apiCatalogos.get("api/tipo"))?.data?.result;
    },
    async obtenerTipoPersona(tipoAsuntoId) {
      this.tipoPersona = [];
      this.tipoPersona = (
        await apiCatalogos.get("api/tipopersona", {
          params: { tipoAsuntoId: tipoAsuntoId },
        })
      )?.data;
    },
    async obtenerTiposAnexo(tipoAsuntoId) {
      this.tiposAnexo = [];
      this.tiposAnexo = (
        await apiCatalogos.get("api/anexo", {
          params: {
            CatTipoAsuntoId: tipoAsuntoId,
            CatOrganismoId: 0,
            CatTipoCatalogoAsuntoId: 502,
          },
        })
      )?.data;
    },
    async obtenerTiposAnexoTipoCatalogo(tipoAsuntoId, catOrganismoId, catTipoCatalogoAsuntoId) {
      this.tiposAnexo = [];
      this.tiposAnexo = (
        await apiCatalogos.get("api/anexo", {
          params: {
            CatTipoAsuntoId: tipoAsuntoId,
            CatOrganismoId: catOrganismoId,
            CatTipoCatalogoAsuntoId: catTipoCatalogoAsuntoId,
          },
        })
      )?.data;
    },
    async obtenerDescripcionesAnexo(tipoAsuntoId) {
      this.descripcionesAnexo = [];
      this.descripcionesAnexo = (
        await apiCatalogos.get("api/anexo", {
          params: {
            CatTipoAsuntoId: tipoAsuntoId,
            CatOrganismoId: 0,
            CatTipoCatalogoAsuntoId: 17,
          },
        })
      )?.data;
    },
    async obtenerCaracteresAnexo(tipoAsuntoId) {
      this.caracteresAnexo = [];
      this.caracteresAnexo = (
        await apiCatalogos.get("api/anexo", {
          params: {
            CatTipoAsuntoId: tipoAsuntoId,
            CatOrganismoId: 0,
            CatTipoCatalogoAsuntoId: 27,
          },
        })
      )?.data;
    },
    async obtenerTipoPersonaCaracter(catTipoAsuntoId) {
      this.tipoPersonaCaracter = [];
      this.tipoPersonaCaracter = (
        await apiCatalogos.get("api/personacaracter", {
          params: { TipoAsuntoId: catTipoAsuntoId },
        })
      )?.data;
    },
    async obtenerTipoPersonaJuridica(catTipoAsuntoId) {
      this.tipoPersonaJuridica = [];
      this.tipoPersonaJuridica = (
        await apiCatalogos.get("api/tipopersonajuridica", {
          params: { tipoAsuntoId: catTipoAsuntoId },
        })
      )?.data;
    },
    async obtenerClasificacionAutoridadGenerica() {
      this.clasificacionAutoridadGenerica = [];
      this.clasificacionAutoridadGenerica = (
        await apiCatalogos.get("api/clasificacionautoridadgenerica")
      )?.data;
    },
    resetCuaderno() {
      this.cuadernos = [];
    },
    async getZonas() {
      this.zonas = [];
      this.zonas = (await apiCatalogos.get("api/zonas"))?.data;
    },
    async obtenerContenidoTramite() {
      this.contenidosTramite =
        (await apiCatalogos.get("api/contenidoTramite"))?.data || [];
    },
    async obtenerTitulares() {
      this.titulares =
        (
          await apiCatalogos.get("api/proyecto/empleados", {
            params: { tipoEmpleado: 1 },
          })
        )?.data || [];
    },
    async obtenerSecretarios() {
      this.secretarios =
        (
          await apiCatalogos.get("api/proyecto/empleados", {
            params: { tipoEmpleado: 2 },
          })
        )?.data || [];
    },
    async obtenerTiposSentencia(idTipoAsunto) {
      this.tipoSentencia =
        (
          await apiCatalogos.get("api/proyecto/tipoSentencia", {
            params: { catTipoAsuntoId: idTipoAsunto },
          })
        )?.data || [];
    },
    async obtenerSentidosSentencias(idTipoAsunto) {
      this.sentidosSentencia =
        (
          await apiCatalogos.get("api/proyecto/tipoSentido", {
            params: { catTipoAsuntoId : idTipoAsunto },
          })
        )?.data || [];
    },
    async obtenerProyectoMotivos(idTipoAsunto) {
      this.proyectoMotivos =
        (await apiProyectos.get("api/proyecto/motivos", {
          params: { catTipoAsuntoId : idTipoAsunto },
        }))?.data || [];
    },
    async obtenerCatalogoGenerico(idCatalogo) {
      const data =
        (
          await apiCatalogos.get("api/generico", {
            params: { catalogoId: idCatalogo },
          })
        )?.data || [];
      return data;
    },
    async obtenerSexo() {
      this.sexo = (await apiCatalogos.get("api/sexo"))?.data || [];
    },
    async getTipoAcuse() {
      const resultado = (await apiCatalogos.get("api/tipoacuse"))?.data || [];
      return resultado;
    },
    async getTipoComunicacion() {
      this.tipoComunicacion =
        (await apiCatalogos.get("api/tipocomunicacion"))?.data || [];
      return this.tipoComunicacion;
    },
    async getOrganismosOCC() {
      this.organismosOcc =
        (await apiCatalogos.get("api/organismosocc"))?.data || [];
      return this.organismosOcc;
    },
    async getDiasInhabiles(fInicio, fFin) {
      try {
        const result = await apiCatalogos.get("api/diasinhabiles", {
          params: {
            fechaInicio: fInicio,
            fechaFin: fFin,
          },
        });
        return result.data.map((e) => e.fechaInhabil.split("T")[0]);
      } catch (error) {
        throw new Error(error);
      }
    },
    async obtenerAdiencia(tipoAsuntoId) {
      this.audiencias = [];
      this.audiencias = (
        await apiCatalogos.get("api/audiencia", {
          params: { TipoAsuntoId: tipoAsuntoId },
        })
      )?.data;
    },
    async obtenerResultadoAdiencia(idAudiencia) {
      this.resultadosAudiencia = [];
      this.resultadosAudiencia = (
        await apiCatalogos.get("api/resultadoAudiencia", {
          params: { idTipoAudiencia: idAudiencia },
        })
      )?.data;
    },
    async obtenerOpcionesCatalogosDependientes(catalogoPadreId, catalogoId) {
      this.catalogoDependiente = [];
      this.catalogoDependiente = (
        await apiCatalogos.get("api/catalogodependiente", {
          params: {
            CatalogoId: catalogoId,
            CatalogoPadreId: catalogoPadreId,
          },
        })
      )?.data;
      return this.catalogoDependiente;
    },
  },
});
