export class Utils {
  /**
   * Agrega caracteres a la izquierda en texto
   * @param {*} cadenaOriginal texto original
   * @param {*} caracter caracter a agregar
   * @param {*} cantidad cantidad que debe tener texto original
   * @returns
   */
  static agregarCaracteresIzquierda(cadenaOriginal, caracter, cantidad) {
    if (cantidad <= 0) {
      return cadenaOriginal;
    }
    if (cantidad <= cadenaOriginal.length) {
      return cadenaOriginal;
    }
    const caracteresAAgregar = caracter.repeat(
      cantidad - cadenaOriginal.length,
    );
    return caracteresAAgregar + cadenaOriginal;
  }
  /**
   * Elimina tildes
   * @param {*} str cadena a quitar tiles
   * @returns {*}  string "cadena sin tildes"
   */
  static eliminarTildes(str) {
    return str.normalize("NFD").replace(/[\u0300-\u036f]/g, "");
  }

  /**
   * Filtra las opciones del combo
   * @param {*} val valor a buscar
   * @param {*} opciones arreglo de opciones completo
   * @param {*} prop propieda en la que se va a buscar el valor si no se envia es por que las opciones es un arreglo de cadenas ['hola', 'mundo']
   * @returns opciones filtradas
   */
  static filtrarCombo(val, opciones, prop = null) {
    if (val) {
      if (prop) {
        return opciones?.filter(
          (v) =>
            v[prop].toLowerCase().includes(val.toLowerCase()) ||
            Utils.eliminarTildes(v[prop].toLowerCase()).includes(
              val.toLowerCase(),
            ),
        );
      } else {
        return opciones?.filter(
          (v) =>
            v.toLowerCase().includes(val.toLowerCase()) ||
            Utils.eliminarTildes(v.toLowerCase()).includes(val.toLowerCase()),
        );
      }
    } else {
      return opciones;
    }
  }

  /**
   * Marca la primera opcion encontrada en combo
   * @param {*} val valor a buscar
   * @param {*} ref es la referencia a QSelect
   */
  static marcaPrimeraOpcionCombo(val, ref) {
    if (val && ref.options.length > 0) {
      ref.setOptionIndex(-1); // reset optionIndex in case there is something selected
      ref.moveOptionSelection(1, true); // focus the first selectable option and do not update the input-value
    }
  }
  /**
   * Indica si un objeto esta vacío
   * @param {*} val valor a buscar
   */
  static isEmpty(obj) {
    for (var prop in obj) {
      if (obj.hasOwnProperty(prop)) return false;
    }
    return true;
  }
  /**
   * Genera guid
   * @returns guid
   */
  static uuidv4() {
    return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(
      /[xy]/g,
      function (c) {
        const r = (Math.random() * 16) | 0,
          v = c == "x" ? r : (r & 0x3) | 0x8;
        return v.toString(16);
      },
    );
  }
  static base64ToUrlObj(strBase64, esDescarga) {
    if (!strBase64 || typeof strBase64 !== "string") {
      return;
    }
    const binary = window.atob(strBase64.replace(/\s/g, ""));
    const len = binary.length;
    const buffer = new ArrayBuffer(len);
    let view = new Uint8Array(buffer);
    for (var i = 0; i < len; i++) {
      view[i] = binary.charCodeAt(i);
    }
    // crea el objeto blob con tipo "application/pdf"
    var blob = new Blob([view], { type: "application/pdf" });
    var pdfSrc = URL.createObjectURL(blob);
    if (esDescarga == true) {
      return blob;
    } else {
      return pdfSrc;
    }
  }
  static base64ToBlobWord(strBase64) {
    if (!strBase64 || typeof strBase64 !== "string") {
      return;
    }
    const binary = window.atob(strBase64.replace(/\s/g, ""));
    const len = binary.length;
    const buffer = new ArrayBuffer(len);
    let view = new Uint8Array(buffer);
    for (var i = 0; i < len; i++) {
      view[i] = binary.charCodeAt(i);
    }
    // crea el objeto blob con tipo "'application/octet-stream'"
    var blob = new Blob([view], {
      type: "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
    });
    return blob;
  }
  /**
   * Archivo a base 64
   * @param {*} file
   * @returns base64
   */
  static fileToBase64(file) {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result);
      reader.onerror = reject;
    });
  }
  /**
   * Archivo a blob
   * @param {*} file
   * @returns blob
   */
  static fileToBlob(file) {
    if (!file) return file;
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsArrayBuffer(file);
      reader.onload = () =>
        resolve({
          blob: new Blob([new Uint8Array(reader.result)], { type: file.type }),
          size: file.size,
          name: file.name,
        });
      reader.onerror = reject;
    });
  }
  static blobToFile(theBlob, fileName) {
    return new File([theBlob], fileName, {
      lastModified: new Date().getTime(),
      type: theBlob.type,
    });
  }
  static todayInSelectDateFormat() {
    const today = new Date();
    return `${today.getDate() / today.getMonth() / today}`;
  }

  static recortarTexto(texto, longitudMaxima) {
    return texto.length > longitudMaxima
      ? `${texto.slice(0, longitudMaxima)} ...`
      : `${texto}.`;
  }
}
