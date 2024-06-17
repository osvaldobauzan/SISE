import { Notify } from "quasar";
// configuracion general
let config = {
  position: "top",
  timeout: 5000,
  progress: true,
  actions: [{ icon: "close", round: true, color: "white" }],
};

/**
 * muestra notificacion de correcto
 * @param {*} mensaje texto a mostrar
 */
function correcto(mensaje) {
  let opciones = { ...config };
  opciones.message = mensaje;
  opciones.type = "positive";
  Notify.create(opciones);
}
/**
 * muestra notificacion de error
 * @param {*} mensaje texto a mostrar
 */
function error(mensaje = "Error no controlado") {
  let opciones = { ...config };
  opciones.message = mensaje;
  opciones.type = "negative";
  Notify.create(opciones);
}
/**
 * muestra notificacion de precaucion
 * @param {*} mensaje texto a mostrar
 */
function precaucion(mensaje) {
  let opciones = { ...config };
  opciones.message = mensaje;
  opciones.type = "warning";
  Notify.create(opciones);
}
/**
 * muesta notificacion de informacion
 * @param {*} mensaje texto a mostrar
 */
function info(mensaje) {
  let opciones = { ...config };
  opciones.message = mensaje;
  opciones.type = "info";
  Notify.create(opciones);
}

export const noty = {
  error: (msg) => error(msg),
  precaucion: (msg) => precaucion(msg),
  correcto: (msg) => correcto(msg),
  info: (msg) => info(msg),
};
