import { Loading, QSpinner } from "quasar";

/**
 * muestra loader
 */
function showLoader() {
  Loading.show({
    spinnerColor: "primary",
    spinner: QSpinner,
  });
}
/**
 * muestra loader
 */
function showLoaderMessage(msg) {
  Loading.show({
    spinnerColor: "primary",
    spinner: QSpinner,
    backgroundColor: "grey-1",
    message: msg,
    messageColor: "black",
    html: true,
  });
}
/**
 * oculta loader
 */
function hideLoader() {
  Loading.hide();
}
export const loader = {
  show: () => showLoader(),
  hide: () => hideLoader(),
  showLoaderMessage: (msg) => showLoaderMessage(msg),
};
