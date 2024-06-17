import { useFirmadorStore } from "../stores/firmador-store";
import { loader } from "./loader";
import { noty } from "./notify";
export class Firmador {
  static async obtenerURLGrafico(documentosAFirmar) {
    loader.show();
    const tramiteStore = useFirmadorStore();
    try {
      const respuesta = await tramiteStore.obtenerURLGrafico(documentosAFirmar);
      localStorage.setItem("cveTransaccion", respuesta.cveTransaccion);
      window.location.href = respuesta.urlApp;
    } catch (error) {
      noty.error(error);
    }
    loader.hide();
  }

  static async obtenerURLGraficoOficialia(documentosAFirmar) {
    loader.show();
    const oficialiaStore = useFirmadorStore();
    try {
      const respuesta = await oficialiaStore.obtenerURLGraficoOficialia(documentosAFirmar);
      localStorage.setItem("cveTransaccion", respuesta.cveTransaccion);
      window.location.href = respuesta.urlApp;
    } catch (error) {
      noty.error(error);
    }
    loader.hide();
  }

  static async obtenerURLGraficoSentencias(documentosAFirmar) {
    loader.show();
    const sentenciaStore = useFirmadorStore();
    try {
      const respuesta = await sentenciaStore.obtenerURLGraficoSentencias(documentosAFirmar);
      localStorage.setItem("cveTransaccion", respuesta.cveTransaccion);
      window.location.href = respuesta.urlApp;
    } catch (error) {
      noty.error(error);
    }
    loader.hide();
  }
}
