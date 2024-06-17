import { Utils } from "./utils";
import * as docx from "docx-preview";

const mensajes = {
  requerido: "Este campo es obligatorio",
  formatoInvalido: "El formato ingresado no es válido",
  valorMax: "El número debe ser menor o igual a ",
  valorMin: "El número debe ser mayor o igual a ",
  alfaNumerico: "Este campo solo acepta números y letras",
  valorNumerico: "Este campo solo acepta números",
  alfaNumericoPuntoEspacio:
    "Este campo solo acepta caracteres alfanuméricos, espacios y los siguientes caracteres especiales: .'¨",
  archivoExtension: "Formato de archivo no es ",
  requeridoPromovente:
    "Si no desea agregar promovente, seleccione 'nuevo promovente' y no llene ningún campo",
  longitudMinima: "La longitud mínima debe de ser de",
};

export class Validaciones {
  static validaInputRequerido(valor) {
    if (!valor) {
      return mensajes.requerido;
    }
    return true;
  }
  static validaInputNumericoRequeridosinNull(valor) {
    //if (isNaN(valor) || valor === null) {
    if (isNaN(valor)) {
      return mensajes.requerido;
    }
    return true;
  }
  static validaSelectRequerido(valor) {
    if (valor && (valor + "").length > 0) {
      return true;
    }
    return mensajes.requerido;
  }
  static validaSelectRequeridoCont(valor) {
    if (valor && (valor + "").length > 0) {
      return true;
    }
    // return mensajes.requerido;
    return true;
  }
  static validaSelectRequeridoPromovente(valor) {
    if (valor && (valor + "").length > 0) {
      return true;
    }
    return mensajes.requeridoPromovente;
  }
  static validaSelectMultipleRequerido(valor) {
    if (valor && valor.length > 0) {
      return true;
    }
    return mensajes.requerido;
  }
  static validaFecha(valor) {
    if (
      !/^(0[1-9]|[12][0-9]|3[01])\/(0[1-9]|1[0-2])\/([1][9][5-9][0-9]|2[0][0-9][0-9])$/.test(
        valor,
      )
    ) {
      return mensajes.formatoInvalido;
    }
    return true;
  }
  static validaHora(valor) {
    if (!/^(0[0-9]|[1][0-9]|2[0-3]):(0[0-9]|[1-5][0-9])$/.test(valor)) {
      return mensajes.formatoInvalido;
    }
    return true;
  }
  static validaHoraLaboral(valor) {
    if (!(/^(0[9]|1[0-3]):([0-5][0-9])$/.test(valor) || /^(|1[4]):([0][0])$/.test(valor))) {
      return mensajes.formatoInvalido;
    }
    return true;
  }

  static validaLongitudMinima(valor, longitud) {
    if (valor.length < longitud) {
      return `${mensajes.longitudMinima} ${longitud} caracteres`;
    }
    return true;
  }
  // ex>334343/2023
  static validaNoExpediente(valor) {
    if (!/^([1-9][0-9]{0,5})\/([1][9][5-9][0-9]|2[0][0-9][0-9])$/.test(valor)) {
      return mensajes.formatoInvalido;
    }
    return true;
  }

  static validaNoOCC(valor) {
    if (valor === "") {
      return true;
    } else if (!/^(?!0+$)\d{1,18}(\/\d{4})?$/.test(valor)) {
      return mensajes.formatoInvalido;
    }
    return true;
  }
  static validaFolioAnio(valor) {
    if (!/^\d{1,18}\/([1][9][5-9][0-9]|2[0][0-9][0-9])$/.test(valor)) {
      return mensajes.formatoInvalido;
    }
    return true;
  }
  static validaValorMin(valor, min) {
    if (!(+valor >= min)) {
      return true;
      //return mensajes.valorMin + min;
    }
    return true;
  }
  static validaValorMinPromocion(valor, min) {
    if (!(+valor >= min)) {
      //return true;
      return mensajes.valorMin + min;
    }
    return true;
  }
  static validaValorMax(valor, max) {
    if (!(+valor <= max)) {
      return mensajes.valorMax + max;
    }
    return true;
  }
  static validaAlfaNumerico() {
    if (valor && !/^[0-9a-zA-ZñÑáéíóúÁÉÍÓÚäëïöüÄËÏÖÜ]+$/.test(valor)) {
      return mensajes.formatoInvalido;
    }
    return true;
  }
  static validaAlfanumericoConAlgunosCaracteresEspeciales(valor) {
    if (valor && !/^[0-9a-zA-ZñÑáéíóúÁÉÍÓÚäëïöüÄËÏÖÜ'\.\s]+$/.test(valor)) {
      return mensajes.alfaNumericoPuntoEspacio;
    }
    return true;
  }
  static validaValorNumerico(valor) {
    if (valor && !/^[0-9]+$/.test(valor)) {
      return mensajes.valorNumerico;
    }
    return true;
  }
  static async validaExtension(valor, extension) {
    if (valor && valor.blob && extension) {
      switch (extension) {
        case "docx": {
          const newDiv = document.createElement("div");
          try {
            await docx.renderAsync(valor.blob, newDiv);
            newDiv.remove();
            return true;
          } catch (error) {
            return mensajes.archivoExtension + extension;
          }
        }
        case "pdf": {
          const base64 = (
            await Utils.fileToBase64(Utils.blobToFile(valor.blob, valor.name))
          ).split(",")[1];
          const binaryString = window.atob(base64.replace(/\s/g, ""));
          const firstFewBytes = binaryString.substring(0, 5);
          if (firstFewBytes === "%PDF-") {
            return true;
          }
          return mensajes.archivoExtension + extension;
        }
        case "doc": {
          return true;
        }
        default:
          break;
      }
    }
    return true;
  }
}
