import { noty } from "./notify";
/**
 * muestra el error
 * @param {error a objecto completo} error
 */
function mostrarError(error) {
  let mensaje =
    "No se pudo cargar por un problema técnico, inténtalo nuevamente";
  if (error?.code && error?.code.toUpperCase() === "ERR_NETWORK") {
    mensaje = "Sin conexión a servidor de datos";
  } else if (error?.response?.data?.mensaje) {
    mensaje = error?.response?.data?.mensaje;
  } else if (error?.response?.data?.Mensaje) {
    mensaje = error?.response?.data?.Mensaje;
  } else if (typeof error === "string" && error.includes("La promoción")) {
    mensaje = error;
  }
  noty.error(mensaje);
}
function archivoInvalido(rejectedEntries, ext) {
  if (rejectedEntries[0].failedPropValidation === "accept") {
    noty.error(
      `Error: El archivo seleccionado tiene una extensión no válida. Por favor, elija un archivo con una extensión ${ext}`,
    );
    return;
  }
  if (rejectedEntries[0].failedPropValidation === "max-file-size") {
    noty.error(
      "Error: El peso máximo permitido para el archivo a cargar es de 30 MB",
    );
    return;
  }
}
export const manejoErrores = {
  mostrarError: (msg) => mostrarError(msg),
  archivoInvalido: (val, ext = "PDF") => archivoInvalido(val, ext),
};
